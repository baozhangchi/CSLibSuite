using System.IO;
using System.Text;
using System.Xml.Serialization;

// ReSharper disable once CheckNamespace
namespace System.Xml
{
    /// <summary>
    /// 序列化
    /// </summary>
    public static class XmlSerializer
    {
        #region 序列化
        /// <summary>
        /// 对象序列化为Xml字符串
        /// </summary>
        /// <param name="item">序列化对象</param>
        /// <param name="encoding">编码，默认为UTF8</param>
        /// <param name="removeDefaultNamespaces">是否移除默认命名空间</param>
        /// <returns>xml字符串</returns>
        public static string ToXml(this object item, Encoding encoding = null, bool removeDefaultNamespaces = false)
        {
            using (var stream = item.ToXmlStream(encoding, removeDefaultNamespaces))
            {
                using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 对象序列化为Xml字符串
        /// </summary>
        /// <typeparam name="T">被序列化对象类型</typeparam>
        /// <param name="item">序列化对象</param>
        /// <param name="encoding">编码，默认为UTF8</param>
        /// <param name="removeDefaultNamespaces">是否移除默认命名空间</param>
        /// <returns>xml字符串</returns>
        public static string ToXml<T>(this T item, Encoding encoding = null, bool removeDefaultNamespaces = false)
        {
            using (var stream = item.ToXmlStream(encoding, removeDefaultNamespaces))
            {
                using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 对象序列化为字节流
        /// </summary>
        /// <typeparam name="T">被序列化对象类型</typeparam>
        /// <param name="item">序列化对象</param>
        /// <param name="encoding">编码，默认为UTF8</param>
        /// <param name="removeDefaultNamespaces">是否移除默认命名空间</param>
        /// <returns>xml字节流</returns>
        public static Stream ToXmlStream<T>(this T item, Encoding encoding = null, bool removeDefaultNamespaces = false)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms, encoding ?? Encoding.UTF8);
            Serialization.XmlSerializer serializer = new Serialization.XmlSerializer(typeof(T));
            if (removeDefaultNamespaces)
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(writer, item, namespaces);
            }
            else
            {
                serializer.Serialize(writer, item);
            }

            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 对象序列化为字节流
        /// </summary>
        /// <param name="item">序列化对象</param>
        /// <param name="encoding">编码，默认为UTF8</param>
        /// <param name="removeDefaultNamespaces">是否移除默认命名空间</param>
        /// <returns>xml字节流</returns>
        public static Stream ToXmlStream(this object item, Encoding encoding = null, bool removeDefaultNamespaces = false)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms, encoding ?? Encoding.UTF8);
            Serialization.XmlSerializer serializer = new Serialization.XmlSerializer(item.GetType());
            if (removeDefaultNamespaces)
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(writer, item, namespaces);
            }
            else
            {
                serializer.Serialize(writer, item);
            }

            ms.Position = 0;
            return ms;
        }
        #endregion

        #region 反序列化
        /// <summary>
        /// xml字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns>反序列化结果</returns>
        public static T ToObject<T>(this string xml, Encoding encoding = null)
        {
            using (var stream = new MemoryStream())
            {
                var buffer = (encoding ?? Encoding.UTF8).GetBytes(xml);
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                return stream.ToObject<T>();
            }
        }

        /// <summary>
        /// xml字符串反序列化为对象
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="type">目标对象类型</param>
        /// <param name="encoding">编码</param>
        /// <returns>反序列化结果</returns>
        public static object ToObject(this string xml, Type type, Encoding encoding = null)
        {
            using (var stream = new MemoryStream())
            {
                var buffer = (encoding ?? Encoding.UTF8).GetBytes(xml);
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                return stream.ToObject(type);
            }
        }

        /// <summary>
        /// 字节流反序列化为对象
        /// </summary>
        /// <param name="stream">字节流</param>
        /// <param name="type">目标对象类型</param>
        /// <returns>反序列化结果</returns>
        public static object ToObject(this Stream stream, Type type)
        {
            //XmlSerializer xz = new XmlSerializer(type);
            //return xz.Deserialize(stream);
            using (var reader = XmlReader.Create(stream))
            {
                var formatter = new Serialization.XmlSerializer(type);
                return formatter.Deserialize(reader);
            }
        }

        /// <summary>
        /// 字节流反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="stream">字节流</param>
        /// <returns>反序列化结果</returns>
        public static T ToObject<T>(this Stream stream)
        {
            return (T)stream.ToObject(typeof(T));
        }
        #endregion
    }
}
