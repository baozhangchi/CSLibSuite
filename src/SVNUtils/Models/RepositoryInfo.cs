using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNUtils.Models
{
    /// <summary>
    /// 仓库信息
    /// </summary>
    public class RepositoryInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 访问Url
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string URL { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public uint Type { get; set; }
    }
}
