using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;
namespace NivelStocareDate
{
    public class AdministrareClienti_FisierText
    {
        private string numeFisier;

        public AdministrareClienti_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            using (Stream s = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        public void AddClient(Client c)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(c.ToStringFisier());
            }
        }

        public List<Client> GetClienti()
        {
            List<Client> lista = new List<Client>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(linie))
                        lista.Add(new Client(linie));
                }
            }
            return lista;
        }
        public bool ModificaClient(Client clientActualizat)
        {
            List<Client> clienti = GetClienti();
            bool actualizat = false;

            foreach (var c in clienti)
            {
                if (c.IdClient == clientActualizat.IdClient)
                {
                    c.Nume = clientActualizat.Nume;
                    c.Prenume = clientActualizat.Prenume;
                    c.Telefon = clientActualizat.Telefon;
                    c.Email = clientActualizat.Email; 
                    actualizat = true;
                    break;
                }
            }

            if (actualizat)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(numeFisier, false))
                    {
                        foreach (var c in clienti)
                        {
                            sw.WriteLine(c.ToStringFisier());
                        }
                    }
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
