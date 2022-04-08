using SVNUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Management.Automation;
#if NET5_0_OR_GREATER
using System.Threading.Tasks;
#else
using System.Collections.ObjectModel;
#endif

namespace SVNUtils
{
    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 转List<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
#if NET5_0_OR_GREATER
        public static List<T> ToList<T>(this PSDataCollection<PSObject> sources)
#else
        public static List<T> ToList<T>(this Collection<PSObject> sources)
#endif
            where T : class, new()
        {
            List<T> list = new List<T>();
            var type = typeof(T);
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var item in sources)
            {
                var tmp = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    var psProperty = item.Properties.FirstOrDefault(x => x.Name == property.Name);
                    if (psProperty != null)
                    {
                        if (property.PropertyType == typeof(Rule))
                        {
                            property.SetValue(tmp, (Rule)psProperty.Value);
                        }
                        else
                        {
                            property.SetValue(tmp, psProperty.Value);
                        }
                    }
                }

                list.Add(tmp);
            }

            return list;
        }

        /// <summary>
        /// 生成加密字符串对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string source)
        {
            SecureString secureString = new SecureString();
            foreach (var item in source)
            {
                secureString.AppendChar(item);
            }

            secureString.MakeReadOnly();
            return secureString;
        }
    }
}
