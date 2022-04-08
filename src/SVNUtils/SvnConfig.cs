using SVNUtils.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SVNUtils
{
    /// <summary>
    /// SVN配置操作类
    /// </summary>
    public static class SvnConfig
    {
        // ReSharper disable once InconsistentNaming
        private const string USER_PASSWORD_CONFIG_FILENAME = "htpasswd";
        private static readonly ConfigInfo Config;

        static SvnConfig()
        {
#if NET5_0_OR_GREATER
            Config = GetConfigAsync().Result;
#else
            Config = GetConfig();
#endif
        }

        /// <summary>
        /// SVN用户名、密码配置文件地址
        /// </summary>
        public static string UserPasswordConfigFile => Path.Combine(Config.RepositoriesRoot, USER_PASSWORD_CONFIG_FILENAME);

        /// <summary>
        /// 获取SVN配置
        /// </summary>
        /// <returns><see cref="ConfigInfo"/></returns>
#if NET5_0_OR_GREATER
        public static async Task<ConfigInfo> GetConfigAsync()
#else
        public static ConfigInfo GetConfig()
#endif
        {
            var ps = PowershellHost.SimpleHostedRunspace.Default;
#if NET5_0_OR_GREATER
            var result = await ps.RunScript("Get-SvnServerConfiguration");
#else
            var result = ps.RunScript("Get-SvnServerConfiguration");
#endif
            return result.ToList<ConfigInfo>().Single();
        }
    }
}
