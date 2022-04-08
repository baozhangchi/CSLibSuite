using System.ComponentModel;
using Utils.TypeConverters;

namespace SVNUtils.Models
{
    /// <summary>
    /// SVN规则
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Rule : uint
    {
        /// <summary>
        /// 不可访问
        /// </summary>
        [Description("不可访问")] NoAccess,

        /// <summary>
        /// 只读
        /// </summary>
        [Description("只读")] ReadOnly,

        /// <summary>
        /// 可读可写
        /// </summary>
        [Description("可读可写")] ReadWrite,
    }
}