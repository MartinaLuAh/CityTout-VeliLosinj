namespace StadtfuehrungVeliLosinj
{
    partial class RanglisteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RanglisteForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewRangliste = new System.Windows.Forms.DataGridView();
            this.buttonSchließen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRangliste)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewRangliste
            // 
            this.dataGridViewRangliste.AllowUserToAddRows = false;
            this.dataGridViewRangliste.AllowUserToDeleteRows = false;
            this.dataGridViewRangliste.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRangliste.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRangliste.Location = new System.Drawing.Point(8, 8);
            this.dataGridViewRangliste.Name = "dataGridViewRangliste";
            this.dataGridViewRangliste.ReadOnly = true;
            this.dataGridViewRangliste.RowHeadersWidth = 51;
            this.dataGridViewRangliste.RowTemplate.Height = 24;
            this.dataGridViewRangliste.Size = new System.Drawing.Size(507, 416);
            this.dataGridViewRangliste.TabIndex = 1;
            // 
            // buttonSchließen
            // 
            this.buttonSchließen.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonSchließen.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSchließen.Location = new System.Drawing.Point(567, 387);
            this.buttonSchließen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSchließen.Name = "buttonSchließen";
            this.buttonSchließen.Size = new System.Drawing.Size(196, 37);
            this.buttonSchließen.TabIndex = 6;
            this.buttonSchließen.UseVisualStyleBackColor = false;
            this.buttonSchließen.Click += new System.EventHandler(this.buttonSchließen_Click);
            // 
            // RanglisteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSchließen);
            this.Controls.Add(this.dataGridViewRangliste);
            this.Controls.Add(this.pictureBox1);
            this.Name = "RanglisteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRangliste)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridViewRangliste;
        private System.Windows.Forms.Button buttonSchließen;
    }
}