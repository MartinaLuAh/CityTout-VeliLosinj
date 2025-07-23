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
    public partial class SehenswuerdigkeitForm : Form
    {
        //Objekt mit Sehenswürdigkeitdaten
        private SehenswuerdigkeitDaten _Sehenswuerdigkeit;

        //Benutzerobjekt
        private Benutzer _Benutzer;

        //Gibt an, ob die letzte Sehenswürdigkeit ist
        private bool _IstLetzte;
        public SehenswuerdigkeitForm(SehenswuerdigkeitDaten sehenswuerdigkeit, Benutzer _benutzer, bool _istLetzte)
        {
            //Initialisierung der Komponenten
            InitializeComponent();

            //Speichert die übergebene Sehenswuerdigkeit 
            this._Sehenswuerdigkeit = sehenswuerdigkeit;

            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Merkt sich, ob dies die letzte Sehenswürdigkeit ist
            this._IstLetzte = _istLetzte;

            //Kulturinfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);

            //Texte auf der Form aktualisieren
            AktualisiereTexte();

            //Bild der Sehenswürdigkeit anzeigen
            pictureBoxSehenswuerdigkeiteBild.Image = _Sehenswuerdigkeit._Bild;
        }

        // Event Handler für den Button "Schließen"
        private void buttonSchließen_Click(Object sender, EventArgs e)
        {
            //Wenn dies die letzte Sehenswürdigkeit ist, beende die Tour
            if (_IstLetzte && this.Owner is HauptForm hauptForm)
            {
                this.Hide();
                hauptForm.EndeDerTour();
            }
            else
            {
                //Schließt das aktuelle Formular
                this.Close();
            }
        }

        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache  
        private void AktualisiereTexte()
        {
            try
            {
                this.Text = _Sehenswuerdigkeit._Name;
                this.richTextBoxSehenswuerdigkeitBeschreibung.Text = _Sehenswuerdigkeit._Beschreibung;
                this.buttonSchließen.Text = StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.buttonSchließenText;
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }
 
        }
    }
}
