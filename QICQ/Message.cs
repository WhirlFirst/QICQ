using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QICQ
{
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public EType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgBody
        {
            get;
            set;
        }

        public long Length
        {
            get;
            set;
        }
        public enum EType
        {
            /// <summary>
            /// 普通信息
            /// </summary>
            msg,
            /// <summary>
            /// 动图
            /// </summary>
            gif,
            /// <summary>
            /// 客户端用户相互聊天
            /// </summary>
            file,
            /// <summary>
            /// 闪屏
            /// </summary>
            sp
        }
    }
}
