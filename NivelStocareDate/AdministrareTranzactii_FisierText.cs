using System;
using System.Collections.Generic;
using System.IO;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareTranzactii_FisierText
    {
        private string numeFisier;

        // Constructorul clasei - primește numele fișierului (ex: "tranzactii.txt")
        // și se asigură că acesta este creat dacă nu există deja
        public AdministrareTranzactii_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            using (Stream s = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        // Metodă pentru adăugarea unei tranzacții noi în fișier (Append)
        public void AddTranzactie(Tranzactie t)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                // Salvează linia formatată standardizat cu ';' din modelul curat
                sw.WriteLine(t.ToStringFisier());
            }
        }

        // Metodă pentru citirea tuturor tranzacțiilor din fișier
        public List<Tranzactie> GetTranzactii()
        {
            List<Tranzactie> lista = new List<Tranzactie>();

            try
            {
                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(linie))
                        {
                            // Folosește constructorul dedicat pe care l-am făcut în Tranzactie.cs
                            lista.Add(new Tranzactie(linie));
                        }
                    }
                }
            }
            catch (IOException)
            {
                // În caz de eroare la citirea fișierului, returnăm o listă goală pentru a nu crăpa aplicația
                return new List<Tranzactie>();
            }

            return lista;
        }
    }
}