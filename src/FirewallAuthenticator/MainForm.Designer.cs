namespace FirewallAuthenticator
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ofdXMLfile = new System.Windows.Forms.OpenFileDialog();
            this.gbXMLFile = new System.Windows.Forms.GroupBox();
            this.btnXMlFile = new System.Windows.Forms.Button();
            this.txtXMLFile = new System.Windows.Forms.TextBox();
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.btnAuthenticate = new System.Windows.Forms.Button();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblUName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.gbURLGroups = new System.Windows.Forms.GroupBox();
            this.lblURLGroups = new System.Windows.Forms.Label();
            this.clbURLGroups = new System.Windows.Forms.CheckedListBox();
            this.ttUrls = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reauthMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createXMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mustEnterPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreCertificateErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reauthTimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOneHour = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTwoHours = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFourHours = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.authTimer = new System.Windows.Forms.Timer(this.components);
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.gbXMLFile.SuspendLayout();
            this.gbLogin.SuspendLayout();
            this.gbResults.SuspendLayout();
            this.gbURLGroups.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdXMLfile
            // 
            this.ofdXMLfile.FileName = "xmlfile";
            // 
            // gbXMLFile
            // 
            this.gbXMLFile.Controls.Add(this.btnXMlFile);
            this.gbXMLFile.Controls.Add(this.txtXMLFile);
            this.gbXMLFile.Location = new System.Drawing.Point(26, 84);
            this.gbXMLFile.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbXMLFile.Name = "gbXMLFile";
            this.gbXMLFile.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbXMLFile.Size = new System.Drawing.Size(1018, 130);
            this.gbXMLFile.TabIndex = 0;
            this.gbXMLFile.TabStop = false;
            this.gbXMLFile.Text = "Firewall URLs File";
            // 
            // btnXMlFile
            // 
            this.btnXMlFile.Location = new System.Drawing.Point(839, 39);
            this.btnXMlFile.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnXMlFile.Name = "btnXMlFile";
            this.btnXMlFile.Size = new System.Drawing.Size(162, 62);
            this.btnXMlFile.TabIndex = 1;
            this.btnXMlFile.Text = "Browse";
            this.btnXMlFile.UseVisualStyleBackColor = true;
            this.btnXMlFile.Click += new System.EventHandler(this.btnXMlFile_Click);
            // 
            // txtXMLFile
            // 
            this.txtXMLFile.Location = new System.Drawing.Point(13, 47);
            this.txtXMLFile.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtXMLFile.Name = "txtXMLFile";
            this.txtXMLFile.Size = new System.Drawing.Size(806, 39);
            this.txtXMLFile.TabIndex = 0;
            this.txtXMLFile.Text = "Click Browse to select an XML File with Firewall URLs";
            this.txtXMLFile.TextChanged += new System.EventHandler(this.txtXMLFile_TextChanged);
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.btnAuthenticate);
            this.gbLogin.Controls.Add(this.lblPwd);
            this.gbLogin.Controls.Add(this.lblUName);
            this.gbLogin.Controls.Add(this.txtPassword);
            this.gbLogin.Controls.Add(this.txtUsername);
            this.gbLogin.Location = new System.Drawing.Point(674, 229);
            this.gbLogin.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbLogin.Size = new System.Drawing.Size(373, 337);
            this.gbLogin.TabIndex = 1;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Credentials";
            // 
            // btnAuthenticate
            // 
            this.btnAuthenticate.Location = new System.Drawing.Point(20, 236);
            this.btnAuthenticate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnAuthenticate.Name = "btnAuthenticate";
            this.btnAuthenticate.Size = new System.Drawing.Size(327, 57);
            this.btnAuthenticate.TabIndex = 4;
            this.btnAuthenticate.Text = "Authenticate!";
            this.btnAuthenticate.UseVisualStyleBackColor = true;
#pragma warning disable CA1416 // Validate platform compatibility
            this.btnAuthenticate.Click += new System.EventHandler(this.btnAuthenticate_Click);
