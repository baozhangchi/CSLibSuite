using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNUtils.Models
{
    /// <summary>
    /// 成员信息
    /// </summary>
    public class MemberInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 成员类型
        /// </summary>
        public MemberType MemberType => Id.StartsWith("@") ? MemberType.LocalGroup : MemberType.LocalUser;
    }
}
