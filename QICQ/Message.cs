using System;

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
            /// 语音消息
            /// </summary>
            voice,
            /// <summary>
            /// 客户端文件
            /// </summary>
            file,
            /// <summary>
            /// 抖动
            /// </summary>
            dd,
            /// <summary>
            /// 连接
            /// </summary>
            con,
            /// <summary>
            /// 下线
            /// </summary>
            lgo
        }
    }
}
