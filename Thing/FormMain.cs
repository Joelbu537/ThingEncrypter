using System.Diagnostics;
using System.Security.AccessControl;
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
        private void FormMain_Load(object sender, EventArgs e)
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
                object o = new object();
                button_selectKey_Click(o, EventArgs.Empty);
            }
            button_selectKey_Click(sender, e);
            UpdateFiles();
        }
        //UI Methods
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
                    if (listBox_directories.Items[index].ToString() != null && listBox_directories.Items == null)
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
        private void button_gotopath_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                button_gotopath_Click(sender, EventArgs.Empty);
            }
        }
        //Base Methods
        private string[] GetSelected()
        {
            List<string> selectedFiles = new List<string>();
            for (int i = 0; i < checkedListBox_files.Items.Count; i++)
            {
                if (checkedListBox_files.GetItemChecked(i))
                {
                    if (i < filepaths.Length)
                    {
                        selectedFiles.Add(filepaths[i]);
                    }
                }
            }
            return selectedFiles.ToArray();
        }
        private void UpdateFiles()
        {
            try
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                checkedListBox_files.Items.Clear();
                listBox_directories.Items.Clear();
            }
            catch { }
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
                if (IsProtected(textBox_path.Text))
                {
                    label_restricted.Visible = true;
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
                    if (!IsProtected(filepaths[i]))
                    {
                        int lastindx = files[i].LastIndexOf('\\');
                        files[i] = files[i].Substring(lastindx);
                        files[i] = files[i].Replace("\\", "");
                        progressBar.Value++;
                        checkedListBox_files.Items.Add(files[i]);
                    }
                }
                progressBar.Value = progressBar.Maximum;
            }
            catch (Exception ex) { }
        }
        static bool IsProtected(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    return IsProtectedFile(fileInfo);
                }
                else if (Directory.Exists(path))
                {
                    var directoryInfo = new DirectoryInfo(path);
                    return IsProtectedDirectory(directoryInfo);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return true; //Kein access
            }

            return false;
        }
        static bool IsProtectedFile(FileInfo fileInfo)
        {
            FileSecurity fileSecurity = fileInfo.GetAccessControl();
            AuthorizationRuleCollection rules = fileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

            foreach (FileSystemAccessRule rule in rules)
            {
                if ((rule.FileSystemRights & FileSystemRights.Write) == FileSystemRights.Write)
                {
                    if (rule.AccessControlType == AccessControlType.Deny)
                    {
                        return true;
                    }
                }
            }

            return false; // Kein Schutz gefunden
        }
        static bool IsProtectedDirectory(DirectoryInfo directoryInfo)
        {
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
            AuthorizationRuleCollection rules = directorySecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

            foreach (FileSystemAccessRule rule in rules)
            {
                if ((rule.FileSystemRights & FileSystemRights.Write) == FileSystemRights.Write)
                {
                    if (rule.AccessControlType == AccessControlType.Deny)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        //Base File Methods
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

                    File.Delete(path);
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

                    File.Delete(path);
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
        }
        //CRYPTO STUFF
        private void UpdateProgressBar()
        {
            try
            {
                progressBar.Value++;
                Debug.WriteLine("a");
            }
            catch
            {
                Debug.WriteLine("AHHHHHHHHHHHHHHHHHHHHHHHHHH");
            }
        }
        private void ResetProgressBar(int value)
        {
            progressBar.Value = 0;
            progressBar.Maximum = value;
            Debug.WriteLine("Progressbar set to " + value);
        }
        //Encrypt Selected
        private void button_encryptSelected_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(EncryptSelected_Click);
            t.Start();
        }
        private async void EncryptSelected_Click()
        {
            DateTime start = DateTime.Now;
            string[] targets = GetSelected();
            if (targets.Length > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ResetProgressBar(targets.Length);
                });
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];
                Debug.WriteLine(tasks.Length + " " + targets.Length);

                for (int i = 0; i < tasks.Length; i++)
                {
                    Debug.WriteLine("Running Task " + i);
                    tasks[i] = EncryptFile(targets[i]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateProgressBar();
                    });
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateFiles();
                });

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
        //Encrypt All
        private void button_encryptAll_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(EncryptAll_Click);
            t.Start();
        }
        private async void EncryptAll_Click()
        {
            DateTime start = DateTime.Now;
            string[] targets = filepaths.ToArray();
            for (int i = 0; i < targets.Length; i++)
            {
                Debug.WriteLine(targets[i]);
            }
            if (targets.Length > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ResetProgressBar(targets.Length);
                });
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];

                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = EncryptFile(targets[i]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateProgressBar();
                    });
                }
                EncryptionStatus[] status = await Task.WhenAll(tasks);
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateFiles();
                });

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
        //Decrypt Selected
        private void button_decryptSelected_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(DecryptSelected_Click);
            t.Start();
        }
        private async void DecryptSelected_Click()
        {
            DateTime start = DateTime.Now;
            string[] targets = GetSelected();
            if (targets.Length > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ResetProgressBar(targets.Length);
                });
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];
                Debug.WriteLine(tasks.Length + " " + targets.Length);

                for (int i = 0; i < tasks.Length; i++)
                {
                    int index = i;
                    tasks[index] = DecryptFile(targets[index]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateProgressBar();
                    });
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateFiles();
                });

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
        //Decrypt All
        private void Button_decryptAll_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(DecryptAll_Click);
            t.Start();
        }
        private async void DecryptAll_Click()
        {
            DateTime start = DateTime.Now;
            string[] targets = filepaths.ToArray();
            if (targets.Length > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ResetProgressBar(targets.Length);
                });
                Task<EncryptionStatus>[] tasks = new Task<EncryptionStatus>[targets.Length];

                for (int i = 0; i < tasks.Length; i++)
                {
                    Debug.WriteLine("Running Task " + i);
                    tasks[i] = DecryptFile(targets[i]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateProgressBar();
                    });
                }

                EncryptionStatus[] status = await Task.WhenAll(tasks);
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateFiles();
                });

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
            }
            else
            {
                DialogResult r = MessageBox.Show("No files selected!", "Error!", MessageBoxButtons.OK);
            }
        }

        //ENUMS
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