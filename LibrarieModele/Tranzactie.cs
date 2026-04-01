using System;
namespace LibrarieModele
{
    public class Tranzactie
    {
        public Client Vanzator { get; set; }
        public Client Cumparator { get; set; }
        public Masina MasinaVanduta { get; set; } // Obiect de tip Masina
        public DateTime DataTranzactie { get; set; }
        public decimal Pret { get; set; }

        public Tranzactie(Client vanzator, Client cumparator, Masina masina, decimal pret, DateTime data)
        {
            Vanzator = vanzator;
            Cumparator = cumparator;
            MasinaVanduta = masina;
            Pret = pret;
            DataTranzactie = data;
        }
    }
}
