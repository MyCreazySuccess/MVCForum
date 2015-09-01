//-----------------------------------------------------------------------
// <copyright file="GetIP.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: GetIP.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Better517Na._517VPNClient.Common
{
    /// <summary>
    /// 获取IP
    /// </summary>
    public class GetIP
    {
        /// <summary>
        /// ip
        /// </summary>
        private static string ipaddress = string.Empty;

        /// <summary>
        /// 获取本地地址
        /// </summary>
        /// <param name="adapterName">适配器名称</param>
        /// <returns>返回IP</returns>
        public string GetLocalIp(string adapterName)
        {
            string ip = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(ipaddress))
                {
                    ////获取所有的连接
                    NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface network in networkInterfaces)
                    {
                        if (network.Name.Contains(adapterName))
                        {
                            /////读取每一个网络连接的IP信息
                            IPInterfaceProperties properties = network.GetIPProperties();

                            ////每一个网络连接有多行IP信息
                            foreach (IPAddressInformation address in properties.UnicastAddresses)
                            {
                                /////读取IPv4地址
                                if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                                {
                                    continue;
                                }

                                if (IPAddress.IsLoopback(address.Address))
                                {
                                    continue;
                                }

                                ip = address.Address.ToString();
                            }

                            break;
                        }
                    }
                }
                else
                {
                    ip = ipaddress;
                }
            }
            catch (Exception ex)
            {
                MessagePipe.ExcuteWriteMessageEvent("获取本地IP异常", Color.Red, true);
            }

            return ip;
        }
    }
}
