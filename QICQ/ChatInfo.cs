using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace QICQ
{
    public partial class ChatInfo : UserControl
    {
        string member;
        string user;
        string[] IP;
        public ChatInfo()
        {
            InitializeComponent();
        }
        public ChatInfo(string username, string mem,string[] ip)
        {
            member = mem;
            IP = ip;
            user = username;
            InitializeComponent();
            TItle.Text = "与"+member+"的聊天";
        }

        private void delbtn_MouseEnter(object sender, EventArgs e)
        {
            delbtn.BackColor = Color.Red;
            delbtn.ForeColor = Color.White;
        }

        private void delbtn_MouseLeave(object sender, EventArgs e)
        {
            delbtn.BackColor = Color.White;
            delbtn.ForeColor = Color.Black;
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            string del = member.Replace("201601", "");
            del = del.Replace('，', '_')+".rtf";
            File.Delete("Data/Chat/"+ del);
        }

        public Socket Connect_GroupChat(string IP, string ID, string Users_Broadcast_Msg)
        {
            int port = Int32.Parse(ID.Substring(ID.Length - 4));
            Message message = new Message()
            {
                MsgBody = Users_Broadcast_Msg,
                Type = Message.EType.con
            };
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse(IP), port);
            Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpClient.Connect(serverIp);
            //连接成功后向对方发送除对方外，所有群聊者的学号
            byte[] data = SerHelper.Serialize(message);
            tcpClient.Send(data);
            return tcpClient;
        }

        public void openbtn_Click(object sender, EventArgs e)
        {
            int Number_Connected = 0;
            string Users_Broadcast_Msg;
            foreach(string ipa in IP)
            {
                if(ipa=="n")
                {
                    MessageBox.Show("要发起会话的好友有人不在线", "无法发起会话"
                                     , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string[] memarr = member.Split('，');
            Socket[] Chatters = new Socket[IP.Length];
            //向所有选中的人广播除了它自己外其他人的ID
            int i = 0;
            foreach (string ip in IP)
            {
                //广播信息的第一条ID是自己的ID，ID与ID之间是连续的，通过/来分割
                Users_Broadcast_Msg = user;
                int j = 0;
                foreach (string iitem in memarr)
                {
                    if (i!=j) Users_Broadcast_Msg += "_" + iitem;
                    j += 1;
                }
                try
                {
                    Chatters[Number_Connected] = Connect_GroupChat(
                    ip, memarr[i], Users_Broadcast_Msg);
                    Number_Connected++;
                }
                catch (Exception)
                {
                    MessageBox.Show("未知错误，有好友可能虚假在线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                i += 1;
            }
            Thread Thread_Chat = new Thread(() =>
            Application.Run(new ChatDialog(user, member, Chatters, Number_Connected)));
            Thread_Chat.Start();
        }
    }
}
