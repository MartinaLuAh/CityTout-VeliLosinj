using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace StadtfuehrungVeliLosinj
{
    public partial class RegistrierungForm : Form
    {
        //Verbindung zur Datenbank über App.config
        string _connectionString = ConfigurationManager.ConnectionStrings["MySqlVerbindung"].ConnectionString;

        //Benutzerobjekt
        private Benutzer _Benutzer;
        public RegistrierungForm(Benutzer _benutzer)
        {
            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Kulturnfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);

            //Initialisierung der Komponenten
            InitializeComponent();

            //Textänderung Events für Validierung registrieren
            this.textBoxEmail.TextChanged += textBoxEmail_TextChanged;
            this.textBoxPasswort.TextChanged += textBoxPasswort_TextChanged;

            //Texte auf der Form aktualisieren
            AktualisiereTexte();
        }

        //Validiert das E-mail Format
        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            string _email = textBoxEmail.Text.Trim();
            string _pattern = @"^[a-zA-Z0-9.+-_]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(_email, _pattern)) 
            {
                textBoxEmail.BackColor = Color.MistyRose;
                labelEmailNachricht.Text = StadtfuehrungVeliLosinj.Messages.EmailNachricht;
                labelEmailNachricht.Visible = true;
            }
            else
            {
                textBoxEmail.BackColor = Color.White;
                labelEmailNachricht.Visible = false;
            }

        }

        //Validiert das Passwortformat
        private void textBoxPasswort_TextChanged(object sender, EventArgs e)   
        {
            string _passwort = textBoxPasswort.Text.Trim();

            if (_passwort.Length < 8 || !Regex.IsMatch(_passwort, @"[a-zA-Z]") || !Regex.IsMatch(_passwort, @"[!@#/$%&\*]"))
            {
                textBoxPasswort.BackColor = Color.MistyRose;
                labelPasswortNachricht.Text = StadtfuehrungVeliLosinj.Messages.PasswortNachricht;
                labelPasswortNachricht.Visible = true;
            }
            else
            {
                textBoxPasswort.BackColor = Color.White;
                labelPasswortNachricht.Visible = false;
            }

        }



        // Event Handler für den Button "Abbrechen"
        private void buttonAbbrechen_Click(object sender, EventArgs e)
        {
            // Zurück zur Startseite
            StartSeiteForm start = new StartSeiteForm();
            start.Show();
            this.Close();
        }


        // Event Handler für den Button "Registrieren"
        private void buttonRegistrieren_Click(object sender, EventArgs e)
        {
            //Holt die eingegebenen Benutzerdaten aus den Textboxen
            string _eMail = this.textBoxEmail.Text.Trim();
            string _benutzername = this.textBoxBenutzername.Text.Trim();
            string _passwort = this.textBoxPasswort.Text.Trim();

            // Überprüft ob Felder leer sind
            if (_eMail.Length == 0 || _benutzername.Length == 0 || _passwort.Length == 0)
            {
                MeinMessageBox.ShowMessage(Messages.AlleFelderAusfüllen, 1000);
                return;
            }

            //Passwort validieren
            if (_passwort.Length < 8 || !Regex.IsMatch(_passwort, @"[a-zA-Z]") || !Regex.IsMatch(_passwort, @"[!@#/$%&\*]"))
            {
                MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.PasswortNachricht, 1000);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    //Prüfen, ob Benutzername bereits existiert
                    string getUserQuery = "SELECT COUNT(*) FROM benutzer WHERE benutzername = @name";
                    MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, conn);
                    getUserCmd.Parameters.AddWithValue("@name", _benutzername);
                    object _result = getUserCmd.ExecuteScalar();
                    int _count = Convert.ToInt32(_result);

                    if(_count > 0 )
                    {
                        MeinMessageBox.ShowMessage(Messages.BenutzernameVergeben, 3000);
                        return;
                    }

                    //Neuen Benutzer in die Datenbank einfügen
                    string _benutzerEinfugen = "INSERT INTO benutzer (email, Benutzername, Passwort) VALUES (@email, @name, @passwort)";
                    MySqlCommand insertUserCmd = new MySqlCommand(_benutzerEinfugen, conn);
                    insertUserCmd.Parameters.AddWithValue("@email", _eMail);
                    insertUserCmd.Parameters.AddWithValue("@name", _benutzername);
                    insertUserCmd.Parameters.AddWithValue("@passwort", _passwort);

                    insertUserCmd.ExecuteNonQuery();
                    MeinMessageBox.ShowMessage(Messages.RegistrierungErfolgreich, 1000);
                    
                    //Erstellt ein neues Benutzerobjekt mit den eingegebenen Daten
                    _Benutzer = new Benutzer(_eMail, _benutzername, _passwort, _Benutzer._Sprache, 0);

                    //Hauptformular öffnen
                    HauptForm haupt = new HauptForm(_Benutzer);
                    this.Close();
                    haupt.Show();
                }
                catch(Exception ex)
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
                this.Text = StadtfuehrungVeliLosinj.Messages.RegistrierungTitel;
                this.labelEmail.Text = StadtfuehrungVeliLosinj.Messages.EmailLabel;
                this.labelBenutzername.Text = StadtfuehrungVeliLosinj.Messages.BenutzernameLabel;
                this.labelPasswort.Text = StadtfuehrungVeliLosinj.Messages.PasswortLabel;
                this.buttonRegistrieren.Text = StadtfuehrungVeliLosinj.Messages.RegistrierenButtonText;
                this.buttonAbbrechen.Text = StadtfuehrungVeliLosinj.Messages.AbbrechenButtonText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }


        }

        //Passwort ein/ausblenden
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxPasswort.UseSystemPasswordChar = !textBoxPasswort.UseSystemPasswordChar;
        }
    }
}
