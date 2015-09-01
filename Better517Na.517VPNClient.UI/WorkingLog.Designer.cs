namespace Better517Na._517VPNClient.UI
{
    partial class WorkingLog
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
            this.rtbShowWorkingLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbShowWorkingLog
            // 
            this.rtbShowWorkingLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbShowWorkingLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.rtbShowWorkingLog.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbShowWorkingLog.ForeColor = System.Drawing.Color.White;
            this.rtbShowWorkingLog.Location = new System.Drawing.Point(-3, -2);
            this.rtbShowWorkingLog.Name = "rtbShowWorkingLog";
            this.rtbShowWorkingLog.Size = new System.Drawing.Size(697, 533);
            this.rtbShowWorkingLog.TabIndex = 0;
            this.rtbShowWorkingLog.Text = "";
            // 
            // WorkingLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 526);
            this.Controls.Add(this.rtbShowWorkingLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkingLog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "运行日志";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkingLog_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbShowWorkingLog;

    }
}