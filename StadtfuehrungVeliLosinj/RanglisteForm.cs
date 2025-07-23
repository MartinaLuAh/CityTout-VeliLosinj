using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StadtfuehrungVeliLosinj
{
    public partial class RanglisteForm : Form
    {
        //Verbindung zur Datenbank über App.config
        string _connectionString = ConfigurationManager.ConnectionStrings["MySqlVerbindung"].ConnectionString;

        //Benutzerobjekt
        private Benutzer _Benutzer;

        //Liste zur Speicherung aller Benutzer
        private List<Benutzer> benutzers = new List<Benutzer>();

        public RanglisteForm(Benutzer _benutzer)
        {
            
            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Kulturnfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);

            //Initialisierung der Komponenten
            InitializeComponent();

            //Texte auf der Form aktualisieren
            AktualisiereTexte();

            try
            {
                //Lädt die Benutzer in die Liste
                BenutzerInListeEinfugen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }     
        }

        //Lädt die Benutzerdaten aus der Datenbank in die Liste
        private void BenutzerInListeEinfugen()
        {
            benutzers.Clear();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string userQuery = "SELECT benutzername, punkte FROM benutzer";
                    MySqlCommand userCmd = new MySqlCommand(userQuery, conn);

                    using (MySqlDataReader reader = userCmd.ExecuteReader())
                    {
                        while (reader.Read())
                            try 
                            {
                                string _benutzername = reader.GetString("benutzername");
                                int _punkte = reader.GetInt32("punkte");

                                benutzers.Add(new Benutzer(_benutzername, "", "", Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName, _punkte));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerBenutzerHinzufuegen + "\n" + ex.Message);
                            }
                    }
                }
            
                if(benutzers.Count == 0)
                {
                    MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.DatenbankIstLeer, 3000);
                    return;
                }
                else
                {
                    BenutzerlisteSortieren();
                    BenutzerInDataGridView();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }
        }

        //Sortiert die Benutzerliste nach Punkten mit Bubble Sort
        private void BenutzerlisteSortieren()
        {
            if(benutzers.Count <= 1)
            {
                return;
            }
            for(int i=0; i<benutzers.Count-1; i++)
            {
                for(int j=0; j<benutzers.Count -1; j++)
                {
                    if (benutzers[j]._Punkte < benutzers[j + 1]._Punkte)
                    {
                        (benutzers[j], benutzers[j + 1]) = (benutzers[j + 1], benutzers[j]);
                    }
                }
            }
        }

        //Zeigt die Benutzerliste in der DataGridView an
        private void BenutzerInDataGridView()
        {
            dataGridViewRangliste.DataSource = null;
            dataGridViewRangliste.DataSource = benutzers;

            dataGridViewRangliste.Columns["_Benutzername"].HeaderText = StadtfuehrungVeliLosinj.Messages.BenutzernameLabel;
            dataGridViewRangliste.Columns["_Punkte"].HeaderText = StadtfuehrungVeliLosinj.Messages.LabelPunkteText;

            dataGridViewRangliste.Columns["_Passwort"].Visible = false;
            dataGridViewRangliste.Columns["_Email"].Visible = false;
            dataGridViewRangliste.Columns["_Sprache"].Visible = false;

        }

        // Event Handler für den Button "Schließen"
        private void buttonSchließen_Click(object sender, EventArgs e)
        {
            //Schließt das aktuelle Formular
            this.Close();
        }

        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache 
        private void AktualisiereTexte()
        {
            try
            {
                this.Text = StadtfuehrungVeliLosinj.Messages.RanglisteTitelText;
                this.buttonSchließen.Text = StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.buttonSchließenText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }


        }
    }
}
