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
#if NET5_0_OR_GREATER
        public static async Task<List<RuleInfo>> GetRulesAsync(string repository, string path)
#else
        public static List<RuleInfo> GetRules(string repository, string path)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            var result = await ps.RunScript($"Select-SvnAccessRule -Path {path} -Repository {repository}");
#else
            var result = ps.RunScript($"Select-SvnAccessRule -Path {path} -Repository {repository}");
#endif
            return result.ToList<RuleInfo>();
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">成员ID</param>
        /// <param name="rule">规则<see cref="Rule"/></param>
#if NET5_0_OR_GREATER
        public static async Task AddRuleAsync(string repository, string path, string accountId, Rule rule)
#else
        public static void AddRule(string repository, string path, string accountId, Rule rule)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"Add-SvnAccessRule",
                 new Dictionary<string, object>()
                 {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path },
                    { "Access", (uint)rule }
                 });
#else
            ps.RunScript($"Add-SvnAccessRule",
                new Dictionary<string, object>()
                {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path },
                    { "Access", (uint)rule }
                });
#endif
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">成员ID</param>
        /// <param name="rule">规则<see cref="Rule"/></param>
#if NET5_0_OR_GREATER
        public static async Task UpdateRuleAsync(string repository, string path, string accountId, Rule rule)
#else
        public static void UpdateRule(string repository, string path, string accountId, Rule rule)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"Set-SvnAccessRule",
                  new Dictionary<string, object>()
                  {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path },
                    { "Access", (uint)rule }
                  });
#else
            ps.RunScript($"Set-SvnAccessRule",
                new Dictionary<string, object>()
                {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path },
                    { "Access", (uint)rule }
                });
#endif
        }

        /// <summary>
        /// 移除规则
        /// </summary>
        /// <param name="repository">仓库名称</param>
        /// <param name="path">路径</param>
        /// <param name="accountId">成员ID</param>
#if NET5_0_OR_GREATER
        public static async Task RemoveAsync(string repository, string path, string accountId)
#else
        public static void Remove(string repository, string path, string accountId)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"Remove-SvnAccessRule",
                 new Dictionary<string, object>()
                 {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path }, { "PassThru", null }
                 });
#else
            ps.RunScript($"Remove-SvnAccessRule",
                new Dictionary<string, object>()
                {
                    { "AccountId", accountId }, { "Repository", repository }, { "Path", path }, { "PassThru", null }
                });
#endif
        }
    }
}