//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: Program.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Better517Na._517VPNClient.UI
{
    /// <summary>
    /// 入口
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The only one
        /// </summary>
        private static Mutex onlyOne;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            onlyOne = new Mutex(true, Process.GetCurrentProcess().ProcessName);
            if (onlyOne.WaitOne(0, false))
            {
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("517VPN客户端已启动！", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
