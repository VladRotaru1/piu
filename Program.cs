using System;
using TargMasini;

// 1. Initializam clasa de gestiune
ManagerDate manager = new ManagerDate();

// 2. Cream o masina noua (folosind clasa creata anterior)
Masina masina1 = new Masina("Opel", "Astra 1.4", 2015, "Gri", "AC, ABS");

// 3. Cream o tranzactie
Tranzactie t1 = new Tranzactie("Popescu Ion", "Ionescu Maria", masina1, 5500, DateTime.Now);

// 4. Adaugam tranzactia (aici se va verifica si avertizarea)
manager.AdaugaTranzactie(t1);

// Tinem consola deschisa ca sa vedem rezultatul
Console.WriteLine("Apasa orice tasta pentru a inchide...");
Console.ReadKey();
