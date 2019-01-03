namespace QICQ
{
    partial class ChatDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatDialog));
            this.sendbtn = new System.Windows.Forms.Button();
            this.recivebox = new CCWin.SkinControl.SkinChatRichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findlabel = new System.Windows.Forms.ToolStripLabel();
            this.findtxt = new System.Windows.Forms.ToolStripTextBox();
            this.findbtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.shakebtn = new System.Windows.Forms.ToolStripButton();
            this.sendbox = new CCWin.SkinControl.SkinChatRichTextBox();
            this.filebtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fileBar = new CCWin.SkinControl.SkinProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendbtn
            // 
            this.sendbtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.sendbtn.FlatAppearance.BorderSize = 0;
            this.sendbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendbtn.Location = new System.Drawing.Point(598, 421);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(89, 22);
            this.sendbtn.TabIndex = 2;
            this.sendbtn.Text = "发送消息";
            this.sendbtn.UseVisualStyleBackColor = true;
            this.sendbtn.Click += new System.EventHandler(this.sendbtn_Click);
            // 
            // recivebox
            // 
            this.recivebox.Location = new System.Drawing.Point(7, 37);
            this.recivebox.Name = "recivebox";
            this.recivebox.SelectControl = null;
            this.recivebox.SelectControlIndex = 0;
            this.recivebox.SelectControlPoint = new System.Drawing.Point(0, 0);
            this.recivebox.Size = new System.Drawing.Size(680, 225);
            this.recivebox.TabIndex = 4;
            this.recivebox.Text = "";
            this.recivebox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.recivebox_KeyDown);
            this.recivebox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.recivebox_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.findlabel,
            this.findtxt,
            this.findbtn,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.shakebtn});
            this.toolStrip1.Location = new System.Drawing.Point(7, 265);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // findlabel
            // 
            this.findlabel.Name = "findlabel";
            this.findlabel.Size = new System.Drawing.Size(92, 22);
            this.findlabel.Text = "查找聊天内容：";
            // 
            // findtxt
            // 
            this.findtxt.Name = "findtxt";
            this.findtxt.Size = new System.Drawing.Size(100, 25);
            this.findtxt.TextChanged += new System.EventHandler(this.findtxt_TextChanged);
            // 
            // findbtn
            // 
            this.findbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findbtn.Image = global::QICQ.Properties.Resources.search;
            this.findbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findbtn.Name = "findbtn";
            this.findbtn.Size = new System.Drawing.Size(23, 22);
            this.findbtn.Text = "toolStripButton2";
            this.findbtn.Click += new System.EventHandler(this.findbtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::QICQ.Properties.Resources.yy;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "录制语音消息";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::QICQ.Properties.Resources.play;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "播放语音消息";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // shakebtn
            // 
            this.shakebtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shakebtn.Image = global::QICQ.Properties.Resources.dd;
            this.shakebtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shakebtn.Name = "shakebtn";
            this.shakebtn.Size = new System.Drawing.Size(23, 22);
            this.shakebtn.Text = "toolStripButton3";
            this.shakebtn.ToolTipText = "窗口抖动";
            this.shakebtn.Click += new System.EventHandler(this.shakebtn_Click);
            // 
            // sendbox
            // 
            this.sendbox.Location = new System.Drawing.Point(7, 293);
            this.sendbox.Name = "sendbox";
            this.sendbox.SelectControl = null;
            this.sendbox.SelectControlIndex = 0;
            this.sendbox.SelectControlPoint = new System.Drawing.Point(0, 0);
            this.sendbox.Size = new System.Drawing.Size(680, 122);
            this.sendbox.TabIndex = 15;
            this.sendbox.Text = "";
            this.sendbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendbox_KeyDown);
            this.sendbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sendbox_KeyPress);
            // 
            // filebtn
            // 
            this.filebtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.filebtn.FlatAppearance.BorderSize = 0;
            this.filebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filebtn.Location = new System.Drawing.Point(503, 421);
            this.filebtn.Name = "filebtn";
            this.filebtn.Size = new System.Drawing.Size(89, 22);
            this.filebtn.TabIndex = 16;
            this.filebtn.Text = "文件传输";
            this.filebtn.UseVisualStyleBackColor = true;
            this.filebtn.Click += new System.EventHandler(this.filebtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileBar
            // 
            this.fileBar.Back = null;
            this.fileBar.BackColor = System.Drawing.Color.Transparent;
            this.fileBar.BarBack = null;
            this.fileBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.fileBar.ForeColor = System.Drawing.Color.Red;
            this.fileBar.Location = new System.Drawing.Point(8, 421);
            this.fileBar.Name = "fileBar";
            this.fileBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.fileBar.Size = new System.Drawing.Size(489, 22);
            this.fileBar.TabIndex = 17;
            this.fileBar.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 450);
            this.Controls.Add(this.fileBar);
            this.Controls.Add(this.filebtn);
            this.Controls.Add(this.sendbox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.recivebox);
            this.Controls.Add(this.sendbtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatDialog";
            this.Text = "ChatDialog";
            this.TitleOffset = new System.Drawing.Point(30, 0);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatDialog_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button sendbtn;
        private CCWin.SkinControl.SkinChatRichTextBox recivebox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private CCWin.SkinControl.SkinChatRichTextBox sendbox;
        private System.Windows.Forms.Button filebtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private CCWin.SkinControl.SkinProgressBar fileBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox findtxt;
        private System.Windows.Forms.ToolStripButton findbtn;
        private System.Windows.Forms.ToolStripLabel findlabel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton shakebtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}