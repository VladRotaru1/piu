using LibrarieModele;
using NivelStocareDate;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        AdministrareMasini_FisierText adminMasini;
        AdministrareClienti_FisierText adminClienti;
        private string _statusAplicatie;
        public string StatusAplicatie
        {
            get => _statusAplicatie;
            set { _statusAplicatie = value; OnPropertyChanged(); }
        }

        private string _numarStatisticiText;
        public string NumarStatisticiText
        {
            get => _numarStatisticiText;
            set { _numarStatisticiText = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private const int MAX_CARACTERE = 15;
        private const int AN_MINIM = 1900;

        public MainWindow()
        {
            InitializeComponent();

            string numeFisier = "masini.txt";
            adminMasini = new AdministrareMasini_FisierText(numeFisier);
            // Inițializare fișier pentru a doua entitate
            adminClienti = new AdministrareClienti_FisierText("clienti.txt");

            // Activarea legăturii (DataContext) pentru Data Binding
            this.DataContext = this;

            // Dimensiuni ideale pentru ecranul pe 2 coloane
            this.Width = 1000;
            this.Height = 700;

            // Mesaje inițiale transmise direct prin binding în interfață
            StatusAplicatie = "Sistem pregătit. Gata pentru operații CRUD.";
            ActualizeazaStatisticiStatut();
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
                int nouID = 1; // Pornim de la 1 dacă fișierul e gol
                var masiniExistente = adminMasini.GetMasini();

                if (masiniExistente != null && masiniExistente.Count > 0)
                {
                    // Căutăm cel mai mare ID din listă
                    int idMaxim = 0;
                    foreach (var masina in masiniExistente)
                    {
                        if (masina.IDMasina > idMaxim)
                        {
                            idMaxim = masina.IDMasina;
                        }
                    }
                    nouID = idMaxim + 1; // Noul ID va fi cu 1 mai mare decât cel mai mare găsit
                }

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
            cbAerConditionat.IsChecked = cbNavigatie.IsChecked = cbCutieAutomata.IsChecked = cbScauneIncalzite.IsChecked = cbSenzoriParcare.IsChecked = false;
            rbAlb.IsChecked = rbNegru.IsChecked = rbRosu.IsChecked = rbGri.IsChecked = rbAlbastru.IsChecked = false;
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
            // 1. Luăm lista completă de mașini din fișier
            List<Masina> masini = adminMasini.GetMasini();

            // Dacă fișierul este gol, nu avem ce căuta
            if (masini == null || masini.Count == 0)
            {
                MessageBox.Show("Nu există mașini salvate în baza de date.");
                return;
            }

            // 2. Preluăm criteriile introduse de tine în formularele de sus
            string filtruFirma = txtFirma.Text.ToLower().Trim();
            string filtruModel = txtModel.Text.ToLower().Trim();
            string filtruAn = txtAn.Text.Trim();

            // Preluăm culoarea selectată în acest moment în RadioButtons
            CuloareMasina culoareSelectata = CuloareMasina.Alb;
            if (rbAlb.IsChecked == true) culoareSelectata = CuloareMasina.Alb;
            if (rbNegru.IsChecked == true) culoareSelectata = CuloareMasina.Negru;
            if (rbRosu.IsChecked == true) culoareSelectata = CuloareMasina.Rosu;
            if (rbGri.IsChecked == true) culoareSelectata = CuloareMasina.Gri;
            if (rbAlbastru.IsChecked == true) culoareSelectata = CuloareMasina.Albastru;

            // Preluăm opțiunile bifate în CheckBoxes
            Dotari dotariSelectate = Dotari.None;
            if (cbAerConditionat.IsChecked == true) dotariSelectate |= Dotari.AerConditionat;
            if (cbNavigatie.IsChecked == true) dotariSelectate |= Dotari.Navigatie;
            if (cbCutieAutomata.IsChecked == true) dotariSelectate |= Dotari.CutieAutomata;
            if (cbScauneIncalzite.IsChecked == true) dotariSelectate |= Dotari.ScauneIncalzite;
            if (cbSenzoriParcare.IsChecked == true) dotariSelectate |= Dotari.SenzoriParcare;

            // 3. Aplicăm filtrarea pas cu pas (dacă un câmp este gol, programul îl ignoră și caută după restul)
            var rezultate = masini.Where(m =>
                (string.IsNullOrEmpty(filtruFirma) || (m.Firma != null && m.Firma.ToLower().Contains(filtruFirma))) &&
                (string.IsNullOrEmpty(filtruModel) || (m.Model != null && m.Model.ToLower().Contains(filtruModel))) &&
                (string.IsNullOrEmpty(filtruAn) || m.AnFabricatie.ToString() == filtruAn) &&
                (m.Culoare == culoareSelectata) &&
                ((m.Optiuni & dotariSelectate) == dotariSelectate) // Verifică dacă mașina are cel puțin dotările bifate de tine
            ).ToList();

            // 4. Trimitem lista filtrată în DataGrid (tabel)
            dgMasini.ItemsSource = null;
            dgMasini.ItemsSource = rezultate;

            // 5. Notificare dacă nu s-a găsit nimic
            if (rezultate.Count == 0)
            {
                MessageBox.Show("Nu s-au găsit mașini care să corespundă exact criteriilor selectate în formular.");
            }
        }

        // 2. Butonul care salvează totul
        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            // 1. Verificăm dacă ai selectat mașina pe care vrei să o modifici
            if (dgMasini.SelectedItem is Masina masinaDeModificat)
            {
                // 2. Suprascriem proprietățile ei cu noile date scrise de tine în câmpurile de sus
                if(txtFirma.Text != string.Empty)
                    masinaDeModificat.Firma = txtFirma.Text;
                if(txtModel.Text != string.Empty)
                    masinaDeModificat.Model = txtModel.Text;
                if(txtAn.Text != string.Empty)
                {
                    if (int.TryParse(txtAn.Text, out int anNou))
                    {
                        masinaDeModificat.AnFabricatie = anNou;
                    }
                }

                // Preluăm culoarea nouă de la butoanele de sus
                if (rbAlb.IsChecked == true) masinaDeModificat.Culoare = CuloareMasina.Alb;
                else if (rbNegru.IsChecked == true) masinaDeModificat.Culoare = CuloareMasina.Negru;
                else if (rbRosu.IsChecked == true) masinaDeModificat.Culoare = CuloareMasina.Rosu;
                else if (rbGri.IsChecked == true) masinaDeModificat.Culoare = CuloareMasina.Gri;
                else if (rbAlbastru.IsChecked == true) masinaDeModificat.Culoare = CuloareMasina.Albastru;
                else masinaDeModificat.Culoare = masinaDeModificat.Culoare; // Dacă nu s-a schimbat, păstrăm culoarea veche

                // Preluăm opțiunile noi din CheckBox
                Dotari dotariNoi = Dotari.None;
                if (cbAerConditionat.IsChecked == true) dotariNoi |= Dotari.AerConditionat;
                if (cbNavigatie.IsChecked == true) dotariNoi |= Dotari.Navigatie;
                if (cbCutieAutomata.IsChecked == true) dotariNoi |= Dotari.CutieAutomata;
                if (cbScauneIncalzite.IsChecked == true) dotariNoi |= Dotari.ScauneIncalzite;
                if (cbSenzoriParcare.IsChecked == true) dotariNoi |= Dotari.SenzoriParcare;

                if (dotariNoi == Dotari.None) dotariNoi = masinaDeModificat.Optiuni; // Dacă nu s-a schimbat, păstrăm opțiunile vechi
                masinaDeModificat.Optiuni = dotariNoi;

                // 3. Trimitem obiectul modificat către fișierul text
                bool succes = adminMasini.ModificaMasina(masinaDeModificat);

                if (succes)
                {
                    MessageBox.Show("Informațiile mașinii au fost actualizate cu succes folosind datele din formular!");
                    ActualizeazaAfisare(); // Dă refresh la listă ca să vezi schimbarea
                }
                else
                {
                    MessageBox.Show("Eroare la salvarea modificărilor în fișier.");
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați mai întâi mașina din listă pe care doriți să o modificați.");
            }
        }
        private void ActualizeazaAfisare()
        {
            List<Masina> masini = adminMasini.GetMasini();
            dgMasini.ItemsSource = masini;
        }

        private void btnAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            dgMasini.ItemsSource = null;
            dgMasini.ItemsSource = adminMasini.GetMasini();
        }
        /// CLIENT
        private void btnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeClient.Text) || string.IsNullOrWhiteSpace(txtPrenumeClient.Text))
            {
                MessageBox.Show("Introduceți numele și prenumele clientului!");
                return;
            }

            int nouID = 1;
            var clientiExistenti = adminClienti.GetClienti();
            if (clientiExistenti != null && clientiExistenti.Count > 0)
            {
                nouID = clientiExistenti.Max(c => c.IdClient) + 1;
            }
      
            Client c = new Client(nouID, txtNumeClient.Text, txtPrenumeClient.Text, txtTelefonClient.Text, txtEmailClient.Text);
            adminClienti.AddClient(c);
            MessageBox.Show("Client salvat cu succes!");

            StatusAplicatie = $"Adăugat client: {c.Nume} {c.Prenume}";
            ActualizeazaStatisticiStatut();
            dgClienti.ItemsSource = adminClienti.GetClienti();
            ReseteazaClient();
        }

        private void btnAfiseazaClienti_Click(object sender, RoutedEventArgs e)
        {
            dgClienti.ItemsSource = null;
            dgClienti.ItemsSource = adminClienti.GetClienti();

            StatusAplicatie = "Au fost afișați clienții din baza de date.";
            ActualizeazaStatisticiStatut();
            ReseteazaClient();
        }

        private void btnModificaClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClienti.SelectedItem is Client clientSelectat)
            {
                clientSelectat.IdClient = clientSelectat.IdClient;
                if (txtNumeClient.Text != string.Empty)
                    clientSelectat.Nume = txtNumeClient.Text;
                if (txtPrenumeClient.Text != string.Empty)
                    clientSelectat.Prenume = txtPrenumeClient.Text;
                if (txtTelefonClient.Text != string.Empty)
                    clientSelectat.Telefon = txtTelefonClient.Text;
                if (txtEmailClient.Text != string.Empty)
                        clientSelectat.Email = txtEmailClient.Text;

                bool succes = adminClienti.ModificaClient(clientSelectat);
                if (succes)
                {
                    MessageBox.Show("Datele clientului au fost modificate cu succes!");
                    StatusAplicatie = $"S-a modificat clientul cu ID-ul {clientSelectat.IdClient}.";
                    dgClienti.ItemsSource = null;
                    dgClienti.ItemsSource = adminClienti.GetClienti();
                }
            }
            else
            {
                MessageBox.Show("Selectați mai întâi un client din tabel.");
            }
            ReseteazaClient();
        }

        private void ActualizeazaStatisticiStatut()
        {
            var totalMasini = adminMasini.GetMasini()?.Count ?? 0;
            var totalClienti = adminClienti.GetClienti()?.Count ?? 0;

            NumarStatisticiText = $"Bază activă: {totalMasini} Mașini stocate | {totalClienti} Clienți înregistrați";
        }
        private void ReseteazaClient()
        {   
            txtNumeClient.Text = string.Empty;
            txtPrenumeClient.Text = string.Empty;
            txtTelefonClient.Text = string.Empty;
            txtEmailClient.Text = string.Empty;
        }
    }
}