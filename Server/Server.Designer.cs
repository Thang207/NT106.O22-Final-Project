﻿namespace Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.btnOpenServer = new System.Windows.Forms.Button();
            this.btnCloseSever = new System.Windows.Forms.Button();
            this.MaxUser_tb = new System.Windows.Forms.TextBox();
            this.MaxTable_tb = new System.Windows.Forms.TextBox();
            this.ServerLog_lb = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenServer
            // 
            this.btnOpenServer.Location = new System.Drawing.Point(460, 58);
            this.btnOpenServer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOpenServer.Name = "btnOpenServer";
            this.btnOpenServer.Size = new System.Drawing.Size(74, 23);
            this.btnOpenServer.TabIndex = 0;
            this.btnOpenServer.Text = "Open";
            this.btnOpenServer.UseVisualStyleBackColor = true;
            this.btnOpenServer.Click += new System.EventHandler(this.btnOpenServer_Click);
            // 
            // btnCloseSever
            // 
            this.btnCloseSever.Enabled = false;
            this.btnCloseSever.Location = new System.Drawing.Point(460, 86);
            this.btnCloseSever.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCloseSever.Name = "btnCloseSever";
            this.btnCloseSever.Size = new System.Drawing.Size(74, 23);
            this.btnCloseSever.TabIndex = 1;
            this.btnCloseSever.Text = "Close";
            this.btnCloseSever.UseVisualStyleBackColor = true;
            this.btnCloseSever.Click += new System.EventHandler(this.btnCloseServer_Click);
            // 
            // MaxUser_tb
            // 
            this.MaxUser_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxUser_tb.Location = new System.Drawing.Point(487, 6);
            this.MaxUser_tb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaxUser_tb.Name = "MaxUser_tb";
            this.MaxUser_tb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaxUser_tb.Size = new System.Drawing.Size(48, 20);
            this.MaxUser_tb.TabIndex = 2;
            this.MaxUser_tb.Text = "4";
            // 
            // MaxTable_tb
            // 
            this.MaxTable_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxTable_tb.Location = new System.Drawing.Point(487, 32);
            this.MaxTable_tb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaxTable_tb.Name = "MaxTable_tb";
            this.MaxTable_tb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaxTable_tb.Size = new System.Drawing.Size(48, 20);
            this.MaxTable_tb.TabIndex = 3;
            this.MaxTable_tb.Text = "3";
            // 
            // ServerLog_lb
            // 
            this.ServerLog_lb.FormattingEnabled = true;
            this.ServerLog_lb.HorizontalScrollbar = true;
            this.ServerLog_lb.Location = new System.Drawing.Point(10, 7);
            this.ServerLog_lb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ServerLog_lb.Name = "ServerLog_lb";
            this.ServerLog_lb.Size = new System.Drawing.Size(386, 277);
            this.ServerLog_lb.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(401, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Players (1-300):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Rooms (1-150):";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Emoji", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(400, 219);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 65);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "🗑️";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 296);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerLog_lb);
            this.Controls.Add(this.MaxTable_tb);
            this.Controls.Add(this.MaxUser_tb);
            this.Controls.Add(this.btnCloseSever);
            this.Controls.Add(this.btnOpenServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenServer;
        private System.Windows.Forms.Button btnCloseSever;
        private System.Windows.Forms.TextBox MaxUser_tb;
        private System.Windows.Forms.TextBox MaxTable_tb;
        private System.Windows.Forms.ListBox ServerLog_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
    }
}

