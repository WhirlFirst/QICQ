using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CCWin;
using System.Net;
using System.Net.Sockets;
using System.IO;//命名空间
namespace QICQ
{
    public partial class Login : Skin_DevExpress
    {
        public delegate void Entrust();
        IPAddress Server = IPAddress.Parse("166.111.140.14");
        int port = 8000;
        string Username;
        string Pwd;
        Socket client;
        Thread fThread;
        public Login()
        {
            InitializeComponent();
        }


        private void CallBack()
        {
            Main main = new Main(Username, client,this);
            main.Show();
            this.Hide();
        }

        private void BackNormal()
        {
            lgbtn.Show();
            gif.Hide();
            ing.Hide();
            username.Enabled = true;
            pwd.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lgbtn.Show();
            username.Enabled = true;
            pwd.Enabled = true;
            gif.Hide();
            ing.Hide();
            usertext.Parent = pictureBox1;
            Point p = new Point(58, 156);
            usertext.Location = p;
            pwdtext.Parent = pictureBox1;
            Point pp = new Point(74, 186);
            pwdtext.Location = pp;
            closebtn.Parent = pictureBox1;
            Point cp = new Point(381, 0);
            closebtn.Location = cp;
            username.Parent = pictureBox1;
            pwd.Parent = pictureBox1;
            QICQ.Parent = pictureBox1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username = username.Text.ToString();
            Pwd = pwd.Text.ToString();
            //检查用户名和密码
            if (Username == "")
            {
                MessageBox.Show("请输入用户名", "登陆错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (Pwd == "")
            {
                MessageBox.Show("请输入密码", "登陆错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
            lgbtn.Hide();
            username.Enabled=false;
            pwd.Enabled = false;
            gif.Show();
            ing.Show();
        }

        private void SleepT()
        {
            //尝试用同步套接字连接
            IPEndPoint ip_port = new IPEndPoint(Server, port);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                client.Connect(ip_port);
            }
            catch (SocketException)
            {
                MessageBox.Show("连接失败，请检查网络后重新连接", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.BeginInvoke(new Entrust(BackNormal));
                return;
            }
            string login_mess = Username + "_" + Pwd;
            byte[] login_byte = new byte[1024];
            login_byte = Encoding.ASCII.GetBytes(login_mess);
            try
            {
                client.Send(login_byte, login_byte.Length, 0);
            }
            catch (Exception)
            {
                MessageBox.Show("连接超时，请重新连接", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            byte[] receive_byte = new byte[1024];
            int number = client.Receive(receive_byte, receive_byte.Length, 0);

            string receive_mess = Encoding.Default.GetString(receive_byte, 0, number);
            if (receive_mess == "lol")
            {
                MessageBox.Show("登录成功");
                this.BeginInvoke(new Entrust(CallBack));
            }
            else
            {
                MessageBox.Show("账号或密码错误", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void lgbtn_MouseEnter(object sender, EventArgs e)
        {
            lgbtn.BackColor = Color.DodgerBlue;
            lgbtn.ForeColor = Color.White;
        }

        private void lgbtn_MouseLeave(object sender, EventArgs e)
        {
            lgbtn.BackColor = Color.White;
            lgbtn.ForeColor = Color.Black;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(fThread!=null) fThread.Abort();
            
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closebtn_MouseEnter(object sender, EventArgs e)
        {
            closebtn.ForeColor = Color.Red;
        }

        private void closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.ForeColor = Color.White;
        }
    }
}
