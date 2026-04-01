using System;
using System.Collections.Generic;
using System.Linq;
using LibrarieModele;
namespace NivelStocareDate
{
    public class ManagerDate
    {
        // Lista unde se vor salva toate tranzactiile
        private List<Tranzactie> tranzactii = new List<Tranzactie>();

        public void AdaugaTranzactie(Tranzactie t)
        {
            tranzactii.Add(t);
        }
        public List<string> VerificaAvertizari(Tranzactie t)
        {
            var numeAvertizate = new List<string>();

            bool dejaCumparator = tranzactii.Any(item =>
                item.DataTranzactie.Date == t.DataTranzactie.Date &&
                item.Cumparator == t.Cumparator);

            bool dejaVanzator = tranzactii.Any(item =>
                item.DataTranzactie.Date == t.DataTranzactie.Date &&
                item.Vanzator == t.Vanzator);

            if (dejaCumparator) numeAvertizate.Add($"{t.Cumparator.Nume} {t.Cumparator.Prenume}");
            if (dejaVanzator) numeAvertizate.Add($"{t.Vanzator.Nume} {t.Vanzator.Prenume}");

            return numeAvertizate;
        }

        public List<Tranzactie> GetTranzactii()
        {
            return tranzactii;
        }

        public List<Tranzactie> FiltreazaDupaData(DateTime data)
        {
            return tranzactii.Where(t => t.DataTranzactie.Date == data.Date).ToList();
        }

        public List<Tranzactie> FiltreazaDupaFirma(string firma)
        {
            return tranzactii.Where(t => t.MasinaVanduta.Firma.Equals(firma, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Tranzactie> FiltreazaDupaPret(decimal pretMinim, decimal pretMaxim)
        {
            return tranzactii.Where(t => t.Pret >= pretMinim && t.Pret <= pretMaxim).ToList();
        }
    }
}