using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StadtfuehrungVeliLosinj
{
    public partial class RaetselForm : Form
    {
        //Rätselobjekt mit Frage und Antworten
        private Raetsel _raetsel;

        //Antwort des Benutzers
        private string _benutzerAntwort = "";

        //Gibt an, ob die Antwort korrekt war
        public bool _HatRichtigBeantworten { get; set; }

        //Benutzerobjekt
        private Benutzer _Benutzer;

        public RaetselForm(Raetsel raetsel, Benutzer _benutzer)
        { 
            //Initialisierung der Komponenten
            InitializeComponent();

            //Speichert das übergebene Rätsel Objekt
            this._raetsel = raetsel;

            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Punktestand anzeigen
            PunkteAnzeigen();

            //Kulturinfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);


            //Texte auf der Form aktualisieren
            AktualisiereTexte();
        }

        // Event Handler für den Button "Abbrechen"
        private void buttonAbbrechen_Click(object sender, EventArgs e)
        {
            //Schließt das Formular
            this.Close();
        }

        // Event Handler für den Button "AntwortA"
        private void buttonAntwortA_Click(object sender, EventArgs e)
        {
            _benutzerAntwort = "A";
            //Prüft, ob die Antwort korrekt ist
            AntwortPruefung();
        }

        // Event Handler für den Button "AntwortB"
        private void buttonAntwortB_Click(object sender, EventArgs e)
        {
            _benutzerAntwort = "B";
            //Prüft, ob die Antwort korrekt ist
            AntwortPruefung();
        }

        // Event Handler für den Button "AntwortC"
        private void buttonAntwortC_Click(object sender, EventArgs e)
        {
            _benutzerAntwort = "C";
            //Prüft, ob die Antwort korrekt ist
            AntwortPruefung();
        }

        //Methode AntwortPruefung. Prüft, ob die Antwort korrekt ist
        private void AntwortPruefung()
        {
            try
            {
                //Vergleicht die Benutzerantwort mit der richtigen Antwort
                if(_benutzerAntwort.Equals(_raetsel._RichtigeAntwort.ToString()))
                {
                    //Wenn richtig ist: Punkte hinzufügen, Erfolgsmeldung, Wert setzen, 
                    PunkteHinzufuegen();
                    MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.RaetselText.RichtigeAntwort, 1000);
                    
                    _HatRichtigBeantworten = true;
                    
                    this.Close();
                }
                else
                {
                    //Wenn falsch ist: Punkte abziehen, Fehlermeldung 
                    PunkteAbgezogen();
                    MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.RaetselText.FalscheAntwort, 1000);                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }
           
        }

        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache  
        private void AktualisiereTexte()
        {
            try
            {
                labelRaetselFrage.Text = _raetsel._Frage;
                buttonAntwortA.Text = "A: " + _raetsel._AntwortA;
                buttonAntwortB.Text = "B: " + _raetsel._AntwortB;
                buttonAntwortC.Text = "C: " + _raetsel._AntwortC;
                buttonAbbrechen.Text = StadtfuehrungVeliLosinj.Messages.AbbrechenButtonText;
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }
            
        }

        //Fügt dem Benutzer Punkte hinzu
        private void PunkteHinzufuegen()
        {
            _Benutzer._Punkte += 5;
            PunkteAnzeigen();
        }

        //Zieht dem Benutzer Punkte ab
        private void PunkteAbgezogen()
        {
            
            _Benutzer._Punkte -= 2;
            if(_Benutzer._Punkte < 0)
            {
                _Benutzer._Punkte = 0;
            }
            PunkteAnzeigen();
        }

        //Zeigt den aktuellen Punktestand an
        private void PunkteAnzeigen()
        {
            labelPunkte.Text =$" {StadtfuehrungVeliLosinj.Messages.LabelPunkteText} {_Benutzer._Punkte}";
        }
    }
}
