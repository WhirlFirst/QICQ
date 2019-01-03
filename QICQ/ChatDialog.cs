using CCWin;
using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace QICQ
{

    public partial class ChatDialog : Skin_DevExpress
    {
        // 导入Windows自带的外部音频处理函数
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        string userID;
        string friends;
        string save = "";
        volatile Socket[] sockets;
        int conNum;
        int start = 0;
        int count = 0;
        int times = 0;
        int recordflag = 0;
        int playflag = 0;
        bool testBoxUsed = false;
        List<string> connected;
        Thread renew;
        private volatile bool canStop = false;
        private volatile bool filefinsh = false;

        #region 代理函数
        private delegate void RichBox_Show(string str, Color color, HorizontalAlignment direction);
        private delegate void DoWark(int value);
        private delegate int GetTxtLen();
        private delegate GifBox Paste(Image image, int p);
        private delegate void receive_save(Socket File_client, Message msg);
        private delegate void Socket_close(Socket File_client);
        private delegate void Shakeshake();
        #endregion

        public ChatDialog()
        {
            InitializeComponent();
        }
        public ChatDialog(string user, string chatters, Socket[] tsockets, int num, List<string> cc)
        {
            InitializeComponent();
            connected = cc;
            this.Text = "与" + chatters + "的聊天";
            userID = user;
            friends = chatters;
            sockets = tsockets;
            conNum = num;
            string[] savename = friends.Split('，');
            Array.Sort(savename);
            foreach (string na in savename)
            {
                save += na.Substring(na.Length - 4) + "_";
            }
            save = save.Substring(0, save.Length - 1);
            if (File.Exists("Data/" + userID + "/Chat/" + save + ".rtf"))
            {
                recivebox.LoadFile("Data/" + userID + "/Chat/" + save + ".rtf");
                ShowMsg_inRichTextBox("\n\n", Color.LightSkyBlue, HorizontalAlignment.Right);
            }
            renew = new Thread(() => Recive(sockets));
            renew.SetApartmentState(ApartmentState.STA);
            renew.IsBackground = true;
            renew.Start();
            recivebox.ShortcutsEnabled = false;
            sendbox.ShortcutsEnabled = false;
        }

        #region 文本框写入函数
        /// <summary>
        /// 以特定样式与颜色写入str
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        /// <param name="direction"></param>
        public void ShowMsg_inRichTextBox(string str, Color color, HorizontalAlignment direction)
        {
            recivebox.SelectionColor = color;
            recivebox.SelectionAlignment = direction;
            recivebox.AppendText(str);
            recivebox.ScrollToCaret();
        }
        #endregion

        #region 异步接受客户端消息
        /// <summary>
        /// 异步接受客户端消息
        /// </summary>
        /// <param name="links"></param>
        public void Recive(Socket[] links)
        {
            if (!canStop)
            {
                byte[] data = new byte[1024 * 2];
                try
                {
                    foreach (Socket tcpClient in links)   //遍历所有连接的套接字
                    {
                        if (tcpClient == null) break;
                        if (!tcpClient.Connected) return;
                        tcpClient.BeginReceive(data, 0, data.Length, SocketFlags.None,
                        asyncResult =>
                        {
                            int length = 0;
                            try
                            {
                                if (!tcpClient.Connected) return;
                                length = tcpClient.EndReceive(asyncResult);
                                if (length == 0)
                                {
                                    return;
                                }
                                Message msg = SerHelper.Deserialize<Message>(data);
                                switch (msg.Type)
                                {
                                    case Message.EType.msg:
                                        //如果当前写字框没有被占用
                                        while (testBoxUsed) { };
                                        //等到其他线程解除了写字框的占用
                                        testBoxUsed = true;   
                                        RichBox_Show rb_s = new RichBox_Show(ShowMsg_inRichTextBox);
                                        string show_string = msg.MsgBody;
                                        this.Invoke(rb_s, new object[] { "\n", Color.Black, HorizontalAlignment.Left });
                                        this.Invoke(rb_s, new object[] { show_string, Color.Black, HorizontalAlignment.Left });
                                        testBoxUsed = false; 
                                        if (sockets.Length > 1)
                                        {
                                            foreach (Socket Client in sockets)
                                            {
                                                if (Client == null) break;
                                                if (Client == tcpClient) continue;
                                                if (Client.Connected) AsynSend(Client, msg);
                                            }
                                        }
                                        break;

                                    case Message.EType.file:
                                        receive_save r_s = new receive_save(ReceiveFileConnect);
                                        this.Invoke(r_s, new object[] { tcpClient, msg });
                                        while (!filefinsh) { }
                                        filefinsh = false;
                                        break;
                                    case Message.EType.lgo:
                                        MessageBox.Show("好友" + msg.MsgBody + "退出了会话", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        if ((conNum > 1) && (msg.Length == 0))
                                        {
                                            conNum = conNum - 1;
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(this.Close));
                                            return;
                                        }
                                        if (sockets.Length > 1)
                                        {
                                            Socket_close socket_ = new Socket_close(SocketClose);
                                            this.Invoke(socket_, new object[] { tcpClient });
                                            foreach (Socket Client in sockets)
                                            {
                                                if (Client == null) break;
                                                if (Client == tcpClient) continue;
                                                if (Client.Connected) AsynSend(Client, msg);
                                            }
                                        }
                                        break;
                                    case Message.EType.dd:
                                        Shakeshake shakeshake = new Shakeshake(Shake);
                                        this.Invoke(shakeshake);
                                        if (sockets.Length > 1)
                                        {
                                            foreach (Socket Client in sockets)
                                            {
                                                if (Client == null) break;
                                                if (Client == tcpClient) continue;
                                                if (Client.Connected) AsynSend(Client, msg);
                                            }
                                        }
                                        break;
                                    case Message.EType.voice:
                                        receive_save r_v = new receive_save(ReceiveVoice);
                                        this.Invoke(r_v, new object[] { tcpClient, msg });
                                        while (!filefinsh) { }
                                        filefinsh = false;
                                        break;

                                }
                                Recive(links);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, ex.ToString(), "出现异常",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), "出现异常",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Invoke(new Action(this.Close));
                    return;
                }
            }
            else return;


            void SocketClose(Socket socket)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
        #endregion

        #region 异步发送消息
        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="tcpClient">客户端套接字</param>
        /// <param name="message">发送消息</param>
        /// //if_relay表示此信息是否是服务器转发客户端的信息
        public void AsynSend(Socket tcpClient, Message message)
        {
            Message chartMessage = message;
            byte[] data = SerHelper.Serialize(chartMessage);
            tcpClient.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
            {
                //完成发送消息
                try
                {
                    int length = tcpClient.EndSend(asyncResult);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.ToString(), "发送失败",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }, null);
        }
        #endregion

        private void sendbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string send_msg = sendbox.Text;

                //如果当前写字框没有被占用
                while (testBoxUsed) { };
                //等到其他线程解除了写字框的占用
                testBoxUsed = true;  
                RichBox_Show rb_s = new RichBox_Show(ShowMsg_inRichTextBox);
                string show_string = DateTime.Now.ToString()
                                            + "\n" + userID + "说：\n" + send_msg + "\n";
                this.Invoke(rb_s, new object[] { "\n", Color.Blue, HorizontalAlignment.Right });
                this.Invoke(rb_s, new object[] { show_string, Color.Blue, HorizontalAlignment.Right });
                testBoxUsed = false; 
                sendbox.Text = "";
                send_msg = DateTime.Now.ToString() + "\n" + userID + "说：\n" + send_msg + "\n";
                Message msg = new Message
                {
                    Type = Message.EType.msg,
                    MsgBody = send_msg,
                };
                foreach (Socket Client in sockets)
                {
                    if (Client == null) break;
                    if (Client.Connected) AsynSend(Client, msg);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("好友已关闭会话，不能发送信息", "出错啦。。。",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 关闭窗口前向好友发送下线信息，并保存本地聊天记录
        private void ChatDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Message msg;
            if (sockets.Length > 1)
            {
                msg = new Message
                {
                    Type = Message.EType.lgo,
                    MsgBody = "群主" + userID,
                    Length = 1
                };
            }
            else
            {
                msg = new Message
                {
                    Type = Message.EType.lgo,
                    MsgBody = userID,
                    Length = 0
                };
            }
            foreach (Socket Client in sockets)
            {
                if (Client == null) break;
                if (!Client.Connected) continue;
                byte[] data = SerHelper.Serialize(msg);
                Client.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
                {
                    //完成发送消息
                    try
                    {
                        int length = Client.EndSend(asyncResult);
                        Client.Shutdown(SocketShutdown.Both);
                        Client.Close();
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(ex.ToString(), "发送失败",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }, null);
            }
            string[] savename = friends.Split('，');
            foreach (string a in savename)
            {
                connected.Remove(a);
            }
            canStop = true;
            ShowMsg_inRichTextBox("\n", Color.LightSkyBlue, HorizontalAlignment.Center);
            ShowMsg_inRichTextBox("以上为" + DateTime.Now.ToString() + "之前的聊天记录", Color.LightSkyBlue, HorizontalAlignment.Center);
            recivebox.SaveFile("Data/" + userID + "/Chat/" + save + ".rtf");
            return;
        }
        #endregion

        #region 同步接收文件
        /// <summary>
        /// 同步接收文件
        /// </summary>
        /// <param name="File_client"></param>
        /// <param name="msg"></param>
        public void ReceiveFileConnect(Socket File_client, Message msg)
        {
            Thread savefile = new Thread(() =>
            {
                DialogResult dr = MessageBox.Show("好友要向你传一个文件，是否接收？", "提示"
                                                , MessageBoxButtons.OKCancel);
                string name = msg.MsgBody;
                long len = msg.Length;
                if (dr == DialogResult.OK)
                {
                    saveFileDialog1.Title = "请保存文件";
                    saveFileDialog1.Filter = "文件类型" + name + "|*" + name;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fileSavePath = saveFileDialog1.FileName;//获得用户保存文件的路径
                        int total = 0;
                        int received;
                        int buffer_size = 1024 * 1024;
                        byte[] buffer = new byte[buffer_size];
                        FileStream fs = new FileStream(fileSavePath, FileMode.Create, FileAccess.Write);
                        DoWark @do = new DoWark(setBar);
                        this.Invoke(new Action(this.fileBar.Show));
                        this.BeginInvoke(@do, new object[] { 0 });
                        while (true)
                        {
                            if ((len - total) < buffer_size)
                            {
                                buffer = new byte[len-total];
                                buffer_size = (int)len - total;
                            }
                            received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                            fs.Write(buffer, 0, received);
                            fs.Flush();
                            total += received;
                            this.BeginInvoke(@do, new object[] { (int)(((double)total / (double)len) * 100.0) });
                            if (total == len)
                            {
                                break;//接受完全部文件
                            }
                        }
                        filefinsh = true;
                        this.Invoke(new Action(this.fileBar.Hide));
                        fs.Close();
                        try
                        {
                            //自动向好友发送收到文件的回复
                            string send_msg = "我收到文件了！";
                            send_msg = DateTime.Now.ToString() + "\n" + userID + "说：\n" + send_msg + "\n";
                            Message finishmsg = new Message
                            {
                                Type = Message.EType.msg,
                                MsgBody = send_msg,
                            };
                            foreach (Socket Client in sockets)
                            {
                                if (Client == null) break;
                                if (Client.Connected) AsynSend(Client, finishmsg);
                            }

                        }
                        catch (Exception)
                        {

                        }
                        MessageBox.Show(fileSavePath + "文件接收完毕", "信息提示");
                        // 如果是群聊发起者，发给其他好友
                        string strOpenFileName = fileSavePath;//打开的文件的全限定名
                        FileInfo fi = new FileInfo(strOpenFileName);
                        byte[] data = SerHelper.Serialize(msg);
                        foreach (Socket Client in sockets)
                        {
                            if (Client == null) break;
                            if (Client == File_client) continue;
                            if (!Client.Connected) continue;
                            Client.Send(data);
                            Thread.Sleep(500);
                            Client.BeginSendFile(strOpenFileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, new AsyncCallback(sendfinish), null);

                            void sendfinish(IAsyncResult iar)
                            {
                                Client.EndSendFile(iar);
                            }
                        }
                    }
                    else
                    {
                        //用户在保存文件对话框中没有选择文件，所以只转发，先缓存到本地
                        string fileSavePath = "Data/" + userID + "/Tmp/f1" + userID.Substring(userID.Length - 4) + name;
                        int total = 0;
                        int received;
                        int buffer_size = 1024 * 1024;
                        byte[] buffer = new byte[buffer_size];
                        FileStream fs = new FileStream(fileSavePath, FileMode.Create, FileAccess.Write);
                        while (true)
                        {
                            received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                            fs.Write(buffer, 0, received);
                            fs.Flush();
                            total += received;
                            if (total == len)
                            {
                                break;
                            }
                        }
                        filefinsh = true;
                        fs.Close();
                        try
                        {
                            //自动向好友发送收到文件的回复
                            string send_msg = "文件缓存完毕！";
                            send_msg = DateTime.Now.ToString() + "\n" + userID + "说：\n" + send_msg + "\n";
                            Message finishmsg = new Message
                            {
                                Type = Message.EType.msg,
                                MsgBody = send_msg,
                            };
                            foreach (Socket Client in sockets)
                            {
                                if (Client == null) break;
                                if (Client.Connected) AsynSend(Client, finishmsg);
                            }

                        }
                        catch (Exception)
                        {

                        }
                        string strOpenFileName = fileSavePath;//打开的文件的全限定名
                        FileInfo fi = new FileInfo(strOpenFileName);
                        byte[] data = SerHelper.Serialize(msg);
                        foreach (Socket Client in sockets)
                        {
                            if (Client == null) break;
                            if (Client == File_client) continue;
                            if (!Client.Connected) continue;
                            Client.Send(data);
                            Thread.Sleep(500);
                            Client.BeginSendFile(strOpenFileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, new AsyncCallback(sendfinish), null);

                            void sendfinish(IAsyncResult iar)
                            {
                                Client.EndSendFile(iar);
                            }
                        }
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    //用户选择取消的操作，只转发,先缓存到本地
                    string fileSavePath = "Data/" + userID + "/Tmp/f1" + userID.Substring(userID.Length - 4) + name;
                    int total = 0;
                    int received;
                    int buffer_size = 1024 * 1024;
                    byte[] buffer = new byte[buffer_size];
                    FileStream fs = new FileStream(fileSavePath, FileMode.Create, FileAccess.Write);
                    while (true)
                    {
                        received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                        fs.Write(buffer, 0, received);
                        fs.Flush();
                        total += received;
                        if (total == len)
                        {
                            break;
                        }
                    }
                    filefinsh = true;
                    fs.Close();
                    try
                    {
                        //自动向好友发送收到文件的回复
                        string send_msg = "文件缓存完毕！";
                        send_msg = DateTime.Now.ToString() + "\n" + userID + "说：\n" + send_msg + "\n";
                        Message finishmsg = new Message
                        {
                            Type = Message.EType.msg,
                            MsgBody = send_msg,
                        };
                        foreach (Socket Client in sockets)
                        {
                            if (Client == null) break;
                            if (Client.Connected) AsynSend(Client, finishmsg);
                        }

                    }
                    catch (Exception)
                    {

                    }
                    string strOpenFileName = fileSavePath;//打开的文件的全限定名
                    FileInfo fi = new FileInfo(strOpenFileName);
                    byte[] data = SerHelper.Serialize(msg);
                    foreach (Socket Client in sockets)
                    {
                        if (Client == null) break;
                        if (Client == File_client) continue;
                        if (!Client.Connected) continue;
                        Client.Send(data);
                        Thread.Sleep(500);
                        Client.BeginSendFile(strOpenFileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, new AsyncCallback(sendfinish), null);

                        void sendfinish(IAsyncResult iar)
                        {
                            Client.EndSendFile(iar);
                        }
                    }
                }
                return;
            });
            //设置为单线程，否则无法打开文件选择框
            savefile.SetApartmentState(ApartmentState.STA);
            savefile.Start();
            return;
        }
        #endregion

        #region 异步发送文件
        private void filebtn_Click(object sender, EventArgs e)
        {
            Thread readfile = new Thread(() =>
            {
                openFileDialog1.Title = "请选择要传输的文件";
                openFileDialog1.Multiselect = false;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string strOpenFileName = openFileDialog1.FileName;//打开的文件名
                    string[] names = strOpenFileName.Split('.');
                    string post ="." + names[names.Length-1];
                    FileInfo fi = new FileInfo(strOpenFileName);
                    //先发送文件长度以及后缀名
                    Message FileMessage = new Message()
                    {
                        MsgBody = post,
                        Type = Message.EType.file,
                        Length = fi.Length,
                    };
                    byte[] data = SerHelper.Serialize(FileMessage);
                    foreach (Socket Client in sockets)
                    {
                        if (Client == null) break;
                        if (!Client.Connected) continue;
                        Client.Send(data);
                        Thread.Sleep(500);
                        Client.BeginSendFile(strOpenFileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, new AsyncCallback(sendfinish), null);

                        void sendfinish(IAsyncResult iar)
                        {
                            Client.EndSendFile(iar);
                        }
                    }
                    MessageBox.Show(strOpenFileName + "文件传输成功,为了保证文件顺利传输，请在看到对面回复文件接收成功后再发送其他消息！", "信息提示");
                }
                else
                {
                    MessageBox.Show("你没有选择文件", "信息提示");
                }
                return;
            });
            readfile.SetApartmentState(ApartmentState.STA);
            readfile.Start();
            return;
        }
        #endregion

        #region 设置进度条
        private void setBar(int value)
        {
            this.fileBar.Value = value;
            this.fileBar.Update();
        }
        #endregion

        #region 聊天记录查询
        private void findbtn_Click(object sender, EventArgs e)
        {
            string find = findtxt.Text.ToString();
            if (find == "")
            {
                MessageBox.Show("请输入要查询的内容", "查询错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (start >= recivebox.Text.Length)
            {
                MessageBox.Show("已查找到尾部");

                start = 0;

            }

            else

            {

                start = recivebox.Find(find, start, RichTextBoxFinds.MatchCase);

                if (start == -1)

                {

                    if (count == 0)

                    {
                        MessageBox.Show("没有该字符");
                        start = 0;
                    }

                    else

                    {
                        MessageBox.Show("已查找到尾部！");
                        start = 0;
                        count = 0;
                    }

                }

                else

                {

                    start = start + find.Length;
                    count += 1;
                    recivebox.Focus();

                }

            }
        }
        #endregion

        private void findtxt_TextChanged(object sender, EventArgs e)
        {
            start = 0;
            count = 0;
        }

        #region 语音录制及发送
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (recordflag == 0)
            {
                mciSendString("set wave bitpersample 8", "", 0, 0);  //设为8位
                mciSendString("set wave samplespersec 11025", "", 0, 0);//设置每个声道播放和记录时的样本频率为20Hz-20000Hz
                mciSendString("set wave channels 2", "", 0, 0);  //设为立体声
                mciSendString("set wave format tag pcm", "", 0, 0);//设置PCM（脉冲编码调制）
                mciSendString("open new type WAVEAudio alias movie", "", 0, 0); //打开一个新的录音文件
                mciSendString("record movie", "", 0, 0);  //开始录音
                recordflag = 1;
                toolStripButton1.Image = Properties.Resources.yying;
            }
            else
            {
                File.Delete("Data/" + userID + "/Tmp/" + "recordvoice.wav");
                mciSendString("stop movie", "", 0, 0);//停止录音
                mciSendString("save movie" + " " + "Data/" + userID + "/Tmp/" + "recordvoice.wav", "", 0, 0);//保存录音文件，recordvoice.wav为录音文件
                mciSendString("close movie", "", 0, 0);   //关闭录音
                toolStripButton1.Image = Properties.Resources.yy;
                recordflag = 0;
                FileInfo fi = new FileInfo("Data/" + userID + "/Tmp/" + "recordvoice.wav");
                try
                {
                    Message msg = new Message
                    {
                        Type = Message.EType.voice,
                        MsgBody = userID,
                        Length = fi.Length
                    };
                    byte[] data = SerHelper.Serialize(msg);
                    foreach (Socket Client in sockets)
                    {
                        if (Client == null) break;
                        if (!Client.Connected) continue;
                        Client.Send(data);
                        Thread.Sleep(500);
                        Client.BeginSendFile("Data/" + userID + "/Tmp/" + "recordvoice.wav", null, null, TransmitFileOptions.UseDefaultWorkerThread, new AsyncCallback(sendfinish), null);

                        void sendfinish(IAsyncResult iar)
                        {
                            Client.EndSendFile(iar);
                            MessageBox.Show("语音消息发送成功", "信息提示");
                        }
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("好友已关闭会话，不能发送语音", "出错啦。。。",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 语音播放
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string audiofile = "Data/" + userID + "/Tmp/recive.wav";//音频文件路径
            string CommandString = "open " + "\"" + audiofile + "\"" + " alias Mp3File";
            mciSendString(CommandString, null, 0, 0);
            CommandString = "set Mp3File time format ms";
            mciSendString(CommandString, null, 0, 0);
            CommandString = "seek Mp3File to 0";
            mciSendString(CommandString, null, 0, 0);
            CommandString = "play Mp3File";
            mciSendString(CommandString, null, 0, 0);
            playflag = 1;
        }
        #endregion

        #region 窗口抖动
        private void Shake()
        {
            times = 0;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int count1 = times % 4;
            if (count1 < 2)
            {
                Point new_loc = new Point(this.Location.X - 5, this.Location.Y - 5);
                this.Location = new_loc;
                times++;
            }
            else if (count1 < 4)
            {
                Point new_loc = new Point(this.Location.X + 5, this.Location.Y + 5);
                this.Location = new_loc;
                times++;
            }
            else if (count1 < 6)
            {
                Point new_loc = new Point(this.Location.X + 5, this.Location.Y - 5);
                this.Location = new_loc;
                times++;
            }
            else if (count1 < 8)
            {
                Point new_loc = new Point(this.Location.X - 5, this.Location.Y + 5);
                this.Location = new_loc;
                times++;
            }
            if (times == 20)
                timer1.Stop();
        }
        private void shakebtn_Click(object sender, EventArgs e)
        {
            try
            {
                Message msg = new Message
                {
                    Type = Message.EType.dd,
                };
                foreach (Socket Client in sockets)
                {
                    if (Client == null) break;
                    if (Client.Connected) AsynSend(Client, msg);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("好友已关闭会话，不能发送信息", "出错啦。。。",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 异步接受语音消息
        public void ReceiveVoice(Socket File_client, Message msg)
        {
            if (playflag == 1)
            {
                mciSendString("close Mp3File", null, 0, 0);
                playflag = 0;
            }
            Thread savevoice = new Thread(() =>
            {
                File.Delete("Data/" + userID + "/Tmp/recive.wav");
                long len = msg.Length;
                string fileSavePath = "Data/" + userID + "/Tmp/recive.wav";
                int total = 0;
                int received;
                int buffer_size = 1024 * 1024;
                byte[] buffer = new byte[buffer_size];
                FileStream fs = new FileStream(fileSavePath, FileMode.Create, FileAccess.Write);
                while (true)
                {
                    received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                    fs.Write(buffer, 0, received);
                    fs.Flush();
                    total += received;
                    if (total == len)
                    {
                        break;
                    }
                }
                filefinsh = true;
                fs.Close();
                string strOpenFileName = fileSavePath;//打开的文件的全限定名
                FileInfo fi = new FileInfo(strOpenFileName);
                byte[] data = SerHelper.Serialize(msg);
                foreach (Socket Client in sockets)
                {
                    if (Client == null) break;
                    if (Client == File_client) continue;
                    if (!Client.Connected) continue;
                    Client.Send(data);
                    Thread.Sleep(500);
                    Client.BeginSendFile(strOpenFileName, null, null, TransmitFileOptions.UseDefaultWorkerThread, new AsyncCallback(sendfinish), null);

                    void sendfinish(IAsyncResult iar)
                    {
                        Client.EndSendFile(iar);
                    }
                }
                MessageBox.Show("好友" + msg.MsgBody + "向您发送了语音消息！", "信息提示");
                return;
            });
            savevoice.Start();
            return;
        }
        #endregion

        #region 快捷键发送消息
        private void sendbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                sendbtn_Click(sender, e);
        }
        #endregion
    }
}

