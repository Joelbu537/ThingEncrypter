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
            label_encryptionOperations = new Label();
            label_decryptionsOptions = new Label();
            panel_decrypt = new Panel();
            _decryptSelected = new Button();
            button_decryptAll = new Button();
            panel_encrypt.SuspendLayout();
            panel_decrypt.SuspendLayout();
            SuspendLayout();
            // 
            // checkedListBox_files
            // 
            checkedListBox_files.FormattingEnabled = true;
            checkedListBox_files.Items.AddRange(new object[] { "Datei1", "Datei2", "Datei3", "Datei4", "Datei5", "Datei6" });
            checkedListBox_files.Location = new Point(355, 74);
            checkedListBox_files.Name = "checkedListBox_files";
            checkedListBox_files.Size = new Size(310, 328);
            checkedListBox_files.TabIndex = 0;
            // 
            // button_gotopath
            // 
            button_gotopath.Enabled = false;
            button_gotopath.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_gotopath.Location = new Point(690, 12);
            button_gotopath.Margin = new Padding(5);
            button_gotopath.Name = "button_gotopath";
            button_gotopath.Size = new Size(75, 23);
            button_gotopath.TabIndex = 1;
            button_gotopath.Text = "Go!";
            button_gotopath.UseVisualStyleBackColor = true;
            button_gotopath.Click += button_gotopath_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(237, 426);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(400, 23);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 2;
            progressBar.Visible = false;
            // 
            // textBox_path
            // 
            textBox_path.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_path.Location = new Point(15, 12);
            textBox_path.MinimumSize = new Size(600, 23);
            textBox_path.Name = "textBox_path";
            textBox_path.Size = new Size(650, 25);
            textBox_path.TabIndex = 3;
            textBox_path.TextChanged += textBox_path_TextChanged;
            // 
            // listBox_directories
            // 
            listBox_directories.Font = new Font("Segoe UI", 10F);
            listBox_directories.FormattingEnabled = true;
            listBox_directories.ItemHeight = 17;
            listBox_directories.Location = new Point(15, 75);
            listBox_directories.Name = "listBox_directories";
            listBox_directories.Size = new Size(310, 327);
            listBox_directories.TabIndex = 4;
            listBox_directories.SelectedIndexChanged += listBox_directories_SelectedIndexChanged;
            // 
            // label_directory
            // 
            label_directory.AutoSize = true;
            label_directory.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_directory.Location = new Point(112, 45);
            label_directory.Name = "label_directory";
            label_directory.Size = new Size(97, 23);
            label_directory.TabIndex = 5;
            label_directory.Text = "Directories";
            // 
            // label_files
            // 
            label_files.AutoSize = true;
            label_files.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold);
            label_files.Location = new Point(490, 45);
            label_files.Name = "label_files";
            label_files.Size = new Size(45, 23);
            label_files.TabIndex = 6;
            label_files.Text = "Files";
            // 
            // button_encryptSelected
            // 
            button_encryptSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_encryptSelected.Location = new Point(10, 14);
            button_encryptSelected.Margin = new Padding(5);
            button_encryptSelected.Name = "button_encryptSelected";
            button_encryptSelected.Size = new Size(75, 23);
            button_encryptSelected.TabIndex = 7;
            button_encryptSelected.Text = "Selected";
            button_encryptSelected.UseVisualStyleBackColor = true;
            button_encryptSelected.Click += button_encryptSelected_Click;
            // 
            // button_encryptAll
            // 
            button_encryptAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_encryptAll.Location = new Point(10, 47);
            button_encryptAll.Margin = new Padding(5);
            button_encryptAll.Name = "button_encryptAll";
            button_encryptAll.Size = new Size(75, 23);
            button_encryptAll.TabIndex = 8;
            button_encryptAll.Text = "All";
            button_encryptAll.UseVisualStyleBackColor = true;
            // 
            // panel_encrypt
            // 
            panel_encrypt.BackColor = SystemColors.ControlDark;
            panel_encrypt.BackgroundImageLayout = ImageLayout.None;
            panel_encrypt.Controls.Add(button_encryptSelected);
            panel_encrypt.Controls.Add(button_encryptAll);
            panel_encrypt.Location = new Point(677, 92);
            panel_encrypt.Name = "panel_encrypt";
            panel_encrypt.Size = new Size(95, 84);
            panel_encrypt.TabIndex = 1;
            // 
            // label_encryptionOperations
            // 
            label_encryptionOperations.AutoSize = true;
            label_encryptionOperations.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_encryptionOperations.Location = new Point(701, 74);
            label_encryptionOperations.Name = "label_encryptionOperations";
            label_encryptionOperations.Size = new Size(49, 15);
            label_encryptionOperations.TabIndex = 7;
            label_encryptionOperations.Text = "Encrypt";
            // 
            // label_decryptionsOptions
            // 
            label_decryptionsOptions.AutoSize = true;
            label_decryptionsOptions.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_decryptionsOptions.Location = new Point(701, 191);
            label_decryptionsOptions.Name = "label_decryptionsOptions";
            label_decryptionsOptions.Size = new Size(52, 15);
            label_decryptionsOptions.TabIndex = 9;
            label_decryptionsOptions.Text = "Decrypt";
            // 
            // panel_decrypt
            // 
            panel_decrypt.BackColor = SystemColors.ControlDark;
            panel_decrypt.BackgroundImageLayout = ImageLayout.None;
            panel_decrypt.Controls.Add(_decryptSelected);
            panel_decrypt.Controls.Add(button_decryptAll);
            panel_decrypt.Location = new Point(677, 209);
            panel_decrypt.Name = "panel_decrypt";
            panel_decrypt.Size = new Size(95, 84);
            panel_decrypt.TabIndex = 8;
            // 
            // _decryptSelected
            // 
            _decryptSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _decryptSelected.Location = new Point(10, 14);
            _decryptSelected.Margin = new Padding(5);
            _decryptSelected.Name = "_decryptSelected";
            _decryptSelected.Size = new Size(75, 23);
            _decryptSelected.TabIndex = 7;
            _decryptSelected.Text = "Selected";
            _decryptSelected.UseVisualStyleBackColor = true;
            // 
            // button_decryptAll
            // 
            button_decryptAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button_decryptAll.Location = new Point(10, 47);
            button_decryptAll.Margin = new Padding(5);
            button_decryptAll.Name = "button_decryptAll";
            button_decryptAll.Size = new Size(75, 23);
            button_decryptAll.TabIndex = 8;
            button_decryptAll.Text = "All";
            button_decryptAll.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 461);
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
            MaximizeBox = false;
            MinimumSize = new Size(810, 500);
            Name = "FormMain";
            ShowIcon = false;
            Text = "Ding";
            Load += Form1_Load;
            panel_encrypt.ResumeLayout(false);
            panel_decrypt.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private Button _decryptSelected;
        private Button button_decryptAll;
    }
}
