using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using CCWin;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;//命名空间
using CCWin.SkinControl;

namespace QICQ
{
    public partial class ChatDialog : Skin_DevExpress
    {
        string userID;
        string friends;
        volatile Socket[] sockets;
        int conNum;
        bool richTextBox_show_writing = false;
        Thread renew;
        private volatile bool canStop = false;
        private volatile bool filefinsh = false;
        private delegate void RichBox_Show(string str, Color color, HorizontalAlignment direction);
        private delegate void Disconnect();
        private delegate GifBox Paste(Image image,int p);
        private delegate void receive_save(Socket File_client, Message msg);
        public ChatDialog()
        {
            InitializeComponent();
        }
        public ChatDialog(string user,string chatters, Socket[] tsockets,int num)
        {
            InitializeComponent();
            this.Text = "与" + chatters + "的聊天";
            userID = user;
            friends = chatters;
            sockets = tsockets;
            conNum = num;
            renew = new Thread(() => AsynRecive(sockets));
            renew.SetApartmentState(ApartmentState.STA);
            renew.IsBackground = true;
            renew.Start();
            //AsynRecive(sockets);//异步接收消息
        }

        public void ShowMsg_inRichTextBox(string str, Color color, HorizontalAlignment direction)
        {
            recivebox.SelectionColor = color;
            recivebox.SelectionAlignment = direction;
            //向文本框的文本追加文本
            recivebox.AppendText(str);
        }

