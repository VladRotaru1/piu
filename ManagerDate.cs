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
    }
}
