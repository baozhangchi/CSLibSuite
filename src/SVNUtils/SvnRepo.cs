using SVNUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVNUtils
{
    /// <summary>
    /// SVN版本库对象类
    /// </summary>
    public static class SvnRepo
    {
        /// <summary>
        /// 获取所有版本库
        /// </summary>
        /// <returns><see cref="RepositoryInfo"/>集合</returns>
#if NET5_0_OR_GREATER
        public static async Task<List<RepositoryInfo>> GetRepositoriesAsync()
#else
        public static List<RepositoryInfo> GetRepositories()
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            var result = await ps.RunScript("Get-SvnRepository");
#else
            var result = ps.RunScript("Get-SvnRepository");
#endif
            return result.ToList<RepositoryInfo>();
        }

        /// <summary>
        /// 按名称获取版本库
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
#if NET5_0_OR_GREATER
        public static async Task<RepositoryInfo> TryGetRepositoryAsync(string repositoryName)
#else
        public static RepositoryInfo TryGetRepository(string repositoryName)
#endif
        {
            try
            {
                var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
                var result = await ps.RunScript("Get-SvnRepository", new Dictionary<string, object>() { { "Name", repositoryName } });
#else
                var result = ps.RunScript("Get-SvnRepository", new Dictionary<string, object>() { { "Name", repositoryName } });
#endif
                return result?.ToList<RepositoryInfo>().FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建新版本库
        /// </summary>
        /// <param name="name">版本库名称</param>
#if NET5_0_OR_GREATER
        public static async Task CreateRepositoryAsync(string name)
#else
        public static void CreateRepository(string name)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"New-SvnRepository", new Dictionary<string, object> { { "Name", name } });
#else
            ps.RunScript($"New-SvnRepository", new Dictionary<string, object> { { "Name", name } });
#endif
        }

        /// <summary>
        /// 删除版本库
        /// </summary>
        /// <param name="repository">版本库名称</param>
#if NET5_0_OR_GREATER
        public static async Task DeleteRepositoryAsync(string repository)
#else
        public static void DeleteRepository(string repository)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"Remove-SvnRepository",
                   new Dictionary<string, object> { { "Name", repository } });
#else
            ps.RunScript($"Remove-SvnRepository",
                new Dictionary<string, object> { { "Name", repository } });
#endif
        }

        /// <summary>
        /// 获取所有版本库项目
        /// </summary>
        /// <param name="repository">版本库名称</param>
        /// <param name="path">项目地址</param>
        /// <returns>版本库项目<see cref="RepositoryItemInfo"/>的集合</returns>
#if NET5_0_OR_GREATER
        public static async Task<List<RepositoryItemInfo>> GetRepositoryItemsAsync(string repository, string path)
#else
        public static List<RepositoryItemInfo> GetRepositoryItems(string repository, string path)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!path.StartsWith("/"))
                {
                    path = '/' + path;
                }
            }
#if NET5_0_OR_GREATER
            var result = await ps.RunScript("Get-SvnRepositoryItem",
                new Dictionary<string, object>() { { "Repository", repository }, { "Path", path } });
#else
            var result = ps.RunScript("Get-SvnRepositoryItem",
                new Dictionary<string, object>() { { "Repository", repository }, { "Path", path } });
#endif
            return result.ToList<RepositoryItemInfo>();
        }

#if NET5_0_OR_GREATER
        public static async Task<RepositoryItemInfo> TryGetRepositoryItemAsync(string repository, string path)
#else
        public static RepositoryItemInfo TryGetRepositoryItem(string repository, string path)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!path.StartsWith("/"))
                {
                    path = '/' + path;
                }
            }
#if NET5_0_OR_GREATER
            var result = await ps.RunScript("Get-SvnRepositoryItem",
                    new Dictionary<string, object>() { { "Repository", repository }, { "Path", path } });
#else
                var result = ps.RunScript("Get-SvnRepositoryItem",
                        new Dictionary<string, object>() { { "Repository", repository }, { "Path", path } });
#endif
            return result?.ToList<RepositoryItemInfo>().FirstOrDefault();
        }

        /// <summary>
        /// 创建版本库项目
        /// </summary>
        /// <param name="repository">版本库名称</param>
        /// <param name="path">项目地址</param>
        /// <param name="type">项目类型</param>
        /// <returns></returns>
#if NET5_0_OR_GREATER
        public static async Task CreateRepositoryItemAsync(string repository, string path, string type = "Folder")
#else
        public static void CreateRepositoryItem(string repository, string path, string type = "Folder")
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"New-SvnRepositoryItem",
                  new Dictionary<string, object>()
                      { { "Repository", repository }, { "Path", path }, { "Type", type } });
#else
            ps.RunScript($"New-SvnRepositoryItem",
                new Dictionary<string, object>()
                    { { "Repository", repository }, { "Path", path }, { "Type", type } });
#endif
        }

        /// <summary>
        /// 删除版本库项目
        /// </summary>
        /// <param name="repository">版本库名称</param>
        /// <param name="path">项目地址</param>
#if NET5_0_OR_GREATER
        public static async Task DeleteRepositoryItemAsync(string repository, string path)
#else
        public static void DeleteRepositoryItem(string repository, string path)
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            await ps.RunScript($"Remove-SvnRepositoryItem",
                 new Dictionary<string, object> { { "Repository", repository }, { "Path", path } });
#else
            ps.RunScript($"Remove-SvnRepositoryItem",
                new Dictionary<string, object> { { "Repository", repository }, { "Path", path } });
#endif
        }
    }
}