        #region 异步接受客户端消息
        /// <summary>
        /// 异步接受客户端消息
        /// </summary>
        /// <param name="tcpClient"></param>
        public void AsynRecive(Socket[] links)
        {
            if (!canStop)
            {
                byte[] data = new byte[1024];
                try
                {
                    foreach (Socket tcpClient in links)   //遍历所有连接的套接字
                    {
                        if (tcpClient == null) break;
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
                                    MessageBox.Show("好友退出了会话", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (this.IsHandleCreated)
                                    {
                                        this.Invoke(new Action(this.Close));
                                    }
                                    return;
                                }
                                Message msg = SerHelper.Deserialize<Message>(data);
                                switch (msg.Type)
                                {
                                    case Message.EType.msg:
                                        //如果当前写字框没有被占用
                                        while (richTextBox_show_writing) { };
                                        //等到其他线程解除了写字框的占用
                                        richTextBox_show_writing = true;   //占用之
                                        RichBox_Show rb_s = new RichBox_Show(ShowMsg_inRichTextBox);
                                        string show_string = msg.MsgBody;
                                        this.Invoke(rb_s, new object[] { show_string, Color.Black, HorizontalAlignment.Left });
                                        richTextBox_show_writing = false;  //恢复不被占用
                                        break;
                                    case Message.EType.file:
                                        receive_save r_s = new receive_save(ReceiveFileConnect);
                                        this.Invoke(r_s, new object[] { tcpClient,msg });
                                        while (!filefinsh) { }
                                        filefinsh = false;
                                        break;
                                }
                                //string rcv_msg = Encoding.UTF8.GetString(data, 0, length);
                                ////如果是服务器，则向其他客户端转发该消息
                                //foreach (Socket otherClient in links)
                                //{
                                //    if (otherClient == null) break;
                                //    if (otherClient != tcpClient)
                                //        AsynSend(otherClient, rcv_msg);
                                //}

                                //if (rcv_msg == "<file>")
                                //{
                                //    receive_save r_s = new receive_save(ReceiveFileConnect);
                                //    this.Invoke(r_s, new object[] { tcpClient });
                                //    while (!filefinsh) { }
                                //    filefinsh = false;
                                //}
                                //else if (rcv_msg == "<__cmd__shake__>")
                                //{
                                //    //Shake shake = new Shake(Window_Shake);
                                //    //this.Invoke(shake, new object[] { });
                                //}
                                //else
                                //{
                                //    //如果当前写字框没有被占用
                                //    while (richTextBox_show_writing) { };
                                //    //等到其他线程解除了写字框的占用
                                //    richTextBox_show_writing = true;   //占用之
                                //    RichBox_Show rb_s = new RichBox_Show(ShowMsg_inRichTextBox);
                                //    string show_string = rcv_msg;
                                //    this.Invoke(rb_s, new object[] { show_string, Color.Black, HorizontalAlignment.Left });
                                //    richTextBox_show_writing = false;  //恢复不被占用
                                //}
                                AsynRecive(links);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "出现异常",
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
        }
        #endregion

        #region 异步发送消息
        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="tcpClient">客户端套接字</param>
        /// <param name="message">发送消息</param>
        /// //if_relay表示此信息是否是服务器转发客户端的信息
        public void AsynSend(Socket tcpClient, string message)
        {
            Message chartMessage = new Message()
            {
                MsgBody = message,
                Type = Message.EType.msg,
            };
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


        private void ChatDialog_Load(object sender, EventArgs e)
        {

        }

        private void sendbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string send_msg = sendbox.Text;
                //如果当前写字框没有被占用
                while (richTextBox_show_writing) { };
                //等到其他线程解除了写字框的占用
                richTextBox_show_writing = true;   //占用之
                RichBox_Show rb_s = new RichBox_Show(ShowMsg_inRichTextBox);
                string show_string = DateTime.Now.ToString()
                                            + "\n"+"说：\n" + send_msg + "\n\n";
                this.Invoke(rb_s, new object[] { show_string, Color.Blue, HorizontalAlignment.Right });
                richTextBox_show_writing = false;  //恢复不被占用
                sendbox.Text = "";
                send_msg = DateTime.Now.ToString() + "\n" + userID + "说：\n" + send_msg + "\n\n";

                foreach (Socket Client in sockets)
                {
                    if (Client == null) break;
                    AsynSend(Client, send_msg);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("好友已关闭会话，不能发送信息", "出错啦。。。",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChatDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            canStop = true;
            foreach (Socket Client in sockets)
            {
                if (Client == null) break;
                if (!Client.Connected) continue;
                Client.Shutdown(SocketShutdown.Both);
                Client.Close();
            }

            
        }

        private void test_Click(object sender, EventArgs e)
        {
            Thread readfile = new Thread(() =>
            {
                OpenFileDialog openImageDlg = new OpenFileDialog();
                openImageDlg.Filter = "所有图片(*.bmp,*.gif,*.jpg)|*.bmp;*.gif;*jpg";
                openImageDlg.Title = "选择图片";
                Bitmap bmp;
                if (openImageDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openImageDlg.FileName;
                    if (null == fileName || fileName.Trim().Length == 0)
                        return;
                    try
                    {
                        bmp = new Bitmap(fileName);
                        Clipboard.SetDataObject(bmp);
                        DataFormats.Format dataFormat =
                        DataFormats.GetFormat(DataFormats.Bitmap);
                        Paste p = new Paste(recivebox.InsertImage);
                        recivebox.BeginInvoke(p, new object[] { bmp, 0 });
                        //if (recivebox.CanPaste(dataFormat))
                        //    recivebox.Paste(dataFormat);
                        return;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("图片插入失败。" + exc.Message, "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            });
            readfile.SetApartmentState(System.Threading.ApartmentState.STA);
            readfile.Start();
            readfile.Join();
        }

        private void recivebox_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        #region 同步接收文件
        /// <summary>
        /// 同步接收文件
        /// </summary>
        /// <param name="tcpServer"></param>

        //因为接收消息的操作是异步的，它是线程池中的一个子线程，不是主线程，无法调用SaveFileDialog，
        //故需要在一部接收消息的线程中，用委托来调用同步文件接收程序
        public void ReceiveFileConnect(Socket File_client, Message msg)
        {
            Thread savefile = new Thread(() =>
            {
                DialogResult dr = MessageBox.Show("好友要向你传一个文件，是否接收？", "提示"
                                                , MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    //用户选择确认的操作
                    saveFileDialog1.Title = "请保存文件";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string name = msg.MsgBody;
                        long len = msg.Length;
                        string fileSavePath = saveFileDialog1.FileName;//获得用户保存文件的路径
                        int total = 0;
                        int received;
                        int buffer_size = 1024*1024;
                        byte[] buffer = new byte[buffer_size];
                        fileSavePath = fileSavePath + name;
                        FileStream fs = new FileStream(fileSavePath, FileMode.Create, FileAccess.Write);
                        while (true)
                        {
                            received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                            string string_send = Encoding.UTF8.GetString(buffer, 0, received);
                            foreach (Socket otherClient in sockets)
                            {
                                if (otherClient == null) break;
                                if (otherClient != File_client)
                                    AsynSend(otherClient, string_send);
                            }
                            fs.Write(buffer, 0, received);
                            fs.Flush();
                            total += received;
                            if (total==len)
                            {
                                break;
                            }
                        }
                        fs.Close();
                        MessageBox.Show(fileSavePath + "文件接收完毕", "信息提示");
                    }
                    else
                    {
                        //用户在保存文件对话框中没有选择文件，所以只转发
                        int total = 0;
                        int received;
                        int buffer_size = 1000000;
                        byte[] buffer = new byte[buffer_size];
                        while (true)
                        {
                            received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                            string string_send = Encoding.UTF8.GetString(buffer, 0, received);
                            foreach (Socket otherClient in sockets)
                            {
                                if (otherClient == null) break;
                                if (otherClient != File_client)
                                    AsynSend(otherClient, string_send);
                            }
                            total += received;
                            if (received < buffer_size)
                            {
                                break;
                            }
                        }
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    //用户选择取消的操作，只转发
                    int total = 0;
                    int received;
                    int buffer_size = 1000000;
                    byte[] buffer = new byte[buffer_size];
                    while (true)
                    {
                        received = File_client.Receive(buffer, buffer_size, SocketFlags.None);
                        string string_send = Encoding.UTF8.GetString(buffer, 0, received);
                        foreach (Socket otherClient in sockets)
                        {
                            if (otherClient == null) break;
                            if (otherClient != File_client)
                                AsynSend(otherClient, string_send);
                        }
                        total += received;
                        if (received < buffer_size)
                        {
                            break;
                        }
                    }
                }
                filefinsh = true;
                return;
            });
            savefile.SetApartmentState(ApartmentState.STA);
            savefile.Start();
        }
        #endregion

        private void filebtn_Click(object sender, EventArgs e)
        {
            Thread readfile = new Thread(() =>
            {
                openFileDialog1.Title = "请选择要传输的文件";
                openFileDialog1.Multiselect = false;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String strOpenFileName = openFileDialog1.FileName;//打开的文件的全限定名
                    string post = strOpenFileName.Substring(strOpenFileName.Length - 4);
                    FileInfo fi = new FileInfo(strOpenFileName);

                    Message FileMessage = new Message()
                    {
                        MsgBody = post,
                        Type = Message.EType.file,
                        Length=fi.Length,
                    };
                    byte[] data = SerHelper.Serialize(FileMessage);
                    foreach (Socket Client in sockets)
                    {
                        if (Client == null) break;
                        Client.Send(data);
                        Client.BeginSendFile(strOpenFileName, null, null, TransmitFileOptions.UseDefaultWorkerThread,new AsyncCallback(sendfinish),null);

                        void sendfinish(IAsyncResult iar)
                        {
                            Client.EndSendFile(iar);
                        }
                    }
                    MessageBox.Show(strOpenFileName + "文件传输成功", "信息提示");
                }
                else
                {
                    MessageBox.Show("你没有选择文件", "信息提示");
                }
                return;
            });
            readfile.SetApartmentState(ApartmentState.STA);
            readfile.Start();
        }
    }
}
