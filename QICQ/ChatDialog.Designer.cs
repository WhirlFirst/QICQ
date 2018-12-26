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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatDialog));
            this.sendbtn = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.recivebox = new CCWin.SkinControl.SkinChatRichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolFace = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.sendbox = new CCWin.SkinControl.SkinChatRichTextBox();
            this.filebtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendbtn
            // 
            this.sendbtn.FlatAppearance.BorderSize = 0;
            this.sendbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendbtn.Location = new System.Drawing.Point(598, 405);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(89, 38);
            this.sendbtn.TabIndex = 2;
            this.sendbtn.Text = "发送消息";
            this.sendbtn.UseVisualStyleBackColor = true;
            this.sendbtn.Click += new System.EventHandler(this.sendbtn_Click);
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(598, 268);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 3;
            this.test.Text = "button1";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
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
            this.recivebox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.recivebox_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFace,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(7, 265);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(535, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolFace
            // 
            this.toolFace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFace.Image = ((System.Drawing.Image)(resources.GetObject("toolFace.Image")));
            this.toolFace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFace.Name = "toolFace";
            this.toolFace.Size = new System.Drawing.Size(23, 22);
            this.toolFace.Text = "表情";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // sendbox
            // 
            this.sendbox.Location = new System.Drawing.Point(7, 293);
            this.sendbox.Name = "sendbox";
            this.sendbox.SelectControl = null;
            this.sendbox.SelectControlIndex = 0;
            this.sendbox.SelectControlPoint = new System.Drawing.Point(0, 0);
            this.sendbox.Size = new System.Drawing.Size(680, 106);
            this.sendbox.TabIndex = 15;
            this.sendbox.Text = "";
            // 
            // filebtn
            // 
            this.filebtn.FlatAppearance.BorderSize = 0;
            this.filebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filebtn.Location = new System.Drawing.Point(503, 405);
            this.filebtn.Name = "filebtn";
            this.filebtn.Size = new System.Drawing.Size(89, 38);
            this.filebtn.TabIndex = 16;
            this.filebtn.Text = "文件传输";
            this.filebtn.UseVisualStyleBackColor = true;
            this.filebtn.Click += new System.EventHandler(this.filebtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ChatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 450);
            this.Controls.Add(this.filebtn);
            this.Controls.Add(this.sendbox);
            this.Controls.Add(this.test);
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
        private System.Windows.Forms.Button test;
        private CCWin.SkinControl.SkinChatRichTextBox recivebox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolFace;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private CCWin.SkinControl.SkinChatRichTextBox sendbox;
        private System.Windows.Forms.Button filebtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}