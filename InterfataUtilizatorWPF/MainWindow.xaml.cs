using LibrarieModele;
using NivelStocareDate;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterfataUtilizatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdministrareMasini_FisierText adminMasini;
        private const int MAX_CARACTERE = 15;
        private const int AN_MINIM = 1900;

        public MainWindow()
        {
            InitializeComponent();

            string numeFisier = "masini.txt";
            adminMasini = new AdministrareMasini_FisierText(numeFisier);
        }
        private void btnAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            // Instanțiem o mașină (Entitatea ta)
            Masina masinaMea = new Masina(1, "Dacia", "Logan", 2022, CuloareMasina.Alb, Dotari.ScauneIncalzite);

            // Actualizăm etichetele din XAML
            lblFirma.Content = "Marcă: " + masinaMea.Firma;
            lblModel.Content = "Model: " + masinaMea.Model;
            lblAn.Content = "An Fabricație: " + masinaMea.AnFabricatie;
            lblCuloare.Content = "Culoare: " + masinaMea.Culoare;
            lblOptiuni.Content = "Opțiuni: " + masinaMea.Optiuni;

        }
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            ResetareCuloriEtichete();
            txtMesajEroare.Visibility = Visibility.Collapsed;

            bool dateValide = true;
            string erori = "";

            // 1. Validare Firma
            if (string.IsNullOrWhiteSpace(txtFirma.Text) || txtFirma.Text.Length > MAX_CARACTERE)
            {
                lblFirma.Foreground = Brushes.Red;
                erori += "Firma invalidă (max 15 car). ";
                dateValide = false;
            }
            // 2. Validare Model
            if (string.IsNullOrEmpty(txtModel.Text) || txtModel.Text.Length > MAX_CARACTERE)
            {
                lblModel.Foreground = Brushes.Red;
                erori += "Model invalid (max 15 car). ";
                dateValide = false;
            }

            // 2. Validare An (trebuie să fie număr)
            if (!int.TryParse(txtAn.Text, out int an) || an < AN_MINIM || an > DateTime.Now.Year)
            {
                lblAn.Foreground = Brushes.Red;
                erori += "An invalid. ";
                dateValide = false;
            }

            // Dacă totul e ok
            if (dateValide)
            {
                // Aici vei apela adminMasini.AddMasina(nouaMasina) din NivelStocareDate
                // Pentru test, afișăm un mesaj de succes
                int nouID = adminMasini.GetMasini().Count + 1;

                Masina m = new Masina(nouID, txtFirma.Text, txtModel.Text, an, CuloareMasina.Alb, Dotari.None);

                adminMasini.AddMasina(m);
                MessageBox.Show("Mașina a fost adăugată cu succes!");
            }
            else
            {
                txtMesajEroare.Text = erori;
                txtMesajEroare.Visibility = Visibility.Visible;
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtFirma.Text = txtModel.Text = txtAn.Text = txtCuloare.Text = txtOptiuni.Text = string.Empty;
            ResetareCuloriEtichete();
            txtMesajEroare.Visibility = Visibility.Collapsed;
        }
        private void ResetareCuloriEtichete()
        {
            lblFirma.Foreground = Brushes.Black;
            lblModel.Foreground = Brushes.Black;
            lblAn.Foreground = Brushes.Black;
            lblCuloare.Foreground = Brushes.Black;
            lblOptiuni.Foreground = Brushes.Black;
        }
    }
}