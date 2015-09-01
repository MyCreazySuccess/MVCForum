//-----------------------------------------------------------------------
// <copyright file="TestLink.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: TestLink.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Better517Na._517VPNClient.Common;

namespace Better517Na._517VPNClient.Bussiness
{
    /// <summary>
    /// 测试连接
    /// </summary>
    public class TestLink
    {
        /// <summary>
        /// 获取本地系统进程信息
        /// </summary>
        private Process controlPing = new Process();

        /// <summary>
        /// 得到Ping结果字符串
        /// </summary>
        /// <param name="strHost">主机名或ip</param>
        /// <param name="packetSize">发送测试包大小</param>
        /// <param name="timeOut">超时</param>
        /// <param name="tryTimes">测试次数</param>
        /// <returns>结果</returns>
        public string GetPingStr(string strHost, int packetSize, int timeOut, int tryTimes)
        {
            return this.LaunchPingStr(string.Format("{0} -n {1} -l {2} -w {3}", strHost, tryTimes, packetSize, timeOut), "ping.exe");
        }

        /// <summary>
        /// 添加网络路由
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="mask">mask</param>
        /// <param name="gateWay">gateWay</param>
        /// <returns>返回添加结果</returns>
        public string AddNetWorkRoute(string ip, string mask, string gateWay)
        {
            return this.ExcuteOrder(string.Format("route  add   {0}  mask  {1}  {2}", ip, mask, gateWay));
        }

        /// <summary>
        /// 取的默认网关
        /// </summary>
        /// <returns>返回网关地址</returns>
        public string GetDefaultGateWay()
        {
            string defaultGateWay = string.Empty;
            try
            {
                string gateWayStr = this.ExcuteOrder("route print | find \"0.0.0.0\"");
                List<string> temp = gateWayStr.Split(new char[] { ' ' }).ToList();
                List<string> iplist = new List<string>();
                foreach (var str in temp)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        iplist.Add(str);
                        if (iplist.Count > 3)
                        {
                            break;
                        }
                    }
                }

                if (iplist != null && iplist.Count > 3)
                {
                    defaultGateWay = iplist[2];
                }
            }
            catch (Exception ex)
            {
                MessagePipe.ExcuteWriteMessageEvent("取默认网关异常" + ex.Message.ToString(), Color.Green, false);
            }

            return defaultGateWay;
        }

        /// <summary>
        /// 得到Ping的结果包括统计信息
        /// </summary>
        /// <param name="strCommandline">传入的命令行</param>
        /// <param name="fileName">文件名称</param>
        /// <returns>KB</returns>
        private string LaunchPingStr(string strCommandline, string fileName)
        {
            string strBuffer = string.Empty;
            try
            {
                this.SetProcess(strCommandline, fileName);
                this.controlPing.Start();
                strBuffer = this.controlPing.StandardOutput.ReadToEnd();
            }
            finally
            {
                this.controlPing.Close();
            }

            return strBuffer;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>返回结果</returns>
        private string ExcuteOrder(string command)
        {
            string result = string.Empty;
            Process p = new Process();

            try
            {
                ////确定程序名
                p.StartInfo.FileName = "cmd.exe";

                ////确定程式命令行
                p.StartInfo.Arguments = "/c " + command;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.Start();

                ////输出出流取得命令行结果果
                result = p.StandardOutput.ReadToEnd();
                if (string.IsNullOrEmpty(result))
                {
                    result = p.StandardError.ReadToEnd();
                }
            }
            finally
            {
                p.Close();
            }

            return result;
        }

        /// <summary>
        /// 设属性
        /// </summary>
        /// <param name="strCommandline">传入的命令行</param>
        /// <param name="fileName">文件名称</param>
        private void SetProcess(string strCommandline, string fileName)
        {
            ////命令行
            this.controlPing.StartInfo.Arguments = strCommandline;

            ////是否使用操作系统外壳来执行
            this.controlPing.StartInfo.UseShellExecute = false;

            ////是否在新窗口中启动
            this.controlPing.StartInfo.CreateNoWindow = true;

            ////exe名称默认的在System32下
            this.controlPing.StartInfo.FileName = fileName;
            this.controlPing.StartInfo.RedirectStandardInput = true;
            this.controlPing.StartInfo.RedirectStandardOutput = true;
            this.controlPing.StartInfo.RedirectStandardError = true;
        }
    }
}
