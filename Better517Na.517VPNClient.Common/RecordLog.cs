//-----------------------------------------------------------------------
// <copyright file="RecordLog.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: RecordLog.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Better.Infrastructures.Log;

namespace Better517Na._517VPNClient.Common
{
    /// <summary>
    /// 记录日志
    /// </summary>
    public class RecordLog
    {
        /// <summary>
        /// 记录交互日志
        /// </summary>
        /// <param name="key1">key1(记录账号)</param>
        /// <param name="key2">记录操作类型（比如：连接VPN）</param>
        /// <param name="request">请求的参数</param>
        /// <param name="receive">返回的数据</param>
        /// <param name="useTime">用时</param>
        /// <param name="sendAddress">请求连接的VPN地址</param>
        public static void RecordInteractionLog(string key1, string key2, string request, string receive, TimeSpan useTime, string sendAddress)
        {
            InteractionParam param = new InteractionParam();
            param.Module = "VPN服务器交互";
            param.Key1 = key1;
            param.Key2 = key2;
            param.ReceiveContent = receive;
            param.SendAddress = sendAddress;
            param.SendContent = request;
            param.TimeSpan = useTime;

            LogManager.Log.InteractionLog(param);
        }
    }
}
