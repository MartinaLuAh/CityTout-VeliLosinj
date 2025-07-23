using System.Windows.Forms;

namespace StadtfuehrungVeliLosinj
{
     public partial class StartSeiteForm : Form
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartSeiteForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxSprache = new System.Windows.Forms.ComboBox();
            this.labelStartTitel = new System.Windows.Forms.Label();
            this.labelSprache = new System.Windows.Forms.Label();
            this.buttonAnmelden = new System.Windows.Forms.Button();
            this.buttonRegistrieren = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxSprache
            // 
            resources.ApplyResources(this.comboBoxSprache, "comboBoxSprache");
            this.comboBoxSprache.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.comboBoxSprache.FormattingEnabled = true;
            this.comboBoxSprache.Name = "comboBoxSprache";
            this.comboBoxSprache.SelectedIndexChanged += new System.EventHandler(this.comboBoxSprache_SelectedIndexChanged);
            // 
            // labelStartTitel
            // 
            resources.ApplyResources(this.labelStartTitel, "labelStartTitel");
            this.labelStartTitel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelStartTitel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStartTitel.Name = "labelStartTitel";
            // 
            // labelSprache
            // 
            this.labelSprache.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.labelSprache, "labelSprache");
            this.labelSprache.Name = "labelSprache";
            // 
            // buttonAnmelden
            // 
            this.buttonAnmelden.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.buttonAnmelden, "buttonAnmelden");
            this.buttonAnmelden.Name = "buttonAnmelden";
            this.buttonAnmelden.UseVisualStyleBackColor = false;
            this.buttonAnmelden.Click += new System.EventHandler(this.buttonAnmelden_Click);
            // 
            // buttonRegistrieren
            // 
            this.buttonRegistrieren.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.buttonRegistrieren, "buttonRegistrieren");
            this.buttonRegistrieren.Name = "buttonRegistrieren";
            this.buttonRegistrieren.UseVisualStyleBackColor = false;
            this.buttonRegistrieren.Click += new System.EventHandler(this.buttonRegistrieren_Click);
            // 
            // StartSeiteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRegistrieren);
            this.Controls.Add(this.buttonAnmelden);
            this.Controls.Add(this.labelSprache);
            this.Controls.Add(this.labelStartTitel);
            this.Controls.Add(this.comboBoxSprache);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartSeiteForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxSprache;
        private System.Windows.Forms.Label labelStartTitel;
        private System.Windows.Forms.Label labelSprache;
        private System.Windows.Forms.Button buttonAnmelden;
        private System.Windows.Forms.Button buttonRegistrieren;
    }
}

