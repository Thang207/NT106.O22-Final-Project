﻿namespace Tetris
{
    partial class TetrisRoom
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
            this.components = new System.ComponentModel.Container();
            this.gb_p1 = new System.Windows.Forms.GroupBox();
            this.pn_p1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gb_p2 = new System.Windows.Forms.GroupBox();
            this.pn_p2 = new System.Windows.Forms.Panel();
            this.announcementTimer = new System.Windows.Forms.Timer(this.components);
            this.announcementLabel = new System.Windows.Forms.Label();
            this.gb_p1.SuspendLayout();
            this.pn_p1.SuspendLayout();
            this.gb_p2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_p1
            // 
            this.gb_p1.Controls.Add(this.pn_p1);
            this.gb_p1.Controls.Add(this.panel1);
            this.gb_p1.Location = new System.Drawing.Point(12, 12);
            this.gb_p1.Name = "gb_p1";
            this.gb_p1.Size = new System.Drawing.Size(272, 329);
            this.gb_p1.TabIndex = 0;
            this.gb_p1.TabStop = false;
            this.gb_p1.Text = "Player 1";
            // 
            // pn_p1
            // 
            this.pn_p1.Controls.Add(this.announcementLabel);
            this.pn_p1.Location = new System.Drawing.Point(6, 21);
            this.pn_p1.Name = "pn_p1";
            this.pn_p1.Size = new System.Drawing.Size(260, 302);
            this.pn_p1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(6, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 302);
            this.panel1.TabIndex = 0;
            // 
            // gb_p2
            // 
            this.gb_p2.Controls.Add(this.pn_p2);
            this.gb_p2.Location = new System.Drawing.Point(398, 12);
            this.gb_p2.Name = "gb_p2";
            this.gb_p2.Size = new System.Drawing.Size(272, 329);
            this.gb_p2.TabIndex = 0;
            this.gb_p2.TabStop = false;
            this.gb_p2.Text = "Player 2";
            // 
            // pn_p2
            // 
            this.pn_p2.Location = new System.Drawing.Point(6, 21);
            this.pn_p2.Name = "pn_p2";
            this.pn_p2.Size = new System.Drawing.Size(260, 302);
            this.pn_p2.TabIndex = 0;
            // 
            // announcementTimer
            // 
            this.announcementTimer.Interval = 2000;
            this.announcementTimer.Tick += new System.EventHandler(this.announcement_Tick);
            // 
            // announcementLabel
            // 
            this.announcementLabel.AutoSize = true;
            this.announcementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.announcementLabel.ForeColor = System.Drawing.Color.Teal;
            this.announcementLabel.Location = new System.Drawing.Point(-14, 32);
            this.announcementLabel.Name = "announcementLabel";
            this.announcementLabel.Size = new System.Drawing.Size(688, 79);
            this.announcementLabel.TabIndex = 1;
            this.announcementLabel.Text = "announcement Label";
            this.announcementLabel.Visible = false;
            // 
            // TetrisRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 352);
            this.Controls.Add(this.gb_p2);
            this.Controls.Add(this.gb_p1);
            this.Name = "TetrisRoom";
            this.Text = "MultiPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TetrisRoom_FormClosing);
            this.gb_p1.ResumeLayout(false);
            this.pn_p1.ResumeLayout(false);
            this.pn_p1.PerformLayout();
            this.gb_p2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_p1;
        private System.Windows.Forms.GroupBox gb_p2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pn_p1;
        private System.Windows.Forms.Panel pn_p2;
        private System.Windows.Forms.Timer announcementTimer;
        private System.Windows.Forms.Label announcementLabel;
    }
}