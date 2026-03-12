using System;
namespace TargMasini
{
    public class Masina
    {
        public string Firma { get; set; }
        public string Model { get; set; }
        public int AnFabricatie { get; set; }
        public string Culoare { get; set; }
        public string Optiuni { get; set; }

        // Constructor pentru a crea rapid o masina
        public Masina(string firma, string model, int an, string culoare, string optiuni)
        {
            Firma = firma;
            Model = model;
            AnFabricatie = an;
            Culoare = culoare;
            Optiuni = optiuni;
        }
    }
}
