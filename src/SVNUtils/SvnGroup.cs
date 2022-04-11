using PowershellHost;
using SVNUtils.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SVNUtils
{
    /// <summary>
    /// SVN用户组
    /// </summary>
    public static class SvnGroup
    {
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns>用户组<see cref="MemberInfo"/>的集合</returns>
        public static async Task<List<MemberInfo>> GetGroupsAsync()
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync("Get-SvnLocalGroup");
            return result.ToList<MemberInfo>();
        }

        /// <summary>
        /// 按名称获取组
        /// </summary>
        /// <param name="name">组名</param>
        /// <returns></returns>
        public static async Task<MemberInfo> GetGroupAsync(string name)
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync($"Get-SvnLocalGroup", new Dictionary<string, object> { { "Name", name } });
            return result.ToList<MemberInfo>().FirstOrDefault();
        }

        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <param name="name">用户组名</param>
        public static async Task CreateGroupAsync(string name)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"New-SvnLocalGroup", new Dictionary<string, object> { { "Name", name } });
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="name">用户组名</param>
        public static async Task DeleteGroupAsync(string name)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Remove-SvnLocalGroup", new Dictionary<string, object> { { "Name", name } });
        }

        /// <summary>
        /// 添加用户组成员
        /// </summary>
        /// <param name="name">用户组名</param>
        /// <param name="memberId">成员名</param>
        public static async Task AddGroupMemberAsync(string name, string memberId)
        {
            var memberType = memberId.StartsWith("@") ? MemberType.LocalGroup : MemberType.LocalUser;
            var ps = CustomHostedRunspace.Default;
            if (memberType == MemberType.LocalUser)
            {
                await ps.RunScriptAsync(
                     $"Add-SvnLocalGroupMember -Member (Get-SvnLocalUser -Id '{memberId}') -Name {name}");
            }
            else
            {
                await ps.RunScriptAsync(
                     $"Add-SvnLocalGroupMember -Member (Get-SvnLocalGroup -Id '{memberId}') -Name {name}");
            }
        }

        /// <summary>
        /// 获取用户组成员
        /// </summary>
        /// <param name="name">用户组名</param>
        /// <returns>用户组成员<see cref="MemberInfo"/>的集合</returns>
        public static async Task<List<MemberInfo>> GetGroupMembersAsync(string name)
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunScriptAsync($"Get-SvnLocalGroupMember -Name {name}");
            return result.ToList<MemberInfo>();
        }

        /// <summary>
        /// 删除用户组成员
        /// </summary>
        /// <param name="name">用户组名</param>
        /// <param name="memberId">成员ID</param>
        public static async Task DeleteGroupMemberAsync(string name, string memberId)
        {
            var memberType = memberId.StartsWith("@") ? MemberType.LocalGroup : MemberType.LocalUser;
            var ps = CustomHostedRunspace.Default;
            if (memberType == MemberType.LocalGroup)
                await ps.RunScriptAsync($"Remove-SvnLocalGroupMember -Member (Get-SvnLocalGroup -Id '{memberId}') -Name {name}");
            else
                await ps.RunScriptAsync($"Remove-SvnLocalGroupMember -Member (Get-SvnLocalUser -Id '{memberId}') -Name {name}");
        }
    }
}