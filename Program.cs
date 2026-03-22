using System;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using TargMasini;

// 1. Initializam clasa de gestiune
ManagerDate manager = new ManagerDate();

// 2. Cream o masina noua (folosind clasa creata anterior)
Masina masina1 = new Masina("Opel", "Astra 1.4", 2015, "Gri", "AC, ABS");
Masina masina2 = new Masina("VW", "Polo", 2019, "ALB", "Standard");
// 3. Cream o tranzactie
Tranzactie t1 = new Tranzactie("Popescu Ion", "Ionescu Maria", masina1, 5500, DateTime.Now);
Tranzactie t2 = new Tranzactie("Vasilica Maria", "Ion Marius", masina2, 7000, DateTime.Now);
// 4. Adaugam tranzactia (aici se va verifica si avertizarea)
manager.AdaugaTranzactie(t1);
manager.AdaugaTranzactie(t2);

manager.AfiseazaTranzactii();

Console.Write("\nIntroduceti firma cautata: ");
string firma = Console.ReadLine();

manager.AfiseazaTranzactiiDupaFirma(firma);
manager.AfiseazaTranzactiiDupaData(DateTime.Now);
manager.AfiseazaTranzactiiDupaPret(5000, 6000);

// Tinem consola deschisa ca sa vedem rezultatul
Console.WriteLine("Apasa orice tasta pentru a inchide...");
Console.ReadKey();
