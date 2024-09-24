using System.Diagnostics;
using System.Security.Cryptography;

namespace Thing
{
    public partial class FormMain : Form
    {
        private static DriveInfo[] drives;
        private static string[] filepaths;
        private static byte[] key;
        private static byte[] iv;
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                comboBox_drives.Items.Add(drives[i].Name);
                comboBox_drives.SelectedIndex = 0;
            }
            textBox_path.Text = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(textBox_path.Text);
            bool fcontains = false;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains("ThingEncrypterKey.tek"))
                {
                    fcontains = true;
                }
            }
            if (fcontains)
            {
                button_selectKey.Enabled = true;
            }
            UpdateFiles();
        }
        private void textBox_path_TextChanged(object sender, EventArgs e)
        {
            if (Path.Exists(textBox_path.Text) && textBox_path.Text.Contains("\\"))
            {
                button_gotopath.Enabled = true;
            }
            else
            {
                button_gotopath.Enabled = false;
            }
        }

        private void button_gotopath_Click(object sender, EventArgs e)
        {
            UpdateFiles();
        }
        private void UpdateFiles()
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            label_restricted.Enabled = false;
            checkedListBox_files.Items.Clear();
            listBox_directories.Items.Clear();
            try
            {
                if (!textBox_path.Text.EndsWith(":"))
                {
                    listBox_directories.Items.Add("[RETURN]");
                }
                if (textBox_path.Text.EndsWith(":"))
                {
                    textBox_path.Text += "\\";
                }
                string[] files = Directory.GetFiles(textBox_path.Text);
                filepaths = files.ToArray(); //POINTER!!!! HOLY SHIT!!!!!1!!
                string[] directories = Directory.GetDirectories(textBox_path.Text);
                progressBar.Maximum = files.Length + directories.Length;
                if (progressBar.Maximum == 0)
                {
                    progressBar.Maximum = 1;
                }
                for (int i = 0; i < directories.Length; i++)
                {
                    int lastindx = directories[i].LastIndexOf('\\');
                    directories[i] = directories[i].Substring(lastindx);
                    directories[i] = directories[i].Replace("\\", "");
                    progressBar.Value++;
                    listBox_directories.Items.Add(directories[i]);
                }
                for (int i = 0; i < files.Length; i++)
                {
                    int lastindx = files[i].LastIndexOf('\\');
                    files[i] = files[i].Substring(lastindx);
                    files[i] = files[i].Replace("\\", "");
                    progressBar.Value++;
                    checkedListBox_files.Items.Add(files[i]);
                }
                progressBar.Value = progressBar.Maximum;
            }
            catch (UnauthorizedAccessException ex)
            {
                label_restricted.Visible = true;
            }
            catch (Exception ex) { }
        }

        private void listBox_directories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_directories.SelectedItem != null)
            {
                if (listBox_directories.SelectedItem.ToString() == "[RETURN]")
                {

                    int lastindex = textBox_path.Text.LastIndexOf("\\");
                    textBox_path.Text = textBox_path.Text.Substring(0, lastindex);
                }
                else
                {
                    int index = listBox_directories.SelectedIndex;
                    if (listBox_directories.Items[index].ToString() != null)
                    {
                        string directoryname = listBox_directories.Items[index].ToString();
                        textBox_path.Text = Path.Combine(textBox_path.Text, directoryname);
                    }
                }
                UpdateFiles();
            }
        }

        private void comboBox_drives_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < drives.Length; i++)
            {
                if (comboBox_drives.Text == drives[i].ToString())
                {
                    textBox_path.Text = drives[i].ToString();
                }
            }
            UpdateFiles();
        }
        private string[] GetSelected()
        {
            List<string> selectedFiles = new List<string>();
            for (int i = 0; i < checkedListBox_files.Items.Count; i++)
            {
                if (checkedListBox_files.GetItemChecked(i))
                {
                    // Benutze den i-th Index für den filepaths Array
                    if (i < filepaths.Length) // Sicherstellen, dass der Index gültig ist
                    {
                        selectedFiles.Add(filepaths[i]);
                    }
                }
            }
            return selectedFiles.ToArray();
        }
        private void button_generateNewKey_Click(object sender, EventArgs e)
        {
            byte[] generateKey = new byte[32];
            byte[] generateIV = new byte[16];
            RandomNumberGenerator.Fill(generateKey);
            RandomNumberGenerator.Fill(generateIV);
            while (generateKey.Contains<byte>(255))
            {
                RandomNumberGenerator.Fill(generateKey);
            }
            while (generateIV.Contains<byte>(255))
            {
                RandomNumberGenerator.Fill(generateIV);
            }
            List<byte> key = new List<byte>();
            key.AddRange(generateKey);
            key.AddRange(generateIV);
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            bool fcontains = false;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains("ThingEncrypterKey.tek"))
                {
                    fcontains = true;
                }
            }
            if (fcontains)
            {
                DialogResult r = MessageBox.Show("A .tek key already exists. If overwritten, all files encrypted with the old key cannot be decrypted anymore!\r\n\r\nDo you want to generate a new key?", "Error!", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"ThingEncrypterKey.tek");
                    File.WriteAllBytes(path, key.ToArray());
                    button_generateNewKey.Enabled = false;
                    button_selectKey.Enabled = true;
                }
            }
            else
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"ThingEncrypterKey.tek");
                File.WriteAllBytes(path, key.ToArray());
                button_generateNewKey.Enabled = false;
                button_selectKey.Enabled = true;
            }
            UpdateFiles();
        }
        private void button_selectKey_Click(object sender, EventArgs e)
        {
            string keypath = Path.Combine(Directory.GetCurrentDirectory(), "ThingEncrypterKey.tek");
            if (File.Exists(keypath))
            {
                byte[] bytes = File.ReadAllBytes(keypath);
                key = bytes.Take(32).ToArray();
                iv = bytes.Skip(Math.Max(0, bytes.Length - 16)).Take(16).ToArray();
                label_keystatus.Text = "TRUE";
                label_keystatus.ForeColor = Color.Green;
                button_selectKey.Enabled = false;
                button_generateNewKey.Enabled = false;
                button_decryptAll.Enabled = true;
                button_decryptallANDsub.Enabled = true;
                button_decryptSelected.Enabled = true;
                button_encryptAll.Enabled = true;
                button_encryptSelected.Enabled = true;
                button_encryptallANDsub.Enabled = true;
            }
        }
        private async Task<EncryptionStatus> EncryptFile(string path)
        {
            try
            {
                // ThingEncryptedFile - .tef
                string extension = Path.GetExtension(path);
                if (extension == ".tef")
                {
                    return EncryptionStatus.AlreadyEncrypted;
                }
                else
                {
                    string newFilePath = path + ".tef";

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = key;
                        aes.IV = iv;

                        using (FileStream fsInput = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                        using (FileStream fsOutput = new FileStream(newFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                        using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            await fsInput.CopyToAsync(cs);
                        }
                    }

                    return EncryptionStatus.Encrypted;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine($"Access denied: {ex.Message}");
                return EncryptionStatus.AccesDenied;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return EncryptionStatus.Other;
            }
            finally
            {
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch { }
                }
            }
        }
        private async Task<EncryptionStatus> DecryptFile(string path)
        {
            try
            {
                // ThingEncryptedFile - .tef
                string extension = Path.GetExtension(path);
                if (extension != ".tef")
                {
                    return EncryptionStatus.AlreadyDecrypted;
                }
                else
                {
                    string newFilePath = path.Replace(".tef", "");

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = key;
                        aes.IV = iv;

                        using (FileStream fsInput = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                        using (FileStream fsOutput = new FileStream(newFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                        using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            await fsInput.CopyToAsync(cs);
                        }
                    }

                    return EncryptionStatus.Decrypted;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine($"Access denied: {ex.Message}");
                return EncryptionStatus.AccesDenied;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return EncryptionStatus.Other;
            }
            finally
            {
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch { }
                }
            }
        }
        //CRYPTO STUFF
        private async void button_encryptSelected_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            string[] targets = GetSelected();
            if (targets.Length > 0)
            {
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];
                Debug.WriteLine(tasks.Length + " " + targets.Length);

                for (int i = 0; i < tasks.Length; i++)
                {
                    Debug.WriteLine("Running Task " + i);
                    tasks[i] = EncryptFile(targets[i]);
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                Debug.WriteLine("All Tasks completed!");
                int success = 0;
                int alreadyencrypted = 0;
                int acceserror = 0;
                int other = 0;
                foreach (EncryptionStatus estatus in status)
                {
                    if (estatus == EncryptionStatus.Encrypted) success++;
                    if (estatus == EncryptionStatus.AlreadyEncrypted) alreadyencrypted++;
                    if (estatus == EncryptionStatus.AccesDenied) acceserror++;
                    if (estatus == EncryptionStatus.Other) other++;
                }
                DateTime end = DateTime.Now;
                TimeSpan duration = end - start;
                string formattedDuration = $"{duration.Minutes}m {duration.Seconds}s {duration.Milliseconds:0000}ms";
                string resultstring = $"The encryption process has been completed in {formattedDuration}.";
                if (success > 0) resultstring += $"\r\n{success} files have been encrypted.";
                if (alreadyencrypted > 0) resultstring += $"\r\n{alreadyencrypted} files were already encrypted.";
                if (acceserror > 0) resultstring += $"\r\n{acceserror} files could not be accessed.";
                if (other > 0) resultstring += $"\r\n{other} files could not be encrypted due to unknown reasons.";
                DialogResult r = MessageBox.Show(resultstring, "Encryption completed!", MessageBoxButtons.OK);
                UpdateFiles();
            }
            else
            {
                DialogResult r = MessageBox.Show("No files selected!", "Error!", MessageBoxButtons.OK);
            }
        }
        private async void button_encryptAll_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            string[] targets = filepaths.ToArray();
            for (int i = 0; i < targets.Length; i++)
            {
                Debug.WriteLine(targets[i]);
            }
            if (targets.Length > 0)
            {
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];
                Debug.WriteLine(tasks.Length + " " + targets.Length);

                for(int i = 0; i < tasks.Length; i++)
                {
                    Debug.WriteLine("Running Task " + i);
                    tasks[i] = EncryptFile(targets[i]);
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                Debug.WriteLine("All Tasks completed!");

                int success = 0;
                int alreadyencrypted = 0;
                int acceserror = 0;
                int other = 0;
                foreach (EncryptionStatus estatus in status)
                {
                    if (estatus == EncryptionStatus.Encrypted) success++;
                    else if (estatus == EncryptionStatus.AlreadyEncrypted) alreadyencrypted++;
                    else if (estatus == EncryptionStatus.AccesDenied) acceserror++;
                    else if (estatus == EncryptionStatus.Other) other++;
                }
                DateTime end = DateTime.Now;
                TimeSpan duration = end - start;
                string formattedDuration = $"{duration.Minutes}m {duration.Seconds}s {duration.Milliseconds:0000}ms";
                string resultstring = $"The encryption process has been completed in {formattedDuration}.";
                if (success > 0) resultstring += $"\r\n{success} files have been encrypted.";
                if (alreadyencrypted > 0) resultstring += $"\r\n{alreadyencrypted} files were already encrypted.";
                if (acceserror > 0) resultstring += $"\r\n{acceserror} files could not be accessed.";
                if (other > 0) resultstring += $"\r\n{other} files could not be encrypted due to unknown reasons.";
                DialogResult r = MessageBox.Show(resultstring, "Encryption completed!", MessageBoxButtons.OK);
                UpdateFiles();
            }
            else
            {
                DialogResult r = MessageBox.Show("No files selected!", "Error!", MessageBoxButtons.OK);
            }
        }
        private async void button_decryptSelected_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            string[] targets = GetSelected();
            if (targets.Length > 0)
            {
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];
                Debug.WriteLine(tasks.Length + " " + targets.Length);

                for (int i = 0; i < tasks.Length; i++)
                {
                    int index = i;
                    Debug.WriteLine("Running Task " + index);
                    tasks[index] = DecryptFile(targets[index]);
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                Debug.WriteLine("All Tasks completed!");
                int success = 0;
                int alreadydecrypted = 0;
                int acceserror = 0;
                int other = 0;
                foreach (EncryptionStatus estatus in status)
                {
                    if (estatus == EncryptionStatus.Decrypted) success++;
                    if (estatus == EncryptionStatus.AlreadyDecrypted) alreadydecrypted++;
                    if (estatus == EncryptionStatus.AccesDenied) acceserror++;
                    if (estatus == EncryptionStatus.Other) other++;
                }
                DateTime end = DateTime.Now;
                TimeSpan duration = end - start;
                string formattedDuration = $"{duration.Minutes}m {duration.Seconds}s {duration.Milliseconds:0000}ms";
                string resultstring = $"The decryption process has been completed in {formattedDuration}.";
                if (success > 0) resultstring += $"\r\n{success} files have been decrypted.";
                if (alreadydecrypted > 0) resultstring += $"\r\n{alreadydecrypted} files were already decrypted.";
                if (acceserror > 0) resultstring += $"\r\n{acceserror} files could not be accessed.";
                if (other > 0) resultstring += $"\r\n{other} files could not be decrypted due to unknown reasons.";
                DialogResult r = MessageBox.Show(resultstring, "Decryption completed!", MessageBoxButtons.OK);
                UpdateFiles();
            }
            else
            {
                DialogResult r = MessageBox.Show("No files selected!", "Error!", MessageBoxButtons.OK);
            }
        }
        private async void button_decryptAll_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            string[] targets = filepaths.ToArray();
            if (targets.Length > 0)
            {
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];
                for (int i = 0; i < tasks.Length; i++)
                {
                    int index = i;
                    Debug.WriteLine("Running Task " + index);
                    tasks[index] = DecryptFile(targets[index]);
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                Debug.WriteLine("All Tasks completed!");
                int success = 0;
                int alreadydecrypted = 0;
                int acceserror = 0;
                int other = 0;
                foreach (EncryptionStatus estatus in status)
                {
                    if (estatus == EncryptionStatus.Decrypted) success++;
                    if (estatus == EncryptionStatus.AlreadyDecrypted) alreadydecrypted++;
                    if (estatus == EncryptionStatus.AccesDenied) acceserror++;
                    if (estatus == EncryptionStatus.Other) other++;
                }
                DateTime end = DateTime.Now;
                TimeSpan duration = end - start;
                string formattedDuration = $"{duration.Minutes}m {duration.Seconds}s {duration.Milliseconds:0000}ms";
                string resultstring = $"The decryption process has been completed in {formattedDuration}.";
                if (success > 0) resultstring += $"\r\n{success} files have been decrypted.";
                if (alreadydecrypted > 0) resultstring += $"\r\n{alreadydecrypted} files were already decrypted.";
                if (acceserror > 0) resultstring += $"\r\n{acceserror} files could not be accessed.";
                if (other > 0) resultstring += $"\r\n{other} files could not be decrypted due to unknown reasons.";
                DialogResult r = MessageBox.Show(resultstring, "Decryption completed!", MessageBoxButtons.OK);
                UpdateFiles();
            }
            else
            {
                DialogResult r = MessageBox.Show("No files selected!", "Error!", MessageBoxButtons.OK);
            }
        }
        private enum EncryptionStatus
        {
            Decrypted,
            Encrypted,
            AlreadyEncrypted,
            AlreadyDecrypted,
            AccesDenied,
            Other
        }
    }
}