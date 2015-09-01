//-----------------------------------------------------------------------
// <copyright file="SignalControl.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: SignalControl.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Better517Na._517VPNClient.Common
{
    /// <summary>
    /// 信号控制
    /// </summary>
    public class SignalControl
    {
        /// <summary>
        /// 等待信号
        /// </summary>
        private static AutoResetEvent waitSignal = new AutoResetEvent(false);

        /// <summary>
        /// 等待信号
        /// </summary>
        public static AutoResetEvent WaitSignal
        {
            get { return waitSignal; }
            set { waitSignal = value; }
        }

        /// <summary>
        /// 是否暂停
        /// </summary>
        public static bool Issuspend
        {
            get;
            set;
        }
    }
}
