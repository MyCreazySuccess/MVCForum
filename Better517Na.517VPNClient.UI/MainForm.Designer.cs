namespace Better517Na._517VPNClient.UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolItemSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItWorkingLog = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnLinkVPN = new System.Windows.Forms.Button();
            this.BtnDisconnectVpn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StartBtn = new System.Windows.Forms.Button();
            this.ListViewVpnShow = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStatuAccount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStatuIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStatuLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.MenuItWorkingLog});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(674, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolItemSaveLog,
            this.ToolItemExit});
            this.设置ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // ToolItemSaveLog
            // 
            this.ToolItemSaveLog.Name = "ToolItemSaveLog";
            this.ToolItemSaveLog.Size = new System.Drawing.Size(162, 24);
            this.ToolItemSaveLog.Text = "保存运行日志";
            this.ToolItemSaveLog.Click += new System.EventHandler(this.ToolItemSaveLog_Click);
            // 
            // ToolItemExit
            // 
            this.ToolItemExit.Name = "ToolItemExit";
            this.ToolItemExit.Size = new System.Drawing.Size(162, 24);
            this.ToolItemExit.Text = "退出";
            this.ToolItemExit.Click += new System.EventHandler(this.ToolItemExit_Click);
            // 
            // MenuItWorkingLog
            // 
            this.MenuItWorkingLog.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MenuItWorkingLog.Name = "MenuItWorkingLog";
            this.MenuItWorkingLog.Size = new System.Drawing.Size(105, 24);
            this.MenuItWorkingLog.Text = "查看运行日志";
            this.MenuItWorkingLog.Click += new System.EventHandler(this.MenuItWorkingLog_Click);
            // 
            // BtnLinkVPN
            // 
            this.BtnLinkVPN.Enabled = false;
            this.BtnLinkVPN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnLinkVPN.Location = new System.Drawing.Point(245, 14);
            this.BtnLinkVPN.Name = "BtnLinkVPN";
            this.BtnLinkVPN.Size = new System.Drawing.Size(85, 31);
            this.BtnLinkVPN.TabIndex = 0;
            this.BtnLinkVPN.Text = "连接";
            this.BtnLinkVPN.UseVisualStyleBackColor = true;
            this.BtnLinkVPN.Click += new System.EventHandler(this.BtnLinkVPN_Click);
            // 
            // BtnDisconnectVpn
            // 
            this.BtnDisconnectVpn.Enabled = false;
            this.BtnDisconnectVpn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnDisconnectVpn.Location = new System.Drawing.Point(363, 14);
            this.BtnDisconnectVpn.Name = "BtnDisconnectVpn";
            this.BtnDisconnectVpn.Size = new System.Drawing.Size(80, 31);
            this.BtnDisconnectVpn.TabIndex = 1;
            this.BtnDisconnectVpn.Text = "断开";
            this.BtnDisconnectVpn.UseVisualStyleBackColor = true;
            this.BtnDisconnectVpn.Click += new System.EventHandler(this.BtnDisconnectVpn_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(474, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 31);
            this.button3.TabIndex = 2;
            this.button3.Text = "测速";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.StartBtn);
            this.panel1.Controls.Add(this.BtnLinkVPN);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.BtnDisconnectVpn);
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 73);
            this.panel1.TabIndex = 3;
            // 
            // StartBtn
            // 
            this.StartBtn.Enabled = false;
            this.StartBtn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartBtn.Location = new System.Drawing.Point(130, 14);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(85, 31);
            this.StartBtn.TabIndex = 5;
            this.StartBtn.Text = "启动";
            this.StartBtn.UseVisualStyleBackColor = true;
            // 
            // ListViewVpnShow
            // 
            this.ListViewVpnShow.BackColor = System.Drawing.SystemColors.Info;
            this.ListViewVpnShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader1});
            this.ListViewVpnShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewVpnShow.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListViewVpnShow.FullRowSelect = true;
            this.ListViewVpnShow.GridLines = true;
            this.ListViewVpnShow.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewVpnShow.Location = new System.Drawing.Point(0, 0);
            this.ListViewVpnShow.Name = "ListViewVpnShow";
            this.ListViewVpnShow.Size = new System.Drawing.Size(674, 345);
            this.ListViewVpnShow.TabIndex = 4;
            this.ListViewVpnShow.UseCompatibleStateImageBehavior = false;
            this.ListViewVpnShow.View = System.Windows.Forms.View.Details;
            this.ListViewVpnShow.SelectedIndexChanged += new System.EventHandler(this.ListViewVpnShow_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "来源";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "地区";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "线路";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "IP";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 179;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "测速";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 100;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.ListViewVpnShow);
            this.panel2.Location = new System.Drawing.Point(0, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 345);
            this.panel2.TabIndex = 5;
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.ToolStatuAccount,
            this.toolStripStatusLabel3,
            this.ToolStatuIP,
            this.toolStripStatusLabel2,
            this.ToolStatuLink});
            this.statusStrip.Location = new System.Drawing.Point(0, 437);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(674, 25);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(96, 20);
            this.toolStripStatusLabel1.Text = "当前锁定账号:";
            // 
            // ToolStatuAccount
            // 
            this.ToolStatuAccount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolStatuAccount.Name = "ToolStatuAccount";
            this.ToolStatuAccount.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel3.Text = "当前使用IP:";
            // 
            // ToolStatuIP
            // 
            this.ToolStatuIP.Name = "ToolStatuIP";
            this.ToolStatuIP.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(79, 20);
            this.toolStripStatusLabel2.Text = "连接状态：";
            // 
            // ToolStatuLink
            // 
            this.ToolStatuLink.Name = "ToolStatuLink";
            this.ToolStatuLink.Size = new System.Drawing.Size(51, 20);
            this.ToolStatuLink.Text = "未连接";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 462);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "517VPN客户端";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolItemSaveLog;
        private System.Windows.Forms.ToolStripMenuItem ToolItemExit;
        private System.Windows.Forms.ToolStripMenuItem MenuItWorkingLog;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button BtnDisconnectVpn;
        private System.Windows.Forms.Button BtnLinkVPN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView ListViewVpnShow;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStatuAccount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel ToolStatuIP;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel ToolStatuLink;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button StartBtn;
    }
}

