using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtfuehrungVeliLosinj
{

    public class Benutzer
    {
        //Benutzername des Benutzers
        public string _Benutzername { get; set; }

        //Passwort des Benutzers
        public string _Passwort { get; set; }

        //E-mail des Benutzers
        public string _Email { get; set; }

        //Sprache des Benutzers
        public string _Sprache { get; set; }

        //Punkte des Benutzers
        public int _Punkte { get; set; }


        //Konstruktor zum Erstellen eines neuen Benutzers
        public Benutzer(string benutzername, string passwort, string email, string sprache, int punkte)
        {
            this._Benutzername = benutzername;
            this._Passwort = passwort;
            this._Email = email;
            this._Sprache = sprache;
            this._Punkte = punkte;

        }
    }
}

