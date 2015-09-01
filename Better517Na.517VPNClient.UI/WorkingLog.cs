//-----------------------------------------------------------------------
// <copyright file="WorkingLog.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: WorkingLog.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Better.Infrastructures.Log;
using Better517Na._517VPNClient.Common;

namespace Better517Na._517VPNClient.UI
{
    /// <summary>
    /// 运行日志窗体
    /// </summary>
    public partial class WorkingLog : Form
    {
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public WorkingLog()
        {
            LogStr = new StringBuilder();
            this.InitializeComponent();
        }

        /// <summary>
        /// 日志字符串
        /// </summary>
        public static StringBuilder LogStr
        {
            get;
            set;
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="color">字体颜色</param>
        /// <param name="iswriteLog">是否记录日志</param>
        public void ShowMessage(string msg, Color color, bool iswriteLog)
        {
            if (rtbShowWorkingLog.InvokeRequired == false)
            {
                if (rtbShowWorkingLog.Text.Length > 20000)
                {
                    rtbShowWorkingLog.Clear();
                    LogStr.Remove(0, LogStr.Length);
                }

                try
                {
                    int tempTextLength = rtbShowWorkingLog.Text.Length;
                    string strInfo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg;
                    if (iswriteLog)
                    {
                        LogManager.Log.WriteException(new AppException(strInfo, null, ExceptionLevel.Info));
                    }

                    rtbShowWorkingLog.AppendText(strInfo + Environment.NewLine);
                    rtbShowWorkingLog.Select(tempTextLength, strInfo.Length);
                    rtbShowWorkingLog.SelectionColor = color;
                    rtbShowWorkingLog.Select(rtbShowWorkingLog.Text.Length, 0);
                    rtbShowWorkingLog.ScrollToCaret();
                    LogStr.Append(rtbShowWorkingLog.Text);
                }
                catch (Exception ex)
                {
                    string forStyleCope = "吃掉他" + ex.Message;
                }
            }
            else
            {
                WriteMessageOnScreen delegateShowMsg = new WriteMessageOnScreen(this.ShowMessage);
                this.BeginInvoke(delegateShowMsg, msg, color, iswriteLog);
            }
        }

        /// <summary>
        /// 关闭窗体前的事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void WorkingLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
