using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SmartSoftware.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            set { SetAndNotify(ref listaKorisnika, value); }
        }


        //Window mainWindow = this;
         private DispatcherTimer timer = new DispatcherTimer
    (
    TimeSpan.FromSeconds(5),
    DispatcherPriority.ApplicationIdle,// Or DispatcherPriority.SystemIdle

    (s, e) => {
        //SystemSounds.Beep.Play(); Type t = s.GetType(); MessageBox.Show(e.ToString());
    }, // or something similar
    Application.Current.Dispatcher
    );

        public MainWindow()
        {
            InitializeComponent();
           
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // Metoda timer_Tick će se izvršavati na svakih 40ms, tj. 25 puta u sekundi.
            // U ovoj metodi će biti osvežavan prikaz proteklog vremena štoperice.
            //txtDisplay.Text = stopwatch.Elapsed.ToString(txtDisplayFormat);
        }


        private void popuniListuKorisnika(DbItemKorisnici[] nizKorisnika)
        {
            ListaKorisnika = new ObservableCollection<Korisnici>();
            foreach (var item in nizKorisnika)
            {
                Korisnici korisnik = new Korisnici()
                {
                    IdKorisnici = item.id_korisnici,
                    IdUloge = item.id_uloge,
                    IzabranaUloga = item.id_uloge - 1,
                    ImeKorisnika = item.ime,
                    PrezimeKorisnika = item.prezime,
                    MejlKorisnika = item.mejl,
                    BrojTelefonaKorisnika = item.broj_telefona,
                    BrojOstvarenihPoena = Convert.ToDouble(item.brojOstvarenihPoena),
                    Username = item.username,
                    Lozinka = item.lozinka,
                    NazivUloge = item.naziv_uloge,
                    DeletedItem = item.deletedField
                };
                ListaKorisnika.Add(korisnik);
            }
            
        }

        private void btnUlogujSe_Click(object sender, RoutedEventArgs e)
        {


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            SmartSoftwareServiceReference.DbItemOblastiOpreme[] niz = service.OblastiOpreme();

            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.PrikaziKorisnike(null);

            this.popuniListuKorisnika(nizKorisnika);

            string textBoxUser = textBoxKorisnickoIme.Text;
            string textBoxPass = textBoxLozinka.Text;
            int brojac = 0;





            foreach (var item in ListaKorisnika)
            {

                if (textBoxUser == item.Username && textBoxPass == item.Lozinka)
                {
                    
                    if (item.IdUloge != 1 && item.IdUloge != 2)
                    {
                        MessageBox.Show("morate se ulogovati kao administrator ili prodavac");
                    }
                    else
                    {
                        brojac++;
                        GlavniProzor glavni = new GlavniProzor(item.IdUloge);
                        this.Close();
                        glavni.Show();
                        
                    }
                    break;
                }
            }

            if (brojac == 0)
            {
                MessageBox.Show("Korisničko ime i/ili lozinka je netačna");
            }

           

                
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }



        #region PropertyChangedImpl
        protected void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion        
    }
}