using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNUtils.Models
{
    /// <summary>
    /// 仓库项目信息
    /// </summary>
    public class RepositoryItemInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件夹
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 所属仓库名
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// 访问Url
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string URL { get; set; }

        /// <summary>
        /// 仓库项目类型
        /// </summary>
        public uint Type { get; set; }
    }
}
