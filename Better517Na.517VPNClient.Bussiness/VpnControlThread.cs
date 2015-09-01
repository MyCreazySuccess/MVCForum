//-----------------------------------------------------------------------
// <copyright file="VpnControlThread.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: VpnControlThread.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Better.Infrastructures.Log;
using Better517Na._517VPNClient.Common;
using Better517Na.AutoTicketIPMonitor.Business;
using Better517Na.Common.SysParam;
using Better517Na.VPNDataService.Model;
using Better517Na.VPNServerCheck.Bussiness;
using Better517NA.InfrastructureThread.Threading;
using Better.SecurityUtility;

namespace Better517Na._517VPNClient.Bussiness
{
    /// <summary>
    /// vpn控制线程
    /// </summary>
    public class VpnControlThread : BetterThread
    {
        /// <summary>
        /// 替换IP的时间
        /// </summary>
        private DateTime replaceIpTime = DateTime.MinValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        public VpnControlThread()
        {
            ProductVpnServers = new List<MVPNServer>();
        }

        /// <summary>
        /// vpnHelper（为了手动而设置）
        /// </summary>
        public static VPNHelper VpnHelper
        {
            get;
            set;
        }

        /// <summary>
        /// 当前使用的账号（为了手动而设置）
        /// </summary>
        public static MVPNAccount VpnAccount
        {
            get;
            set;
        }

        /// <summary>
        /// VpnServer（为了手动而设置）
        /// </summary>
        public static MVPNServer VpnServer
        {
            get;
            set;
        }

        /// <summary>
        /// 生产VPN服务器数据（为了手动而设置）
        /// </summary>
        public static List<MVPNServer> ProductVpnServers
        {
            get;
            set;
        }

        /// <summary>
        /// 是否连接上VPN服务器
        /// </summary>
        /// <param name="vpnAccount">VPN服务器供应账号信息</param>
        /// <param name="vpnServer">VPN服务器信息</param>
        /// <returns>返回连接结果</returns>
        public bool IslinkVpnServer(MVPNAccount vpnAccount, MVPNServer vpnServer)
        {
            bool linkResult = false;
            VpnHelper = new VPNHelper(vpnServer.Address, vpnServer.Area + vpnServer.Line, VpnAccount.Account, VpnAccount.Password);
            bool createOrUpdateResult = VpnHelper.CreateOrUpdateVPN();
            if (createOrUpdateResult)
            {
                ////连接VPN
                linkResult = VpnHelper.TryConnectVPN();
                if (linkResult)
                {
                    DateTime startTime = DateTime.Now;
                    string testLinkInfo = new TestLink().GetPingStr("www.baidu.com", 32, 200, 3);
                    if (testLinkInfo.Contains("100% 丢失"))
                    {
                        ////访问超时，也认为IP不可用
                        ////记录交互日志
                        string request = "VPN通道名称:" + vpnServer.Area + vpnServer.Line + ";VPN账号:" + vpnAccount.Account;
                        RecordLog.RecordInteractionLog(vpnAccount.Account, "连接VPN成功后，测试请求外网", request, testLinkInfo, DateTime.Now.Subtract(startTime), vpnServer.Address);
                        linkResult = false;
                    }

                    MessagePipe.ExcuteWriteMessageEvent(testLinkInfo, Color.Green, false);
                }
            }

            return linkResult;
        }

