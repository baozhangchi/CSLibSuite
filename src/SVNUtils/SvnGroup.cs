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
#if NET5_0_OR_GREATER
        public static async Task<List<MemberInfo>> GetGroupsAsync()
#else
        public static List<MemberInfo> GetGroups()
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            var result = await ps.RunScript("Get-SvnLocalGroup");
#else
            var result = ps.RunScript("Get-SvnLocalGroup");
#endif
            return result.ToList<MemberInfo>();
        }

        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <param name="name">用户组名</param>
#if NET5_0_OR_GREATER
        public static async Task CreateGroupAsync(string name)
#else
        public static void CreateGroup(string name)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"New-SvnLocalGroup -Name {name}");
#else
            ps.RunScript($"New-SvnLocalGroup -Name {name}");
#endif
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="name">用户组名</param>
#if NET5_0_OR_GREATER
        public static async Task DeleteGroupAsync(string name)
#else
        public static void DeleteGroup(string name)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"Remove-SvnLocalGroup -Name {name}");
#else
            ps.RunScript($"Remove-SvnLocalGroup -Name {name}");
#endif
        }

        /// <summary>
        /// 添加用户组成员
        /// </summary>
        /// <param name="name">用户组名</param>
        /// <param name="memberId">成员名</param>
#if NET5_0_OR_GREATER
        public static async Task AddGroupMemberAsync(string name, string memberId)
#else
        public static void AddGroupMember(string name, string memberId)
#endif
        {
            var memberType = memberId.StartsWith("@") ? MemberType.LocalGroup : MemberType.LocalUser;
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            if (memberType == MemberType.LocalUser)
            {
                await ps.RunScript(
                     $"Add-SvnLocalGroupMember -Member (Get-SvnLocalUser -Id '{memberId}') -Name {name}");
            }
            else
            {
                await ps.RunScript(
                     $"Add-SvnLocalGroupMember -Member (Get-SvnLocalGroup -Id '{memberId}') -Name {name}");
            }
#else
            if (memberType == MemberType.LocalUser)
            {
                ps.RunScript(
                    $"Add-SvnLocalGroupMember -Member (Get-SvnLocalUser -Id '{memberId}') -Name {name}");
            }
            else
            {
                ps.RunScript(
                    $"Add-SvnLocalGroupMember -Member (Get-SvnLocalGroup -Id '{memberId}') -Name {name}");
            }
#endif
        }

        /// <summary>
        /// 获取用户组成员
        /// </summary>
        /// <param name="name">用户组名</param>
        /// <returns>用户组成员<see cref="MemberInfo"/>的集合</returns>
#if NET5_0_OR_GREATER
        public static async Task<List<MemberInfo>> GetGroupMembersAsync(string name)
#else
        public static List<MemberInfo> GetGroupMembers(string name)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            var result = await ps.RunScript($"Get-SvnLocalGroupMember -Name {name}");
#else
            var result = ps.RunScript($"Get-SvnLocalGroupMember -Name {name}");
#endif
            return result.ToList<MemberInfo>();
        }

        /// <summary>
        /// 删除用户组成员
        /// </summary>
        /// <param name="name">用户组名</param>
        /// <param name="memberId">成员ID</param>
#if NET5_0_OR_GREATER
        public static async Task DeleteGroupMemberAsync(string name, string memberId)
#else
        public static void DeleteGroupMember(string name, string memberId)
#endif
        {
            var memberType = memberId.StartsWith("@") ? MemberType.LocalGroup : MemberType.LocalUser;
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            if (memberType == MemberType.LocalGroup)
                await ps.RunScript($"Remove-SvnLocalGroupMember -Member (Get-SvnLocalGroup -Id '{memberId}') -Name {name}");
            else
                await ps.RunScript($"Remove-SvnLocalGroupMember -Member (Get-SvnLocalUser -Id '{memberId}') -Name {name}");
#else
            if (memberType == MemberType.LocalGroup)
                ps.RunScript($"Remove-SvnLocalGroupMember -Member (Get-SvnLocalGroup -Id '{memberId}') -Name {name}");
            else
                ps.RunScript($"Remove-SvnLocalGroupMember -Member (Get-SvnLocalUser -Id '{memberId}') -Name {name}");
#endif
        }
    }
}