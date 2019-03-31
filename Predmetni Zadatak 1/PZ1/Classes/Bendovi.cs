using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    [Serializable]
    public class Bendovi
    {
        
        public String Ime { get; set; }
        public String Poreklo { get; set; }
        public String Zanr { get; set; }
        public int GodOsn { get; set; }
        public String Opis { get; set; }
        public String Putanja { get; set; }
        //public DateTime DateeTime { get =>d; set=> d= value; }
        public string Date { get; set; }
        //public DateTime D { get => d.D; set => d = value; }

        public Bendovi() { }

        public Bendovi(string ime,string poreklo,string zanr,int godina,string opis,string putanja,string da) {

            Ime = ime;
            Poreklo = poreklo;
            Zanr = zanr;
            GodOsn = godina;
            Opis = opis;
            Putanja = putanja;
            da = Vreme.Date();
            Date = da;
        }
    }
}
