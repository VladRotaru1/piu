using System;
using System.Text.RegularExpressions;
namespace LibrarieModele
{
    public enum CuloareMasina
    {
        Alb = 1, 
        Negru = 2, 
        Rosu = 3, 
        Gri = 4, 
        Albastru = 5
    }

    [Flags]
    public enum Dotari
    {
        None = 0,
        AerConditionat = 1,
        Navigatie = 2,
        CutieAutomata = 4,
        ScauneIncalzite = 8,
        SenzoriParcare = 16
    }
    public class Masina
    {   
        public int IDMasina { get; set; }
        public string Firma { get; set; }
        public string Model { get; set; }
        public int AnFabricatie { get; set; }
        public CuloareMasina Culoare { get; set; }
        public Dotari Optiuni { get; set; }

        public Masina(int id, string firma, string model, int an, CuloareMasina culoare, Dotari optiuni)
        {   
            IDMasina = id;
            Firma = firma;
            Model = model;
            AnFabricatie = an;
            Culoare = culoare;
            Optiuni = optiuni;
        }
        public Masina(Masina masina)
        {   
            IDMasina = masina.IDMasina;
            Firma = masina.Firma;
            Model = masina.Model;
            AnFabricatie = masina.AnFabricatie;
            Culoare = masina.Culoare;
            Optiuni = masina.Optiuni;
        }
        public string ToStringFisier()
        {
            // Transformăm totul într-un șir de caractere separat prin virgulă
            return $"{IDMasina};{Firma};{Model};{AnFabricatie};{Culoare};{Optiuni}";
        }

        // Constructor nou pentru citirea din fișier
        public Masina(string linieFisier)
        {
            var date = linieFisier.Split(';');
            IDMasina = int.Parse(date[0]);
            Firma = date[1];
            Model = date[2];
            AnFabricatie = int.Parse(date[3]);
            Culoare = (CuloareMasina)Enum.Parse(typeof(CuloareMasina), date[4]);
            Optiuni = (Dotari)Enum.Parse(typeof(Dotari), date[5]);
        }
    }
}
    