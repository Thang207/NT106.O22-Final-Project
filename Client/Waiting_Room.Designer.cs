﻿namespace Client
{
    partial class Waiting_Room
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
            this.Local_tb = new System.Windows.Forms.TextBox();
            this.Server_tb = new System.Windows.Forms.TextBox();
            this.UserName_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Connect_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Local_tb
            // 
            this.Local_tb.Location = new System.Drawing.Point(362, 12);
            this.Local_tb.Name = "Local_tb";
            this.Local_tb.Size = new System.Drawing.Size(100, 20);
            this.Local_tb.TabIndex = 0;
            // 
            // Server_tb
            // 
            this.Server_tb.Location = new System.Drawing.Point(362, 38);
            this.Server_tb.Name = "Server_tb";
            this.Server_tb.Size = new System.Drawing.Size(100, 20);
            this.Server_tb.TabIndex = 1;
            // 
            // UserName_tb
            // 
            this.UserName_tb.Location = new System.Drawing.Point(120, 12);
            this.UserName_tb.Name = "UserName_tb";
            this.UserName_tb.Size = new System.Drawing.Size(100, 20);
            this.UserName_tb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "UserName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Local";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Server";
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(387, 73);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(75, 23);
            this.Connect_btn.TabIndex = 7;
            this.Connect_btn.Text = "Connect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(39, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 252);
            this.panel1.TabIndex = 8;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Connect_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserName_tb);
            this.Controls.Add(this.Server_tb);
            this.Controls.Add(this.Local_tb);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(8, 403);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(8, 4);
            this.listBox1.TabIndex = 3;
        }

        #endregion

        private System.Windows.Forms.TextBox Local_tb;
        private System.Windows.Forms.TextBox Server_tb;
        private System.Windows.Forms.TextBox UserName_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
    }
}
