namespace Server
{
    partial class Server
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
            this.Connect_btn = new System.Windows.Forms.Button();
            this.Disconnect_btn = new System.Windows.Forms.Button();
            this.MaxUser_tb = new System.Windows.Forms.TextBox();
            this.MaxTable_tb = new System.Windows.Forms.TextBox();
            this.ServerLog_lb = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(695, 69);
            this.Connect_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(100, 28);
            this.Connect_btn.TabIndex = 0;
            this.Connect_btn.Text = "Connect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // Disconnect_btn
            // 
            this.Disconnect_btn.Location = new System.Drawing.Point(695, 104);
            this.Disconnect_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Disconnect_btn.Name = "Disconnect_btn";
            this.Disconnect_btn.Size = new System.Drawing.Size(100, 28);
            this.Disconnect_btn.TabIndex = 1;
            this.Disconnect_btn.Text = "Disconnet";
            this.Disconnect_btn.UseVisualStyleBackColor = true;
            this.Disconnect_btn.Click += new System.EventHandler(this.Disconnect_btn_Click);
            // 
            // MaxUser_tb
            // 
            this.MaxUser_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxUser_tb.Location = new System.Drawing.Point(649, 7);
            this.MaxUser_tb.Margin = new System.Windows.Forms.Padding(4);
            this.MaxUser_tb.Name = "MaxUser_tb";
            this.MaxUser_tb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaxUser_tb.Size = new System.Drawing.Size(146, 22);
            this.MaxUser_tb.TabIndex = 2;
            this.MaxUser_tb.Text = "4";
            // 
            // MaxTable_tb
            // 
            this.MaxTable_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxTable_tb.Location = new System.Drawing.Point(649, 39);
            this.MaxTable_tb.Margin = new System.Windows.Forms.Padding(4);
            this.MaxTable_tb.Name = "MaxTable_tb";
            this.MaxTable_tb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaxTable_tb.Size = new System.Drawing.Size(146, 22);
            this.MaxTable_tb.TabIndex = 3;
            this.MaxTable_tb.Text = "3";
            // 
            // ServerLog_lb
            // 
            this.ServerLog_lb.FormattingEnabled = true;
            this.ServerLog_lb.ItemHeight = 16;
            this.ServerLog_lb.Location = new System.Drawing.Point(13, 9);
            this.ServerLog_lb.Margin = new System.Windows.Forms.Padding(4);
            this.ServerLog_lb.Name = "ServerLog_lb";
            this.ServerLog_lb.Size = new System.Drawing.Size(408, 340);
            this.ServerLog_lb.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(429, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Số người chơi tối đa (1-300):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Số phòng chơi tối đa (1-300):";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(432, 325);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 364);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerLog_lb);
            this.Controls.Add(this.MaxTable_tb);
            this.Controls.Add(this.MaxUser_tb);
            this.Controls.Add(this.Disconnect_btn);
            this.Controls.Add(this.Connect_btn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Button Disconnect_btn;
        private System.Windows.Forms.TextBox MaxUser_tb;
        private System.Windows.Forms.TextBox MaxTable_tb;
        private System.Windows.Forms.ListBox ServerLog_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
    }
}

