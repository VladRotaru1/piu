using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;
namespace NivelStocareDate
{
    public class AdministrareMasini_FisierText
    {
        private string numeFisier;

        public AdministrareMasini_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream sFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            sFisierText.Close(); 
        }

        public void AddMasina(Masina masina)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(masina.ToStringFisier());
            }
        }

        public List<Masina> GetMasini()
        {
            List<Masina> masini = new List<Masina>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    masini.Add(new Masina(linie));
                }
            }
            return masini;
        }
        public List<Masina> GetMasina(string firma)
        {   
            List<Masina> masini = new List<Masina>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    Masina m = new Masina(linie);
                    if (m.Firma == firma)
                    {
                        masini.Add(m);
                    }
                }
            }
            return masini;
        }
        public Masina GetMasina(int id)
        {
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    Masina m = new Masina(linie);
                    if (m.IDMasina == id)
                    {
                        return m;
                    }
                }
            }
            return null;
        }
        public void UpdateMasina(Masina masinaModificata)
        {
            List<Masina> masini = GetMasini(); // Citim tot
            bool gasit = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false)) 
            {
                foreach (var m in masini)
                { 
                    if (m.IDMasina == masinaModificata.IDMasina)
                    {
                        sw.WriteLine(masinaModificata.ToStringFisier());
                        gasit = true;
                    }
                    else
                    {
                        sw.WriteLine(m.ToStringFisier());
                    }
                }
            }
        }
        public bool ModificaMasina(Masina masinaModificata)
        {
            List<Masina> masini = GetMasini();
            bool gasit = false;

            for (int i = 0; i < masini.Count; i++)
            {
                if (masini[i].IDMasina == masinaModificata.IDMasina)
                {
                    masini[i] = masinaModificata;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
            {
                File.WriteAllText(numeFisier, string.Empty); // Golește fișierul
                foreach (var m in masini)
                {
                    AddMasina(m); // Scrie mașinile (cea modificată + restul)
                }
            }
            return gasit;
        }
    }
}
