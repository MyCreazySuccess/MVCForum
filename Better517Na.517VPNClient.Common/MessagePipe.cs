//-----------------------------------------------------------------------
// <copyright file="MessagePipe.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: MessagePipe.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Drawing;
using Better517Na.VPNDataService.Model;

namespace Better517Na._517VPNClient.Common
{
    /// <summary>
    /// 自定义向屏幕写信息代理
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="color">颜色</param>
    /// <param name="iswriteLog">是否记录日志</param>
    public delegate void WriteMessageOnScreen(string msg, Color color, bool iswriteLog);

    /// <summary>
    /// 显示vpn信息代理
    /// </summary>
    /// <param name="vpnServers">vpnServers</param>
    public delegate void ShowVpnServerInfo(List<MVPNServer> vpnServers);

    /// <summary>
    /// 清空ListView里面的数据
    /// </summary>
    /// <param name="name">删除指定name的数据</param>
    public delegate void DelVpnServerInfo(string name);

    /// <summary>
    /// 显示账号
    /// </summary>
    /// <param name="account">account</param>
    public delegate void ShowAccountInfo(string account);

    /// <summary>
    /// 显示IP
    /// </summary>
    /// <param name="ipaddress">ipaddress</param>
    public delegate void ShowIpInfo(string ipaddress);

    /// <summary>
    /// 显示IP
    /// </summary>
    /// <param name="linkStr">linkStr</param>
    public delegate void ShowLinkInfo(string linkStr);

    /// <summary>
    /// 信息显示
    /// </summary>
    public class MessagePipe
    {
        /// <summary>
        /// 写信息事件
        /// </summary>
        public static event WriteMessageOnScreen WriteMessageEvent;

        /// <summary>
        /// 绑定VPN信息事件
        /// </summary>
        public static event ShowVpnServerInfo BindVpnInfoEvent;

        /// <summary>
        /// 显示账号事件
        /// </summary>
        public static event ShowAccountInfo ShowAccountInfoEvent;

        /// <summary>
        /// 显示IP事件
        /// </summary>
        public static event ShowIpInfo ShowIpInfoEvent;

        /// <summary>
        /// 显示连接状态事件
        /// </summary>
        public static event ShowLinkInfo ShowLinkInfoEvent;

        /// <summary>
        /// 清空ListView里面的数据
        /// </summary>
        public static event DelVpnServerInfo DelVpnServerInfoEvent;

        /// <summary>
        /// 执行删除数据
        /// </summary>
        /// <param name="name">name</param>
        public static void ExcuteDelVpnServerInfoEvent(string name)
        {
            if (DelVpnServerInfoEvent != null)
            {
                DelVpnServerInfoEvent(name);
            }
        }

        /// <summary>
        /// 执行连接状态绑定事件
        /// </summary>
        /// <param name="linkStatu">linkStatu</param>
        public static void ExcuteShowLinkInfoEvent(string linkStatu)
        {
            if (ShowLinkInfoEvent != null)
            {
                ShowLinkInfoEvent(linkStatu);
            }
        }

        /// <summary>
        /// 执行IP绑定事件
        /// </summary>
        /// <param name="ip">ip</param>
        public static void ExcuteShowIpInfoEvent(string ip)
        {
            if (ShowIpInfoEvent != null)
            {
                ShowIpInfoEvent(ip);
            }
        }

        /// <summary>
        /// 执行账号绑定事件
        /// </summary>
        /// <param name="account">账号</param>
        public static void ExcuteShowAccountInfoEvent(string account)
        {
            if (ShowAccountInfoEvent != null)
            {
                ShowAccountInfoEvent(account);
            }
        }

        /// <summary>
        /// 执行VPN绑定VPN事件
        /// </summary>
        /// <param name="vpnServers">vpn服务器</param>
        public static void ExcuteBindVpnServerInfoEvent(List<MVPNServer> vpnServers)
        {
            if (BindVpnInfoEvent != null)
            {
                BindVpnInfoEvent(vpnServers);
            }
        }

        /// <summary>
        /// 执行写信息事件
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="color">颜色</param>
        /// <param name="iswriteLog">是否记录日志</param>
        public static void ExcuteWriteMessageEvent(string message, Color color, bool iswriteLog)
        {
            if (WriteMessageEvent != null)
            {
                WriteMessageEvent(message, color, iswriteLog);
            }
        }
    }
}
