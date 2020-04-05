namespace NextVozDownloadImage
{
    partial class Form1
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            ""}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.lvDownloadProcess = new System.Windows.Forms.ListView();
            this.colLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProcess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grOptions = new System.Windows.Forms.GroupBox();
            this.cbCreateDirByThreadName = new System.Windows.Forms.CheckBox();
            this.cbIgnoreSmallImage = new System.Windows.Forms.CheckBox();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.nrToPage = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nrMaxImageInSubDirectory = new System.Windows.Forms.NumericUpDown();
            this.cbDownloadAllPage = new System.Windows.Forms.CheckBox();
            this.cbSubDirectory = new System.Windows.Forms.CheckBox();
            this.nrFormPage = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectSavePath = new System.Windows.Forms.Button();
            this.nrNumberThreads = new System.Windows.Forms.NumericUpDown();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbTotalDownloaded = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbTotalSize = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnStartDownload = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.grOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrToPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrMaxImageInSubDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrFormPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrNumberThreads)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Link:";
            // 
            // txtLink
            // 
            this.txtLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLink.Location = new System.Drawing.Point(51, 5);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(564, 20);
            this.txtLink.TabIndex = 1;
            this.txtLink.TextChanged += new System.EventHandler(this.txtLink_TextChanged);
            // 
            // lvDownloadProcess
            // 
            this.lvDownloadProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDownloadProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLink,
            this.colProcess});
            this.lvDownloadProcess.HideSelection = false;
            this.lvDownloadProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lvDownloadProcess.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvDownloadProcess.Location = new System.Drawing.Point(0, 161);
            this.lvDownloadProcess.Name = "lvDownloadProcess";
            this.lvDownloadProcess.Size = new System.Drawing.Size(770, 265);
            this.lvDownloadProcess.TabIndex = 2;
            this.lvDownloadProcess.UseCompatibleStateImageBehavior = false;
            this.lvDownloadProcess.View = System.Windows.Forms.View.Details;
            // 
            // colLink
            // 
            this.colLink.Name = "colLink";
            this.colLink.Text = "Link";
            this.colLink.Width = 450;
            // 
            // colProcess
            // 
            this.colProcess.Name = "colProcess";
            this.colProcess.Text = "Process";
            this.colProcess.Width = 300;
            // 
            // grOptions
            // 
            this.grOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grOptions.Controls.Add(this.cbCreateDirByThreadName);
            this.grOptions.Controls.Add(this.cbIgnoreSmallImage);
            this.grOptions.Controls.Add(this.lbTotalPage);
            this.grOptions.Controls.Add(this.btnLogin);
            this.grOptions.Controls.Add(this.nrToPage);
            this.grOptions.Controls.Add(this.label8);
            this.grOptions.Controls.Add(this.nrMaxImageInSubDirectory);
            this.grOptions.Controls.Add(this.cbDownloadAllPage);
            this.grOptions.Controls.Add(this.cbSubDirectory);
            this.grOptions.Controls.Add(this.nrFormPage);
            this.grOptions.Controls.Add(this.label5);
            this.grOptions.Controls.Add(this.label3);
            this.grOptions.Controls.Add(this.btnSelectSavePath);
            this.grOptions.Controls.Add(this.nrNumberThreads);
            this.grOptions.Controls.Add(this.txtSavePath);
            this.grOptions.Controls.Add(this.label1);
            this.grOptions.Location = new System.Drawing.Point(3, 31);
            this.grOptions.Name = "grOptions";
            this.grOptions.Size = new System.Drawing.Size(767, 124);
            this.grOptions.TabIndex = 3;
            this.grOptions.TabStop = false;
            this.grOptions.Text = "Tùy chọn";
            this.grOptions.Enter += new System.EventHandler(this.grOptions_Enter);
            // 
            // cbCreateDirByThreadName
            // 
            this.cbCreateDirByThreadName.AutoSize = true;
            this.cbCreateDirByThreadName.Checked = true;
            this.cbCreateDirByThreadName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateDirByThreadName.Location = new System.Drawing.Point(48, 46);
            this.cbCreateDirByThreadName.Name = "cbCreateDirByThreadName";
            this.cbCreateDirByThreadName.Size = new System.Drawing.Size(164, 17);
            this.cbCreateDirByThreadName.TabIndex = 7;
            this.cbCreateDirByThreadName.Text = "Tạo thư mục theo tên tiêu đề";
            this.cbCreateDirByThreadName.UseVisualStyleBackColor = true;
            this.cbCreateDirByThreadName.CheckedChanged += new System.EventHandler(this.cbCreateDirByThreadName_CheckedChanged);
            // 
            // cbIgnoreSmallImage
            // 
            this.cbIgnoreSmallImage.AutoSize = true;
            this.cbIgnoreSmallImage.Location = new System.Drawing.Point(346, 46);
            this.cbIgnoreSmallImage.Name = "cbIgnoreSmallImage";
            this.cbIgnoreSmallImage.Size = new System.Drawing.Size(152, 17);
            this.cbIgnoreSmallImage.TabIndex = 7;
            this.cbIgnoreSmallImage.Text = "Bỏ qua ảnh nhỏ (200x200)";
            this.cbIgnoreSmallImage.UseVisualStyleBackColor = true;
            this.cbIgnoreSmallImage.CheckedChanged += new System.EventHandler(this.cbIgnoreSmallImage_CheckedChanged);
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Location = new System.Drawing.Point(183, 20);
            this.lbTotalPage.Name = "lbTotalPage";
            this.lbTotalPage.Size = new System.Drawing.Size(18, 13);
            this.lbTotalPage.TabIndex = 10;
            this.lbTotalPage.Text = "/0";
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnLogin.Location = new System.Drawing.Point(618, 10);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(144, 27);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // nrToPage
            // 
            this.nrToPage.Enabled = false;
            this.nrToPage.Location = new System.Drawing.Point(122, 16);
            this.nrToPage.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nrToPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nrToPage.Name = "nrToPage";
            this.nrToPage.Size = new System.Drawing.Size(57, 20);
            this.nrToPage.TabIndex = 12;
            this.nrToPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nrToPage.ValueChanged += new System.EventHandler(this.nrToPage_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(106, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "-";
            // 
            // nrMaxImageInSubDirectory
            // 
            this.nrMaxImageInSubDirectory.Enabled = false;
            this.nrMaxImageInSubDirectory.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nrMaxImageInSubDirectory.Location = new System.Drawing.Point(153, 64);
            this.nrMaxImageInSubDirectory.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nrMaxImageInSubDirectory.Name = "nrMaxImageInSubDirectory";
            this.nrMaxImageInSubDirectory.Size = new System.Drawing.Size(62, 20);
            this.nrMaxImageInSubDirectory.TabIndex = 8;
            this.nrMaxImageInSubDirectory.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nrMaxImageInSubDirectory.ValueChanged += new System.EventHandler(this.nrMaxImageInSubDirectory_ValueChanged);
            // 
            // cbDownloadAllPage
            // 
            this.cbDownloadAllPage.AutoSize = true;
            this.cbDownloadAllPage.Checked = true;
            this.cbDownloadAllPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDownloadAllPage.Location = new System.Drawing.Point(235, 18);
            this.cbDownloadAllPage.Name = "cbDownloadAllPage";
            this.cbDownloadAllPage.Size = new System.Drawing.Size(66, 17);
            this.cbDownloadAllPage.TabIndex = 7;
            this.cbDownloadAllPage.Text = "Toàn bộ";
            this.cbDownloadAllPage.UseVisualStyleBackColor = true;
            this.cbDownloadAllPage.CheckedChanged += new System.EventHandler(this.cbDownloadAllPage_CheckedChanged);
            // 
            // cbSubDirectory
            // 
            this.cbSubDirectory.AutoSize = true;
            this.cbSubDirectory.Location = new System.Drawing.Point(48, 68);
            this.cbSubDirectory.Name = "cbSubDirectory";
            this.cbSubDirectory.Size = new System.Drawing.Size(107, 17);
            this.cbSubDirectory.TabIndex = 7;
            this.cbSubDirectory.Text = "Tạo thư mục con";
            this.cbSubDirectory.UseVisualStyleBackColor = true;
            this.cbSubDirectory.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // nrFormPage
            // 
            this.nrFormPage.Enabled = false;
            this.nrFormPage.Location = new System.Drawing.Point(48, 16);
            this.nrFormPage.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nrFormPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nrFormPage.Name = "nrFormPage";
            this.nrFormPage.Size = new System.Drawing.Size(53, 20);
            this.nrFormPage.TabIndex = 9;
            this.nrFormPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nrFormPage.ValueChanged += new System.EventHandler(this.nrFormPage_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Page:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Số luồng:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnSelectSavePath
            // 
            this.btnSelectSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSavePath.Enabled = false;
            this.btnSelectSavePath.Location = new System.Drawing.Point(729, 93);
            this.btnSelectSavePath.Name = "btnSelectSavePath";
            this.btnSelectSavePath.Size = new System.Drawing.Size(33, 22);
            this.btnSelectSavePath.TabIndex = 5;
            this.btnSelectSavePath.Text = "...";
            this.btnSelectSavePath.UseVisualStyleBackColor = true;
            this.btnSelectSavePath.Click += new System.EventHandler(this.btnSelectSavePath_Click);
            // 
            // nrNumberThreads
            // 
            this.nrNumberThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nrNumberThreads.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nrNumberThreads.Location = new System.Drawing.Point(548, 15);
            this.nrNumberThreads.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nrNumberThreads.Name = "nrNumberThreads";
            this.nrNumberThreads.Size = new System.Drawing.Size(63, 20);
            this.nrNumberThreads.TabIndex = 4;
            this.nrNumberThreads.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nrNumberThreads.ValueChanged += new System.EventHandler(this.nrNumberThreads_ValueChanged);
            // 
            // txtSavePath
            // 
            this.txtSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSavePath.Enabled = false;
            this.txtSavePath.Location = new System.Drawing.Point(48, 94);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(680, 20);
            this.txtSavePath.TabIndex = 1;
            this.txtSavePath.TextChanged += new System.EventHandler(this.txtSavePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lưu tại:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(588, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ảnh đã tải:";
            // 
            // lbTotalDownloaded
            // 
            this.lbTotalDownloaded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotalDownloaded.AutoSize = true;
            this.lbTotalDownloaded.Location = new System.Drawing.Point(645, 7);
            this.lbTotalDownloaded.Name = "lbTotalDownloaded";
            this.lbTotalDownloaded.Size = new System.Drawing.Size(13, 13);
            this.lbTotalDownloaded.TabIndex = 4;
            this.lbTotalDownloaded.Text = "0";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(687, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Size:";
            // 
            // lbTotalSize
            // 
            this.lbTotalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotalSize.AutoSize = true;
            this.lbTotalSize.Location = new System.Drawing.Point(718, 7);
            this.lbTotalSize.Name = "lbTotalSize";
            this.lbTotalSize.Size = new System.Drawing.Size(30, 13);
            this.lbTotalSize.TabIndex = 4;
            this.lbTotalSize.Text = "0 KB";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lbTotalDownloaded);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbTotalSize);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 24);
            this.panel1.TabIndex = 6;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(57, 6);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(16, 13);
            this.lbStatus.TabIndex = 5;
            this.lbStatus.Text = "...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Trạng thái:";
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartDownload.Enabled = false;
            this.btnStartDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnStartDownload.Location = new System.Drawing.Point(621, 1);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(62, 27);
            this.btnStartDownload.TabIndex = 7;
            this.btnStartDownload.Text = "Tải";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPause.Location = new System.Drawing.Point(684, 1);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(81, 27);
            this.btnPause.TabIndex = 7;
            this.btnPause.Text = "Tạm ngừng";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 449);
            this.Controls.Add(this.grOptions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStartDownload);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.lvDownloadProcess);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NextVoz Download Image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grOptions.ResumeLayout(false);
            this.grOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrToPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrMaxImageInSubDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrFormPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nrNumberThreads)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.ListView lvDownloadProcess;
        private System.Windows.Forms.GroupBox grOptions;
        private System.Windows.Forms.Button btnSelectSavePath;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nrNumberThreads;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nrFormPage;
        private System.Windows.Forms.NumericUpDown nrMaxImageInSubDirectory;
        private System.Windows.Forms.CheckBox cbSubDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbTotalDownloaded;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbTotalSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nrToPage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbDownloadAllPage;
        private System.Windows.Forms.ColumnHeader colLink;
        private System.Windows.Forms.ColumnHeader colProcess;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.CheckBox cbIgnoreSmallImage;
        private System.Windows.Forms.CheckBox cbCreateDirByThreadName;
    }
}

