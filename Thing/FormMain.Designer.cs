namespace Thing
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkedListBox_files = new CheckedListBox();
            button_gotopath = new Button();
            progressBar = new ProgressBar();
            textBox_path = new TextBox();
            listBox_directories = new ListBox();
            label_directory = new Label();
            label_files = new Label();
            button_encryptSelected = new Button();
            button_encryptAll = new Button();
            panel_encrypt = new Panel();
            button_encryptallANDsub = new Button();
            label_encryptionOperations = new Label();
            label_decryptionsOptions = new Label();
            panel_decrypt = new Panel();
            button_decryptallANDsub = new Button();
            button_decryptSelected = new Button();
            button_decryptAll = new Button();
            comboBox_drives = new ComboBox();
            label_key = new Label();
            panel1 = new Panel();
            button_generateNewKey = new Button();
            button_selectKey = new Button();
            label_keydisplay = new Label();
            label_keystatus = new Label();
            panel_encrypt.SuspendLayout();
            panel_decrypt.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // checkedListBox_files
            // 
            checkedListBox_files.FormattingEnabled = true;
            checkedListBox_files.Location = new Point(760, 182);
            checkedListBox_files.Margin = new Padding(6, 7, 6, 7);
            checkedListBox_files.Name = "checkedListBox_files";
            checkedListBox_files.Size = new Size(660, 764);
            checkedListBox_files.TabIndex = 0;
            // 
            // button_gotopath
            // 
            button_gotopath.Enabled = false;
            button_gotopath.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_gotopath.Location = new Point(1479, 30);
            button_gotopath.Margin = new Padding(10, 12, 10, 12);
            button_gotopath.Name = "button_gotopath";
            button_gotopath.Size = new Size(160, 56);
            button_gotopath.TabIndex = 1;
            button_gotopath.Text = "Go!";
            button_gotopath.UseVisualStyleBackColor = true;
            button_gotopath.Click += button_gotopath_Click;
            button_gotopath.KeyPress += button_gotopath_Press;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(32, 1060);
            progressBar.Margin = new Padding(6, 7, 6, 7);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1390, 56);
            progressBar.TabIndex = 2;
            progressBar.Visible = false;
            // 
            // textBox_path
            // 
            textBox_path.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_path.Location = new Point(222, 36);
            textBox_path.Margin = new Padding(6, 7, 6, 7);
            textBox_path.MinimumSize = new Size(1198, 23);
            textBox_path.Name = "textBox_path";
            textBox_path.Size = new Size(1198, 46);
            textBox_path.TabIndex = 3;
            textBox_path.TextChanged += textBox_path_TextChanged;
            // 
            // listBox_directories
            // 
            listBox_directories.Font = new Font("Segoe UI", 10F);
            listBox_directories.FormattingEnabled = true;
            listBox_directories.ItemHeight = 41;
            listBox_directories.Location = new Point(32, 185);
            listBox_directories.Margin = new Padding(6, 7, 6, 7);
            listBox_directories.Name = "listBox_directories";
            listBox_directories.Size = new Size(660, 783);
            listBox_directories.TabIndex = 4;
            listBox_directories.SelectedIndexChanged += listBox_directories_SelectedIndexChanged;
            // 
            // label_directory
            // 
            label_directory.AutoSize = true;
            label_directory.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_directory.Location = new Point(240, 111);
            label_directory.Margin = new Padding(6, 0, 6, 0);
            label_directory.Name = "label_directory";
            label_directory.Size = new Size(222, 52);
            label_directory.TabIndex = 5;
            label_directory.Text = "Directories";
            // 
            // label_files
            // 
            label_files.AutoSize = true;
            label_files.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold);
            label_files.Location = new Point(1050, 111);
            label_files.Margin = new Padding(6, 0, 6, 0);
            label_files.Name = "label_files";
            label_files.Size = new Size(102, 52);
            label_files.TabIndex = 6;
            label_files.Text = "Files";
            // 
            // button_encryptSelected
            // 
            button_encryptSelected.Enabled = false;
            button_encryptSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_encryptSelected.Location = new Point(21, 22);
            button_encryptSelected.Margin = new Padding(10, 12, 10, 12);
            button_encryptSelected.Name = "button_encryptSelected";
            button_encryptSelected.Size = new Size(160, 56);
            button_encryptSelected.TabIndex = 7;
            button_encryptSelected.Text = "Selected";
            button_encryptSelected.UseVisualStyleBackColor = true;
            button_encryptSelected.Click += button_encryptSelected_Click;
            // 
            // button_encryptAll
            // 
            button_encryptAll.Enabled = false;
            button_encryptAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_encryptAll.Location = new Point(21, 104);
            button_encryptAll.Margin = new Padding(10, 12, 10, 12);
            button_encryptAll.Name = "button_encryptAll";
            button_encryptAll.Size = new Size(160, 56);
            button_encryptAll.TabIndex = 8;
            button_encryptAll.Text = "All";
            button_encryptAll.UseVisualStyleBackColor = true;
            button_encryptAll.Click += button_encryptAll_Click;
            // 
            // panel_encrypt
            // 
            panel_encrypt.BackColor = SystemColors.ControlDark;
            panel_encrypt.BackgroundImageLayout = ImageLayout.None;
            panel_encrypt.Controls.Add(button_encryptallANDsub);
            panel_encrypt.Controls.Add(button_encryptSelected);
            panel_encrypt.Controls.Add(button_encryptAll);
            panel_encrypt.Location = new Point(1450, 226);
            panel_encrypt.Margin = new Padding(6, 7, 6, 7);
            panel_encrypt.Name = "panel_encrypt";
            panel_encrypt.Size = new Size(204, 265);
            panel_encrypt.TabIndex = 1;
            // 
            // button_encryptallANDsub
            // 
            button_encryptallANDsub.Enabled = false;
            button_encryptallANDsub.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_encryptallANDsub.Location = new Point(21, 184);
            button_encryptallANDsub.Margin = new Padding(10, 12, 10, 12);
            button_encryptallANDsub.Name = "button_encryptallANDsub";
            button_encryptallANDsub.Size = new Size(160, 56);
            button_encryptallANDsub.TabIndex = 9;
            button_encryptallANDsub.Text = "Sub + All";
            button_encryptallANDsub.UseVisualStyleBackColor = true;
            // 
            // label_encryptionOperations
            // 
            label_encryptionOperations.AutoSize = true;
            label_encryptionOperations.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_encryptionOperations.Location = new Point(1502, 182);
            label_encryptionOperations.Margin = new Padding(6, 0, 6, 0);
            label_encryptionOperations.Name = "label_encryptionOperations";
            label_encryptionOperations.Size = new Size(115, 37);
            label_encryptionOperations.TabIndex = 7;
            label_encryptionOperations.Text = "Encrypt";
            // 
            // label_decryptionsOptions
            // 
            label_decryptionsOptions.AutoSize = true;
            label_decryptionsOptions.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_decryptionsOptions.Location = new Point(1502, 503);
            label_decryptionsOptions.Margin = new Padding(6, 0, 6, 0);
            label_decryptionsOptions.Name = "label_decryptionsOptions";
            label_decryptionsOptions.Size = new Size(120, 37);
            label_decryptionsOptions.TabIndex = 9;
            label_decryptionsOptions.Text = "Decrypt";
            // 
            // panel_decrypt
            // 
            panel_decrypt.BackColor = SystemColors.ControlDark;
            panel_decrypt.BackgroundImageLayout = ImageLayout.None;
            panel_decrypt.Controls.Add(button_decryptallANDsub);
            panel_decrypt.Controls.Add(button_decryptSelected);
            panel_decrypt.Controls.Add(button_decryptAll);
            panel_decrypt.Location = new Point(1450, 548);
            panel_decrypt.Margin = new Padding(6, 7, 6, 7);
            panel_decrypt.Name = "panel_decrypt";
            panel_decrypt.Size = new Size(204, 265);
            panel_decrypt.TabIndex = 8;
            // 
            // button_decryptallANDsub
            // 
            button_decryptallANDsub.Enabled = false;
            button_decryptallANDsub.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_decryptallANDsub.Location = new Point(21, 184);
            button_decryptallANDsub.Margin = new Padding(10, 12, 10, 12);
            button_decryptallANDsub.Name = "button_decryptallANDsub";
            button_decryptallANDsub.Size = new Size(160, 56);
            button_decryptallANDsub.TabIndex = 10;
            button_decryptallANDsub.Text = "Sub + All";
            button_decryptallANDsub.UseVisualStyleBackColor = true;
            // 
            // button_decryptSelected
            // 
            button_decryptSelected.Enabled = false;
            button_decryptSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_decryptSelected.Location = new Point(21, 22);
            button_decryptSelected.Margin = new Padding(10, 12, 10, 12);
            button_decryptSelected.Name = "button_decryptSelected";
            button_decryptSelected.Size = new Size(160, 56);
            button_decryptSelected.TabIndex = 7;
            button_decryptSelected.Text = "Selected";
            button_decryptSelected.UseVisualStyleBackColor = true;
            button_decryptSelected.Click += button_decryptSelected_Click;
            // 
            // button_decryptAll
            // 
            button_decryptAll.Enabled = false;
            button_decryptAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_decryptAll.Location = new Point(21, 104);
            button_decryptAll.Margin = new Padding(10, 12, 10, 12);
            button_decryptAll.Name = "button_decryptAll";
            button_decryptAll.Size = new Size(160, 56);
            button_decryptAll.TabIndex = 8;
            button_decryptAll.Text = "All";
            button_decryptAll.UseVisualStyleBackColor = true;
            button_decryptAll.Click += Button_decryptAll_Click;
            // 
            // comboBox_drives
            // 
            comboBox_drives.FormattingEnabled = true;
            comboBox_drives.Location = new Point(48, 36);
            comboBox_drives.Margin = new Padding(4, 4, 4, 4);
            comboBox_drives.MaxDropDownItems = 26;
            comboBox_drives.MaxLength = 3;
            comboBox_drives.Name = "comboBox_drives";
            comboBox_drives.Size = new Size(148, 45);
            comboBox_drives.TabIndex = 10;
            comboBox_drives.SelectedIndexChanged += comboBox_drives_SelectedIndexChanged;
            // 
            // label_key
            // 
            label_key.AutoSize = true;
            label_key.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_key.Location = new Point(1521, 821);
            label_key.Margin = new Padding(6, 0, 6, 0);
            label_key.Name = "label_key";
            label_key.Size = new Size(65, 37);
            label_key.TabIndex = 12;
            label_key.Text = "Key";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDark;
            panel1.BackgroundImageLayout = ImageLayout.None;
            panel1.Controls.Add(button_generateNewKey);
            panel1.Controls.Add(button_selectKey);
            panel1.Location = new Point(1450, 926);
            panel1.Margin = new Padding(6, 7, 6, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(204, 189);
            panel1.TabIndex = 11;
            // 
            // button_generateNewKey
            // 
            button_generateNewKey.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_generateNewKey.Location = new Point(21, 22);
            button_generateNewKey.Margin = new Padding(10, 12, 10, 12);
            button_generateNewKey.Name = "button_generateNewKey";
            button_generateNewKey.Size = new Size(160, 56);
            button_generateNewKey.TabIndex = 7;
            button_generateNewKey.Text = "Generate New";
            button_generateNewKey.UseVisualStyleBackColor = true;
            button_generateNewKey.Click += button_generateNewKey_Click;
            // 
            // button_selectKey
            // 
            button_selectKey.Enabled = false;
            button_selectKey.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_selectKey.Location = new Point(21, 104);
            button_selectKey.Margin = new Padding(10, 12, 10, 12);
            button_selectKey.Name = "button_selectKey";
            button_selectKey.Size = new Size(160, 56);
            button_selectKey.TabIndex = 8;
            button_selectKey.Text = "Load";
            button_selectKey.UseVisualStyleBackColor = true;
            button_selectKey.Click += button_selectKey_Click;
            // 
            // label_keydisplay
            // 
            label_keydisplay.AutoSize = true;
            label_keydisplay.Location = new Point(1450, 882);
            label_keydisplay.Margin = new Padding(4, 0, 4, 0);
            label_keydisplay.Name = "label_keydisplay";
            label_keydisplay.Size = new Size(112, 37);
            label_keydisplay.TabIndex = 13;
            label_keydisplay.Text = "Loaded:";
            // 
            // label_keystatus
            // 
            label_keystatus.AutoSize = true;
            label_keystatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_keystatus.ForeColor = Color.Red;
            label_keystatus.Location = new Point(1566, 882);
            label_keystatus.Margin = new Padding(4, 0, 4, 0);
            label_keystatus.Name = "label_keystatus";
            label_keystatus.Size = new Size(92, 37);
            label_keystatus.TabIndex = 14;
            label_keystatus.Text = "FALSE";
            label_keystatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1712, 1137);
            Controls.Add(label_keystatus);
            Controls.Add(label_keydisplay);
            Controls.Add(panel1);
            Controls.Add(label_key);
            Controls.Add(comboBox_drives);
            Controls.Add(label_decryptionsOptions);
            Controls.Add(panel_decrypt);
            Controls.Add(label_encryptionOperations);
            Controls.Add(label_files);
            Controls.Add(label_directory);
            Controls.Add(listBox_directories);
            Controls.Add(textBox_path);
            Controls.Add(progressBar);
            Controls.Add(button_gotopath);
            Controls.Add(checkedListBox_files);
            Controls.Add(panel_encrypt);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6, 7, 6, 7);
            MaximizeBox = false;
            MinimumSize = new Size(1708, 1140);
            Name = "FormMain";
            ShowIcon = false;
            Text = "ThingEncrypter";
            Load += Form1_Load;
            panel_encrypt.ResumeLayout(false);
            panel_decrypt.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void Button_encryptAll_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private CheckedListBox checkedListBox_files;
        private Button button_gotopath;
        private ProgressBar progressBar;
        private TextBox textBox_path;
        private ListBox listBox_directories;
        private Label label_directory;
        private Label label_files;
        private Button button_encryptSelected;
        private Button button_encryptAll;
        private Panel panel_encrypt;
        private Label label_encryptionOperations;
        private Label label_decryptionsOptions;
        private Panel panel_decrypt;
        private Button button_decryptSelected;
        private Button button_decryptAll;
        private ComboBox comboBox_drives;
        private Button button_encryptallANDsub;
        private Button button_decryptallANDsub;
        private Label label_key;
        private Panel panel1;
        private Button button_generateNewKey;
        private Button button_selectKey;
        private Label label_keydisplay;
        private Label label_keystatus;
    }
}
