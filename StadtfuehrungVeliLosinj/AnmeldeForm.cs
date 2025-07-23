using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using StadtfuehrungVeliLosinj.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Threading;
using System.Configuration;


namespace StadtfuehrungVeliLosinj
{
    public partial class AnmeldeForm : Form
    {
        //Verbindung zur Datenbank über App.config
        string _connectionString = ConfigurationManager.ConnectionStrings["MySqlVerbindung"].ConnectionString;

        //Benutzerobjekt
        private Benutzer _Benutzer;

        public AnmeldeForm(Benutzer _benutzer)
        {
            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Kulturinfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);

            //Initialisierung der Komponenten
            InitializeComponent();

            //Texte auf der Form aktualisieren
            AktualisiereTexte();
        }

        // Event Handler für den Button "Abbrechen"
        private void buttonAbbrechen_Click(object sender, EventArgs e)
        {
            // Zurück zur Startseite
            StartSeiteForm start = new StartSeiteForm();
            start.Show();
            this.Close();
        }


        // Event Handler für den Button "Anmelden"
        private void buttonAnmelden_Click(object sender, EventArgs e)
        {
            //Holt die eingegebenen Benutzerdaten  aus den Textboxen
            string _benutzername = textBoxBenutzername.Text.Trim();
            string _passwort = textBoxPasswort.Text.Trim();
     
           // Überprüft ob Felder leer sind
           if (_benutzername.Length == 0 || _passwort.Length == 0)
           {
               MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.AlleFelderAusfüllen, 1000);
               return;
           }

            
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    // SQL Abfrage zum Überprüfen der Zugangsdaten
                    string getUserQuery = "SELECT COUNT(*) FROM benutzer WHERE BINARY benutzername = @name AND BINARY passwort = @passwort";
                    MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, conn);
                    getUserCmd.Parameters.AddWithValue("@name", _benutzername);
                    getUserCmd.Parameters.AddWithValue("@passwort", _passwort);
                    object _result = getUserCmd.ExecuteScalar();
                    int _count = Convert.ToInt32(_result);
                  
                  
                    //Prüft, ob Benutzer existiert
                    if (_count > 0)
                    {
                        //Punkte des Benutzers abfragen
                        string punkteQuery = "SELECT punkte FROM benutzer WHERE BINARY benutzername = @name";
                        MySqlCommand punkteCmd = new MySqlCommand(punkteQuery, conn);
                        punkteCmd.Parameters.AddWithValue("@name", _benutzername);
                        object _resultPunkte = punkteCmd.ExecuteScalar();
                        int punkte = Convert.ToInt32(_resultPunkte);

                        //Erstellt ein neues Benutzerobjekt mit Punkten
                        _Benutzer = new Benutzer(_benutzername, _passwort, "", Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName, punkte);                        
                        
                        MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.AnmeldungErfolgreich, 1000);
                        
                        //Hauptformular öffnen
                        HauptForm haupt = new HauptForm(_Benutzer);
                        this.Close();
                        haupt.Show();         
                    }
                    else
                    {
                        MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.AnmeldungFehlgeschlagen, 1000);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
                }
            }
        }
        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache
        private void AktualisiereTexte()
        {
            try
            {
                this.Text = StadtfuehrungVeliLosinj.Messages.AnmeldungTitel;
                this.labelBenutzername.Text = StadtfuehrungVeliLosinj.Messages.BenutzernameLabel;
                this.labelPasswort.Text = StadtfuehrungVeliLosinj.Messages.PasswortLabel;
                this.buttonAnmelden.Text = StadtfuehrungVeliLosinj.Messages.AnmeldenButtonText;
                this.buttonAbbrechen.Text = StadtfuehrungVeliLosinj.Messages.AbbrechenButtonText;
                this.buttonPasswortZurückSetzen.Text = StadtfuehrungVeliLosinj.Messages.buttonPasswortZurueckSetzen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }

        }

        // Event Handler für den Button "Passwort Zurücksetzen"
        private void buttonPasswortZurückSetzen_Click(object sender, EventArgs e)
        {
            PasswortZurueckSetzenForm passzurueck = new PasswortZurueckSetzenForm(_Benutzer);
            passzurueck.ShowDialog();
            this.Close();
        }

        //Passwort ein/ausblenden
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxPasswort.UseSystemPasswordChar = !textBoxPasswort.UseSystemPasswordChar;
        }
    }
}
