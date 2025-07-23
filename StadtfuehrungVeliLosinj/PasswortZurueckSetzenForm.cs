using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StadtfuehrungVeliLosinj
{
    public partial class PasswortZurueckSetzenForm : Form
    {
        //Verbindung zur Datenbank über App.config
        string _connectionString = ConfigurationManager.ConnectionStrings["MySqlVerbindung"].ConnectionString;


        //Benutzerobjekt
        private Benutzer _Benutzer;

        public PasswortZurueckSetzenForm(Benutzer _benutzer)
        {
            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Kulturnfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);

            //Initialisierung der Komponenten
            InitializeComponent();

            //Verbindet die Textfelder mit den entsprechenden Methoden zur Live Validierung der Eingabe
            this.textBoxPasswort.TextChanged += textBoxPasswort_TextChanged;
            this.textBoxPasswortBestaetigen.TextChanged += textBoxPasswortBestaetigen_TextChanged;

            //Texte auf der Form aktualisieren
            AktualisiereTexte();
        }



        //Validiert das Passwortformat
        private void textBoxPasswort_TextChanged(object sender, EventArgs e)
        {
            string _passwort1 = textBoxPasswort.Text.Trim();

            if (_passwort1.Length < 8 || !Regex.IsMatch(_passwort1, @"[a-zA-Z]") || !Regex.IsMatch(_passwort1, @"[!@#/$%&\*]"))
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

        //Prüft, ob die Passwortbestätigung mit dem Passwort übereinstimmt
        private void textBoxPasswortBestaetigen_TextChanged(object sender, EventArgs e)
        {
            string _passwort1 = textBoxPasswort.Text.Trim();
            string _passwort2 = textBoxPasswortBestaetigen.Text.Trim();

            if (_passwort1 != _passwort2)
            {
                textBoxPasswortBestaetigen.BackColor = Color.MistyRose;
                labelPasswortBestaetigenNachricht.Text = StadtfuehrungVeliLosinj.Messages.PasswortBestaetigenNachricht;
                labelPasswortBestaetigenNachricht.Visible = true;
            }
            else
            {
                textBoxPasswortBestaetigen.BackColor = Color.White;
                labelPasswortBestaetigenNachricht.Visible = false;
            }

        }

        //Methode für Passwort Zurücksetzen
        private void buttonPasswortZurueckSetzen_Click(object sender, EventArgs e)
        {
            string _benutzername = this.textBoxBenutzername.Text.Trim();
            string _passwort1 = this.textBoxPasswort.Text.Trim();
            string _passwort2 = this.textBoxPasswortBestaetigen.Text.Trim();

            // Überprüft ob Felder leer sind
            if (_benutzername.Length == 0 || _passwort1.Length == 0 || _passwort2.Length == 0)
            {
                MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.AlleFelderAusfüllen, 3000);
                return;
            }

            //Prüft, ob die beiden Passwörter übereinstimmen
            if (_passwort1 != _passwort2)
            {
                MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.PasswortBestaetigenNachricht, 3000);
                return;
            }

            //Passwort validieren
            if (_passwort1.Length < 8 || !Regex.IsMatch(_passwort1, @"[a-zA-Z]") || !Regex.IsMatch(_passwort1, @"[!@#/$%&\*]"))
            {
                MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.PasswortNachricht, 3000);
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

                    if (_count == 0)
                    {
                        MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.BenutzernameExistiertNicht, 3000);
                        return;
                    }

                    //Passwort aktualisieren
                    string updateUserQuery = "UPDATE benutzer SET passwort = @passwort WHERE benutzername = @name";
                    MySqlCommand updateUserCmd = new MySqlCommand(updateUserQuery, conn);
                    updateUserCmd.Parameters.AddWithValue("@passwort", _passwort1);
                    updateUserCmd.Parameters.AddWithValue("@name", _benutzername);


                    updateUserCmd.ExecuteNonQuery();
                    MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.PasswortZurueckGesetzt, 3000);

                    //Zurück zum Anmeldeformular
                    AnmeldeForm anmelde = new AnmeldeForm(_Benutzer);
                    this.Close();
                    anmelde.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
                }

            }

        }

        // Event Handler für den Button "Abbrechen"
        private void buttonAbbrechen_Click(object sender, EventArgs e)
        {
            // Zurück zur Anmeldeform
            AnmeldeForm anmelde = new AnmeldeForm(_Benutzer);
            anmelde.Show();
            this.Close();
        }

        //Passwort ein/ausblenden
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxPasswort.UseSystemPasswordChar = !textBoxPasswort.UseSystemPasswordChar;
        }

        //Passwort ein/ausblenden
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxPasswortBestaetigen.UseSystemPasswordChar = !textBoxPasswortBestaetigen.UseSystemPasswordChar;
        }

        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache 
        private void AktualisiereTexte()
        {
            this.Text = StadtfuehrungVeliLosinj.Messages.buttonPasswortZurueckSetzen;
            this.labelBenutzername.Text = StadtfuehrungVeliLosinj.Messages.BenutzernameLabel;
            this.labelPasswort.Text = StadtfuehrungVeliLosinj.Messages.PasswortLabel;
            this.labelPasswortBestaetigen.Text = StadtfuehrungVeliLosinj.Messages.PasswortBestaetigen;
            this.buttonPasswortZurueckSetzen.Text = StadtfuehrungVeliLosinj.Messages.buttonPasswortZurueckSetzen;
            this.buttonAbbrechen.Text = StadtfuehrungVeliLosinj.Messages.AbbrechenButtonText;
        }

    }
}
