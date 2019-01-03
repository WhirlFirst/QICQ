using CCWin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;//命名空间
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace QICQ
{
    public partial class Main : Skin_DevExpress
    {
        public string username;
        Thread fresh;
        Thread msginfo;
        Socket Socket_user_server;//本机套接字
        Socket tcpServer;
        IPAddress Server = IPAddress.Parse("166.111.140.14");
        IPAddress local_ip;
        Login login;
        public List<string> connected = new List<string> { };
        private volatile bool canStop = false;//线程间通讯标识符

        #region 代理函数，保证多线程访问
        public delegate int GetValueForChannelNoCallBack();
        public delegate string GetItem(int x, int y);
        public delegate void SetItem(int x, int y, string z);
        public delegate void AddItem(string[] z);
        public delegate void SetBgc(int x, int y, Color c);
        public delegate ListView.SelectedListViewItemCollection GetS();
        public delegate void AddChatInfo(Control z);

        private int GetCount()
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    GetValueForChannelNoCallBack stcb = new GetValueForChannelNoCallBack(GetCount);

                    IAsyncResult ia = userlist.BeginInvoke(stcb);
                    return (int)userlist.EndInvoke(ia);  //这里需要利用EndInvoke来获取返回值

                }
                else
                {
                    return userlist.Items.Count;
                }
            }
            catch (Exception) { return 0; }
        }

        private int GetSCount()
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    GetValueForChannelNoCallBack stcb = new GetValueForChannelNoCallBack(GetSCount);

                    IAsyncResult ia = userlist.BeginInvoke(stcb);
                    return (int)userlist.EndInvoke(ia);  //这里需要利用EndInvoke来获取返回值

                }
                else
                {
                    return userlist.SelectedItems.Count;
                }
            }
            catch (Exception) { return 0; }
        }

        private ListView.SelectedListViewItemCollection GetSelect()
        {
            if (this.userlist.InvokeRequired)
            {
                GetS stcb = new GetS(GetSelect);

                IAsyncResult ia = userlist.BeginInvoke(stcb);
                return (ListView.SelectedListViewItemCollection)userlist.EndInvoke(ia);  //这里需要利用EndInvoke来获取返回值

            }
            else
            {
                return userlist.SelectedItems;
            }
        }

        private string GetListItem(int x, int y)
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    GetItem stcb = new GetItem(GetListItem);

                    IAsyncResult ia = userlist.BeginInvoke(stcb, new object[] { x, y });
                    return (string)userlist.EndInvoke(ia);  //这里需要利用EndInvoke来获取返回值

                }
                else
                {
                    return userlist.Items[x].SubItems[y].Text.ToString();
                }
            }
            catch (Exception) { return ""; }
        }

        private void SetListItem(int x, int y, string z)
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    SetItem stcb = new SetItem(SetListItem);

                    userlist.BeginInvoke(stcb, new object[] { x, y, z });

                }
                else
                {
                    userlist.Items[x].SubItems[y].Text = z;
                }
            }
            catch (Exception) { }
        }

        private void AddListItem(string[] z)
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    AddItem stcb = new AddItem(AddListItem);

                    userlist.BeginInvoke(stcb, new object[] { z });

                }
                else
                {
                    ListViewItem mid_list = new ListViewItem(z);
                    mid_list.BackColor = Color.LightBlue;
                    userlist.Items.Add(mid_list);
                }
            }
            catch (Exception) { }
        }

        private void AddChat(Control z)
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    AddChatInfo stcb = new AddChatInfo(AddChat);

                    flowLayoutPanel1.BeginInvoke(stcb, new object[] { z });

                }
                else
                {
                    flowLayoutPanel1.Controls.Add(z);
                }
            }
            catch (Exception) { }
        }

        private void SetListColor(int x, int y, Color c)
        {
            try
            {
                if (this.userlist.InvokeRequired)
                {
                    SetBgc stcb = new SetBgc(SetListColor);

                    userlist.BeginInvoke(stcb, new object[] { x, y, c });

                }
                else
                {
                    userlist.Items[x].SubItems[y].BackColor = c;
                }
            }
            catch (Exception) { }
        }
        #endregion

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重载主函数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="client"></param>
        /// <param name="tlogin"></param>
        public Main(string user, Socket client, Login tlogin)
        {
            InitializeComponent();
            username = user;
            Socket_user_server = client;
            login = tlogin;
            IPAddress[] ip_list = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipAddress in ip_list)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    local_ip = ipAddress;
                }
            }
            holo.Text = "你好！" + username;

            Directory.CreateDirectory("Data/" + user + "/Chat/");
            Directory.CreateDirectory("Data/" + user + "/Tmp/");
            if (!File.Exists("Data/" + user + "/user.txt"))
            {
                FileStream fs1 = new FileStream("Data/" + user + "/user.txt", FileMode.Create);
                fs1.Close();
            }
            StreamReader sr = new StreamReader("Data/" + user + "/user.txt", Encoding.Default);
            String line;
            this.userlist.BeginUpdate();
            while ((line = sr.ReadLine()) != null)
            {
                string[] mid_string = new string[3];
                mid_string[0] = line;
                mid_string[1] = " ";
                mid_string[2] = "OffLine";
                ListViewItem mid_list = new ListViewItem(mid_string);
                this.userlist.Items.Add(mid_list);
            }
            this.userlist.EndUpdate();
            foreach (ListViewItem item in userlist.Items)
            {
                string oldmsg = item.SubItems[2].Text.ToString();
                if (oldmsg == "OnLine")
                {
                    item.SubItems[0].BackColor = Color.LightGreen;
                }
                else item.SubItems[0].BackColor = Color.LightGray;
            }
            sr.Close();
            flowLayoutPanel1.WrapContents = false;
            tcpServer = StartListening(tcpServer);
            msginfo = new Thread(() =>
            {
                int num = 0;
                int tmp;
                string[] ipAddress;
                DirectoryInfo TheFolder = new DirectoryInfo("Data/" + user + "/Chat/");
                //遍历文件夹
                num = TheFolder.GetFiles().Length;
                foreach (FileInfo NextFolder in TheFolder.GetFiles())
                {
                    string[] arrshow = NextFolder.Name.Substring(0, NextFolder.Name.Length - 4).Split('_');
                    string show = "";
                    ipAddress = new string[arrshow.Length];
                    int i = 0;
                    foreach (string sh in arrshow)
                    {
                        show += "201601" + sh + "，";
                        ipAddress[i] = Search("201601" + sh);
                        i = i + 1;
                    }
                    show = show.Substring(0, show.Length - 1);
                    ChatInfo chatInfo = new ChatInfo(user, show, ipAddress, Socket_user_server, connected);
                    AddChat(chatInfo);
                }
                while (true)
                {
                    DirectoryInfo WFolder = new DirectoryInfo("Data/" + user + "/Chat/");
                    tmp = WFolder.GetFiles().Length;
                    if (tmp != num)
                    {
                        this.Invoke(new Action(flowLayoutPanel1.Controls.Clear));
                        foreach (FileInfo NextFolder in TheFolder.GetFiles())
                        {
                            string[] arrshow = NextFolder.Name.Substring(0, NextFolder.Name.Length - 4).Split('_');
                            string show = "";
                            ipAddress = new string[arrshow.Length];
                            int i = 0;
                            foreach (string sh in arrshow)
                            {
                                show += "201601" + sh + "，";
                                ipAddress[i] = Search("201601" + sh);
                                i = i + 1;
                            }
                            show = show.Substring(0, show.Length - 1);
                            ChatInfo chatInfo = new ChatInfo(user, show, ipAddress, Socket_user_server, connected);
                            AddChat(chatInfo);
                        }
                        num = tmp;
                    }
                }
            });
            fresh = new Thread(new ThreadStart(Searchall));
            fresh.Start();
            msginfo.Start();
        }

        #region 注销
        /// <summary>
        /// 注销
        /// </summary>
        private void Log_out()
        {
            string mess = "logout" + username;
            //开始建立连接
            byte[] login_byte = new byte[1024];
            login_byte = Encoding.ASCII.GetBytes(mess);

            try
            {
                Socket_user_server.Send(login_byte, login_byte.Length, 0);
            }
            catch (Exception)
            {
                MessageBox.Show("请检查网络连接", "注销错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //接收注销信息
            byte[] receive_byte = new byte[1024];
            int number = Socket_user_server.Receive(receive_byte, receive_byte.Length, 0);

            string receive_mess = Encoding.Default.GetString(receive_byte, 0, number);
            if (receive_mess == "loo")
            {
                // MessageBox.Show("成功下线", "信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show(receive_mess+"注销失败", "信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }
        #endregion

        #region 关闭窗口，结束所有异步线程
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            canStop = true;
            fresh.Abort();
            msginfo.Abort();
            tcpServer.Close();
            Thread fThread = new Thread(new ThreadStart(Log_out));
            fThread.Start();
            //修改本地好友列表
            FileStream fs = new FileStream("Data/" + username + "/user.txt", FileMode.Open, FileAccess.Write);
            fs.SetLength(0);//清空
            fs.Close();
            StreamWriter sw = new StreamWriter("Data/" + username + "/user.txt", true);
            //开始写入
            foreach (ListViewItem item in userlist.Items)
            {
                sw.WriteLine(item.SubItems[0].Text);
            }
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            this.Hide();
            login.Show();
        }
        #endregion

        #region 异步查询好友在线状态
        /// <summary>
        /// 异步查询好友在线状态
        /// </summary>
        /// <param name="IDnumber"></param>
        /// <returns></returns>
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
            byte[] IP_receive_byte = new byte[1024];
            int number;
            try
            {
                number = Socket_user_server.Receive(IP_receive_byte);
            }
            catch (Exception)
            {
                return ("");
            }
            string IP_receive_mess = Encoding.Default.GetString(IP_receive_byte, 0, number);
            return IP_receive_mess;            //接收用户信息
        }
        #endregion

        #region 异步动态更新好友列表
        private void Searchall()//循环函数，每5秒自动刷新一次好友列表在线状态
        {
            IPEndPoint ip_port = new IPEndPoint(Server, 8000);
            while (true)
            {
                int flag = 0;
                int num = GetCount();
                for (int i = 0; i < num; i++)
                {
                    string id = GetListItem(i, 0);
                    string msg = Search(id);
                    string oldmsg = GetListItem(i, 2);
                    if (oldmsg != "Connecting")
                    {
                        if (msg == " ")
                        {
                            flag = 1;
                            break;
                        }
                        if (msg == "n")
                        {
                            if (oldmsg != "OffLine")
                            {
                                SetListItem(i, 2, "OffLine");
                                SetListItem(i, 1, " ");
                                SetListColor(i, 0, Color.Orange);
                            }
                        }
                        else
                        {
                            if (oldmsg != "OnLine")
                            {
                                SetListItem(i, 2, "OnLine");
                                SetListItem(i, 1, msg);
                                SetListColor(i, 0, Color.Orange);
                            }
                        }
                        Thread.Sleep(10);
                    }
                }
                if (flag == 1) break;
                Thread.Sleep(5000);
            }
        }
        #endregion

        #region 查询按钮
        /// <summary>
        /// 查询按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Searchbtn_Click(object sender, EventArgs e)
        {
            searchbtn.Enabled = false;
            string ID = searchtext.Text.ToString();
            if (ID == username)
            {
                MessageBox.Show("别添加自己为好友啦", "查询错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                searchbtn.Enabled = true;
                return;
            }
            if (ID == "")
            {
                MessageBox.Show("请输入要查询的学号", "查询错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                searchbtn.Enabled = true;
                return;
            }
            string msg = Search(ID);
            int state = 1;//状态，在线为1，不在线为0，错误为2
            if (msg == "n")
            {
                state = 0;
            }
            else if (msg == "Please send the correct message." || msg == "Incorrect No.")
            {
                state = 2;
                MessageBox.Show("信息输入错误或该同学不存在，请检查学号是否正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                searchbtn.Enabled = true;
                return;
            }
            else if (msg == "")
            {
                MessageBox.Show("信息错误", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                searchbtn.Enabled = true;
                return;
            }
            foreach (ListViewItem item in userlist.Items)
            {
                if (ID == item.SubItems[0].Text)
                {
                    if (state == 0)
                    {
                        item.SubItems[1].Text = " ";
                        item.SubItems[2].Text = "OffLine";
                        MessageBox.Show("该好友不在线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        searchbtn.Enabled = true;
                        return;
                    }
                    else if (state == 1)
                    {
                        item.SubItems[1].Text = msg;
                        item.SubItems[2].Text = "OnLine";
                        MessageBox.Show("该好友在线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        searchbtn.Enabled = true;
                        return;
                    }
                }
            }
            //添加好友
            string[] mid_string = new string[3];
            mid_string[0] = ID;
            if (state == 0)
            {
                mid_string[1] = " ";
                mid_string[2] = "OffLine";
            }
            else
            {
                mid_string[1] = msg;
                mid_string[2] = "OnLine";
            }
            ListViewItem mid_list = new ListViewItem(mid_string);
            if (state == 0) mid_list.BackColor = Color.LightGray;
            else mid_list.BackColor = Color.LightGreen;
            this.userlist.Items.Add(mid_list);
            MessageBox.Show("成功添加好友！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            searchbtn.Enabled = true;
        }
        #endregion

        #region 删除好友
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delid_Click(object sender, EventArgs e)
        {
            if (userlist.SelectedItems.Count > 0 && (skinTabControl1.SelectedIndex == 0))//防止误触发
            {
                foreach (ListViewItem item in userlist.SelectedItems)
                {
                    item.Remove();
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的好友", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region  防止行列被拖动
        private void Userlist_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = userlist.Columns[e.ColumnIndex].Width;
        }
        #endregion

        #region 鼠标单击响应函数，更改显示状态
        private void userlist_MouseClick(object sender, MouseEventArgs e)
        {
            if (userlist.SelectedIndices != null && userlist.SelectedItems.Count > 0)//防止误触发
            {

                foreach (ListViewItem item in userlist.SelectedItems)
                {
                    string oldmsg = item.SubItems[2].Text.ToString();
                    if (oldmsg == "OnLine")
                    {
                        item.SubItems[0].BackColor = Color.LightGreen;
                    }
                    else if (oldmsg == "Connecting")
                    {
                        item.SubItems[0].BackColor = Color.LightBlue;
                    }
                    else item.SubItems[0].BackColor = Color.LightGray;
                }
            }
        }
        #endregion

        #region 双击打开聊天
        private void Userlist_DoubleClick(object sender, EventArgs e)
        {
            Socket[] chatSocket = new Socket[1];
            foreach (ListViewItem item in userlist.SelectedItems)
            {
                foreach (string a in connected)
                {
                    if (item.SubItems[0].Text.ToString() == a)
                    {
                        MessageBox.Show("已经在聊天了", "无法发起会话"
                                  , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (item.SubItems[2].Text.ToString() == "Connecting")
                {
                    MessageBox.Show("你应该已经和Ta在聊天了", "无法发起会话"
                                  , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (item.SubItems[0].Text.ToString() == username)
                {
                    MessageBox.Show("别和自己聊天了，多和别人聊聊吧", "无法发起会话"
                                  , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                };
                if (item.SubItems[2].Text.ToString() == "OffLine")
                {
                    MessageBox.Show("好友不在线", "无法发起会话"
                                  , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                };
                string ID = item.SubItems[0].Text.ToString();
                string IP = item.SubItems[1].Text.ToString();
                int num = item.Index;
                connected.Add(ID);
                Task Thread_Chat = Task.Run(() =>
                Newwindow(ID, IP, num));
                item.SubItems[2].Text = "Connecting";
                item.SubItems[0].BackColor = Color.LightBlue;
                Thread_Chat.GetAwaiter().OnCompleted(() =>
                {
                    int listnum = GetCount();
                    for (int i = 0; i < listnum; i++)
                    {
                        string curid = GetListItem(i, 0);
                        if (curid == ID)
                        {
                            SetListItem(i, 2, "OnLine");
                            SetListColor(i, 0, Color.LightGreen);
                            break;
                        }
                    }
                });
                return;
            }
            // 向聊天窗口传入自己与聊天者学号，聊天者IP地址以及聊天人数
            void Newwindow(string ID, string IP, int num)
            {
                string Users_Broadcast_Msg = username;
                try
                {
                    chatSocket[0] = Connect_GroupChat(
                    IP, ID, Users_Broadcast_Msg);
                }
                catch (Exception)
                {
                    string[] savename = ID.Split('，');
                    foreach (string a in savename)
                    {
                        connected.Remove(a);
                    }
                    MessageBox.Show("未知错误，好友可能虚假在线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetListColor(num, 0, Color.LightGreen);
                    SetListItem(num, 2, "OnLine");
                    return;
                }
                string friends = ID;
                Application.Run(new ChatDialog(username, friends, chatSocket, 1, connected));
            }
        }
        #endregion

        #region 异步发起连接，及发送基本信息
        /// <summary>
        /// 与好友尝试连接，并在连接成功后发送聊天人数及群聊名
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="ID"></param>
        /// <param name="Users_Broadcast_Msg"></param>
        /// <returns></returns>
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

        #region Tcp协议异步监听
        /// <summary>
        /// Tcp协议异步监听
        /// </summary>
        /// <param name="tcpServer"></param>
        /// <returns></returns>
        public Socket StartListening(Socket tcpServer)
        {
            int port = Int32.Parse(username.Substring(username.Length - 4));
            IPEndPoint serverIP = new IPEndPoint(local_ip, port);
            tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(serverIP);
            tcpServer.Listen(20);
            AsynAccept(tcpServer);
            return tcpServer;
        }
        #endregion

        #region 异步接受连接
        /// <summary>
        /// 异步接受连接
        /// </summary>
        /// <param name="socket"></param>
        public void AsynAccept(Socket socket)
        {
            socket.BeginAccept(asyncResult =>
            {
                if (canStop) return;
                Socket tcpClient = socket.EndAccept(asyncResult);
                AsynAccept(socket);     //继续监听其他连接
                AsynRecive_ID(tcpClient);  //接收监听到的这条连接的广播信息
            }, null);
        }
        #endregion

        #region 异步接受客户端消息,更新UI状态
        /// <summary>
        /// 异步接受客户端消息,更新UI状态
        /// </summary>
        /// <param name="soc"></param>
        public void AsynRecive_ID(Socket soc)
        {
            byte[] data = new byte[1024];
            try
            {
                soc.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    Message msg = SerHelper.Deserialize<Message>(data);
                    string Users_Broadcast_Received = msg.MsgBody;
                    string[] conts = Users_Broadcast_Received.Split('_');
                    foreach (string con in conts)
                    {
                        int listnum = GetCount();
                        int flag = 0;
                        for (int i = 0; i < listnum; i++)
                        {
                            string curid = GetListItem(i, 0);
                            if (curid == con)
                            {
                                SetListItem(i, 2, "Connecting");
                                SetListColor(i, 2, Color.LightBlue);
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            string[] mid_string = new string[3];
                            mid_string[0] = con;
                            mid_string[1] = Search(con);
                            mid_string[2] = "Connecting";
                            AddListItem(mid_string);
                        }
                    }

                    Socket[] Connect_received = new Socket[1];
                    Connect_received[0] = soc;
                    string show = Users_Broadcast_Received.Replace('_', '，');
                    string[] names = show.Split('，');
                    foreach (string a in names)
                    {
                        connected.Add(a);
                    }
                    Task Thread_Chat = Task.Run(() =>
                    {
                        Application.Run(new ChatDialog(username, show
                                                           , Connect_received, (int)msg.Length, connected));
                    }
                        );

                    Thread_Chat.GetAwaiter().OnCompleted(() =>
                    {
                        foreach (string con in conts)
                        {
                            int listnum = GetCount();
                            for (int i = 0; i < listnum; i++)
                            {
                                string curid = GetListItem(i, 0);
                                if (curid == con)
                                {
                                    SetListItem(i, 2, "OnLine");
                                    SetListColor(i, 0, Color.LightGreen);
                                    break;
                                }
                            }
                        }
                    });
                }, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "接收失败",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        //UI美化
        private void delid_MouseEnter(object sender, EventArgs e)
        {
            delid.BackColor = Color.Red;
            delid.ForeColor = Color.White;
        }

        private void delid_MouseLeave(object sender, EventArgs e)
        {
            delid.BackColor = Color.White;
            delid.ForeColor = Color.Black;
        }

        #region 帮助按钮
        private void helpbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("查询：输入学号后点击放大镜即可查询。表中好友状态实时更新，状态有变化为黄色，在线为绿色，离线为灰色\n删除：选择表中好友点击恩断义绝就可以删除好友！\n双击表中任意一行就可以与它聊天\n群聊：选中列表中多个好友，点击右下角的群聊按钮即可\n消息列表：可查看先前的聊天对话", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region 群聊按钮响应函数
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.userlist.SelectedItems.Count > 0)//判断listview有被选中项
            {
                foreach (ListViewItem iitem in userlist.SelectedItems)
                {
                    if (iitem.SubItems[2].Text.ToString() == "OffLine")
                    {
                        MessageBox.Show("要发起会话的好友有人不在线", "无法发起会话"
                                      , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    };

                    if (iitem.SubItems[2].Text.ToString() == "Connecting")
                    {
                        MessageBox.Show("已经在聊天了", "无法发起会话"
                                  , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    foreach (string a in connected)
                    {
                        if (iitem.SubItems[2].Text.ToString() == a)
                        {
                            MessageBox.Show("已经在聊天了", "无法发起会话"
                                      , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                int Number_Connected = 0;
                string Users_Broadcast_Msg;
                Socket[] Chatters = new Socket[userlist.SelectedItems.Count];
                //向所有选中的人广播除了它自己外其他人的ID
                foreach (ListViewItem item_outloop in userlist.SelectedItems)
                {
                    if (item_outloop.SubItems[0].Text.ToString() == username) continue;
                    //广播ID,通过_来分割
                    Users_Broadcast_Msg = username;
                    foreach (ListViewItem iitem in userlist.SelectedItems)
                    {
                        if (item_outloop.SubItems[0].Text != iitem.SubItems[0].Text)
                            Users_Broadcast_Msg += "_" + iitem.SubItems[0].Text;
                    }
                    try
                    {
                        Chatters[Number_Connected] = Connect_GroupChat(
                        item_outloop.SubItems[1].Text, item_outloop.SubItems[0].Text, Users_Broadcast_Msg);
                        Number_Connected++;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("未知错误，有好友可能虚假在线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                string friends = "";
                foreach (ListViewItem iitem in userlist.SelectedItems)
                {
                    if (iitem.SubItems[0].Text.ToString() == username) continue;
                    friends += iitem.SubItems[0].Text + "，";
                    iitem.SubItems[2].Text = "Connecting";
                    iitem.SubItems[0].BackColor = Color.LightBlue;
                    connected.Add(iitem.SubItems[0].Text.ToString());
                }
                friends = friends.Substring(0, friends.Length - 1);
                string[] names = friends.Split('，');
                Task Thread_Chat = Task.Run(() =>
                Application.Run(new ChatDialog(username, friends, Chatters, Number_Connected, connected)));
                Thread_Chat.GetAwaiter().OnCompleted(() =>
                {
                    int listnum = GetCount();
                    for (int i = 0; i < listnum; i++)
                    {
                        string curid = GetListItem(i, 0);
                        foreach (string ID in names)
                        {
                            if (curid == ID)
                            {
                                SetListItem(i, 2, "OnLine");
                                SetListColor(i, 0, Color.LightGreen);
                                break;
                            }
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("请选择要发起群聊的好友", "无法发起会话"
                                     , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
    #endregion
}
