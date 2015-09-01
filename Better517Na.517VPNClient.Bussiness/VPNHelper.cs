//-----------------------------------------------------------------------
// <copyright file="VPNHelper.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: VPNHelper.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using Better.Infrastructures.Log;
using Better.SecurityUtility;
using Better517Na._517VPNClient.Common;
using DotRas;

namespace Better517Na.VPNServerCheck.Bussiness
{
    /// <summary>
    /// VPN辅助类
    /// </summary>
    public class VPNHelper
    {
        /// <summary>
        /// 系统路径 C:\windows\system32
        /// </summary>
        private static string winDir = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\";

        /// <summary>
        /// rasdial.exe
        /// </summary>
        private static string rasDialFileName = "rasdial.exe";

        /// <summary>
        /// VPN地址
        /// </summary>
        private string vpnIP = string.Empty;

        /// <summary>
        /// VPN名称
        /// </summary>
        private string vpnName = string.Empty;

        /// <summary>
        /// VPN用户名
        /// </summary>
        private string userName = string.Empty;

        /// <summary>
        /// VPN密码
        /// </summary>
        private string passWord = string.Empty;

        /// <summary>
        /// VPN路径 C:\windows\system32\rasdial.exe
        /// </summary>
        private string vpnProcess = winDir + rasDialFileName;

        /// <summary>
        /// 构造函数
        /// </summary>
        public VPNHelper()
        {
        }

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="vpnIP">VPN地址</param>
        /// <param name="vpnName">VPN名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        public VPNHelper(string vpnIP, string vpnName, string userName, string passWord)
        {
            this.vpnIP = vpnIP;
            this.vpnName = vpnName;
            this.userName = userName;
            this.passWord = passWord;
        }

        /// <summary>
        /// 尝试连接VPN(默认VPN)
        /// </summary>
        /// <returns>返回连接结果</returns>
        public bool TryConnectVPN()
        {
            return this.TryConnectVPN(this.vpnName, this.userName, this.passWord);
        }

        /// <summary>
        /// 尝试断开连接(默认VPN)
        /// </summary>
        /// <returns>释放成功true，释放失败false</returns>
        public bool TryDisConnectVPN()
        {
            return this.TryDisConnectVPN(this.vpnName);
        }

        /// <summary>
        /// 创建或更新一个默认的VPN连接
        /// </summary>
        /// <returns>操作结果</returns>
        public bool CreateOrUpdateVPN()
        {
            return this.CreateOrUpdateVPN(this.vpnName, this.vpnIP);
        }

        /// <summary>
        /// 尝试删除连接(默认VPN)
        /// </summary>
        public void TryDeleteVPN()
        {
            this.TryDeleteVPN(this.vpnName);
        }

        /// <summary>
        /// 尝试连接VPN(指定VPN名称，用户名，密码)
        /// </summary>
        /// <param name="connVpnName">vpn名称</param>
        /// <param name="connUserName">连接的用户名称</param>
        /// <param name="connPassWord">连接的密码</param>
        /// <returns>连接成功true，连接失败false</returns>
        public bool TryConnectVPN(string connVpnName, string connUserName, string connPassWord)
        {
            bool result = false;
            DateTime startTime = DateTime.Now;
            string linkInfo = string.Empty;
            try
            {
                string args = string.Format("{0} {1} {2}", connVpnName, connUserName, SecurityUtility.DecryptString(connPassWord));

                ProcessStartInfo myProcess = new ProcessStartInfo(this.vpnProcess, args);

                myProcess.CreateNoWindow = true;

                myProcess.UseShellExecute = false;
                myProcess.RedirectStandardOutput = true;
                var excuteResult = Process.Start(myProcess);
                linkInfo = excuteResult.StandardOutput.ReadToEnd();
                linkInfo = linkInfo.Replace("\n", string.Empty);
                if (linkInfo.Contains("已连接"))
                {
                    result = true;
                }
                else
                {
                    MessagePipe.ExcuteWriteMessageEvent("登陆失败，账号:" + connUserName + "密码:" + SecurityUtility.EncryptDES(connPassWord) + "VPN服务器:" + this.vpnIP, Color.Green, true);
                }

                MessagePipe.ExcuteWriteMessageEvent(connVpnName + "登陆验证结果:" + linkInfo, Color.Green, false);
            }
            catch (Exception ex)
            {
                string msg = "登陆异常，账号:" + connUserName + "密码:" + SecurityUtility.EncryptDES(connPassWord) + "VPN服务器:" + this.vpnIP;
                LogManager.Log.WriteException(new AppException(msg, ex, ExceptionLevel.Info));
            }
            finally
            {
                ////记录交互日志
                string request = "VPN通道名称:" + connVpnName + ";VPN账号:" + connUserName;
                RecordLog.RecordInteractionLog(connUserName, "通过用账号和密码登陆", request, linkInfo, DateTime.Now.Subtract(startTime), this.vpnIP);
            }

            return result;
        }

