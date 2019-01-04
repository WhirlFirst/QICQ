namespace QICQ
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.lgbtn = new System.Windows.Forms.Button();
            this.usertext = new System.Windows.Forms.Label();
            this.pwdtext = new System.Windows.Forms.Label();
            this.username = new CCWin.SkinControl.SkinTextBox();
            this.pwd = new CCWin.SkinControl.SkinTextBox();
            this.pd = new CCWin.SkinControl.SkinLabel();
            this.gif = new CCWin.SkinControl.GifBox();
            this.ing = new CCWin.SkinControl.SkinLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.closebtn = new System.Windows.Forms.Button();
            this.QICQ = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lgbtn
            // 
            this.lgbtn.BackColor = System.Drawing.Color.White;
            this.lgbtn.CausesValidation = false;
            this.lgbtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.lgbtn.FlatAppearance.BorderSize = 0;
            this.lgbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.lgbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.lgbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lgbtn.Font = new System.Drawing.Font("宋体", 12F);
            this.lgbtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lgbtn.Location = new System.Drawing.Point(167, 233);
            this.lgbtn.Name = "lgbtn";
            this.lgbtn.Size = new System.Drawing.Size(92, 45);
            this.lgbtn.TabIndex = 0;
            this.lgbtn.Text = "登录";
            this.lgbtn.UseVisualStyleBackColor = false;
            this.lgbtn.Click += new System.EventHandler(this.button1_Click);
            this.lgbtn.MouseEnter += new System.EventHandler(this.lgbtn_MouseEnter);
            this.lgbtn.MouseLeave += new System.EventHandler(this.lgbtn_MouseLeave);
            // 
            // usertext
            // 
            this.usertext.AutoSize = true;
            this.usertext.BackColor = System.Drawing.Color.Transparent;
            this.usertext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usertext.ForeColor = System.Drawing.Color.White;
            this.usertext.Location = new System.Drawing.Point(58, 163);
            this.usertext.Name = "usertext";
            this.usertext.Size = new System.Drawing.Size(68, 16);
            this.usertext.TabIndex = 1;
            this.usertext.Text = "用户名:";
            // 
            // pwdtext
            // 
            this.pwdtext.AutoSize = true;
            this.pwdtext.BackColor = System.Drawing.Color.Transparent;
            this.pwdtext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pwdtext.ForeColor = System.Drawing.Color.White;
            this.pwdtext.Location = new System.Drawing.Point(74, 191);
            this.pwdtext.Name = "pwdtext";
            this.pwdtext.Size = new System.Drawing.Size(51, 16);
            this.pwdtext.TabIndex = 2;
            this.pwdtext.Text = "密码:";
            // 
            // username
            // 
            this.username.BackColor = System.Drawing.Color.White;
            this.username.DownBack = null;
            this.username.Icon = null;
            this.username.IconIsButton = false;
            this.username.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.username.IsPasswordChat = '\0';
            this.username.IsSystemPasswordChar = false;
            this.username.Lines = new string[] {
        "2016011493"};
            this.username.Location = new System.Drawing.Point(125, 151);
            this.username.Margin = new System.Windows.Forms.Padding(0);
            this.username.MaxLength = 32767;
            this.username.MinimumSize = new System.Drawing.Size(28, 28);
            this.username.MouseBack = null;
            this.username.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.username.Multiline = false;
            this.username.Name = "username";
            this.username.NormlBack = null;
            this.username.Padding = new System.Windows.Forms.Padding(5);
            this.username.ReadOnly = false;
            this.username.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.username.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.username.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.username.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.username.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.username.SkinTxt.Name = "BaseText";
            this.username.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.username.SkinTxt.TabIndex = 0;
            this.username.SkinTxt.Text = "2016011493";
            this.username.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.username.SkinTxt.WaterText = "";
            this.username.TabIndex = 3;
            this.username.TabStop = true;
            this.username.Text = "2016011493";
            this.username.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.username.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.username.WaterText = "";
            this.username.WordWrap = true;
            // 
            // pwd
            // 
            this.pwd.BackColor = System.Drawing.Color.White;
            this.pwd.DownBack = null;
            this.pwd.Icon = null;
            this.pwd.IconIsButton = false;
            this.pwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.pwd.IsPasswordChat = '*';
            this.pwd.IsSystemPasswordChar = false;
            this.pwd.Lines = new string[] {
        "net2018"};
            this.pwd.Location = new System.Drawing.Point(125, 179);
            this.pwd.Margin = new System.Windows.Forms.Padding(0);
            this.pwd.MaxLength = 32767;
            this.pwd.MinimumSize = new System.Drawing.Size(28, 28);
            this.pwd.MouseBack = null;
            this.pwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.pwd.Multiline = false;
            this.pwd.Name = "pwd";
            this.pwd.NormlBack = null;
            this.pwd.Padding = new System.Windows.Forms.Padding(5);
            this.pwd.ReadOnly = false;
            this.pwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pwd.Size = new System.Drawing.Size(185, 28);
            // 
            // 
            // 
            this.pwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.pwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.pwd.SkinTxt.Name = "BaseText";
            this.pwd.SkinTxt.PasswordChar = '*';
            this.pwd.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.pwd.SkinTxt.TabIndex = 0;
            this.pwd.SkinTxt.Text = "net2018";
            this.pwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.pwd.SkinTxt.WaterText = "";
            this.pwd.TabIndex = 4;
            this.pwd.TabStop = true;
            this.pwd.Text = "net2018";
            this.pwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.pwd.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.pwd.WaterText = "";
            this.pwd.WordWrap = true;
            // 
            // pd
            // 
            this.pd.AutoSize = true;
            this.pd.BackColor = System.Drawing.Color.Transparent;
            this.pd.BorderColor = System.Drawing.Color.White;
            this.pd.Font = new System.Drawing.Font("Gabriola", 12F);
            this.pd.Location = new System.Drawing.Point(296, 260);
            this.pd.Name = "pd";
            this.pd.Size = new System.Drawing.Size(113, 29);
            this.pd.TabIndex = 5;
            this.pd.Text = "Producted By Whirl";
            // 
            // gif
            // 
            this.gif.BorderColor = System.Drawing.Color.Transparent;
            this.gif.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gif.Image = global::QICQ.Properties.Resources.tenor;
            this.gif.Location = new System.Drawing.Point(181, 210);
            this.gif.Name = "gif";
            this.gif.Size = new System.Drawing.Size(58, 58);
            this.gif.TabIndex = 6;
            this.gif.Text = "gifBox1";
            this.gif.Visible = false;
            // 
            // ing
            // 
            this.ing.AutoSize = true;
            this.ing.BackColor = System.Drawing.Color.Transparent;
            this.ing.BorderColor = System.Drawing.Color.White;
            this.ing.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ing.Location = new System.Drawing.Point(164, 264);
            this.ing.Name = "ing";
            this.ing.Size = new System.Drawing.Size(92, 17);
            this.ing.TabIndex = 7;
            this.ing.Text = "登录中，请稍候";
            this.ing.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(416, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson;
            this.closebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.closebtn.Location = new System.Drawing.Point(381, 1);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(35, 27);
            this.closebtn.TabIndex = 9;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // QICQ
            // 
            this.QICQ.AutoSize = true;
            this.QICQ.BackColor = System.Drawing.Color.Transparent;
            this.QICQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QICQ.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.QICQ.ForeColor = System.Drawing.Color.White;
            this.QICQ.Location = new System.Drawing.Point(138, 57);
            this.QICQ.Name = "QICQ";
            this.QICQ.Size = new System.Drawing.Size(160, 64);
            this.QICQ.TabIndex = 10;
            this.QICQ.Text = "QICQ";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 293);
            this.Controls.Add(this.QICQ);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.username);
            this.Controls.Add(this.ing);
            this.Controls.Add(this.pd);
            this.Controls.Add(this.pwd);
            this.Controls.Add(this.pwdtext);
            this.Controls.Add(this.usertext);
            this.Controls.Add(this.lgbtn);
            this.Controls.Add(this.gif);
            this.Controls.Add(this.pictureBox1);
            this.EffectBack = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.TitleOffset = new System.Drawing.Point(38, 0);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lgbtn;
        private System.Windows.Forms.Label usertext;
        private System.Windows.Forms.Label pwdtext;
        private CCWin.SkinControl.SkinTextBox username;
        private CCWin.SkinControl.SkinTextBox pwd;
        private CCWin.SkinControl.SkinLabel pd;
        private CCWin.SkinControl.GifBox gif;
        private CCWin.SkinControl.SkinLabel ing;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label QICQ;
    }
}

