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
            this.btnConnect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textbox_tableindex = new System.Windows.Forms.TextBox();
            this.label_tableindex = new System.Windows.Forms.Label();
            this.btnQuickPlay = new System.Windows.Forms.Button();
            this.lbUserName = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(458, 218);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 34);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(21, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 291);
            this.panel1.TabIndex = 8;
            // 
            // textbox_tableindex
            // 
            this.textbox_tableindex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textbox_tableindex.Location = new System.Drawing.Point(458, 257);
            this.textbox_tableindex.Margin = new System.Windows.Forms.Padding(2);
            this.textbox_tableindex.Name = "textbox_tableindex";
            this.textbox_tableindex.Size = new System.Drawing.Size(100, 20);
            this.textbox_tableindex.TabIndex = 9;
            // 
            // label_tableindex
            // 
            this.label_tableindex.AutoSize = true;
            this.label_tableindex.BackColor = System.Drawing.Color.Transparent;
            this.label_tableindex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tableindex.ForeColor = System.Drawing.Color.White;
            this.label_tableindex.Location = new System.Drawing.Point(499, 349);
            this.label_tableindex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_tableindex.Name = "label_tableindex";
            this.label_tableindex.Size = new System.Drawing.Size(59, 17);
            this.label_tableindex.TabIndex = 11;
            this.label_tableindex.Text = "Tìm bàn";
            // 
            // btnQuickPlay
            // 
            this.btnQuickPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnQuickPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickPlay.FlatAppearance.BorderSize = 0;
            this.btnQuickPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnQuickPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnQuickPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickPlay.ForeColor = System.Drawing.Color.Transparent;
            this.btnQuickPlay.Location = new System.Drawing.Point(70, 35);
            this.btnQuickPlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuickPlay.Name = "btnQuickPlay";
            this.btnQuickPlay.Size = new System.Drawing.Size(164, 38);
            this.btnQuickPlay.TabIndex = 12;
            this.btnQuickPlay.UseVisualStyleBackColor = false;
            this.btnQuickPlay.Click += new System.EventHandler(this.btnQuickPlay_Click);
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.BackColor = System.Drawing.Color.Transparent;
            this.lbUserName.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserName.ForeColor = System.Drawing.Color.White;
            this.lbUserName.Location = new System.Drawing.Point(69, 8);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(126, 24);
            this.lbUserName.TabIndex = 3;
            this.lbUserName.Text = "WELCOME, ";
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.ForeColor = System.Drawing.Color.Transparent;
            this.btnReturn.Location = new System.Drawing.Point(267, 35);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(162, 38);
            this.btnReturn.TabIndex = 0;
            this.btnReturn.TabStop = false;
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // Waiting_Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Client.Properties.Resources.WaitingRoom;
            this.ClientSize = new System.Drawing.Size(445, 399);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnQuickPlay);
            this.Controls.Add(this.label_tableindex);
            this.Controls.Add(this.textbox_tableindex);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lbUserName);
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
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textbox_tableindex;
        private System.Windows.Forms.Label label_tableindex;
        private System.Windows.Forms.Button btnQuickPlay;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Button btnReturn;
    }
}