        /// <summary>
        /// 尝试断开VPN(指定VPN名称)
        /// </summary>
        /// <param name="disConnVpnName">连接的vpn名称</param>
        /// <returns>返回执行结果，释放成功true，释放失败false</returns>
        public bool TryDisConnectVPN(string disConnVpnName)
        {
            bool result = false;
            DateTime startTime = DateTime.Now;
            string linkInfo = string.Empty;
            try
            {
                string args = string.Format(@"{0} /d", disConnVpnName);

                ProcessStartInfo myProcess = new ProcessStartInfo(this.vpnProcess, args);

                myProcess.CreateNoWindow = true;

                myProcess.UseShellExecute = false;
                myProcess.RedirectStandardOutput = true;
                var excuteResult = Process.Start(myProcess);
                linkInfo = excuteResult.StandardOutput.ReadToEnd();
                linkInfo = linkInfo.Replace("\n", string.Empty);
                if (!linkInfo.Contains("没有连接") && linkInfo.Contains("命令已完成"))
                {
                    result = true;
                }

                MessagePipe.ExcuteWriteMessageEvent("释放结果:" + linkInfo, Color.Green, true);
            }
            catch (Exception ex)
            {
                string msg = disConnVpnName + "断开VPN异常" + ex;
                MessagePipe.ExcuteWriteMessageEvent(msg, Color.Red, true);
            }
            finally
            {
                ////记录交互日志
                string request = "VPN通道名称" + disConnVpnName + ";VPN地址:" + this.vpnIP + ";VPN账号:" + this.userName;
                RecordLog.RecordInteractionLog(this.userName, "断开VPN连接", request, linkInfo, DateTime.Now.Subtract(startTime), this.vpnIP);
            }

            return result;
        }

        /// <summary>
        /// 创建或更新一个VPN连接(指定VPN名称，及IP)
        /// </summary>
        /// <param name="updateVPNname">更新vpn名称</param>
        /// <param name="updateVPNip">更新VPN地址</param>
        /// <returns>操作成功：true,操作失败：false</returns>
        public bool CreateOrUpdateVPN(string updateVPNname, string updateVPNip)
        {
            bool result = true;
            DateTime startTime = DateTime.Now;
            string msg = string.Empty;
            try
            {
                RasDialer dialer = new RasDialer();
                RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
                allUsersPhoneBook.Open();

                // 如果已经该名称的VPN已经存在，则更新这个VPN服务器地址
                if (allUsersPhoneBook.Entries.Contains(updateVPNname))
                {
                    allUsersPhoneBook.Entries[updateVPNname].PhoneNumber = updateVPNip;
                    ////不管当前VPN是否连接，服务器地址的更新总能成功，如果正在连接，则需要VPN重启后才能起作用
                    bool updateResult = allUsersPhoneBook.Entries[updateVPNname].Update();
                    msg = "更新VPN服务器通道为:" + updateVPNip;
                    if (updateResult)
                    {
                        MessagePipe.ExcuteWriteMessageEvent(msg + "成功", Color.Green, false);
                    }
                    else
                    {
                        MessagePipe.ExcuteWriteMessageEvent(msg + "失败", Color.Green, false);
                    }
                }
                else
                {
                    ////创建一个新VPN
                    RasEntry entry = RasEntry.CreateVpnEntry(updateVPNname, updateVPNip, RasVpnStrategy.PptpFirst, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));

                    allUsersPhoneBook.Entries.Add(entry);
                    dialer.EntryName = updateVPNname;

                    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                    msg = "新建VPN通道:" + updateVPNip + "成功";
                    MessagePipe.ExcuteWriteMessageEvent(msg, Color.Green, false);
                }
            }
            catch (Exception ex)
            {
                result = false;
                MessagePipe.ExcuteWriteMessageEvent("更新或创建VPN通道异常:" + ex.Message.ToString(), Color.Green, true);
            }
            finally
            {
                ////记录交互日志
                string request = "VPN通道名称:" + updateVPNname + ";VPN账号:" + this.userName + ";VPN地址:" + updateVPNip;
                RecordLog.RecordInteractionLog(updateVPNname, "创建VPN通道", request, msg, DateTime.Now.Subtract(startTime), updateVPNip);
            }

