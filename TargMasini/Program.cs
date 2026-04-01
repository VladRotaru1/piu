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
            ManagerDate manager = new ManagerDate();
            string numefisierMasini = "masini.txt";
            AdministrareMasini_FisierText adminMasini = new AdministrareMasini_FisierText(numefisierMasini);
            string numefisierClienti = "clienti.txt";
            AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText(numefisierClienti);
            string optiune;
            do
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("          SISTEM GESTIUNE TARG AUTO           ");
                Console.WriteLine("==============================================");
                Console.WriteLine("A - Adauga o masina noua");
                Console.WriteLine("L - Listeaza toate masinile din fisier");
                Console.WriteLine("F - Caută masina");
                Console.WriteLine("M - Modifica datele unei masini");
                Console.WriteLine("T - Realizeaza o tranzactie");
                Console.WriteLine("Y - Filtreaza tranzactii dupa firma masinii tranzactionate");
                Console.WriteLine("C - Adauga client");
                Console.WriteLine("E - Listare clienti");
                Console.WriteLine("X - Iesire");
                Console.WriteLine("----------------------------------------------");
                Console.Write("Alegeti o optiune: ");
                optiune = Console.ReadLine().ToUpper();
                switch (optiune)
                {
                    case "A":
                        Masina mNoua = CitireMasinaDeLaTastatura(adminMasini);
                        adminMasini.AddMasina(mNoua);
                        Console.WriteLine("Masina a fost salvata!");
                        Console.ReadLine();
                        break;
                    case "L":
                        Console.WriteLine($"\nMasinile din fisierul {numefisierMasini}:");
                        List<Masina> masini = adminMasini.GetMasini();
                        if (masini.Count == 0)
                            Console.WriteLine("Fisierul este gol.");
                        else
                            foreach (Masina m in masini)
                                Console.WriteLine(m.ToStringFisier());
                        Console.ReadLine();
                        break;
                    case "F":
                        Console.Write("ID-ul masinii pentru căutare: ");
                        int idC = int.Parse(Console.ReadLine());
                        Masina gasita = adminMasini.GetMasina(idC);
                        if (gasita != null)
                            Console.WriteLine($"Am găsit: {gasita.IDMasina} {gasita.Firma} {gasita.Model} {gasita.AnFabricatie} {gasita.Culoare} {gasita.Optiuni}");
                        else
                            Console.WriteLine("Mașina nu există.");
                        Console.ReadLine();
                        break;
                    case "M":
                        ModificareMasina(adminMasini);
                        Console.ReadLine();
                        break;
                    case "T":
                        Console.WriteLine("\n--- REALIZARE TRANZACȚIE ---");
                        Console.WriteLine("Introduceți id-ul mașinii dorite: ");
                        int idT = int.Parse(Console.ReadLine());

                        //Cautam masina în fisier
                        Masina masinaGasita = adminMasini.GetMasina(idT);

                        if (masinaGasita != null)
                        {
                            Console.WriteLine($"Masina a fost gasita: {masinaGasita.IDMasina} {masinaGasita.Firma} {masinaGasita.Model} {masinaGasita.AnFabricatie} {masinaGasita.Culoare} {masinaGasita.Optiuni}");

                            var clienti = adminClienti.GetClienti();
                            if (clienti.Count < 2)
                            {
                                Console.WriteLine("Eroare: Aveti nevoie de cel putin 2 clienti in fisier!");
                                break;
                            }

                            for (int i = 0; i < clienti.Count; i++)
                                Console.WriteLine($"{i + 1}. {clienti[i].Nume} {clienti[i].Prenume}");

                            Console.Write("Selectati ID-ul VANZATORULUI: ");
                            int idxV = int.Parse(Console.ReadLine()) - 1;

                            Console.Write("Selectati ID-ul CUMPARATORULUI: ");
                            int idxC = int.Parse(Console.ReadLine()) - 1;

                            if (idxV == idxC)
                            {
                                Console.WriteLine("Eroare: Vanzatorul nu poate fi aceeasi persoana cu cumparatorul!");
                                break;
                            }
                            //Verificam avertizarile
                            Console.Write("Introduceti pretul negociat: ");
                            int pretnegociat = int.Parse(Console.ReadLine());
                            Tranzactie tranzactieNoua = new Tranzactie(clienti[idxV], clienti[idxC], masinaGasita, pretnegociat, DateTime.Now);
                            var avertizari = manager.VerificaAvertizari(tranzactieNoua);
                            if (avertizari.Count > 0)
                            {
                                foreach (string nume in avertizari)
                                {
                                    Console.WriteLine($"[ATENTIE]: {nume} a mai fost implicat intr-o tranzactie astazi!");
                                    Console.WriteLine("Doriti sa continuati tranzactia? [Y/N]");
                                    string urgenta;
                                    urgenta = Console.ReadLine().ToUpper();
                                    switch (urgenta)
                                    {
                                        case "Y":
                                            Console.WriteLine("Continuam tranzactia...");
                                            break;
                                        case "N":
                                            Console.WriteLine("Tranzactia a fost anulata.");
                                            return;
                                        default:
                                            Console.WriteLine("Optiune invalida! Tranzactia a fost anulata.");
                                            return;
                                    }
                                }
                            }
                            //Salvam tranzactia in manager
                            manager.AdaugaTranzactie(tranzactieNoua);
                            Console.WriteLine("\nTranzactie realizata cu succes!");
                        }
                        else
                        {
                            Console.WriteLine("Eroare: Aceasta masina nu exista in fisier.");
                        }
                        Console.ReadLine();
                        break;
                    case "Y":
                        //Filtreaza dupa firma tranzactia
                        Console.WriteLine("\n--- FILTRARE TRANZACTII DUPA FIRMA ---");
                        Console.Write("Introduceti firma pentru filtrare: ");
                        string firmaFiltrare = Console.ReadLine();
                        var tranzactiiFiltrate = manager.FiltreazaDupaFirma(firmaFiltrare);
                        Console.WriteLine();
                        if (tranzactiiFiltrate.Count == 0)
                            Console.WriteLine("Nu exista tranzactii pentru aceasta firma.");
                        else
                            foreach (var t in tranzactiiFiltrate)
                                Console.WriteLine($"Vanzator: {t.Vanzator.Nume} {t.Vanzator.Prenume}, Cumparator: {t.Cumparator.Nume} {t.Cumparator.Prenume}, Masina: {t.MasinaVanduta.Firma} {t.MasinaVanduta.Model}, Pret: {t.Pret}, Data: {t.DataTranzactie}");
                        Console.ReadLine();
                        break;
                    case "C": // Adaugare Client
                        AddClient(adminClienti);
                        Console.ReadLine();
                        break;

                    case "E": // Listare Clienti
                        Console.WriteLine("--- LISTA CLIENTI ---");
                        var listaClienti = adminClienti.GetClienti();
                        foreach (var c in listaClienti) Console.WriteLine(c.ToString());
                        Console.ReadLine();
                        break;
                    case "X":
                        Console.WriteLine("Programul se inchide...");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Optiune invalida! Apasati orice tasta pentru a reincerca.");
                        Console.ReadLine();
                        break;
                }
            } while (optiune != "X");
            
            Console.WriteLine("Apasa orice tasta pentru a inchide...");
            Console.ReadKey();
        }
        static Masina CitireMasinaDeLaTastatura(AdministrareMasini_FisierText admin)
        {
            Console.WriteLine("\n--- Introducere Date Masina ---");
            var masini = admin.GetMasini();
            int ultimulId = masini.Count > 0 ? masini.Max(m => m.IDMasina) : 0;
            int id = ultimulId + 1;
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

            return new Masina(id, firma, model, an, culoare, optiuni);
        }
        static void ModificareMasina(AdministrareMasini_FisierText admin)
        {
            Console.Write("\nIntroduceti ID-ul pentru modificare: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Masina m = admin.GetMasina(id);
                if (m == null) { Console.WriteLine("Masina nu exista."); return; }

                Console.WriteLine("Ce modificam? 1.Firma 2.Model 3.An 4.Pret 5.Culoare 6.Dotari");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        Console.Write("Firma noua: ");
                        m.Firma = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Model nou: ");
                        m.Model = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("An nou: ");
                        m.AnFabricatie = int.Parse(Console.ReadLine());
                        break;
                    case "4":
                        Console.Write("Culoare noua: ");
                        m.Culoare = (CuloareMasina)Enum.Parse(typeof(CuloareMasina), Console.ReadLine(), true);
                        break;
                    case "5":
                        Console.Write("Dotari noi: ");
                        m.Optiuni = (Dotari)Enum.Parse(typeof(Dotari), Console.ReadLine(), true);
                        break;
                }
                admin.UpdateMasina(m);
                Console.WriteLine("Modificare salvata!");
            }
            Console.ReadLine();
        }
        static void AddClient(AdministrareClienti_FisierText adminClienti)
        {
            Console.WriteLine("--- ADAUGARE CLIENT ---");
            var clientiExistenti = adminClienti.GetClienti();
            int idClientNou = clientiExistenti.Count + 1;

            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Prenume: "); string prenume = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Telefon: "); string tel = Console.ReadLine();

            Client clientNou = new Client(idClientNou, nume, prenume, email, tel);
            adminClienti.AddClient(clientNou);
            Console.WriteLine("Client salvat cu succes!");
        }
    }
}

