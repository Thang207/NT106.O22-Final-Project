namespace Client
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.btnSolo = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSolo
            // 
            this.btnSolo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSolo.BackColor = System.Drawing.Color.Transparent;
            this.btnSolo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSolo.FlatAppearance.BorderSize = 0;
            this.btnSolo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSolo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSolo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolo.ForeColor = System.Drawing.Color.Transparent;
            this.btnSolo.Location = new System.Drawing.Point(128, 496);
            this.btnSolo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSolo.Name = "btnSolo";
            this.btnSolo.Size = new System.Drawing.Size(281, 63);
            this.btnSolo.TabIndex = 0;
            this.btnSolo.TabStop = false;
            this.btnSolo.UseVisualStyleBackColor = false;
            this.btnSolo.Click += new System.EventHandler(this.btnSolo_Click);
            // 
            // btnMulti
            // 
            this.btnMulti.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMulti.BackColor = System.Drawing.Color.Transparent;
            this.btnMulti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMulti.FlatAppearance.BorderSize = 0;
            this.btnMulti.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMulti.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMulti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMulti.ForeColor = System.Drawing.Color.Transparent;
            this.btnMulti.Location = new System.Drawing.Point(128, 588);
            this.btnMulti.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(281, 63);
            this.btnMulti.TabIndex = 0;
            this.btnMulti.TabStop = false;
            this.btnMulti.UseVisualStyleBackColor = false;
            this.btnMulti.Click += new System.EventHandler(this.btnMulti_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Bold);
            this.txtUserName.Location = new System.Drawing.Point(179, 432);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(179, 55);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "WUOC";
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(131, 372);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 59);
            this.label1.TabIndex = 2;
            this.label1.Text = "USERNAME";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Client.Properties.Resources.Menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(533, 759);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnMulti);
            this.Controls.Add(this.btnSolo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "Tetris Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSolo;
        private System.Windows.Forms.Button btnMulti;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
    }
}