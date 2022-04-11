using SVNUtils.Models;
using System;
using System.IO;
using System.Linq;

namespace SVNUtils
{
    /// <summary>
    /// SVN配置操作类
    /// </summary>
    public static class SvnConfig
    {
        // ReSharper disable once InconsistentNaming
        private const string USER_PASSWORD_CONFIG_FILENAME = "htpasswd";
        private const string GROUP_CONFIG_FILENAME = "groups.conf";
        private static readonly ConfigInfo Config = new ConfigInfo();

        static SvnConfig()
        {
            Config = GetConfig();
        }

        /// <summary>
        /// 设置仓库根目录
        /// </summary>
        /// <param name="path"></param>
        public static void SetRepositoriesRoot(string path)
        {
            Config.RepositoriesRoot = path;
        }

        /// <summary>
        /// SVN用户名、密码配置文件地址
        /// </summary>
        public static string UserPasswordConfigFile => string.IsNullOrWhiteSpace(Config.RepositoriesRoot) ? throw new NotImplementedException(nameof(Config.RepositoriesRoot)) : Path.Combine(Config.RepositoriesRoot, USER_PASSWORD_CONFIG_FILENAME);

        /// <summary>
        /// SVN组配置文件地址
        /// </summary>
        public static string GroupConfigFile => string.IsNullOrWhiteSpace(Config.RepositoriesRoot) ? throw new NotImplementedException(nameof(Config.RepositoriesRoot)) : Path.Combine(Config.RepositoriesRoot, GROUP_CONFIG_FILENAME);

        /// <summary>
        /// 获取SVN配置
        /// </summary>
        /// <returns><see cref="ConfigInfo"/></returns>
        public static ConfigInfo GetConfig()
        {
            var ps = PowershellHost.CustomHostedRunspace.Default;
            var result = ps.RunCommandAsync("Get-SvnServerConfiguration").Result;
            return result.ToList<ConfigInfo>().Single();
        }
    }
}
