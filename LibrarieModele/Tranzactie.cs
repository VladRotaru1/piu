using System;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace LibrarieModele
{
    public class Tranzactie
    {   
        public int IdTranzactie { get; set; }
        public Client Vanzator { get; set; }
        public Client Cumparator { get; set; }
        public Masina MasinaVanduta { get; set; } // Obiect de tip Masina
        public DateTime DataTranzactie { get; set; }
        public double Pret { get; set; }

        public Tranzactie(int idTranzactie, Client vanzator, Client cumparator, Masina masina, double pret, DateTime data)
        {
            IdTranzactie = idTranzactie;
            Vanzator = vanzator;
            Cumparator = cumparator;
            MasinaVanduta = masina;
            Pret = pret;
            DataTranzactie = data;
        }
        public Tranzactie(string linieFisier)
        {
            if (string.IsNullOrWhiteSpace(linieFisier)) return;

            var date = linieFisier.Split(';');
            
            // date[0] -> IdTranzactie
            IdTranzactie = int.Parse(date[0]);

            // date[1] -> IDMasina (Instanțiem mașina și îi punem ID-ul citit)
            MasinaVanduta = new Masina(int.Parse(date[1]), "", "", 0, 0, 0);

            // date[2] -> ID Vânzător, date[3] -> ID Cumpărător
            Vanzator = new Client(int.Parse(date[2]), "", "", "", "");
            Cumparator = new Client(int.Parse(date[3]), "", "", "", "");

            // date[4] -> Preț
            Pret = double.Parse(date[4]);

            // date[5] -> DataTranzactie (Format fix formatat regional ca zi.lună.an)
            DataTranzactie = DateTime.Parse(date[5]);
        }
        public string ToStringFisier()
        {
            return $"{IdTranzactie};{MasinaVanduta.IDMasina};{Vanzator.IdClient};{Cumparator.IdClient};{Pret};{DataTranzactie}";
        }
    }
}
