using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNUtils.Models
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class ConfigInfo
    {
        /// <summary>
        /// 仓库根路径
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string RepositoriesRoot { get; set; }

        /// <summary>
        /// 监听端口
        /// </summary>
        public ushort ListenPort { get; set; }

        /// <summary>
        /// 监听地址
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string[] ListeningIPAddress { get; set; }

        /// <summary>
        /// 是否支持SSL
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool EnableSSL { get; set; }
    }
}
