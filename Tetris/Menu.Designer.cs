namespace Tetris
{
    partial class MainMenu
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
            this.btnSolo = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbPlayer = new System.Windows.Forms.GroupBox();
            this.gbPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSolo
            // 
            this.btnSolo.Location = new System.Drawing.Point(13, 13);
            this.btnSolo.Margin = new System.Windows.Forms.Padding(4);
            this.btnSolo.Name = "btnSolo";
            this.btnSolo.Size = new System.Drawing.Size(172, 75);
            this.btnSolo.TabIndex = 0;
            this.btnSolo.Text = "Solo";
            this.btnSolo.UseVisualStyleBackColor = true;
            this.btnSolo.Click += new System.EventHandler(this.btnSolo_Click);
            // 
            // btnMulti
            // 
            this.btnMulti.Location = new System.Drawing.Point(13, 96);
            this.btnMulti.Margin = new System.Windows.Forms.Padding(4);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(172, 75);
            this.btnMulti.TabIndex = 1;
            this.btnMulti.Text = "LAN";
            this.btnMulti.UseVisualStyleBackColor = true;
            this.btnMulti.Click += new System.EventHandler(this.btnMulti_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(193, 13);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(172, 75);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtRoom
            // 
            this.txtRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoom.Location = new System.Drawing.Point(56, 55);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(100, 22);
            this.txtRoom.TabIndex = 3;
            this.txtRoom.Text = "1234";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(56, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 22);
            this.txtName.TabIndex = 3;
            this.txtName.Text = "Wuoc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Room";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name";
            // 
            // gbPlayer
            // 
            this.gbPlayer.Controls.Add(this.txtRoom);
            this.gbPlayer.Controls.Add(this.label2);
            this.gbPlayer.Controls.Add(this.txtName);
            this.gbPlayer.Controls.Add(this.label3);
            this.gbPlayer.Location = new System.Drawing.Point(192, 96);
            this.gbPlayer.Name = "gbPlayer";
            this.gbPlayer.Size = new System.Drawing.Size(200, 100);
            this.gbPlayer.TabIndex = 6;
            this.gbPlayer.TabStop = false;
            this.gbPlayer.Text = "Player Info";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 208);
            this.Controls.Add(this.gbPlayer);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMulti);
            this.Controls.Add(this.btnSolo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.gbPlayer.ResumeLayout(false);
            this.gbPlayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSolo;
        private System.Windows.Forms.Button btnMulti;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbPlayer;
    }
}