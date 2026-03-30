using System;
namespace LibrarieModele
{
    public class Tranzactie
    {
        public string NumeVanzator { get; set; }
        public string NumeCumparator { get; set; }
        public Masina MasinaVanduta { get; set; } // Obiect de tip Masina
        public DateTime DataTranzactie { get; set; }
        public decimal Pret { get; set; }

        public Tranzactie(string vanzator, string cumparator, Masina masina, decimal pret, DateTime data)
        {
            NumeVanzator = vanzator;
            NumeCumparator = cumparator;
            MasinaVanduta = masina;
            Pret = pret;
            DataTranzactie = data;
        }
    }
}
