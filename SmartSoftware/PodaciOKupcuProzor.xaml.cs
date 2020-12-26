using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for PodaciOKupcuProzor.xaml
    /// </summary>
    public partial class PodaciOKupcuProzor : Window, INotifyPropertyChanged
    {
        public PodaciOKupcuProzor()
        {
            InitializeComponent();
            this.DataContext = this;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = service.PrikaziKorisnike(null);
            this.popuniListuKorisnika(korisnici);
        }

        private ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            //set { }
        }

        private Korisnici tmpKorisnik = new Korisnici();

        public Korisnici TmpKorisnik
        {
            get { return tmpKorisnik; }
            set { SetAndNotify(ref tmpKorisnik, value); }
        }

        private void GridPrikazRezultataKorisnika_MouseDown(object sender, MouseButtonEventArgs e)
        {
           // MessageBox.Show("AAAAAAA");
            //Ovde ide PrikazDetaljaKorisnika


            //ICollectionView documentsView = CollectionViewSource.GetDefaultView(this.ListaKorisnika);

            //Korisnici korisnik = documentsView.CurrentItem as Korisnici;
            //ObservableCollection<Korisnici> kolekcija = new ObservableCollection<Korisnici>();
            //kolekcija.Add(korisnik);
            //MessageBox.Show(korisnik.ImeKorisnika);
            //this.PrikazDetaljaKorisnika.ItemsSource = null;
            //this.PrikazDetaljaKorisnika.ItemsSource = kolekcija;

            Grid gIzabranKorisnik = sender as Grid;
            TextBlock t = gIzabranKorisnik.FindName("skrivenId") as TextBlock;
            ObservableCollection<Korisnici> kolekcija = new ObservableCollection<Korisnici>();

            ObservableCollection<Korisnici> k = new ObservableCollection<Korisnici>();
            for (int i = 0; i < ListaKorisnika.Count; i++)
			{
			    if(ListaKorisnika[i].IdKorisnici == Int32.Parse(t.Text))
                {
                    k.Add(ListaKorisnika[i]);
                    //this.TmpKorisnik = this.ListaKorisnika[i];
                    break;
                }
			}
            this.PrikazDetaljaKorisnika.ItemsSource = null;
            this.PrikazDetaljaKorisnika.ItemsSource = k;
            
        }

        private void popuniListuKorisnika(DbItemKorisnici[] korisnici)
        {
            this.ListaKorisnika.Clear();
            foreach (var korisnik in korisnici)
            {
                this.ListaKorisnika.Add
                    (new Korisnici()
                    {
                        BrojOstvarenihPoena = Convert.ToDouble(korisnik.brojOstvarenihPoena),
                        PrezimeKorisnika = korisnik.prezime,
                        MejlKorisnika = korisnik.mejl,
                        IdKorisnici = korisnik.id_korisnici,
                        BrojTelefonaKorisnika = korisnik.broj_telefona,
                        ImeKorisnika = korisnik.ime,
                        Lozinka = korisnik.lozinka,
                        Username = korisnik.username
                    });
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = null;
            if(zaPretragu == null || zaPretragu == "")
            {
                korisnici = service.PrikaziKorisnike(null);
            }
            else
            {
                korisnici = service.PretragaKorisnika(zaPretragu);
            
            }
            this.popuniListuKorisnika(korisnici);
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

        private void btnRegistracijaKorisnika_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            DbItemKorisnici korisnik = new DbItemKorisnici()
            {
                ime = tmpKorisnik.ImeKorisnika,
                prezime = tmpKorisnik.PrezimeKorisnika,
                mejl = tmpKorisnik.MejlKorisnika,
                broj_telefona = tmpKorisnik.BrojTelefonaKorisnika,
                id_uloge = 3
            };
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = service.OpKorisniciInsert(korisnik);
            this.ListaKorisnika.Add(tmpKorisnik);
            this.popuniListuKorisnika(korisnici);
            this.TmpKorisnik = new Korisnici();
        }

        private void btnIzaberiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView documentsView = CollectionViewSource.GetDefaultView(this.ListaKorisnika);

            Korisnici korisnik = documentsView.CurrentItem as Korisnici;
            this.tmpKorisnik = korisnik;

           
            this.Close();
            
        }
    }

}
