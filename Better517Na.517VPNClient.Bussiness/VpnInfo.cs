//-----------------------------------------------------------------------
// <copyright file="VpnInfo.cs" company="517Na Enterprises">
// * Copyright (C) 2015 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5485
// * author  : xukong
// * FileName: VpnInfo.cs
// * history : created by xukong 2015-08-18 17:39:44 
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Better517Na._517VPNClient.Common;
using Better517Na.DBAccessLayer.ProcessBase;
using Better517Na.VPNDataService.IDAL;
using Better517Na.VPNDataService.Model;

namespace Better517Na._517VPNClient.Bussiness
{
    /// <summary>
    /// VPN相关信息
    /// </summary>
    public class VpnInfo
    {
        /// <summary>
        /// 获取有效VPN
        /// </summary>
        /// <param name="vpnProvider">vpn提供商</param>
        /// <param name="keyid">起始keyid</param>
        /// <param name="vpnNumber">vpn数目</param>
        /// <returns>返回VPN数目</returns>
        public List<MVPNServer> GetValidVpn(string vpnProvider, string keyid, int vpnNumber)
        {
            List<MVPNServer> vpnServerList = null;
            IVPNServer vpnServer = Better517Na.VPNDataService.Factory.DALFactory.GetVpnServerDal(Better517Na.VPNDataService.Factory.DBOperType.read);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("Provider", vpnProvider);
            mmb.AddConditionFields("IsValid", 1);
            mmb.AddConditionFields("IsDelete", 0);
            mmb.AddConditionFields("IsUsing", 0);
            mmb.AddConditionFields("KeyID", keyid, DatabaseOperators.GreaterThan);
            mmb.AddUpdateFields("Address", null);
            mmb.AddUpdateFields("KeyID", null);
            mmb.AddUpdateFields("Area", null);
            mmb.AddUpdateFields("Line", null);
            mmb.AddUpdateFields("Provider", null);
            mmb.AddUpdateFields("UsingServer", null);

            vpnServerList = vpnServer.GetModel<MVPNServer>(mmb, vpnNumber);
            return vpnServerList;
        }

        /// <summary>
        /// 获取本机正在使用的VPN服务器（杀进程造成的数据）
        /// </summary>
        /// <param name="provider">VPN服务器提供商</param>
        /// <returns>返回VPN服务器信息</returns>
        public List<MVPNServer> GetLocalUsedVpnServer(string provider)
        {
            List<MVPNServer> vpnServerList = null;
            IVPNServer vpnServer = Better517Na.VPNDataService.Factory.DALFactory.GetVpnServerDal(Better517Na.VPNDataService.Factory.DBOperType.read);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("Provider", provider);
            mmb.AddConditionFields("IsValid", 1);
            mmb.AddConditionFields("IsDelete", 0);
            mmb.AddConditionFields("IsUsing", 1);
            mmb.AddConditionFields("UsingServer", new GetIP().GetLocalIp("本地连接"));
            mmb.AddUpdateFields("Address", null);
            mmb.AddUpdateFields("KeyID", null);
            mmb.AddUpdateFields("Area", null);
            mmb.AddUpdateFields("Line", null);
            mmb.AddUpdateFields("Provider", null);
            mmb.AddUpdateFields("UsingServer", null);

            vpnServerList = vpnServer.GetModel<MVPNServer>(mmb, 20);
            return vpnServerList;
        }

        /// <summary>
        /// 获取一个VPN账号
        /// </summary>
        /// <returns>返回账号</returns>
        public MVPNAccount GetVpnAccount()
        {
            MVPNAccount vpnAccount = null;
            IVPNAccount ivpnAccount = Better517Na.VPNDataService.Factory.DALFactory.GetVpnAccountDal(Better517Na.VPNDataService.Factory.DBOperType.read);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("IsValid", 1);
            mmb.AddConditionFields("IsDelete", 0);
            mmb.AddConditionFields("IsUsing", 0);
            mmb.AddUpdateFields("KeyID", null);
            mmb.AddUpdateFields("Account", null);
            mmb.AddUpdateFields("Password", null);
            mmb.AddUpdateFields("Provider", null);

            vpnAccount = ivpnAccount.GetModel<MVPNAccount>(mmb);
            return vpnAccount;
        }

        /// <summary>
        /// 重新获取一个VPN账号
        /// </summary>
        /// <param name="provider">提供商</param>
        /// <returns>返回账号</returns>
        public MVPNAccount GetNewVpnAccount(string provider)
        {
            MVPNAccount vpnAccount = null;
            IVPNAccount ivpnAccount = Better517Na.VPNDataService.Factory.DALFactory.GetVpnAccountDal(Better517Na.VPNDataService.Factory.DBOperType.read);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("IsValid", 1);
            mmb.AddConditionFields("IsDelete", 0);
            mmb.AddConditionFields("IsUsing", 0);
            mmb.AddConditionFields("Provider", provider, DatabaseOperators.DoesNotEqual);
            mmb.AddUpdateFields("KeyID", null);
            mmb.AddUpdateFields("Account", null);
            mmb.AddUpdateFields("Password", null);
            mmb.AddUpdateFields("Provider", null);

            vpnAccount = ivpnAccount.GetModel<MVPNAccount>(mmb);
            return vpnAccount;
        }

