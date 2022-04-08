using System.ComponentModel;
using Utils.TypeConverters;

namespace SVNUtils.Models
{
    /// <summary>
    /// 成员类型
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MemberType
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")] LocalUser = 1,

        /// <summary>
        /// 用户组
        /// </summary>
        [Description("用户组")] LocalGroup = 2,
    }
}