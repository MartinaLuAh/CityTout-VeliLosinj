namespace StadtfuehrungVeliLosinj
{
    partial class AnmeldeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnmeldeForm));
            this.labelBenutzername = new System.Windows.Forms.Label();
            this.labelPasswort = new System.Windows.Forms.Label();
            this.textBoxBenutzername = new System.Windows.Forms.TextBox();
            this.textBoxPasswort = new System.Windows.Forms.TextBox();
            this.buttonAnmelden = new System.Windows.Forms.Button();
            this.buttonAbbrechen = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonPasswortZurückSetzen = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBenutzername
            // 
            this.labelBenutzername.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labelBenutzername.Location = new System.Drawing.Point(12, 28);
            this.labelBenutzername.Name = "labelBenutzername";
            this.labelBenutzername.Size = new System.Drawing.Size(124, 23);
            this.labelBenutzername.TabIndex = 5;
            // 
            // labelPasswort
            // 
            this.labelPasswort.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labelPasswort.Location = new System.Drawing.Point(12, 72);
            this.labelPasswort.Name = "labelPasswort";
            this.labelPasswort.Size = new System.Drawing.Size(124, 23);
            this.labelPasswort.TabIndex = 4;
            // 
            // textBoxBenutzername
            // 
            this.textBoxBenutzername.Location = new System.Drawing.Point(162, 29);
            this.textBoxBenutzername.Name = "textBoxBenutzername";
            this.textBoxBenutzername.Size = new System.Drawing.Size(169, 22);
            this.textBoxBenutzername.TabIndex = 3;
            // 
            // textBoxPasswort
            // 
            this.textBoxPasswort.Location = new System.Drawing.Point(162, 72);
            this.textBoxPasswort.Name = "textBoxPasswort";
            this.textBoxPasswort.Size = new System.Drawing.Size(169, 22);
            this.textBoxPasswort.TabIndex = 2;
            this.textBoxPasswort.UseSystemPasswordChar = true;
            // 
            // buttonAnmelden
            // 
            this.buttonAnmelden.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonAnmelden.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAnmelden.Location = new System.Drawing.Point(12, 161);
            this.buttonAnmelden.Name = "buttonAnmelden";
            this.buttonAnmelden.Size = new System.Drawing.Size(107, 29);
            this.buttonAnmelden.TabIndex = 1;
            this.buttonAnmelden.UseVisualStyleBackColor = false;
            this.buttonAnmelden.Click += new System.EventHandler(this.buttonAnmelden_Click);
            // 
            // buttonAbbrechen
            // 
            this.buttonAbbrechen.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonAbbrechen.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAbbrechen.Location = new System.Drawing.Point(12, 210);
            this.buttonAbbrechen.Name = "buttonAbbrechen";
            this.buttonAbbrechen.Size = new System.Drawing.Size(107, 31);
            this.buttonAbbrechen.TabIndex = 0;
            this.buttonAbbrechen.UseVisualStyleBackColor = false;
            this.buttonAbbrechen.Click += new System.EventHandler(this.buttonAbbrechen_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(412, 253);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // buttonPasswortZurückSetzen
            // 
            this.buttonPasswortZurückSetzen.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonPasswortZurückSetzen.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonPasswortZurückSetzen.Location = new System.Drawing.Point(149, 161);
            this.buttonPasswortZurückSetzen.Name = "buttonPasswortZurückSetzen";
            this.buttonPasswortZurückSetzen.Size = new System.Drawing.Size(170, 29);
            this.buttonPasswortZurückSetzen.TabIndex = 7;
            this.buttonPasswortZurückSetzen.UseVisualStyleBackColor = false;
            this.buttonPasswortZurückSetzen.Click += new System.EventHandler(this.buttonPasswortZurückSetzen_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(348, 73);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // AnmeldeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 253);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonPasswortZurückSetzen);
            this.Controls.Add(this.buttonAbbrechen);
            this.Controls.Add(this.buttonAnmelden);
            this.Controls.Add(this.textBoxPasswort);
            this.Controls.Add(this.textBoxBenutzername);
            this.Controls.Add(this.labelPasswort);
            this.Controls.Add(this.labelBenutzername);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AnmeldeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelBenutzername;
        private System.Windows.Forms.Label labelPasswort;
        private System.Windows.Forms.TextBox textBoxBenutzername;
        private System.Windows.Forms.TextBox textBoxPasswort;
        private System.Windows.Forms.Button buttonAnmelden;
        private System.Windows.Forms.Button buttonAbbrechen;
        private System.Windows.Forms.Button buttonPasswortZurückSetzen;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}