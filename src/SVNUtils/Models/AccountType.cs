using System.ComponentModel;
using Utils.TypeConverters;

namespace SVNUtils.Models
{
    /// <summary>
    /// 成员类型
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum AccountType
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")] User,

        /// <summary>
        /// 用户组
        /// </summary>
        [Description("用户组")] Group
    }
}