        /// <summary>
        /// 重写方法
        /// </summary>
        protected override void ExcuteMethod()
        {
            while (true)
            {
                try
                {
                    bool isneedLock = true;
                    MessagePipe.ExcuteWriteMessageEvent("517VPN客户端启动成功", Color.Green, false);
                    VpnInfo vpnInfo = new VpnInfo();

                    ////获取账号
                    this.GetVpnAccount(ref isneedLock);
                    if (VpnAccount != null && !string.IsNullOrEmpty(VpnAccount.KeyID))
                    {
                        MessagePipe.ExcuteWriteMessageEvent("517VPN客户端获取到账号" + VpnAccount.Account, Color.Green, false);
                        ////马上锁定账号，锁定成功过，才允许使用这个账号
                        bool lockAccountResult = vpnInfo.UpdateVpnAccountState(VpnAccount);
                        if (lockAccountResult || !isneedLock)
                        {
                            MessagePipe.ExcuteShowAccountInfoEvent(VpnAccount.Account);
                            MessagePipe.ExcuteWriteMessageEvent("517VPN客户端锁定账号:" + VpnAccount.Account + "成功", Color.Green, false);

                            ////清空一下加载的数据
                            MessagePipe.ExcuteDelVpnServerInfoEvent("clear");

                            ////加载出所有可用的VPN服务器信息，先拿使用服务器为本机的IP
                            List<MVPNServer> tempExceptionVpn = vpnInfo.GetLocalUsedVpnServer(VpnAccount.Provider);
                            ProductVpnServers.AddRange(tempExceptionVpn);

                            List<MVPNServer> tempValidVpn = this.GetAllValidVpnServer(VpnAccount.Provider);
                            ProductVpnServers.AddRange(tempValidVpn);

                            ////因为有可能因为异常退出，导致IP一直被占用，所以首先取使用IP为自己服务器的IP
                            if (ProductVpnServers != null && ProductVpnServers.Count > 0)
                            {
                                MessagePipe.ExcuteBindVpnServerInfoEvent(ProductVpnServers);

                                ////锁定成功，取一个可用的VPN服务器建立连接(人工切换IP会有影响，)
                                foreach (var tempVpn in ProductVpnServers)
                                {
                                    TrackID.GetInstance("517VPN");

                                    ////////接收到暂停指令
                                    ////if (SignalControl.Issuspend)
                                    ////{
                                    ////    ////等待解锁指令
                                    ////    SignalControl.WaitSignal.WaitOne();
                                    ////}

                                    VpnServer = tempVpn;
                                    MessagePipe.ExcuteShowIpInfoEvent(tempVpn.Address);
                                    MessagePipe.ExcuteShowLinkInfoEvent("连接中...");

                                    ////IP独占成功，方可连接
                                    if (vpnInfo.UpdateVpnUseStatu(tempVpn) || tempVpn.UsingServer == new GetIP().GetLocalIp("本地连接"))
                                    {
                                        string msg = string.Empty;
                                        bool connectResult = this.IslinkVpnServer(VpnAccount, tempVpn);
                                        if (connectResult)
                                        {
                                            MessagePipe.ExcuteWriteMessageEvent("517客户端连接VPN:" + tempVpn.Address + "成功", Color.Green, false);
                                            MessagePipe.ExcuteShowLinkInfoEvent("连接成功");

                                            ////建立连接成功过后，调用业务组件，如果业务组件需要更换VPN服务器，则删除现有连接，重新建立新连接
                                            bool result = this.InvokeIpMonitor();
                                            if (result)
                                            {
                                                ////需要替换IP，还原数据状态
                                                vpnInfo.UpdateVpnIsValid(tempVpn);
                                                MessagePipe.ExcuteWriteMessageEvent("检测到" + tempVpn.Address + "被封，需要替换IP", Color.Green, false);
                                            }
                                        }
                                        else
                                        {
                                            msg = tempVpn.Provider + "|" + tempVpn.Address;
                                            vpnInfo.UpdateVpnLinkStatu(tempVpn);
                                            MessagePipe.ExcuteWriteMessageEvent(msg + "连接失败", Color.Red, false);
                                        }

                                        msg = tempVpn.Provider + "|" + tempVpn.Address;
                                        MessagePipe.ExcuteDelVpnServerInfoEvent(msg);

                                        ////连接失败，删除连接
                                        if (VpnControlThread.VpnHelper != null)
                                        {
                                            ////退出之前关闭VPN
                                            bool disConnectResult = VpnControlThread.VpnHelper.TryDisConnectVPN();
                                            VpnControlThread.VpnHelper.TryDeleteVPN();
                                            MessagePipe.ExcuteShowLinkInfoEvent("未连接");
                                        }
                                    }
                                }

                                ////表明这一轮可用IP已经使用完了，所以清空一下
                                ProductVpnServers.Clear();
                            }
                        }
                        else
                        {
                            MessagePipe.ExcuteWriteMessageEvent("517VPN客户端没有锁定到账号，休眠40秒", Color.Red, true);
                            ////锁定失败，休眠一分钟
                            Thread.Sleep(TimeSpan.FromSeconds(40));
                        }
                    }
                    else
                    {
                        MessagePipe.ExcuteWriteMessageEvent("517VPN客户端没有获取到账号，休眠40秒", Color.Red, true);
                        ////锁定失败，休眠一分钟
                        Thread.Sleep(TimeSpan.FromSeconds(40));
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Log.WriteException(new AppException("517VPN客户端主流程异常", ex, ExceptionLevel.Error));
                    MessagePipe.ExcuteWriteMessageEvent("517VPN主流程异常" + ex.Message.ToString(), Color.Red, false);
                }
                finally
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
        }

        /// <summary>
        /// 获取账号
        /// </summary>
        /// <param name="isneedLock">是否需要锁</param>
        private void GetVpnAccount(ref bool isneedLock)
        {
            ////程序允许就马上去锁定一个账号
            VpnInfo vpnInfo = new VpnInfo();

            ////随机取账号之前，把原来的账号状态还原
            if (VpnAccount != null)
            {
                vpnInfo.ReleaseVpnAccountState(VpnAccount);
            }

            ////先用本机IP取获取一个账号，因为有可能前面本机锁定了一个账号
            ////后来本机出了异常，程序启动需要重新去取一次这个账号
            MVPNAccount temp = vpnInfo.GetVpnAccountByIP();
            if (temp != null && !string.IsNullOrEmpty(temp.KeyID))
            {
                isneedLock = false;
            }
            else
            {
                ////通过IP没有拿到账号，那就随机取一个账号
                temp = vpnInfo.GetVpnAccount();
            }

            if (temp != null && VpnAccount != null && VpnAccount.KeyID == temp.KeyID)
            {
                ////说明又拿到同一个账号了，看能否重新获取一个
                VpnAccount = vpnInfo.GetNewVpnAccount(temp.Provider);
            }
            else
            {
                VpnAccount = temp;
            }
        }

        /// <summary>
        /// 调用IP监控组件
        /// </summary>
        /// <returns>返回true,Ip被限。返回false，IP没有被限制</returns>
        private bool InvokeIpMonitor()
        {
            bool result = false;
            while (true && !SignalControl.Issuspend)
            {
                double sleepTime = 5;
                try
                {
                    ////取监控间隔时间
                    string intervatTime = SysParamHelper.GetParamById<string>("ITZC_YW_CheckIPLockedIntervalTime", "xxxxx");
                    if (intervatTime == "xxxxx")
                    {
                        intervatTime = "5";
                    }

                    sleepTime = Convert.ToDouble(intervatTime);
                    MessagePipe.ExcuteWriteMessageEvent("开始调用监控组件，调用过后睡" + sleepTime + "分钟", Color.Red, false);
                    //////调用监控组件
                    MatchIpLimitRule iplimit = new MatchIpLimitRule();
                    result = iplimit.IslimitIp();

                    ////如果发现需要替换IP，并且距离上一次替换的时间间隔大于2分钟
                    if (result)
                    {
                        if (DateTime.Now.Subtract(this.replaceIpTime).Minutes > 2)
                        {
                            MessagePipe.ExcuteWriteMessageEvent("检测到IP:" + VpnServer.Address + "被限", Color.Red, true);
                            this.replaceIpTime = DateTime.Now;
                            break;
                        }
                        else
                        {
                            MessagePipe.ExcuteWriteMessageEvent("检测到IP被限，但距离上次替换时间间隔不到2分钟，暂不替换", Color.Red, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessagePipe.ExcuteWriteMessageEvent("调用监控组件异常" + ex.Message.ToString(), Color.Red, true);
                }
                finally
                {
                    Thread.Sleep(TimeSpan.FromMinutes(sleepTime));
                }
            }

            return result;
        }

        /// <summary>
        /// 获取指定供应商的所有可用VPN服务器
        /// </summary>
        /// <param name="provider">VPN服务器提供商</param>
        /// <returns>返回VPN服务器信息</returns>
        private List<MVPNServer> GetAllValidVpnServer(string provider)
        {
            List<MVPNServer> allVpnServers = new List<MVPNServer>();
            VpnInfo vpnInfo = new VpnInfo();
            string keyid = "0";
            while (true)
            {
                List<MVPNServer> temp = vpnInfo.GetValidVpn(provider, keyid, 50);
                if (temp != null && temp.Count > 0)
                {
                    keyid = temp.OrderByDescending(p => p.KeyID).ToList()[0].KeyID;
                    allVpnServers.AddRange(temp);
                }

                if (temp == null || temp.Count < 50)
                {
                    break;
                }
            }

            return allVpnServers;
        }
    }
}
