using LibrarieModele;
using NivelStocareDate; 
using System;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
namespace TargMasini
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Initializam clasa de gestiune
            ManagerDate manager = new ManagerDate();

            // 2. Cream o masina noua (folosind clasa creata anterior)
            Masina masina1 = new Masina("Opel", "Astra 1.4", 2015, CuloareMasina.Gri, Dotari.ScauneIncalzite);
            Masina masina2 = new Masina("VW", "Polo", 2019, CuloareMasina.Alb, Dotari.None);
            Masina masina3 = CitireMasinaDeLaTastatura();
            // 3. Cream o tranzactie
            Tranzactie t1 = new Tranzactie("Popescu Ion", "Ionescu Maria", masina1, 5500, DateTime.Now);
            Tranzactie t2 = new Tranzactie("Vasilica Maria", "Ion Marius", masina2, 7000, DateTime.Now);
            Tranzactie t3 = CitireTranzactieDeLaTastatura(masina3);
            // 4. Adaugam tranzactia (aici se va verifica si avertizarea)
            var avertizari = manager.VerificaAvertizari(t1);
            foreach (var nume in avertizari) Console.WriteLine($"[AVERTIZARE]: {nume} a mai facut o tranzactie azi!");
            manager.AdaugaTranzactie(t1);
            var avertizari2 = manager.VerificaAvertizari(t2);
            foreach (var nume in avertizari2) Console.WriteLine($"[AVERTIZARE]: {nume} a mai facut o tranzactie azi!");
            manager.AdaugaTranzactie(t2);
            manager.AdaugaTranzactie(t3);

            Console.Write("\nIntroduceti firma cautata: ");
            string firma = Console.ReadLine();

            var rezultateFirma = manager.FiltreazaDupaFirma(firma);
            AfisareRezultate(rezultateFirma, $"Rezultate pentru firma {firma}");

            var rezultateData = manager.FiltreazaDupaData(DateTime.Now);
            AfisareRezultate(rezultateData, "Tranzactii de astazi");

            var rezultatePret = manager.FiltreazaDupaPret(5000, 6000);
            AfisareRezultate(rezultatePret, "Tranzactii intre 5000 si 6000 EUR");

            // Tinem consola deschisa ca sa vedem rezultatul
            Console.WriteLine("Apasa orice tasta pentru a inchide...");
            Console.ReadKey();
        }
        // Metodă ajutătoare pentru a nu scrie de 10 ori Console.WriteLine
        static void AfisareRezultate(List<Tranzactie> lista, string titlu)
        {
            Console.WriteLine($"\n--- {titlu.ToUpper()} ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("Nu s-au gasit rezultate.");
            }
            else
            {
                foreach (var t in lista)
                {
                    Console.WriteLine($"Vanzator: {t.NumeVanzator} | Cumparator: {t.NumeCumparator} | " +
                                      $"Masina: {t.MasinaVanduta.Firma} {t.MasinaVanduta.Model} {t.MasinaVanduta.Culoare} {t.MasinaVanduta.Optiuni}| Pret: {t.Pret}");
                }
            }
        }
        static Masina CitireMasinaDeLaTastatura()
        {
            Console.WriteLine("\n--- Introducere Date Masina ---");

            Console.Write("Firma: ");
            string firma = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("An Fabricatie: ");
            int an = int.Parse(Console.ReadLine());

            Console.WriteLine("Alegeti Culoarea (Alb = 1, Negru = 2, Rosu = 3, Gri = 4, Albastru = 5): ");
            CuloareMasina culoare = (CuloareMasina)int.Parse(Console.ReadLine());

            Console.WriteLine("Alegeti dotarile (AerConditionat = 1, Navigatie = 2, CutieAutomata = 4, ScauneIncalzite = 8, SenzoriParcare = 16):");
            Dotari optiuni = (Dotari)int.Parse(Console.ReadLine());

            return new Masina(firma, model, an, culoare, optiuni);
        }
        static Tranzactie CitireTranzactieDeLaTastatura(Masina masina)
        {
            Console.Write("\nNume Vanzator: ");
            string vanzator = Console.ReadLine();
            Console.Write("Nume Cumparator: ");
            string cumparator = Console.ReadLine();
            Console.Write("Pret Tranzactie: ");
            decimal pret = decimal.Parse(Console.ReadLine());

            return new Tranzactie(vanzator, cumparator, masina, pret, DateTime.Now);
        }
    }
}

