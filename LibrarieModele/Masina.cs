using System;
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
        public string Firma { get; set; }
        public string Model { get; set; }
        public int AnFabricatie { get; set; }
        public CuloareMasina Culoare { get; set; }
        public Dotari Optiuni { get; set; }

        public Masina(string firma, string model, int an, CuloareMasina culoare, Dotari optiuni)
        {
            Firma = firma;
            Model = model;
            AnFabricatie = an;
            Culoare = culoare;
            Optiuni = optiuni;
        }
        public Masina(Masina masina)
        {
            Firma = masina.Firma;
            Model = masina.Model;
            AnFabricatie = masina.AnFabricatie;
            Culoare = masina.Culoare;
            Optiuni = masina.Optiuni;
        }
    }
}
    