using PowershellHost;
using SVNUtils.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVNUtils
{
    /// <summary>
    /// SVN规则操作类库
    /// </summary>
    public static class SvnRule
    {
        /// <summary>
        /// 获取所有规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <returns><see cref="RuleInfo"/>集合</returns>
        public static async Task<List<RuleInfo>> GetRulesAsync(string repository, string path)
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync($"Select-SvnAccessRule", new Dictionary<string, object> { { "Path", path }, { "Repository", repository } });
            return result.ToList<RuleInfo>();
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">成员ID</param>
        /// <param name="rule">规则<see cref="Rule"/></param>
        public static async Task AddRuleAsync(string repository, string path, string accountId, Rule rule)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Add-SvnAccessRule",
                 new Dictionary<string, object>()
                 {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path },
                    { "Access", (uint)rule }
                 });
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">成员ID</param>
        /// <param name="rule">规则<see cref="Rule"/></param>
        public static async Task UpdateRuleAsync(string repository, string path, string accountId, Rule rule)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Set-SvnAccessRule",
                  new Dictionary<string, object>()
                  {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path },
                    { "Access", (uint)rule }
                  });
        }
        
        public static async Task<List<RuleInfo>> GetRulesByAccountIdAsync(string accountId)
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync($"Get-SvnAccessRule", new Dictionary<string, object> { { "AccountId", accountId } });
            return result.ToList<RuleInfo>();
        }

        /// <summary>
        /// 移除规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">成员ID</param>
        public static async Task RemoveAsync(string repository, string path, string accountId)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Remove-SvnAccessRule",
                 new Dictionary<string, object>()
                 {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path }, { "PassThru", null }
                 });
        }
    }
}