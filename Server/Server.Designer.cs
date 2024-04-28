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
            this.SuspendLayout();
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(562, 107);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(75, 23);
            this.Connect_btn.TabIndex = 0;
            this.Connect_btn.Text = "Connect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // Disconnect_btn
            // 
            this.Disconnect_btn.Location = new System.Drawing.Point(562, 136);
            this.Disconnect_btn.Name = "Disconnect_btn";
            this.Disconnect_btn.Size = new System.Drawing.Size(75, 23);
            this.Disconnect_btn.TabIndex = 1;
            this.Disconnect_btn.Text = "Disconnet";
            this.Disconnect_btn.UseVisualStyleBackColor = true;
            this.Disconnect_btn.Click += new System.EventHandler(this.Disconnect_btn_Click);
            // 
            // MaxUser_tb
            // 
            this.MaxUser_tb.Location = new System.Drawing.Point(495, 55);
            this.MaxUser_tb.Name = "MaxUser_tb";
            this.MaxUser_tb.Size = new System.Drawing.Size(142, 20);
            this.MaxUser_tb.TabIndex = 2;
            // 
            // MaxTable_tb
            // 
            this.MaxTable_tb.Location = new System.Drawing.Point(495, 81);
            this.MaxTable_tb.Name = "MaxTable_tb";
            this.MaxTable_tb.Size = new System.Drawing.Size(142, 20);
            this.MaxTable_tb.TabIndex = 3;
            // 
            // ServerLog_lb
            // 
            this.ServerLog_lb.FormattingEnabled = true;
            this.ServerLog_lb.Location = new System.Drawing.Point(12, 59);
            this.ServerLog_lb.Name = "ServerLog_lb";
            this.ServerLog_lb.Size = new System.Drawing.Size(305, 225);
            this.ServerLog_lb.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Số người chơi tối đa (1-300):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Số phòng chơi tối đa (1-300):";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 296);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerLog_lb);
            this.Controls.Add(this.MaxTable_tb);
            this.Controls.Add(this.MaxUser_tb);
            this.Controls.Add(this.Disconnect_btn);
            this.Controls.Add(this.Connect_btn);
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
    }
}

