using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNUtils.Models
{
    /// <summary>
    /// 规则信息
    /// </summary>
    public class RuleInfo
    {
        /// <summary>
        /// 规则权限
        /// </summary>
        public Rule Access { get; set; }

        /// <summary>
        /// 成员ID
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public string AccountId { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 规则对应仓库
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// 规则对应仓库地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 规则对应成员类型
        /// </summary>
        public AccountType AccountType => AccountId.StartsWith("@") ? AccountType.Group : AccountType.User;
    }
}
