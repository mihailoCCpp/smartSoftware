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
using Xceed.Wpf.Toolkit;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for KupovinaProzor.xaml
    /// </summary>
    public partial class KupovinaProzor : Window, INotifyPropertyChanged
    {


        public double UkupnaCena { get; set; }

        private double ukupnaCenaSaPopustom;

        public double UkupnaCenaSaPopustom 
        {
            get { return ukupnaCenaSaPopustom; }
            set { SetAndNotify(ref ukupnaCenaSaPopustom, value); }
        }

        private bool vratiSeUglavniProzor = false;


        private bool koristeSePoeni;
        public bool KoristeSePoeni {
            get
            {
                return koristeSePoeni;
            }

            set
            {
                SetAndNotify(ref koristeSePoeni, value);
            }
        }

        private Korisnici tmpKorisnik;

        public Korisnici TmpKorisnik
        {
            get { return tmpKorisnik; }
            set { SetAndNotify(ref tmpKorisnik, value); }
        }

        

        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { korpa = value; }
        }

        public KupovinaProzor()
        {
            InitializeComponent();
            this.DataContext = this;
           
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.vratiSeUglavniProzor = false;
            this.Close();
        }

        private void btnPreskoci_Click(object sender, RoutedEventArgs e)
        {

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            DateTime datum = DateTime.Now;
            
            
            DbItemIstorijaKupovine d = new DbItemIstorijaKupovine()
                {
                    datum_prodaje = datum,
                    Kupac = null,
                    prodavac = new DbItemKorisnici() { id_korisnici = 1 },
                    ukupna_cena_kupovine = this.UkupnaCenaSaPopustom, broj_iskoriscenih_popust_poena = 0
                };
            List<DbItemKupljenaOpremaSaParametrima> listaKupljenjeOpreme = new List<DbItemKupljenaOpremaSaParametrima>();

            foreach (var item in this.Korpa)
            {
                Oprema o = item as Oprema;

                listaKupljenjeOpreme.Add(new DbItemKupljenaOpremaSaParametrima()
                    {
                        cena = o.Cena,
                        cena_opreme_kad_je_prodata = o.Cena,
                        id_oprema = o.IdOprema,
                        prodataKolicina = o.IzabranaKolicina
                    });
            }
                //SmartSoftwareServiceReference.DbItemKupljenaOpremaSaParametrima[] rez = service.ProdajaArtikla();
            
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rezultat = service.KorpaDelete(null);
            Xceed.Wpf.Toolkit.MessageBox.Show("Uspesno ste zavrsili Prodaju !");
            this.vratiSeUglavniProzor = true;
            this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();
            this.Close();
        }

        private void btnKupi_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            DateTime datum = DateTime.Now;
            DbItemIstorijaKupovine d = new DbItemIstorijaKupovine()
            {
                datum_prodaje = datum,
                Kupac = new DbItemKorisnici() { id_korisnici = tmpKorisnik.IdKorisnici },
                prodavac = new DbItemKorisnici() { id_korisnici = 1 },
                ukupna_cena_kupovine = this.UkupnaCenaSaPopustom,
                broj_iskoriscenih_popust_poena = tmpKorisnik.IzabranBrojPoenaZaPopust
            };
            List<DbItemKupljenaOpremaSaParametrima> listaKupljenjeOpreme = new List<DbItemKupljenaOpremaSaParametrima>();

            foreach (var item in this.Korpa)
            {
                Oprema o = item as Oprema;

                listaKupljenjeOpreme.Add(new DbItemKupljenaOpremaSaParametrima()
                {
                    cena = o.Cena,
                    cena_opreme_kad_je_prodata = o.Cena,
                    id_oprema = o.IdOprema,
                    prodataKolicina = o.IzabranaKolicina
                });
            }

            DbItemKupljenaOpremaSaParametrima [] kupljenaOprema = service.ProdajaArtikla(d, listaKupljenjeOpreme.ToArray());

            if(tmpKorisnik != null)
            {
                 
                int brojPoenaDodatno = 0;
                if(this.koristeSePoeni)
                {
                    brojPoenaDodatno = -tmpKorisnik.IzabranBrojPoenaZaPopust;
                }
                else
                {
                    brojPoenaDodatno = Convert.ToInt32(Math.Floor(UkupnaCena / 100));
                }

                DbItemKorisnici korisnik = new DbItemKorisnici()
                {
                    id_korisnici = tmpKorisnik.IdKorisnici,
                    brojOstvarenihPoena = brojPoenaDodatno
                };
                SmartSoftwareServiceReference.DbItemKorisnici[] rez = service.AzurirajBrojPoenaKorisnika(korisnik);
                Xceed.Wpf.Toolkit.MessageBox.Show("Uspesno ste zavrsili Prodaju za korisnika : !" + TmpKorisnik.ImeKorisnika + " !");

            }
            this.vratiSeUglavniProzor = true;
            this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(!this.vratiSeUglavniProzor)
            {
                KorpaProzor korpa = new KorpaProzor();
                korpa.Show();
            }
        }

        private void btnUcitajPodatkeOKupcu_Click(object sender, RoutedEventArgs e)
        {

            PodaciOKupcuProzor prozor = new PodaciOKupcuProzor();
            prozor.ShowDialog();

            this.TmpKorisnik = prozor.TmpKorisnik;
            
            if (TmpKorisnik.IdKorisnici != 0)
            {
                btnKupi.IsEnabled=true;
                this.gridPodaciOKupcu.Visibility = System.Windows.Visibility.Visible;

                tmpKorisnik.BrojPoenaZaPopust = Convert.ToInt32(Math.Floor(UkupnaCena / 2 / 10));

                if (tmpKorisnik.BrojPoenaZaPopust >= Math.Floor(tmpKorisnik.BrojOstvarenihPoena))
                {
                    tmpKorisnik.BrojPoenaZaPopust = Convert.ToInt32(tmpKorisnik.BrojOstvarenihPoena);
                }


                tmpKorisnik.IzabranBrojPoenaZaPopust = TmpKorisnik.BrojPoenaZaPopust;
                if (tmpKorisnik != null)
                {

                    UkupnaCenaSaPopustom = UkupnaCena - (10 * TmpKorisnik.IzabranBrojPoenaZaPopust);
                }
                else
                {
                    UkupnaCenaSaPopustom = UkupnaCena;
                }
            }
            else
            {
                btnKupi.IsEnabled = false;
                this.gridPodaciOKupcu.Visibility = System.Windows.Visibility.Hidden;
                lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Hidden;
                btnVrednostPoena.Visibility = System.Windows.Visibility.Hidden;
                lblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
                tblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
            }
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

        private void btnVrednostPoena_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBox txtBox = (TextBox)spinner.Content;

            int value = String.IsNullOrEmpty(txtBox.Text) ? 0 : Convert.ToInt32(txtBox.Text);

            if (e.Direction == SpinDirection.Increase && value < tmpKorisnik.BrojOstvarenihPoena && value < this.tmpKorisnik.BrojPoenaZaPopust)
                {
                    value = value + 1;
                }
                else
                {
                    if( value > 0)
                    {
                        value = value - 1;
                    }
                }
                txtBox.Text = value.ToString();

                Grid grid = spinner.Parent as Grid;

                int id = 0;
                TextBlock t = grid.FindName("skrivenId") as TextBlock;
                id = Convert.ToInt32(t.Text);

                if (value >= tmpKorisnik.BrojPoenaZaPopust || value >= tmpKorisnik.BrojOstvarenihPoena)
                {
                    spinner.ValidSpinDirection = ValidSpinDirections.Decrease;
                }
                else if (value == 0)
                {
                    spinner.ValidSpinDirection = ValidSpinDirections.Increase;
                }
                else
                {
                    ButtonSpinner b = new ButtonSpinner();
                    spinner.ValidSpinDirection = b.ValidSpinDirection;
                    b = null;
                }
                tmpKorisnik.IzabranBrojPoenaZaPopust = value;
            if(tmpKorisnik != null)
            {

                UkupnaCenaSaPopustom = UkupnaCena - (10 * TmpKorisnik.IzabranBrojPoenaZaPopust);
            }
            else
            {
                UkupnaCenaSaPopustom = UkupnaCena;
            }
            
        }

        private void PoeniKaoPopust_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if (check.IsChecked ?? false)
            {
                lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Visible;
                btnVrednostPoena.Visibility = System.Windows.Visibility.Visible;
                lblCenaSaPopustom.Visibility = System.Windows.Visibility.Visible;
                tblCenaSaPopustom.Visibility = System.Windows.Visibility.Visible;

                this.KoristeSePoeni = true;
            }
            else
            {
                lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Hidden;
                btnVrednostPoena.Visibility = System.Windows.Visibility.Hidden;
                lblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
                tblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;

                this.KoristeSePoeni = false;
            }
        }

    }
}
