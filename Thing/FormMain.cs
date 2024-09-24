using System.Diagnostics;
using System.Security.Cryptography;

namespace Thing
{
    public partial class FormMain : Form
    {
        private static DriveInfo[] drives;
        private string[] filepaths;
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
                string[] directories = Directory.GetDirectories(textBox_path.Text);
                progressBar.Maximum = files.Length + directories.Length;
                if (progressBar.Maximum == 0)
                {
                    progressBar.Maximum = 1;
                }
                filepaths = files;
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

        private void button_encryptSelected_Click(object sender, EventArgs e)
        {
            string[] targets = GetSelected();
            if (targets.Length > 0)
            {
                int totalammount = targets.Length;
                int doneammount = 0;
                foreach (string target in targets)
                {
                    //bool success = EncryptFile(target);
                    //if (success)
                    //    doneammount++;
                }
            }
            else
            {
                DialogResult r = MessageBox.Show("No files selected!", "Error!", MessageBoxButtons.OK);
            }
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
            List<int> ints = new List<int>();
            List<string> strings = new List<string>();
            for (int i = 0; i < checkedListBox_files.Items.Count; i++)
            {
                if (checkedListBox_files.GetItemChecked(i))
                {
                    ints.Add(i);
                }
            }
            for (int i = 0; i < ints.Count; i++)
            {
                strings.Add(filepaths[i].ToString());
            }
            return strings.ToArray();
        }
        /*private bool EncryptFile(string path)
        {
        }*/

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
            key.Add(255);
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
                Debug.WriteLine(path);
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
            }
        }
    }
}