#pragma warning restore CA1416 // Validate platform compatibility
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(13, 153);
            this.lblPwd.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(116, 32);
            this.lblPwd.TabIndex = 3;
            this.lblPwd.Text = "Password:";
            // 
            // lblUName
            // 
            this.lblUName.AutoSize = true;
            this.lblUName.Location = new System.Drawing.Point(13, 89);
            this.lblUName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUName.Name = "lblUName";
            this.lblUName.Size = new System.Drawing.Size(126, 32);
            this.lblUName.TabIndex = 2;
            this.lblUName.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(152, 145);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(190, 39);
            this.txtPassword.TabIndex = 1;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(152, 81);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(190, 39);
            this.txtUsername.TabIndex = 0;
            // 
            // gbResults
            // 
            this.gbResults.Controls.Add(this.txtResults);
            this.gbResults.Location = new System.Drawing.Point(26, 581);
            this.gbResults.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbResults.Name = "gbResults";
            this.gbResults.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbResults.Size = new System.Drawing.Size(1018, 347);
            this.gbResults.TabIndex = 2;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "Authentication Results";
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(13, 47);
            this.txtResults.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(975, 280);
            this.txtResults.TabIndex = 0;
            this.txtResults.Text = "Authentication results will show here.";
            // 
            // gbURLGroups
            // 
            this.gbURLGroups.Controls.Add(this.clbURLGroups);
            this.gbURLGroups.Controls.Add(this.lblURLGroups);
            this.gbURLGroups.Location = new System.Drawing.Point(26, 229);
            this.gbURLGroups.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbURLGroups.Name = "gbURLGroups";
            this.gbURLGroups.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gbURLGroups.Size = new System.Drawing.Size(633, 337);
            this.gbURLGroups.TabIndex = 3;
            this.gbURLGroups.TabStop = false;
            this.gbURLGroups.Text = "URL Groups";
            // 
            // lblURLGroups
            // 
            this.lblURLGroups.Location = new System.Drawing.Point(15, 49);
            this.lblURLGroups.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblURLGroups.Name = "lblURLGroups";
            this.lblURLGroups.Size = new System.Drawing.Size(604, 44);
            this.lblURLGroups.TabIndex = 1;
            this.lblURLGroups.Text = "Select one or more URL groups to authenticate against:";
            // 
            // clbURLGroups
            // 
            this.clbURLGroups.FormattingEnabled = true;
            this.clbURLGroups.Location = new System.Drawing.Point(20, 91);
            this.clbURLGroups.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.clbURLGroups.Name = "clbURLGroups";
            this.clbURLGroups.ScrollAlwaysVisible = true;
            this.clbURLGroups.Size = new System.Drawing.Size(596, 220);
            this.clbURLGroups.TabIndex = 0;
            this.clbURLGroups.MouseMove += new System.Windows.Forms.MouseEventHandler(this.clbURLGroups_MouseMove);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "Firewall Authenticator";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reauthMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(450, 42);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // reauthMenuItem
            // 
            this.reauthMenuItem.Name = "reauthMenuItem";
            this.reauthMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.A)));
            this.reauthMenuItem.Size = new System.Drawing.Size(449, 38);
            this.reauthMenuItem.Text = "Re-authenticate";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 36);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 44);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createXMLFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mustEnterPasswordToolStripMenuItem,
            this.ignoreCertificateErrorsToolStripMenuItem,
            this.reauthTimerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(89, 36);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // createXMLFileToolStripMenuItem
            // 
            this.createXMLFileToolStripMenuItem.Name = "createXMLFileToolStripMenuItem";
            this.createXMLFileToolStripMenuItem.Size = new System.Drawing.Size(421, 44);
            this.createXMLFileToolStripMenuItem.Text = "Create Empty Firewall File";
            this.createXMLFileToolStripMenuItem.Click += new System.EventHandler(this.createXMLFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(418, 6);
            // 
            // mustEnterPasswordToolStripMenuItem
            // 
            this.mustEnterPasswordToolStripMenuItem.Checked = true;
            this.mustEnterPasswordToolStripMenuItem.CheckOnClick = true;
            this.mustEnterPasswordToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mustEnterPasswordToolStripMenuItem.Name = "mustEnterPasswordToolStripMenuItem";
            this.mustEnterPasswordToolStripMenuItem.Size = new System.Drawing.Size(421, 44);
            this.mustEnterPasswordToolStripMenuItem.Text = "Always Require Password";
            // 
            // ignoreCertificateErrorsToolStripMenuItem
            // 
            this.ignoreCertificateErrorsToolStripMenuItem.Checked = true;
            this.ignoreCertificateErrorsToolStripMenuItem.CheckOnClick = true;
            this.ignoreCertificateErrorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreCertificateErrorsToolStripMenuItem.Name = "ignoreCertificateErrorsToolStripMenuItem";
            this.ignoreCertificateErrorsToolStripMenuItem.Size = new System.Drawing.Size(421, 44);
            this.ignoreCertificateErrorsToolStripMenuItem.Text = "Ignore Certificate Errors";
            // 
            // reauthTimerToolStripMenuItem
            // 
            this.reauthTimerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOneHour,
            this.menuItemTwoHours,
            this.menuItemFourHours});
            this.reauthTimerToolStripMenuItem.Name = "reauthTimerToolStripMenuItem";
            this.reauthTimerToolStripMenuItem.Size = new System.Drawing.Size(421, 44);
            this.reauthTimerToolStripMenuItem.Text = "Re-auth Timer";
            // 
            // menuItemOneHour
            // 
            this.menuItemOneHour.CheckOnClick = true;
            this.menuItemOneHour.Name = "menuItemOneHour";
            this.menuItemOneHour.Size = new System.Drawing.Size(227, 44);
            this.menuItemOneHour.Tag = "1";
            this.menuItemOneHour.Text = "1 hour";
            this.menuItemOneHour.CheckStateChanged += new System.EventHandler(this.menuItemOneHour_CheckStateChanged);
            this.menuItemOneHour.Click += new System.EventHandler(this.customToolStripMenuItem_Clicked);
            // 
            // menuItemTwoHours
            // 
            this.menuItemTwoHours.CheckOnClick = true;
            this.menuItemTwoHours.Name = "menuItemTwoHours";
            this.menuItemTwoHours.Size = new System.Drawing.Size(227, 44);
            this.menuItemTwoHours.Tag = "2";
            this.menuItemTwoHours.Text = "2 hours";
            this.menuItemTwoHours.Click += new System.EventHandler(this.customToolStripMenuItem_Clicked);
            // 
            // menuItemFourHours
            // 
            this.menuItemFourHours.CheckOnClick = true;
            this.menuItemFourHours.Name = "menuItemFourHours";
            this.menuItemFourHours.Size = new System.Drawing.Size(227, 44);
            this.menuItemFourHours.Tag = "4";
            this.menuItemFourHours.Text = "4 hours";
            this.menuItemFourHours.Click += new System.EventHandler(this.customToolStripMenuItem_Clicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(13, 5, 0, 5);
            this.menuStrip1.Size = new System.Drawing.Size(1072, 46);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // authTimer
            // 
            this.authTimer.Interval = 300000;
            this.authTimer.Tick += new System.EventHandler(this.authTimer_Tick);
            // 
            // statusTimer
            // 
            this.statusTimer.Interval = 5000;
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnAuthenticate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 950);
            this.Controls.Add(this.gbURLGroups);
            this.Controls.Add(this.gbLogin);
            this.Controls.Add(this.gbResults);
            this.Controls.Add(this.gbXMLFile);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firewall Authenticator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.gbXMLFile.ResumeLayout(false);
            this.gbXMLFile.PerformLayout();
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.gbResults.ResumeLayout(false);
            this.gbURLGroups.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdXMLfile;
        private System.Windows.Forms.GroupBox gbXMLFile;
        private System.Windows.Forms.Button btnXMlFile;
        private System.Windows.Forms.TextBox txtXMLFile;
        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblUName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnAuthenticate;
        private System.Windows.Forms.GroupBox gbResults;
        private System.Windows.Forms.GroupBox gbURLGroups;
        private System.Windows.Forms.CheckedListBox clbURLGroups;
        private System.Windows.Forms.Label lblURLGroups;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.ToolTip ttUrls;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createXMLFileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reauthMenuItem;
        private System.Windows.Forms.Timer authTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mustEnterPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reauthTimerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemOneHour;
        private System.Windows.Forms.ToolStripMenuItem menuItemTwoHours;
        private System.Windows.Forms.ToolStripMenuItem menuItemFourHours;
        private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.ToolStripMenuItem ignoreCertificateErrorsToolStripMenuItem;
    }
}

