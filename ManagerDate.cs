using System;

namespace TargMasini
{
    public class ManagerDate
    {
        // Lista unde se vor salva toate tranzactiile
        private List<Tranzactie> tranzactii = new List<Tranzactie>();

        public void AdaugaTranzactie(Tranzactie t)
        {
            // Verificăm dacă persoana a mai cumpărat/vândut ceva ASTĂZI
            foreach (var item in tranzactii)
            {
                if (item.DataTranzactie.Date == t.DataTranzactie.Date)
                {
                    if (item.NumeCumparator == t.NumeCumparator)
                        Console.WriteLine($"[AVERTIZARE]: {t.NumeCumparator} a mai cumparat o masina azi!");

                    if (item.NumeVanzator == t.NumeVanzator)
                        Console.WriteLine($"[AVERTIZARE]: {t.NumeVanzator} a mai vandut o masina azi!");
                }
            }

            tranzactii.Add(t);
            Console.WriteLine("Tranzactie inregistrata cu succes.");
        }
        public void AfiseazaTranzactii()
        {
            Console.WriteLine("Tranzactii inregistrate:");
            foreach (var t in tranzactii)
            {
                Console.WriteLine($"Vanzator: {t.NumeVanzator}, Cumparator: {t.NumeCumparator}, Masina: {t.MasinaVanduta.Firma} {t.MasinaVanduta.Model}, Pret: {t.Pret}, Data: {t.DataTranzactie}");
            }
        }
        public void AfiseazaTranzactiiDupaData(DateTime data)
        {
            Console.WriteLine($"Tranzactii inregistrate pentru data: {data.ToShortDateString()}");
            foreach (var t in tranzactii)
            {
                if (t.DataTranzactie.Date == data.Date)
                {
                    Console.WriteLine($"Vanzator: {t.NumeVanzator}, Cumparator: {t.NumeCumparator}, Masina: {t.MasinaVanduta.Firma} {t.MasinaVanduta.Model}, Pret: {t.Pret}");
                }
            }
        }
        public void AfiseazaTranzactiiDupaFirma(string firma)
        {
            Console.WriteLine($"Tranzactii inregistrate pentru firma: {firma}");
            foreach (var t in tranzactii)
            {
                if (t.MasinaVanduta.Firma.Equals(firma, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Vanzator: {t.NumeVanzator}, Cumparator: {t.NumeCumparator}, Masina: {t.MasinaVanduta.Firma} {t.MasinaVanduta.Model}, Pret: {t.Pret}, Data: {t.DataTranzactie}");
                }
            }
        }
        public void AfiseazaTranzactiiDupaPret(decimal pretMinim, decimal pretMaxim)
        {
            Console.WriteLine($"Tranzactii inregistrate pentru pret intre {pretMinim} si {pretMaxim}");
            foreach (var t in tranzactii)
            {
                if (t.Pret >= pretMinim && t.Pret <= pretMaxim)
                {
                    Console.WriteLine($"Vanzator: {t.NumeVanzator}, Cumparator: {t.NumeCumparator}, Masina: {t.MasinaVanduta.Firma} {t.MasinaVanduta.Model}, Pret: {t.Pret}, Data: {t.DataTranzactie}");
                }
            }
        }
    }
}
