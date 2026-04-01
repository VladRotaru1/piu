using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class Client
    {
        public int IdClient { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public Client(string linieFisier)
        {
            var date = linieFisier.Split(';');
            IdClient = int.Parse(date[0]);
            Nume = date[1];
            Prenume = date[2];
            Email = date[3];
            Telefon = date[4];
        }

        public Client(int id, string nume, string prenume, string email, string telefon)
        {
            IdClient = id;
            Nume = nume;
            Prenume = prenume;
            Email = email;
            Telefon = telefon;
        }

        public string ToStringFisier()
        {
            return $"{IdClient};{Nume};{Prenume};{Email};{Telefon}";
        }

        public override string ToString()
        {
            return $"ID: {IdClient} | {Nume} {Prenume} | Email: {Email} | Tel: {Telefon}";
        }
    }
}
