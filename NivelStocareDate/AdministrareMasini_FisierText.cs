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
    }
}
