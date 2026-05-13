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
            CuloareMasina culoare = CuloareMasina.Alb;
            if (rbAlb.IsChecked == true)
                culoare = CuloareMasina.Alb;
            if (rbNegru.IsChecked == true)
                culoare = CuloareMasina.Negru;
            if (rbRosu.IsChecked == true)
                culoare = CuloareMasina.Rosu;
            if (rbGri.IsChecked == true)
                culoare = CuloareMasina.Gri;
            if (rbAlbastru.IsChecked == true)
                culoare = CuloareMasina.Albastru;
            Dotari dotari = Dotari.None;
            if (cbAerConditionat.IsChecked == true)
                dotari |= Dotari.AerConditionat;
            if (cbNavigatie.IsChecked == true)
                dotari |= Dotari.Navigatie;
            if (cbCutieAutomata.IsChecked == true)
                dotari |= Dotari.CutieAutomata;
            if (cbScauneIncalzite.IsChecked == true)
                dotari |= Dotari.ScauneIncalzite;
            if (cbSenzoriParcare.IsChecked == true)
                dotari |= Dotari.SenzoriParcare;
            // Dacă totul e ok
            if (dateValide)
            {
                // Aici vei apela adminMasini.AddMasina(nouaMasina) din NivelStocareDate
                // Pentru test, afișăm un mesaj de succes
                int nouID = adminMasini.GetMasini().Count + 1;

                Masina m = new Masina(nouID, txtFirma.Text, txtModel.Text, an, culoare, dotari);

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
            txtFirma.Text = txtModel.Text = txtAn.Text = string.Empty;
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
        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            List<Masina> masini = adminMasini.GetMasini();

            string cautare = txtCautare.Text.ToLower();

            Masina masinaGasita = masini.FirstOrDefault(m => m.Firma.ToLower().Contains(cautare));

            if (masinaGasita != null)
            {
                MessageBox.Show(
                    $"Masina găsită:\n" +
                    $"{masinaGasita.Firma} {masinaGasita.Model}");
            }
            else
            {
                MessageBox.Show("Nu s-a găsit mașina.");
            }
        }
    }
}