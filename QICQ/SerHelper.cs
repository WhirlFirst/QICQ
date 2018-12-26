using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QICQ
{
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    /// <summary>
    /// 负责将对象序列化成byte[]数组
    /// </summary>
    public class SerHelper
    {
        /// <summary>
        /// 将实体序列化成byte[]数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            //序列化流的序列化帮助类
            BinaryFormatter bf = new BinaryFormatter();
            //定义内存流，用来接收通过bf的Serialize()方法将obj对象序列化成byte[]字节数组
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                res = ms.ToArray();
            }
            return res;
        }

        /// <summary>
        /// 根据byte[]字节数组反序列化成ChatMessage对象实体
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] buffer)
        {
            //序列化流的序列化帮助类
            BinaryFormatter bf = new BinaryFormatter();
            T obj;
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                obj = (T)bf.Deserialize(ms);
            }
            return obj;
        }
    }
}