        /// <summary>
        /// 根据本机获取一个VPN账号
        /// </summary>
        /// <returns>返回账号</returns>
        public MVPNAccount GetVpnAccountByIP()
        {
            MVPNAccount vpnAccount = null;
            IVPNAccount ivpnAccount = Better517Na.VPNDataService.Factory.DALFactory.GetVpnAccountDal(Better517Na.VPNDataService.Factory.DBOperType.read);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("IsValid", 1);
            mmb.AddConditionFields("IsDelete", 0);
            mmb.AddConditionFields("UsingServer", new GetIP().GetLocalIp("本地连接"));
            mmb.AddUpdateFields("KeyID", null);
            mmb.AddUpdateFields("Account", null);
            mmb.AddUpdateFields("Password", null);
            mmb.AddUpdateFields("Provider", null);

            vpnAccount = ivpnAccount.GetModel<MVPNAccount>(mmb);
            return vpnAccount;
        }

        /// <summary>
        /// 更新VPN使用状态
        /// </summary>
        /// <param name="vpnServer">vpnServer</param>
        /// <returns>返回更新结果</returns>
        public bool UpdateVpnUseStatu(MVPNServer vpnServer)
        {
            IVPNServer ivpnServer = Better517Na.VPNDataService.Factory.DALFactory.GetVpnServerDal(Better517Na.VPNDataService.Factory.DBOperType.write);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("KeyID", vpnServer.KeyID);
            mmb.AddUpdateFields("IsUsing", 1);
            mmb.AddUpdateFields("IsUsingDesc", "使用中");
            mmb.AddUpdateFields("UsingServer", new GetIP().GetLocalIp("本地连接"));

            return ivpnServer.Update<MVPNServer>(vpnServer, mmb) > 0;
        }

        /// <summary>
        /// 更新VPN使用状态
        /// </summary>
        /// <param name="vpnServer">vpnServer</param>
        /// <returns>返回更新结果</returns>
        public bool UpdateVpnLinkStatu(MVPNServer vpnServer)
        {
            IVPNServer ivpnServer = Better517Na.VPNDataService.Factory.DALFactory.GetVpnServerDal(Better517Na.VPNDataService.Factory.DBOperType.write);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("KeyID", vpnServer.KeyID);
            mmb.AddUpdateFields("IsUsing", 0);
            mmb.AddUpdateFields("IsUsingDesc", "未使用");
            mmb.AddUpdateFields("IsValid", 0);
            mmb.AddUpdateFields("IsValidDesc", "不可用");
            mmb.AddUpdateFields("UsingServer", string.Empty);

            return ivpnServer.Update<MVPNServer>(vpnServer, mmb) > 0;
        }

        /// <summary>
        /// 更新VPN状态
        /// </summary>
        /// <param name="vpnServer">vpnServer</param>
        /// <returns>返回更新结果</returns>
        public bool UpdateVpnIsValid(MVPNServer vpnServer)
        {
            IVPNServer ivpnServer = Better517Na.VPNDataService.Factory.DALFactory.GetVpnServerDal(Better517Na.VPNDataService.Factory.DBOperType.write);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("KeyID", vpnServer.KeyID);
            mmb.AddUpdateFields("IsUsing", 0);
            mmb.AddUpdateFields("IsUsingDesc", "未使用");
            mmb.AddUpdateFields("UsingServer", string.Empty);
            mmb.AddUpdateFields("IsValid", 0);
            mmb.AddUpdateFields("IsValidDesc", "不可用");

            return ivpnServer.Update<MVPNServer>(vpnServer, mmb) > 0;
        }

        /// <summary>
        /// 更新VPN状态
        /// </summary>
        /// <param name="vpnServer">vpnServer</param>
        /// <returns>返回更新结果</returns>
        public bool ExitUpdateVpnIsValid(MVPNServer vpnServer)
        {
            IVPNServer ivpnServer = Better517Na.VPNDataService.Factory.DALFactory.GetVpnServerDal(Better517Na.VPNDataService.Factory.DBOperType.write);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("KeyID", vpnServer.KeyID);
            mmb.AddUpdateFields("IsUsing", 0);
            mmb.AddUpdateFields("IsUsingDesc", "未使用");
            mmb.AddUpdateFields("UsingServer", string.Empty);

            return ivpnServer.Update<MVPNServer>(vpnServer, mmb) > 0;
        }

        /// <summary>
        /// 更新VPN账号状态
        /// </summary>
        /// <param name="vpnAccount">vpn账号对象信息</param>
        /// <returns>返回更新结果</returns>
        public bool UpdateVpnAccountState(MVPNAccount vpnAccount)
        {
            IVPNAccount ivpnAccount = Better517Na.VPNDataService.Factory.DALFactory.GetVpnAccountDal(Better517Na.VPNDataService.Factory.DBOperType.write);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("KeyID", vpnAccount.KeyID);
            mmb.AddUpdateFields("IsUsing", 1);
            mmb.AddUpdateFields("IsUsingDesc", "使用中");
            mmb.AddUpdateFields("UsingServer", new GetIP().GetLocalIp("本地连接"));

            return ivpnAccount.Update<MVPNAccount>(vpnAccount, mmb) > 0;
        }

        /// <summary>
        ///  释放VPN账号
        /// </summary>
        /// <param name="vpnAccount">vpn账号对象信息</param>
        /// <returns>返回释放结果</returns>
        public bool ReleaseVpnAccountState(MVPNAccount vpnAccount)
        {
            IVPNAccount ivpnAccount = Better517Na.VPNDataService.Factory.DALFactory.GetVpnAccountDal(Better517Na.VPNDataService.Factory.DBOperType.write);
            MModelBase mmb = new MModelBase();
            mmb.AddConditionFields("KeyID", vpnAccount.KeyID);
            mmb.AddUpdateFields("IsUsing", 0);
            mmb.AddUpdateFields("IsUsingDesc", "未使用");
            mmb.AddUpdateFields("UsingServer", string.Empty);

            return ivpnAccount.Update<MVPNAccount>(vpnAccount, mmb) > 0;
        }
    }
}
