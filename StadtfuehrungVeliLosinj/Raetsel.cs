using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtfuehrungVeliLosinj
{
    public class Raetsel
    {
        //Die Frage des Rätsels
        public string _Frage { get; set; }

        //Antwortmöglichkeit A
        public string _AntwortA { get; set; }

        //Antwortmöglichkeit B
        public string _AntwortB { get; set; }

        //Antwortmöglichkeit C
        public string _AntwortC { get; set; }

        //Die richtige Antwort
        public char _RichtigeAntwort { get; set; }


        //Konstruktor zum Setzen der Frage und Antworten
        public Raetsel(string frage, string antwortA, string antwortB, string antwortC, char richtigeAntwort) 
        {
            this._Frage = frage;
            this._AntwortA = antwortA;
            this._AntwortB = antwortB;
            this._AntwortC = antwortC;
            this._RichtigeAntwort = richtigeAntwort;
        }

      

    }

}
