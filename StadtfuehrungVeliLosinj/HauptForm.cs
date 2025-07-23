using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StadtfuehrungVeliLosinj
{
    public partial class HauptForm : Form
    {
        //Verbindung zur Datenbank über App.config
        string _connectionString = ConfigurationManager.ConnectionStrings["MySqlVerbindung"].ConnectionString;


        //Liste der Sehenswürdigkeiten
        private List<SehenswuerdigkeitDaten> _sehenswuerdigkeitDatens = new List<SehenswuerdigkeitDaten>();

        //Liste der aktiven Punkte
        private List<Point> _aktivePunkte = new List<Point>();

        //Zähler für gezeichnete Punkte
        private int _aktuellGezeichnet = 1;

        //Timer zur Steuerung des Zeichenintervalls auf der Karte
        private System.Windows.Forms.Timer _timer;

        //Benutzerobjekt
        private Benutzer _Benutzer;

        public HauptForm(Benutzer _benutzer)
        {
            //Speichert das Benutzerobjekt
            this._Benutzer = _benutzer;

            //Kulturinfo setzen für die Sprachumschaltung
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_Benutzer._Sprache);


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

            //Ereignis beim Zeichnen des Bildes verbinden
            pictureBox1.Paint += pictureBox1_Paint;
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

        //Wenn der Benutzer eine neue Sprache auswählt
        private void comboBoxSprache_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string _ausgewaehlteSprache = _Benutzer._Sprache;

                //Ausgewählte Sprache anhand des Index festlegen
                switch (comboBoxSprache.SelectedIndex)
                {
                    case 0:
                        _ausgewaehlteSprache = "de";
                        break;
                    case 1:
                        _ausgewaehlteSprache = "hr";
                        break;
                    case 2:
                        _ausgewaehlteSprache = "it";
                        break;
                    case 3:
                        _ausgewaehlteSprache = "en";
                        break;
                    default:
                        _ausgewaehlteSprache = _Benutzer._Sprache;
                        break;
                }

                //Wenn keine Änderung
               if(_ausgewaehlteSprache == _Benutzer._Sprache)
                {
                    return;
                }

                //Kulturinfo setzen für die Sprachumschaltung
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(_ausgewaehlteSprache);
            
                _Benutzer._Sprache = _ausgewaehlteSprache;

                //Neue Instanz der HauptForm starten mit neuer Sprache
                HauptForm neueHauptForm = new HauptForm(_Benutzer);
                this.Close();
                neueHauptForm.Show();
            }
            catch (CultureNotFoundException ex)
            {

                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerUngueltigeKultur + "\n" + ex.Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.FehlerSpracheAenderung + "\n" + ex.Message);
            }
        }




        //Methode VersteckeAlleButtons, alle Buttons verstecken
        private void VersteckeAlleButtons()
        {
            foreach (var v in _sehenswuerdigkeitDatens)
            {
                if (v._Button != null)
                {
                    v._Button.Visible = false;
                }
            }
        }

        // Event Handler für den Button "Start"
        private void buttonStart_Click(object sender, EventArgs e)
        {
            panelWillkommen.Visible = false;
            buttonStart.Visible = false;

            //Sehenswürdigkeiten in die Liste einfügen
            SehenwuerdigkeitenEinfuegen();

            //Buttons abrunden
            foreach (var v in _sehenswuerdigkeitDatens)
            {
                if (v._Button != null)
                {
                    v._Button.Width = 25;
                    v._Button.Height = 25;
                    ButtonsAbrunden(v._Button);
                }
            }

            //Buttons mit Click Event verbinden
            foreach (var v in _sehenswuerdigkeitDatens)
            {
                v._Button.Click += Sehenswuerdigkeit_Click;
            }


            
     

            //Zuerst nur den ersten Button zeigen
            VersteckeAlleButtons();
            _sehenswuerdigkeitDatens[0]._Entsperrt = true;
            _sehenswuerdigkeitDatens[0]._Button.Visible = true;

            //Rout zeichnen
            aktualisiereRute();
        }

        // Event Handler für den Button "Abmelden"
        private void buttonAbmelden_Click(object sender, EventArgs e)
        {
            //Alle Buttons verstecken
            foreach (var v in _sehenswuerdigkeitDatens)
            {
                if(v._Button != null)
                {
                    v._Button.Visible = false;
                }
            }

            //Punkte löschen
            _aktivePunkte.Clear();
            _aktuellGezeichnet = 0;
            //Route leeren
            pictureBox1.Invalidate();


            StartSeiteForm start = new StartSeiteForm();
            this.Close();
            //Zurück zur Startseite
            start.Show();

      
            
        }

        // Event Handler für den Buttons "Sehenswuerdigkeit"
        private void Sehenswuerdigkeit_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            SehenswuerdigkeitDaten aktuelle = null;

            //Die geklickte Sehenswürdigkeit finden
            foreach (var v in _sehenswuerdigkeitDatens)
            {
                if (v._Button == clickedButton)
                {
                    aktuelle = v;
                    break;
                }
            }
            if (aktuelle != null && aktuelle._Entsperrt)
            { 
                try
                {
                    //Rätsel anzeigen
                    RaetselForm raestelForm = new RaetselForm(aktuelle._Raetsel, _Benutzer);
                    raestelForm.ShowDialog();

                    //Punktestand aktualisieren
                    AktualisierePunkteText();

                    if (raestelForm._HatRichtigBeantworten)
                    {
                        //Nächste Sehenswürdigkeit entsperren
                        int i = _sehenswuerdigkeitDatens.IndexOf(aktuelle);
                        if (i + 1 < _sehenswuerdigkeitDatens.Count)
                        {
                            _sehenswuerdigkeitDatens[i + 1]._Entsperrt = true;
                            _sehenswuerdigkeitDatens[i + 1]._Button.Visible = true;

                            //Route neu zeichen
                            aktualisiereRute();
                        }
                    }
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
                }
                try
                {
                    //SehenswürdigkeitForm (Informationsformular) öffnen
                    bool _istLetzte = (_sehenswuerdigkeitDatens.IndexOf(aktuelle) == _sehenswuerdigkeitDatens.Count - 1);
                    SehenswuerdigkeitForm informationenForm = new SehenswuerdigkeitForm(aktuelle, _Benutzer, _istLetzte);                
                    informationenForm.Owner = this;
                    informationenForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
                }
            }
        }


        //Methode ButtonsAbrunden, rundet die Ecken des Buttons ab
        private void ButtonsAbrunden(Button b)
        {

            int diameter = Math.Max(b.Width,b.Height);
            b.Width = diameter;
            b.Height = diameter;

            //Erstellen eines runden Pfads
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, diameter, diameter);
            b.Region = new Region(path);

            //Button Stil 
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Color.Red;
            b.ForeColor = Color.White;
            b.Font = new Font("Arial", 6, FontStyle.Bold);
            b.TextAlign = ContentAlignment.MiddleCenter;
        }

        
         // Methode aktualisiereRute, zeichnet die Route zwischen freigeschalteten Sehenswürdigkeiten       
        private void aktualisiereRute()
        {
            //Liste der aktiven Punkte neu erstellen
            _aktivePunkte = new List<Point>();

            //Für jeder Sehenswürdigkeit prüfen, ob ihr Button sichtbar ist
            foreach (var s in _sehenswuerdigkeitDatens)
            {
                if (s._Button != null && s._Button.Visible && s._Button.Name != "buttonStart" && s._Button.Name != "buttonAbmelden")
                {
                    Point center = new Point(s._Button.Left + s._Button.Width / 2, s._Button.Top + s._Button.Height / 2);
                    _aktivePunkte.Add(center);
                }
            }

            //Zähler zurücksetzen
            _aktuellGezeichnet = 1;
            if(_aktivePunkte.Count < 2)
            {
                pictureBox1.Invalidate();
                return;
            }

            if(_timer == null)
            {
                _timer = new System.Windows.Forms.Timer();
                _timer.Interval = 200;
                _timer.Tick += _timer_Tick;
            }

            //Timer neu starten 
            _timer.Stop();
            _timer.Start();
        }

        // Methode timer_Tick, wird verwendet, um die Route animiert darzustellen     
        private void _timer_Tick(object sender, EventArgs e)
        {
            //Einen Punkt mehr anzeigen
            _aktuellGezeichnet++;
            if(_aktuellGezeichnet >= _aktivePunkte.Count)
            {
                _timer.Stop();
            }
            //Neuzeichnen auslösen
            pictureBox1.Invalidate();
        }

        //Methode pictureBox1_Paint, zeichnet die Linien zwischen den bereits gezeichneten Punkten
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Wenn weniger als zwei aktive Punkte vorhanden sind, wird abgebrochen
            if(_aktivePunkte.Count < 2 || _aktuellGezeichnet < 2)
            {
                return;
            }
            //Wenn alle Punkte gezeichnet wurden, setze _aktuellGezeichnet auf die Gesamtanzahl
            if (_aktuellGezeichnet > _aktivePunkte.Count)
            {
                _aktuellGezeichnet = _aktivePunkte.Count;
            }

            //Nur die bereits gezeichneten Punkte auswählen
            var _zeichenPunkte = _aktivePunkte.Take(_aktuellGezeichnet).ToArray();

            //Wenn mindestens zwei Punkte wie aktuell gezeichnet werden sollen
            if(_zeichenPunkte.Length >= 2)
            {
                //Bereite das Grafikobjekt und den Stift
                Graphics _graphics = e.Graphics;
                Pen _rute = new Pen(Color.Red, 3);

                //Zeichnet Linien zwischen den Punkten
                _graphics.DrawLines(_rute, _zeichenPunkte);
            }
        }

        //Texte im Formular aktualisieren basierend auf der aktuellen Sprache    
        private void AktualisiereTexte()
        {
            try
            {
                this.Text = StadtfuehrungVeliLosinj.Messages.StartSeiteTitel;
                labelBegrueßungsnachricht.Text = StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.Begrueßungsnachricht;
                labelBeginn.Text = StadtfuehrungVeliLosinj.Messages.labelBeginnText;
                labelSprache.Text = StadtfuehrungVeliLosinj.Messages.SpracheLabelText;
                buttonStart.Text = StadtfuehrungVeliLosinj.Messages.buttonStartText;
                buttonAbmelden.Text = StadtfuehrungVeliLosinj.Messages.buttonAbmeldenText;
         
                panelWillkommen.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }
        }

        private void AktualisierePunkteText()
        {
            this.Text = $" {StadtfuehrungVeliLosinj.Messages.LabelPunkteText} {_Benutzer._Punkte}";
        }

        //Methode EndeDerTour, wird aufgerufen, wenn die Tour zu Ende ist
        public void EndeDerTour() 
        {
            try
            {
                MeinMessageBox.ShowMessage(StadtfuehrungVeliLosinj.Messages.EndeDerTourNachricht, 1000);

                //Speichert die erreichte Punktezahl in die Datenbank
                PunkteSpeichern();
               
                RanglisteForm rangliste = new RanglisteForm(_Benutzer);
                //Ranglisteformular öffnen
                rangliste.ShowDialog();             
                this.Hide();                
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }     
        }

        //Methode PunkteSpeichern
        public void PunkteSpeichern()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    //SQL Befehl zum Aktualisieren der Punktezahl des Benutzers
                    string updateUserQuery = "UPDATE benutzer set punkte = @punkte WHERE benutzername = @name";
                    MySqlCommand updateUserCmd = new MySqlCommand(updateUserQuery, conn);

                    updateUserCmd.Parameters.AddWithValue("@punkte", _Benutzer._Punkte);
                    updateUserCmd.Parameters.AddWithValue("@name", _Benutzer._Benutzername);
                    updateUserCmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(StadtfuehrungVeliLosinj.Messages.TechnischerFehler + "\n" + ex.Message);
            }
        }

        //Methode SehenwuerdigkeitenEinfuegen, erstellt die Liste aller Sehenwürdigkeiten 
        private void SehenwuerdigkeitenEinfuegen()
        {
            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.KircheHeiligeNikolausTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.KircheHeiligeNikolaus,
                Properties.Resources.HlNikolaus,
                buttonKircheHeiligeNikolaus,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeNikolaus_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeNikolaus_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeNikolaus_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeNikolaus_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeNikolaus_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.VillaElisabethTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.VillaElisabeth,
                Properties.Resources.VillaElisabeth,
                buttonVillaElisabeth,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselVillaElisabeth_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselVillaElisabeth_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselVillaElisabeth_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselVillaElisabeth_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselVillaElisabeth_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.KircheHeiligeMariaTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.KircheHeiligeMaria,
                Properties.Resources.HlMaria,
                buttonKircheHeiligeMaria,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeMaria_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeMaria_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeMaria_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeMaria_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeMaria_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.FischmarktTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.Fischmarkt,
                Properties.Resources.Fischmarkt,
                buttonFischmarkt,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselFischmarkt_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselFischmarkt_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselFischmarkt_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselFischmarkt_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselFischmarkt_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.TurmTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.Turm,
                Properties.Resources.Turm,
                buttonTurm,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselTurm_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselTurm_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselTurm_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselTurm_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselTurm_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.KircheHeiligeAntoniusTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.KircheHeiligeAntonius,
                Properties.Resources.HlAntonius,
                buttonKircheHeiligeAntonius,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeAntonius_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeAntonius_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeAntonius_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeAntonius_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselKircheHeiligeAntonius_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.FeralTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.Feral,
                Properties.Resources.Feral,
                buttonFeral,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselFeral_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselFeral_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselFeral_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselFeral_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselFeral_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.HafenVonRovenskaTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.HafenVonRovenska,
                Properties.Resources.Rovenska,
                buttonHafenRovenska,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselHafenVonRovenska_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselHafenVonRovenska_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselHafenVonRovenska_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselHafenVonRovenska_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselHafenVonRovenska_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.OlivenmühleTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.Olivenmühle,
                Properties.Resources.Olivenmuehle, 
                buttonOlivenmuehle,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselOlivenmühle_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselOlivenmühle_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselOlivenmühle_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselOlivenmühle_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselOlivenmühle_RichtigeAntwort[0]
                )));

            _sehenswuerdigkeitDatens.Add(new SehenswuerdigkeitDaten(StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.WellenbrecherTitel,
                StadtfuehrungVeliLosinj.Sehenswuerdigkeiten.Wellenbrecher_Molo_,
                Properties.Resources.Wellenbrecher
                ,
                buttonWellenbrecher,
                new Raetsel(StadtfuehrungVeliLosinj.RaetselText.RätselWellenbrecher_Frage,
                StadtfuehrungVeliLosinj.RaetselText.RätselWellenbrecher_AntwortA,
                StadtfuehrungVeliLosinj.RaetselText.RätselWellenbrecher_AntwortB,
                StadtfuehrungVeliLosinj.RaetselText.RätselWellenbrecher_AntwortC,
                StadtfuehrungVeliLosinj.RaetselText.RätselWellenbrecher_RichtigeAntwort[0]
                )));
        }

       
    }
}