            return result;
        }

        /// <summary>
        /// 删除指定名称的VPN
        /// 如果VPN正在运行，一样会在电话本里删除，但是不会断开连接，所以，最好是先断开连接，再进行删除操作
        /// </summary>
        /// <param name="delVpnName">待删除的VPN名称</param>
        public void TryDeleteVPN(string delVpnName)
        {
            if (!string.IsNullOrEmpty(delVpnName))
            {
                RasDialer dialer = new RasDialer();
                RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
                try
                {
                    allUsersPhoneBook.Open();
                    if (allUsersPhoneBook.Entries.Contains(delVpnName))
                    {
                        allUsersPhoneBook.Entries.Remove(delVpnName);
                    }
                }
                catch (Exception ex)
                {
                    MessagePipe.ExcuteWriteMessageEvent("删除指定VPN名称异常:" + ex.Message.ToString(), Color.Green, true);
                }
            }
        }

        /// <summary>
        /// 判断是否连接到VPN使用cmd ipconfig命令，判断里面的子网掩码和默认网关的值
        /// VPN的默认网关代码为：0.0.0.0
        /// VPN的子网掩码默认值为：255.255.255.255
        /// </summary>
        /// <returns>返回连接结果</returns>
        public bool IsConnectVPN()
        {
            Process myProcess = new Process();
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.FileName = "ipconfig.exe";
            myProcess.Start();
            myProcess.StandardInput.AutoFlush = true;
            myProcess.StandardInput.WriteLine("exit");

            string content = myProcess.StandardOutput.ReadToEnd();
            myProcess.WaitForExit();
            myProcess.Close();
            if (content.Contains("0.0.0.0"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取当前正在连接中的VPN名称
        /// </summary>
        /// <returns>返回当前连接的VPN名称</returns>
        public List<string> GetCurrentConnectingVPNNames()
        {
            List<string> connectingVPNList = new List<string>();
            try
            {
                Process proIP = new Process();

                proIP.StartInfo.FileName = "cmd.exe ";
                proIP.StartInfo.UseShellExecute = false;
                proIP.StartInfo.RedirectStandardInput = true;
                proIP.StartInfo.RedirectStandardOutput = true;
                proIP.StartInfo.RedirectStandardError = true;
                ////不显示cmd窗口 
                proIP.StartInfo.CreateNoWindow = true;
                proIP.Start();

                proIP.StandardInput.WriteLine(rasDialFileName);
                proIP.StandardInput.WriteLine("exit");

                // 命令行运行结果
                string strResult = proIP.StandardOutput.ReadToEnd();
                proIP.Close();

                Regex regger = new Regex("(?<=已连接\r\n)(.*\n)*(?=命令已完成)", RegexOptions.Multiline);

                // 如果匹配，则说有正在连接的VPN
                if (regger.IsMatch(strResult))
                {
                    string[] list = regger.Match(strResult).Value.ToString().Split('\n');
                    for (int index = 0; index < list.Length; index++)
                    {
                        if (list[index] != string.Empty)
                        {
                            connectingVPNList.Add(list[index].Replace("\r", string.Empty));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessagePipe.ExcuteWriteMessageEvent("获取正在连接的VPN异常:" + ex.Message.ToString(), Color.Red, true);
            }

            // 没有正在连接的VPN，则直接返回一个空List<string>
            return connectingVPNList;
        }
    }
}
