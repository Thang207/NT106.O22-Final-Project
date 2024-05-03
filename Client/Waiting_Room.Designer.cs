namespace Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Waiting_Room));
            this.UserName_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Connect_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textbox_tableindex = new System.Windows.Forms.TextBox();
            this.button_find = new System.Windows.Forms.Button();
            this.label_tableindex = new System.Windows.Forms.Label();
            this.button_play = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserName_tb
            // 
            this.UserName_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserName_tb.Location = new System.Drawing.Point(427, 12);
            this.UserName_tb.Name = "UserName_tb";
            this.UserName_tb.Size = new System.Drawing.Size(99, 20);
            this.UserName_tb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(346, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "UserName";
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(427, 38);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(100, 34);
            this.Connect_btn.TabIndex = 7;
            this.Connect_btn.Text = "Connect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 265);
            this.panel1.TabIndex = 8;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(8, 403);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(8, 4);
            this.listBox1.TabIndex = 3;
            // 
            // textbox_tableindex
            // 
            this.textbox_tableindex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textbox_tableindex.Location = new System.Drawing.Point(426, 115);
            this.textbox_tableindex.Margin = new System.Windows.Forms.Padding(2);
            this.textbox_tableindex.Name = "textbox_tableindex";
            this.textbox_tableindex.Size = new System.Drawing.Size(100, 20);
            this.textbox_tableindex.TabIndex = 9;
            // 
            // button_find
            // 
            this.button_find.Location = new System.Drawing.Point(502, 138);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(24, 29);
            this.button_find.TabIndex = 10;
            this.button_find.Text = "🔎";
            this.button_find.UseVisualStyleBackColor = true;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // label_tableindex
            // 
            this.label_tableindex.AutoSize = true;
            this.label_tableindex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tableindex.Location = new System.Drawing.Point(346, 118);
            this.label_tableindex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_tableindex.Name = "label_tableindex";
            this.label_tableindex.Size = new System.Drawing.Size(59, 17);
            this.label_tableindex.TabIndex = 11;
            this.label_tableindex.Text = "Tìm bàn";
            // 
            // button_play
            // 
            this.button_play.Location = new System.Drawing.Point(427, 77);
            this.button_play.Margin = new System.Windows.Forms.Padding(2);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(100, 34);
            this.button_play.TabIndex = 12;
            this.button_play.Text = "Chơi ngay";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.button_play_Click);
            // 
            // Waiting_Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(538, 289);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_play);
            this.Controls.Add(this.label_tableindex);
            this.Controls.Add(this.button_find);
            this.Controls.Add(this.textbox_tableindex);
            this.Controls.Add(this.Connect_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserName_tb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Waiting_Room";
            this.Text = "Client Tetris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox UserName_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textbox_tableindex;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.Label label_tableindex;
        private System.Windows.Forms.Button button_play;
    }
}

