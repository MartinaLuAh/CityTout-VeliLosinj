using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using StadtfuehrungVeliLosinj;

namespace StadtfuehrungVeliLosinj
{
    public partial class StartSeiteForm : Form
    {
        
        public StartSeiteForm()
        {
            //Initialisierung der Komponenten
            InitializeComponent();

            try
            {
                //ComboBox für Sprachauswahl leeren und Sprachen hinzufügen
                comboBoxSprache.Items.Clear();
                comboBoxSprache.Items.AddRange(new string[] { "Deutsch", "Hrvatski", "Italiano", "English" });
           
                //Aktuelle Kultur ermitteln und Index in der ComboBox setzen
                string _aktuellerKultur = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                comboBoxSprache.SelectedIndex = KulturIndex(_aktuellerKultur);

                //Texte auf der Form aktualisieren
                AktualisiereTexte();
            }
            catch (Exception ex)
            {     
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerSpracheLaden + "\n" + ex.Message);
            }
           
        }


        //Gibt den Index der Sprache in der ComboBox anhand des Kurzcode zurück
        private int KulturIndex(string _kultur)
        {
            
            switch (_kultur)
            {
                case "de":
                    return 0;
                case "hr":
                    return 1;
                case "it":
                    return 2;
                case "en":
                    return 3;
                default:
                    return 0;
            }
        }

        private void comboBoxSprache_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                //Ausgewählte Sprache aus der ComboBox ermitteln
                string _ausgewaehlteSprache = Sprache();

                //Kultur des aktuellen Threads setzen
                Kultur(_ausgewaehlteSprache);

                //Texte auf der Form aktualisieren
                AktualisiereTexte();
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerSpracheAenderung + "\n" + ex.Message);
            }
        }
        
        
        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache
        private void AktualisiereTexte()
        {
            try
            {
                this.Text = StadtfuehrungVeliLosinj.Messages.WillkommenText;
                this.labelStartTitel.Text = StadtfuehrungVeliLosinj.Messages.StartSeiteTitel;
                this.labelSprache.Text = StadtfuehrungVeliLosinj.Messages.SpracheLabelText;
                this.comboBoxSprache.Text = StadtfuehrungVeliLosinj.Messages.SpracheComboBox;
                this.buttonAnmelden.Text = StadtfuehrungVeliLosinj.Messages.AnmeldenButtonText;
                this.buttonRegistrieren.Text = StadtfuehrungVeliLosinj.Messages.RegistrierenButtonText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }


        }

        // Event Handler für den Button "Registrieren"
        private void buttonRegistrieren_Click(object sender, EventArgs e)
        {
            //Ausgewählte Sprache aus der ComboBox ermitteln
            string _ausgewaehlteSprache = Sprache();

            //Kultur des aktuellen Threads setzen
            Kultur(_ausgewaehlteSprache);

            try
            {
                //Erstellt ein neues Benutzerobjekt mit der ausgewählten Sprache
                Benutzer neuerBenutzer = new Benutzer("", "", "", _ausgewaehlteSprache, 0);

                //Registrierungsformular öffnen
                RegistrierungForm registrierung = new RegistrierungForm(neuerBenutzer);
                this.Hide();
                registrierung.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerRegistrierungsFormOeffnen + "\n" + ex.Message);
            }
            
        }

        // Event Handler für den Button "Anmelden"
        private void buttonAnmelden_Click(object sender, EventArgs e)
        {
            //Ausgewählte Sprache aus der ComboBox ermitteln
            string _ausgewaehlteSprache = Sprache();

            //Kultur des aktuellen Threads setzen
            Kultur(_ausgewaehlteSprache);

            try
            {
                //Erstellt ein neues Benutzerobjekt mit der ausgewählten Sprache
                Benutzer neuerBenutzer = new Benutzer("", "", "", _ausgewaehlteSprache, 0);

                //Anmeldeformular öffnen
                AnmeldeForm anmelden = new AnmeldeForm(neuerBenutzer);
                this.Hide();
                anmelden.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerAnmeldeFormOeffnen + "\n" + ex.Message);
            }
        }

        //Gibt den Kurzcode der aktuell Sprache aus der ComboBox zurück
        private string Sprache()
        {
            switch (comboBoxSprache.SelectedIndex)
            {
                case 0:
                    return "de";
                case 1:
                    return "hr";
                case 2:
                    return "it";
                case 3:
                    return "en";
                default:
                    return "de";
            }
        }

        //Kultur des aktuellen Threads setzen
        private void Kultur(string _ausgewaehlteSprache)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(_ausgewaehlteSprache);
            }
            catch(CultureNotFoundException ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerUngueltigeKultur + ex.Message);
            }

        }

    }
}
