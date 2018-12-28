using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QICQ
{
    public partial class ChatInfo : UserControl
    {
        public ChatInfo()
        {
            InitializeComponent();
        }
        public ChatInfo(string member)
        {
            InitializeComponent();
            string name = member.Substring(0, member.Length - 4);
            name = name.Replace('_', '，');
            TItle.Text = "与"+name+"的聊天";
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

        }
    }
}
