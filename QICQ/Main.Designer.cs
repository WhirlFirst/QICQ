namespace QICQ
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.holo = new CCWin.SkinControl.SkinLabel();
            this.searchtext = new CCWin.SkinControl.SkinTextBox();
            this.searchbtn = new System.Windows.Forms.Button();
            this.delid = new System.Windows.Forms.Button();
            this.helpbtn = new System.Windows.Forms.Button();
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage1 = new CCWin.SkinControl.SkinTabPage();
            this.userlist = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.state = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.skinTabControl1.SuspendLayout();
            this.skinTabPage1.SuspendLayout();
            this.skinTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // holo
            // 
            this.holo.AutoSize = true;
            this.holo.BackColor = System.Drawing.Color.Transparent;
            this.holo.BorderColor = System.Drawing.Color.White;
            this.holo.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.holo.Location = new System.Drawing.Point(7, 39);
            this.holo.Name = "holo";
            this.holo.Size = new System.Drawing.Size(54, 20);
            this.holo.TabIndex = 0;
            this.holo.Text = "你好！";
            // 
            // searchtext
            // 
            this.searchtext.BackColor = System.Drawing.Color.Transparent;
            this.searchtext.DownBack = null;
            this.searchtext.Icon = null;
            this.searchtext.IconIsButton = false;
            this.searchtext.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.searchtext.IsPasswordChat = '\0';
            this.searchtext.IsSystemPasswordChar = false;
            this.searchtext.Lines = new string[0];
            this.searchtext.Location = new System.Drawing.Point(7, 69);
            this.searchtext.Margin = new System.Windows.Forms.Padding(0);
            this.searchtext.MaxLength = 32767;
            this.searchtext.MinimumSize = new System.Drawing.Size(28, 28);
            this.searchtext.MouseBack = null;
            this.searchtext.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.searchtext.Multiline = false;
            this.searchtext.Name = "searchtext";
            this.searchtext.NormlBack = null;
            this.searchtext.Padding = new System.Windows.Forms.Padding(5);
            this.searchtext.ReadOnly = false;
            this.searchtext.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.searchtext.Size = new System.Drawing.Size(223, 28);
            // 
            // 
            // 
            this.searchtext.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchtext.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchtext.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.searchtext.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.searchtext.SkinTxt.Name = "BaseText";
            this.searchtext.SkinTxt.Size = new System.Drawing.Size(213, 18);
            this.searchtext.SkinTxt.TabIndex = 0;
            this.searchtext.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.searchtext.SkinTxt.WaterText = "";
            this.searchtext.TabIndex = 2;
            this.searchtext.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.searchtext.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.searchtext.WaterText = "";
            this.searchtext.WordWrap = true;
            // 
            // searchbtn
            // 
            this.searchbtn.BackgroundImage = global::QICQ.Properties.Resources.search;
            this.searchbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.searchbtn.FlatAppearance.BorderSize = 0;
            this.searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchbtn.Location = new System.Drawing.Point(233, 68);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(28, 28);
            this.searchbtn.TabIndex = 3;
            this.searchbtn.UseVisualStyleBackColor = true;
            this.searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // delid
            // 
            this.delid.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delid.FlatAppearance.BorderSize = 0;
            this.delid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delid.Location = new System.Drawing.Point(10, 577);
            this.delid.Name = "delid";
            this.delid.Size = new System.Drawing.Size(113, 28);
            this.delid.TabIndex = 4;
            this.delid.Text = "恩断义绝";
            this.delid.UseVisualStyleBackColor = true;
            this.delid.Click += new System.EventHandler(this.Delid_Click);
            this.delid.MouseEnter += new System.EventHandler(this.delid_MouseEnter);
            this.delid.MouseLeave += new System.EventHandler(this.delid_MouseLeave);
            // 
            // helpbtn
            // 
            this.helpbtn.FlatAppearance.BorderSize = 0;
            this.helpbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpbtn.Location = new System.Drawing.Point(194, 39);
            this.helpbtn.Name = "helpbtn";
            this.helpbtn.Size = new System.Drawing.Size(75, 23);
            this.helpbtn.TabIndex = 5;
            this.helpbtn.Text = "帮助";
            this.helpbtn.UseVisualStyleBackColor = true;
            this.helpbtn.Click += new System.EventHandler(this.helpbtn_Click);
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.AnimationStart = true;
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizBlind;
            this.skinTabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.skinTabPage1);
            this.skinTabControl1.Controls.Add(this.skinTabPage2);
            this.skinTabControl1.HeadBack = null;
            this.skinTabControl1.HotTrack = true;
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(125, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(7, 103);
            this.skinTabControl1.Multiline = true;
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageHoverTxtColor = System.Drawing.Color.LightGray;
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.PageNormlTxtColor = System.Drawing.Color.DimGray;
            this.skinTabControl1.SelectedIndex = 1;
            this.skinTabControl1.Size = new System.Drawing.Size(262, 468);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 7;
            // 
            // skinTabPage1
            // 
            this.skinTabPage1.BackColor = System.Drawing.Color.White;
            this.skinTabPage1.Controls.Add(this.userlist);
            this.skinTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage1.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage1.Name = "skinTabPage1";
            this.skinTabPage1.Size = new System.Drawing.Size(262, 432);
            this.skinTabPage1.TabIndex = 0;
            this.skinTabPage1.TabItemImage = null;
            this.skinTabPage1.Text = "好友列表";
            // 
            // userlist
            // 
            this.userlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.ip,
            this.state});
            this.userlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userlist.FullRowSelect = true;
            this.userlist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.userlist.Location = new System.Drawing.Point(0, 0);
            this.userlist.Name = "userlist";
            this.userlist.Size = new System.Drawing.Size(262, 432);
            this.userlist.TabIndex = 3;
            this.userlist.UseCompatibleStateImageBehavior = false;
            this.userlist.View = System.Windows.Forms.View.Details;
            this.userlist.DoubleClick += new System.EventHandler(this.Userlist_DoubleClick);
            this.userlist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.userlist_MouseClick);
            // 
            // id
            // 
            this.id.Text = "学号";
            this.id.Width = 80;
            // 
            // ip
            // 
            this.ip.Text = "IP地址";
            this.ip.Width = 115;
            // 
            // state
            // 
            this.state.Text = "状态";
            // 
            // skinTabPage2
            // 
            this.skinTabPage2.BackColor = System.Drawing.Color.White;
            this.skinTabPage2.Controls.Add(this.flowLayoutPanel1);
            this.skinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage2.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage2.Name = "skinTabPage2";
            this.skinTabPage2.Size = new System.Drawing.Size(262, 432);
            this.skinTabPage2.TabIndex = 1;
            this.skinTabPage2.TabItemImage = null;
            this.skinTabPage2.Text = "消息列表";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(262, 432);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(129, 577);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "群聊";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 622);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.skinTabControl1);
            this.Controls.Add(this.helpbtn);
            this.Controls.Add(this.delid);
            this.Controls.Add(this.searchbtn);
            this.Controls.Add(this.searchtext);
            this.Controls.Add(this.holo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QICQ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.skinTabControl1.ResumeLayout(false);
            this.skinTabPage1.ResumeLayout(false);
            this.skinTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel holo;
        private CCWin.SkinControl.SkinTextBox searchtext;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.Button delid;
        private System.Windows.Forms.Button helpbtn;
        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinTabPage skinTabPage1;
        private CCWin.SkinControl.SkinTabPage skinTabPage2;
        private System.Windows.Forms.ListView userlist;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader ip;
        private System.Windows.Forms.ColumnHeader state;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}