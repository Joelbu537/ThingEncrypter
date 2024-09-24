using System.Diagnostics;

namespace Thing
{
    public partial class FormMain : Form
    {
        private string[] filepaths;
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_path.Text = Directory.GetCurrentDirectory();
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
                filepaths = files;
                for (int i = 0; i < directories.Length; i++)
                {
                    int lastindx = directories[i].LastIndexOf('\\');
                    directories[i] = directories[i].Substring(lastindx);
                    directories[i] = directories[i].Replace("\\", "");
                    listBox_directories.Items.Add(directories[i]);
                }
                for (int i = 0; i < files.Length; i++)
                {
                    int lastindx = files[i].LastIndexOf('\\');
                    files[i] = files[i].Substring(lastindx);
                    files[i] = files[i].Replace("\\", "");
                    if (textBox_path.Text.EndsWith(":"))
                    {
                        
                    }
                    checkedListBox_files.Items.Add(files[i]);
                }
            }
            catch (Exception ex) { }
        }

        private void button_encryptSelected_Click(object sender, EventArgs e)
        {

        }

        private void listBox_directories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox_directories.SelectedItem.ToString() == "[RETURN]")
            {
                //remove thing
                int lastindex = textBox_path.Text.LastIndexOf("\\");
                textBox_path.Text = textBox_path.Text.Substring(0, lastindex);
            }
            else
            {
                int index = listBox_directories.SelectedIndex;
                string directoryname = listBox_directories.Items[index].ToString();
                textBox_path.Text = Path.Combine(textBox_path.Text, directoryname);
            }
            UpdateFiles();
        }
    }
}
