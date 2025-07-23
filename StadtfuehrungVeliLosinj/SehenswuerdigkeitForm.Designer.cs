namespace StadtfuehrungVeliLosinj
{
    partial class SehenswuerdigkeitForm
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
            this.richTextBoxSehenswuerdigkeitBeschreibung = new System.Windows.Forms.RichTextBox();
            this.pictureBoxSehenswuerdigkeiteBild = new System.Windows.Forms.PictureBox();
            this.buttonSchließen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSehenswuerdigkeiteBild)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxSehenswuerdigkeitBeschreibung
            // 
            this.richTextBoxSehenswuerdigkeitBeschreibung.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.richTextBoxSehenswuerdigkeitBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSehenswuerdigkeitBeschreibung.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.richTextBoxSehenswuerdigkeitBeschreibung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSehenswuerdigkeitBeschreibung.ForeColor = System.Drawing.Color.DarkBlue;
            this.richTextBoxSehenswuerdigkeitBeschreibung.Location = new System.Drawing.Point(352, 0);
            this.richTextBoxSehenswuerdigkeitBeschreibung.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBoxSehenswuerdigkeitBeschreibung.Name = "richTextBoxSehenswuerdigkeitBeschreibung";
            this.richTextBoxSehenswuerdigkeitBeschreibung.ReadOnly = true;
            this.richTextBoxSehenswuerdigkeitBeschreibung.Size = new System.Drawing.Size(377, 479);
            this.richTextBoxSehenswuerdigkeitBeschreibung.TabIndex = 0;
            this.richTextBoxSehenswuerdigkeitBeschreibung.TabStop = false;
            this.richTextBoxSehenswuerdigkeitBeschreibung.Text = "";
            this.richTextBoxSehenswuerdigkeitBeschreibung.UseWaitCursor = true;
            // 
            // pictureBoxSehenswuerdigkeiteBild
            // 
            this.pictureBoxSehenswuerdigkeiteBild.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pictureBoxSehenswuerdigkeiteBild.Location = new System.Drawing.Point(1, 0);
            this.pictureBoxSehenswuerdigkeiteBild.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxSehenswuerdigkeiteBild.Name = "pictureBoxSehenswuerdigkeiteBild";
            this.pictureBoxSehenswuerdigkeiteBild.Size = new System.Drawing.Size(347, 479);
            this.pictureBoxSehenswuerdigkeiteBild.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSehenswuerdigkeiteBild.TabIndex = 1;
            this.pictureBoxSehenswuerdigkeiteBild.TabStop = false;
            // 
            // buttonSchließen
            // 
            this.buttonSchließen.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonSchließen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSchließen.ForeColor = System.Drawing.SystemColors.Window;
            this.buttonSchließen.Location = new System.Drawing.Point(244, 440);
            this.buttonSchließen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSchließen.Name = "buttonSchließen";
            this.buttonSchließen.Size = new System.Drawing.Size(92, 28);
            this.buttonSchließen.TabIndex = 3;
            this.buttonSchließen.UseVisualStyleBackColor = false;
            this.buttonSchließen.Click += new System.EventHandler(this.buttonSchließen_Click);
            // 
            // SehenswuerdigkeitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 477);
            this.Controls.Add(this.buttonSchließen);
            this.Controls.Add(this.pictureBoxSehenswuerdigkeiteBild);
            this.Controls.Add(this.richTextBoxSehenswuerdigkeitBeschreibung);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SehenswuerdigkeitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSehenswuerdigkeiteBild)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxSehenswuerdigkeitBeschreibung;
        private System.Windows.Forms.PictureBox pictureBoxSehenswuerdigkeiteBild;
        private System.Windows.Forms.Button buttonSchließen;
    }
}