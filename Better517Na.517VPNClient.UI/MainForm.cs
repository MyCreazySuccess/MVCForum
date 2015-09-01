//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: MainForm.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Better517Na._517VPNClient.Bussiness;
using Better517Na._517VPNClient.Common;
using Better517Na.Common.SysParam;
using Better517Na.VPNDataService.Model;
using Better517NA.InfrastructureThread.Threading;

namespace Better517Na._517VPNClient.UI
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// vpn控制线程
        /// </summary>
        private VpnControlThread vpnControlThread = null;

        /// <summary>
        /// 最开始的选择
        /// </summary>
        private int oldSelect = -1;

        /// <summary>
        /// 运行日志窗体
        /// </summary>
        private WorkingLog workingLog = new WorkingLog();

        /// <summary>
        /// 弹出提示框
        /// </summary>
        private Action<string> bulletMsg = null;

        /// <summary>
        /// 窗体初始化
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            ////检测是否配置路由，如果没有，则弹出提示框，如果配置了，直接启动
            List<MSysDicItemInfo> dicParams = DicParamHelper.GetParamById("ITZC_YW_VpnNetWorkSegmentRoute");
            if (dicParams == null || dicParams.Count == 0)
            {
                MessageBox.Show("没有获取到路由参数，请配置路由参数！", "517VPN客户端提示", MessageBoxButtons.OK);
                System.Environment.Exit(0);
            }
            else
            {
                ////添加路由
                bool addResult = this.AddNetWorkRoute(dicParams);
                if (addResult)
                {
                    this.workingLog.WindowState = FormWindowState.Minimized;
                    this.workingLog.Show();
                    MessagePipe.WriteMessageEvent += this.workingLog.ShowMessage;
                    MessagePipe.BindVpnInfoEvent += this.ShowVpnServer;
                    MessagePipe.DelVpnServerInfoEvent += this.DelVpnServer;
                    MessagePipe.ShowAccountInfoEvent += this.ShowAccount;
                    MessagePipe.ShowIpInfoEvent += this.ShowIP;
                    MessagePipe.ShowLinkInfoEvent += this.ShowLinkStatu;
                    this.bulletMsg = this.BulletMessage;
                    if (this.vpnControlThread == null)
                    {
                        this.vpnControlThread = new VpnControlThread();
                    }

                    this.vpnControlThread.MaxThreadCount = 4;
                    this.vpnControlThread.ExcuteThreadInstance();
                }
                else
                {
                    MessageBox.Show("添加路由失败，程序不能启动?", "517VPN客户端提示", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// 添加网络路由
        /// </summary>
        /// <param name="dicParams">取到的系统参数</param>
        /// <returns>返回添加结果</returns>
        private bool AddNetWorkRoute(List<MSysDicItemInfo> dicParams)
        {
            bool result = false;
            TestLink link = new TestLink();
            try
            {
                ////先获取默认网关
                string defaultGateWay = link.GetDefaultGateWay();
                foreach (var dicParam in dicParams)
                {
                    if (!string.IsNullOrEmpty(defaultGateWay))
                    {
                        List<string> temp = dicParam.ParaValue.Split('|').ToList();

                        //////取到网关，添加路由
                        string addRouteResult = link.AddNetWorkRoute(temp[0], temp[1], defaultGateWay);
                        if (addRouteResult.Contains("操作完成") || addRouteResult.Contains("对象已存在"))
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessagePipe.ExcuteWriteMessageEvent("添加路由出现异常" + ex.Message.ToString(), Color.Red, true);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="msg">信息</param>
        private void BulletMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        /// <summary>
        /// 显示连接状态
        /// </summary>
        /// <param name="linkStatu">linkStatu</param>
        private void ShowLinkStatu(string linkStatu)
        {
            if (this.statusStrip.InvokeRequired == false)
            {
                this.ToolStatuLink.Text = linkStatu;
            }
            else
            {
                ShowAccountInfo accountDelegate = new ShowAccountInfo(this.ShowLinkStatu);
                this.BeginInvoke(accountDelegate, linkStatu);
            }
        }

        /// <summary>
        /// 显示IP
        /// </summary>
        /// <param name="ip">ip</param>
        private void ShowIP(string ip)
        {
            if (this.statusStrip.InvokeRequired == false)
            {
                this.ToolStatuIP.Text = ip;
            }
            else
            {
                ShowAccountInfo accountDelegate = new ShowAccountInfo(this.ShowIP);
                this.BeginInvoke(accountDelegate, ip);
            }
        }

        /// <summary>
        /// 显示账号
        /// </summary>
        /// <param name="account">账号</param>
        private void ShowAccount(string account)
        {
            if (this.statusStrip.InvokeRequired == false)
            {
                this.ToolStatuAccount.Text = account;
            }
            else
            {
                ShowAccountInfo accountDelegate = new ShowAccountInfo(this.ShowAccount);
                this.BeginInvoke(accountDelegate, account);
            }
        }

        /// <summary>
        /// 删除vpn信息
        /// </summary>
        /// <param name="name">name</param>
        private void DelVpnServer(string name)
        {
            DelVpnServerInfo delegateServerInfo = new DelVpnServerInfo(this.DelVpnInfo);
            this.BeginInvoke(delegateServerInfo, name);
        }

        /// <summary>
        /// 显示出取到的vpn服务器信息
        /// </summary>
        /// <param name="vpnServers">vpn服务器信息</param>
        private void ShowVpnServer(List<MVPNServer> vpnServers)
        {
            ShowVpnServerInfo serverInfo = new ShowVpnServerInfo(this.BindVpnInfo);
            this.BeginInvoke(serverInfo, vpnServers);
        }

        /// <summary>
        /// 绑定VPN信息
        /// </summary>
        /// <param name="vpnServers">VPN服务器信息</param>
        private void BindVpnInfo(List<MVPNServer> vpnServers)
        {
            foreach (var temp in vpnServers)
            {
                ListViewItem item = new ListViewItem();
                item.Text = temp.Provider;
                item.Name = temp.Provider + "|" + temp.Address;
                item.SubItems.Add(temp.Area);
                item.SubItems.Add(temp.Line);
                item.SubItems.Add(temp.Address);
                this.ListViewVpnShow.Items.Add(item);
            }
        }

        /// <summary>
        /// 清空VPN信息
        /// </summary>
        /// <param name="name">name</param>
        private void DelVpnInfo(string name)
        {
            if (this.ListViewVpnShow.Items.ContainsKey(name))
            {
                this.ListViewVpnShow.Items.RemoveByKey(name);
            }
            else if (name == "clear")
            {
                this.ListViewVpnShow.Items.Clear();
            }
        }

        /// <summary>
        /// 打开运行日志窗口
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MenuItWorkingLog_Click(object sender, EventArgs e)
        {
            this.workingLog.WindowState = FormWindowState.Normal;
            this.workingLog.Show();
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BtnLinkVPN_Click(object sender, EventArgs e)
        {
            if (this.ListViewVpnShow.SelectedItems.Count > 0)
            {
                try
                {
                    ////发送跳出指令，认定IP被封
                    SignalControl.Issuspend = true;

                    ////令ListView不可以选择
                    this.ListViewVpnShow.Enabled = false;

                    //////

                    ////ListViewItem viewItem = this.ListViewVpnShow.FocusedItem;
                    ////MVPNServer vpnServer = new MVPNServer();
                    ////vpnServer.Provider = viewItem.Text;
                    ////vpnServer.Area = viewItem.SubItems[1].Text.ToString();
                    ////vpnServer.Line = viewItem.SubItems[2].Text.ToString();
                    ////vpnServer.Address = viewItem.SubItems[3].Text.ToString();
                    ////if (this.linkThread == null)
                    ////{
                    ////    this.linkThread = new BetterThread();
                    ////}

                    ////this.linkThread.ExcuteThreadInstance(
                    ////    () =>
                    ////    {
                    ////        if (flagThreadWorking)
                    ////        {
                    ////            flagThreadWorking = false;
                    ////            ////删除现有连接，建立新的连接
                    ////            ////断开VPN
                    ////            if (VpnControlThread.VpnHelper != null)
                    ////            {
                    ////                ////还原原来使用的IP状态

                    ////                ////断开之前判断是否处于连接状态，如果处于连接状态无法删除
                    ////                MessagePipe.ExcuteShowIpInfoEvent(vpnServer.Address);
                    ////                List<string> islinkingVpn = VpnControlThread.VpnHelper.GetCurrentConnectingVPNNames();
                    ////                if (islinkingVpn == null || islinkingVpn.Count == 0)
                    ////                {
                    ////                    VpnControlThread.VpnHelper.TryDisConnectVPN();
                    ////                    VpnControlThread.VpnHelper.TryDeleteVPN();
                    ////                    MessagePipe.ExcuteShowLinkInfoEvent("未连接");
                    ////                    //////建立新的连接
                    ////                    bool linkResult = this.vpnControlThread.IslinkVpnServer(VpnControlThread.VpnAccount, vpnServer);
                    ////                    SignalControl.Issuspend = true;

                    ////                    SignalControl.WaitSignal.WaitOne();
                    ////                    if (linkResult)
                    ////                    {
                    ////                        this.BulletMessage("人工手动替换VPN服务器成功");
                    ////                        MessagePipe.ExcuteShowLinkInfoEvent("连接成功");
                    ////                    }
                    ////                    else
                    ////                    {
                    ////                        this.BulletMessage("人工手动替换VPN服务器失败，请重新选一个!!!");
                    ////                        MessagePipe.ExcuteShowLinkInfoEvent("未连接");
                    ////                    }
                    ////                }
                    ////            }
                    ////            else
                    ////            {
                    ////                this.BulletMessage("有正在连接中的请求，请稍后替换！！！");
                    ////            }
                    ////        }
                    ////    },
                    ////    null,
                    ////    null,
                    ////    TimeSpan.FromSeconds(1));
                }
                catch (Exception ex)
                {
                    MessagePipe.ExcuteWriteMessageEvent("手动替换VPN异常" + ex.Message.ToString(), Color.Red, true);
                }
            }
            else
            {
                MessageBox.Show("请选中要替换的VPN服务器");
            }
        }

        /// <summary>
        /// 断开VPN连接
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BtnDisconnectVpn_Click(object sender, EventArgs e)
        {
            bool disConnectResult = VpnControlThread.VpnHelper.TryDisConnectVPN();
            VpnControlThread.VpnHelper.TryDeleteVPN();
            if (disConnectResult)
            {
                MessageBox.Show("断开成功");
            }
            else
            {
                MessageBox.Show("断开失败");
            }
        }

        /// <summary>
        /// 保存日志按钮
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ToolItemSaveLog_Click(object sender, EventArgs e)
        {
            ////利用SaveFileDialog，让用户指定文件的路径名
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "文本文件|*.txt";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    // 创建文件，将textBox1中的内容保存到文件中
                    // saveDlg.FileName 是用户指定的文件路径
                    fs = File.Open(saveDlg.FileName, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    List<string> recordStr = WorkingLog.LogStr.ToString().Split(Environment.NewLine.ToCharArray()).ToList();
                    foreach (string line in recordStr)
                    {
                        sw.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessagePipe.ExcuteWriteMessageEvent("保存日志文件失败" + ex.Message.ToString(), Color.Red, false);
                }
                finally
                {
                    if (sw != null)
                    {
                        ////关闭文件
                        sw.Flush();
                        sw.Close();
                    }

                    if (fs != null)
                    {
                        fs.Close();
                    }
                }

                ////提示用户：文件保存的位置和文件名
                MessageBox.Show("文件已成功保存到" + saveDlg.FileName);
            }
        }

        /// <summary>
        /// 退出提示
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ToolItemExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出吗?", "517VPN系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.ExitOperate();
                Application.Exit();
            }
        }

        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定退出吗?", "517VPN系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.ExitOperate();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 退出操作
        /// </summary>
        private void ExitOperate()
        {
            if (VpnControlThread.VpnHelper != null)
            {
                ////退出之前关闭VPN
                bool disConnectResult = VpnControlThread.VpnHelper.TryDisConnectVPN();
                VpnControlThread.VpnHelper.TryDeleteVPN();
            }

            if (VpnControlThread.VpnAccount != null)
            {
                ////解锁状态
                VpnInfo vpnInfo = new VpnInfo();
                vpnInfo.ReleaseVpnAccountState(VpnControlThread.VpnAccount);
                if (VpnControlThread.VpnServer != null)
                {
                    vpnInfo.ExitUpdateVpnIsValid(VpnControlThread.VpnServer);
                }
            }
        }

        /// <summary>
        /// 当选中项发生了变化
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ListViewVpnShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////若有选中项 
            if (this.ListViewVpnShow.SelectedIndices.Count > 0)
            {
                if (this.oldSelect == -1)
                {
                    ////设置当前选中项索引 
                    this.ListViewVpnShow.Items[this.ListViewVpnShow.SelectedIndices[0]].BackColor = Color.FromArgb(49, 106, 197);
                    this.oldSelect = this.ListViewVpnShow.SelectedIndices[0];
                }
                else
                {
                    if (this.ListViewVpnShow.SelectedIndices[0] != this.oldSelect)
                    {
                        this.ListViewVpnShow.Items[this.ListViewVpnShow.SelectedIndices[0]].BackColor = Color.FromArgb(49, 106, 197);

                        ////恢复默认背景色 
                        this.ListViewVpnShow.Items[this.oldSelect].BackColor = Color.FromArgb(239, 248, 250);

                        ////设置当前选中项索引 
                        this.oldSelect = this.ListViewVpnShow.SelectedIndices[0];
                    }
                }
            }
            else
            {
                ////若无选中项 
                this.ListViewVpnShow.Items[this.oldSelect].BackColor = Color.FromArgb(239, 248, 250);

                ////设置当前处于无选中项状态 
                this.oldSelect = -1;
            }
        }
    }
}
