using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SVNUtils.Models;

namespace SVNUtilsWebApi.Controllers
{
    /// <summary>
    /// 权限操作相关
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRuleController : ControllerBase
    {
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <param name="repository">存储库</param>
        /// <param name="path">路径</param>
        /// <returns>所有权限</returns>
        [HttpGet("list")]
        public async Task<List<RuleInfo>> GetRulesAsync(string repository, string path)
        {
            return await SVNUtils.SvnRule.GetRulesAsync(repository, path);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="repository">存储库</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">账号ID</param>
        /// <param name="rule">权限</param>
        /// <returns>设置是否成功</returns>
        [HttpPost("add")]
        public async Task<bool> AddRuleAsync(string repository, string path, string accountId, Rule rule)
        {
            await SVNUtils.SvnRule.AddRuleAsync(repository, path, accountId, rule);
            return true;
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="repository">存储库</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">账号ID</param>
        /// <param name="rule">权限</param>
        /// <returns>设置是否成功</returns>
        [HttpPost("update")]
        public async Task<bool> UpdateRuleAsync(string repository, string path, string accountId, Rule rule)
        {
            await SVNUtils.SvnRule.UpdateRuleAsync(repository, path, accountId, rule);
            return true;
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="repository">存储库</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">账号ID</param>
        /// <returns>设置是否成功</returns>
        [HttpDelete("remove")]
        public async Task<bool> RemoveAsync(string repository, string path, string accountId)
        {
            await SVNUtils.SvnRule.RemoveAsync(repository, path, accountId);
            return true;
        }
    }
}
