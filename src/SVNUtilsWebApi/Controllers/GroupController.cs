using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SVNUtils.Models;

namespace SVNUtilsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<MemberInfo>> GetGroupsAsync()
        {
            return await SVNUtils.SvnGroup.GetGroupsAsync();
        }

        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<bool> CreateGroupAsync(string name)
        {
            await SVNUtils.SvnGroup.CreateGroupAsync(name);
            return true;
        }

        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<bool> DeleteGroupAsync(string name)
        {
            await SVNUtils.SvnGroup.DeleteGroupAsync(name);
            return true;
        }

        /// <summary>
        /// 获取所有组成员
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("get-members")]
        public async Task<List<MemberInfo>> GetGroupMembersAsync(string name)
        {
            if (await SVNUtils.SvnGroup.GetGroupAsync(name) == null)
            {
                await SVNUtils.SvnGroup.CreateGroupAsync(name);
            }
            return await SVNUtils.SvnGroup.GetGroupMembersAsync(name);
        }

        /// <summary>
        /// 添加组成员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost("add-member")]
        public async Task<bool> AddGroupMemberAsync(string name, string memberId)
        {
            if (await SVNUtils.SvnGroup.GetGroupAsync(name) == null)
            {
                await SVNUtils.SvnGroup.CreateGroupAsync(name);
            }

            await SVNUtils.SvnGroup.AddGroupMemberAsync(name, memberId);
            return true;
        }

        /// <summary>
        /// 删除组成员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpDelete("delete-member")]
        public async Task<bool> DeleteGroupMemberAsync(string name, string memberId)
        {
            if (await SVNUtils.SvnGroup.GetGroupAsync(name) == null)
            {
                await SVNUtils.SvnGroup.CreateGroupAsync(name);
            }

            await SVNUtils.SvnGroup.DeleteGroupMemberAsync(name, memberId);
            return true;
        }

        /// <summary>
        /// 获取用户组所有权限
        /// </summary>
        /// <param name="groupName">用户组名</param>
        /// <returns></returns>
        [HttpGet("rules")]
        public async Task<List<RuleInfo>> GetRulesAsync(string groupName)
        {
            return await SVNUtils.SvnRule.GetRulesByAccountIdAsync($"@{groupName}");
        }


        /// <summary>
        /// 变更用户组
        /// </summary>
        /// <param name="memberId">成员ID</param>
        /// <param name="oldGroupName">旧组名</param>
        /// <param name="newGroupName">新组名</param>
        /// <returns></returns>
        [HttpPost("change-group")]
        public async Task<bool> ChangeGroupAsync(string memberId, string oldGroupName, string newGroupName)
        {
            await SVNUtils.SvnGroup.DeleteGroupMemberAsync(oldGroupName, memberId);

            if (await SVNUtils.SvnGroup.GetGroupAsync(newGroupName) == null)
            {
                await SVNUtils.SvnGroup.CreateGroupAsync(newGroupName);
            }

            await SVNUtils.SvnGroup.AddGroupMemberAsync(newGroupName, memberId);
            return true;
        }
    }
}
