namespace QICQ
{
    partial class ChatInfo
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TItle = new System.Windows.Forms.Label();
            this.openbtn = new System.Windows.Forms.Button();
            this.delbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TItle
            // 
            this.TItle.AutoEllipsis = true;
            this.TItle.AutoSize = true;
            this.TItle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TItle.Location = new System.Drawing.Point(3, 9);
            this.TItle.Name = "TItle";
            this.TItle.Size = new System.Drawing.Size(92, 17);
            this.TItle.TabIndex = 0;
            this.TItle.Text = "与。。。的聊天";
            // 
            // openbtn
            // 
            this.openbtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.openbtn.FlatAppearance.BorderSize = 0;
            this.openbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openbtn.Location = new System.Drawing.Point(0, 48);
            this.openbtn.Name = "openbtn";
            this.openbtn.Size = new System.Drawing.Size(113, 26);
            this.openbtn.TabIndex = 1;
            this.openbtn.Text = "打开";
            this.openbtn.UseVisualStyleBackColor = true;
            this.openbtn.Click += new System.EventHandler(this.openbtn_Click);
            // 
            // delbtn
            // 
            this.delbtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delbtn.FlatAppearance.BorderSize = 0;
            this.delbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delbtn.Location = new System.Drawing.Point(119, 48);
            this.delbtn.Name = "delbtn";
            this.delbtn.Size = new System.Drawing.Size(113, 26);
            this.delbtn.TabIndex = 2;
            this.delbtn.Text = "删除";
            this.delbtn.UseVisualStyleBackColor = true;
            this.delbtn.Click += new System.EventHandler(this.delbtn_Click);
            this.delbtn.MouseEnter += new System.EventHandler(this.delbtn_MouseEnter);
            this.delbtn.MouseLeave += new System.EventHandler(this.delbtn_MouseLeave);
            // 
            // ChatInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.delbtn);
            this.Controls.Add(this.openbtn);
            this.Controls.Add(this.TItle);
            this.Name = "ChatInfo";
            this.Size = new System.Drawing.Size(235, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TItle;
        private System.Windows.Forms.Button openbtn;
        private System.Windows.Forms.Button delbtn;
    }
}
