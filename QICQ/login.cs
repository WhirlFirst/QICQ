using CCWin;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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
            Main main = new Main(Username, client, this);
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

        /// <summary>
        /// 利用gif作为登录背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region 登录按钮响应
        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            username.Enabled = false;
            pwd.Enabled = false;
            gif.Show();
            ing.Show();
        }
        #endregion

        #region 后台线程连接
        /// <summary>
        /// 另开线程进行连接
        /// </summary>
        private void SleepT()
        {
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
                this.BeginInvoke(new Entrust(CallBack));
            }
            else
            {
                MessageBox.Show("账号或密码错误", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }
        #endregion

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
            if (fThread != null) fThread.Abort();

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
