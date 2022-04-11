using PowershellHost;
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
        public static async Task<List<RepositoryInfo>> GetRepositoriesAsync()
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync("Get-SvnRepository");
            return result.ToList<RepositoryInfo>();
        }

        /// <summary>
        /// 按名称获取版本库
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static async Task<RepositoryInfo> TryGetRepositoryAsync(string repositoryName)
        {
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync("Get-SvnRepository", new Dictionary<string, object>() { { "Name", repositoryName } });
            return result?.ToList<RepositoryInfo>().FirstOrDefault();
        }

        /// <summary>
        /// 创建新版本库
        /// </summary>
        /// <param name="name">版本库名称</param>
        public static async Task CreateRepositoryAsync(string name)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"New-SvnRepository", new Dictionary<string, object> { { "Name", name } });
        }

        /// <summary>
        /// 删除版本库
        /// </summary>
        /// <param name="repository">版本库名称</param>
        public static async Task DeleteRepositoryAsync(string repository)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Remove-SvnRepository",
                   new Dictionary<string, object> { { "Name", repository } });
        }

        /// <summary>
        /// 获取所有版本库项目
        /// </summary>
        /// <param name="repository">版本库名称</param>
        /// <param name="path">项目地址</param>
        /// <returns>版本库项目<see cref="RepositoryItemInfo"/>的集合</returns>
        public static async Task<List<RepositoryItemInfo>> GetRepositoryItemsAsync(string repository, string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!path.StartsWith("/"))
                {
                    path = '/' + path;
                }
            }
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync("Get-SvnRepositoryItem",
                new Dictionary<string, object>() { { "Repository", repository }, { "Path", path } });
            return result.ToList<RepositoryItemInfo>();
        }

        public static async Task<RepositoryItemInfo> TryGetRepositoryItemAsync(string repository, string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (!path.StartsWith("/"))
                {
                    path = '/' + path;
                }
            }
            var ps = CustomHostedRunspace.Default;
            var result = await ps.RunCommandAsync("Get-SvnRepositoryItem",
                    new Dictionary<string, object>() { { "Repository", repository }, { "Path", path } });
            return result?.ToList<RepositoryItemInfo>().FirstOrDefault();
        }

        /// <summary>
        /// 创建版本库项目
        /// </summary>
        /// <param name="repository">版本库名称</param>
        /// <param name="path">项目地址</param>
        /// <param name="type">项目类型</param>
        /// <returns></returns>
        public static async Task CreateRepositoryItemAsync(string repository, string path, string type = "Folder")
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"New-SvnRepositoryItem",
                  new Dictionary<string, object>()
                      { { "Repository", repository }, { "Path", path }, { "Type", type } });
        }

        /// <summary>
        /// 删除版本库项目
        /// </summary>
        /// <param name="repository">版本库名称</param>
        /// <param name="path">项目地址</param>
        public static async Task DeleteRepositoryItemAsync(string repository, string path)
        {
            var ps = CustomHostedRunspace.Default;
            await ps.RunCommandAsync($"Remove-SvnRepositoryItem",
                 new Dictionary<string, object> { { "Repository", repository }, { "Path", path } });
        }
    }
}