using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QICQ
{
    public partial class ChatInfo : UserControl
    {
        string member;
        string user;
        public string[] IP;
        Socket Socket_user_server;
        List<string> connected;
        delegate void Dowork();

        public void Able()
        {
            this.openbtn.Enabled = true;
            delbtn.Enabled = true;
            openbtn.Text = "打开";
        }

        public ChatInfo()
        {
            InitializeComponent();
        }

        #region 自定义控件初始化
        /// <summary>
        /// 自定义控件初始化
        /// </summary>
        /// <param name="username"></param>
        /// <param name="mem"></param>
        /// <param name="ip"></param>
        /// <param name="server"></param>
        /// <param name="vs"></param>
        public ChatInfo(string username, string mem, string[] ip, Socket server, List<string> vs)
        {
            connected = vs;
            member = mem;
            IP = ip;
            user = username;
            Socket_user_server = server;
            InitializeComponent();
            TItle.Text = "与" + member + "的聊天";
        }
        #endregion

        #region 主窗口代码复用
        public string Search(string IDnumber)
        {
            string IP_search = "q" + IDnumber;//向服务器发送的查询信息
            byte[] IP_search_byte = new byte[1024];
            IP_search_byte = Encoding.ASCII.GetBytes(IP_search);
            try
            {
                Socket_user_server.Send(IP_search_byte);
            }
            catch (Exception)
            {
                MessageBox.Show("未知错误，无法连接服务器或者其他", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ("");
            }

            //接收用户信息
            byte[] IP_receive_byte = new byte[1024];
            int number = Socket_user_server.Receive(IP_receive_byte);
            string IP_receive_mess = Encoding.Default.GetString(IP_receive_byte, 0, number);
            return IP_receive_mess;
        }
        public Socket Connect_GroupChat(string IP, string ID, string Users_Broadcast_Msg)
        {
            int port = Int32.Parse(ID.Substring(ID.Length - 4));
            int num = Users_Broadcast_Msg.Split('_').Length;
            Message message = new Message()
            {
                MsgBody = Users_Broadcast_Msg,
                Type = Message.EType.con,
                Length = num
            };
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse(IP), port);
            Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpClient.Connect(serverIp);
            //连接成功后向对方发送除对方外，所有群聊者的学号
            byte[] data = SerHelper.Serialize(message);
            tcpClient.Send(data);
            return tcpClient;
        }
        #endregion

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
            del = del.Replace('，', '_') + ".rtf";
            File.Delete("Data/" + user + "/Chat/" + del);
        }

        #region 发起聊天
        public void openbtn_Click(object sender, EventArgs e)
        {
            openbtn.Enabled = false;
            delbtn.Enabled = false;
            openbtn.Text = "连接中";
            int Number_Connected = 0;
            string Users_Broadcast_Msg;
            string[] friends = member.Split('，');
            int i = 0;
            foreach (string ipa in IP)
            {
                if (ipa == "n")
                {
                    IP[i] = Search(friends[i]);
                    if (IP[i] == "n")
                    {
                        MessageBox.Show("要发起会话的好友有人不在线", "无法发起会话"
                                     , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Dowork dowork = new Dowork(Able);
                        this.Invoke(dowork);
                        return;
                    }
                }
                i = i + 1;
            }
            string[] memarr = member.Split('，');
            foreach (string a in memarr)
            {
                foreach (string b in connected)
                {
                    if (b == a)
                    {
                        MessageBox.Show("已经在聊天了", "无法发起会话"
                                     , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Able();
                        return;
                    }
                }
                connected.Add(a);
            }
            Socket[] Chatters = new Socket[IP.Length];
            //向所有选中的人广播除了它自己外其他人的ID
            Task Thread_Chat = Task.Run(() =>
             {
                 i = 0;
                 foreach (string ip in IP)
                 {
                     //广播信息的第一条ID是自己的ID，ID与ID之间是连续的，通过/来分割
                     Users_Broadcast_Msg = user;
                     int j = 0;
                     foreach (string iitem in memarr)
                     {
                         if (i != j) Users_Broadcast_Msg += "_" + iitem;
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
                         Dowork dowork = new Dowork(Able);
                         this.Invoke(dowork);
                         return;
                     }
                     i += 1;
                 }
                 Application.Run(new ChatDialog(user, member, Chatters, Number_Connected, connected));
             });
            Thread_Chat.GetAwaiter().OnCompleted(() =>
            {
                Able();
            });
        }
        #endregion
    }
}
