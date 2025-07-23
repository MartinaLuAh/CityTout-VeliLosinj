using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StadtfuehrungVeliLosinj
{
    public class SehenswuerdigkeitDaten
    {
        //Name der Sehenswürdigkeit
        public string _Name { get; set; }

        //Beschreibung der Sehenswürdigkeit
        public string _Beschreibung { get; set; }

        //Bild, das die Sehenswürdigkeit darstellt
        public Image _Bild { get; set; }

        //Der zugehörige Button auf der HauptForm
        public Button _Button { get; set; }

        //Das zugehörige Rätsel zur Sehenswürdigkeit
        public Raetsel _Raetsel { get; set; }

        //Gibt an, ob die Sehenswürdigkeit freigeschaltet ist
        public bool _Entsperrt { get; set; }

        //Statischer Zähler für die Anzahl der Sehenswürdigkeit
        public static int Count { get; internal set; }

        //Konstruktor zum Setzen aller Eingeschaften
        public SehenswuerdigkeitDaten(string name, string beschreibung, Image bild, Button button, Raetsel raetsel)
        {
            this._Name = name;
            this._Beschreibung = beschreibung;
            this._Bild = bild;
            this._Button = button;
            this._Raetsel = raetsel;
            this._Entsperrt = false;
        }
    }
}
