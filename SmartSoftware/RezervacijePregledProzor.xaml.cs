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
    /// Interaction logic for RezervacijePregledProzor.xaml
    /// </summary>
    public partial class RezervacijePregledProzor : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Rezervacije> rezervacije = new ObservableCollection<Rezervacije>();

        public ObservableCollection<Rezervacije> Rezervacije
        {
            get { return rezervacije; }
            set { SetAndNotify(ref rezervacije, value); }
        }

        private Rezervacije tmpRezervacija = new Rezervacije();
        private Rezervacije rezervacija;
        private ICollectionView documentsView;
        
        

        public RezervacijePregledProzor()
        {
            documentsView = CollectionViewSource.GetDefaultView(this.Rezervacije);
            rezervacija = documentsView.CurrentItem as Rezervacije;
            this.tmpRezervacija = rezervacija;

            InitializeComponent();
            this.DataContext = this;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemRezervacijaSaOpremom[] rezervacije = service.RezervacijeSelect();
            this.PopuniRezervacije(rezervacije);
        }

        

        private void PopuniRezervacije(DbItemRezervacijaSaOpremom[] ListaRezervacija)
        {
            this.Rezervacije = new ObservableCollection<Rezervacije>();

            for (int i = 0; i < ListaRezervacija.Length; i++)
            {
                Rezervacije r = new Rezervacije()
                {
                    DatumAzuriranjaRezervacije = ListaRezervacija[i].datum_azuriranja_rezervacije, DatumIstekaRezervacije = ListaRezervacija[i].datum_isteka_rezervacije, DatumRezervacije = ListaRezervacija[i].datum_rezervacije, IdRerezervacije = ListaRezervacija[i].id_rezervacije, Ime = ListaRezervacija[i].imeNaRezervacija
                };

                for (int j = 0; j < ListaRezervacija[i].ListaOpremeZaRezervaciju.Length; j++)
                {
                    DbItemOpremaSaParametrima o = ListaRezervacija[i].ListaOpremeZaRezervaciju[j];
                    r.Oprema.Add(new Oprema(null)
                    {
                        Cena = o.cena,
                        IdOprema = o.id_oprema,
                        IdTipOpreme = o.id_tip_opreme,
                        KolicinaNaLageru = o.kolicina_na_lageru,
                        KolicinaURezervi = o.kolicina_u_rezervi,
                        Lager = o.lager,
                        Model = o.model,
                        Name = o.naslov,
                        Opis = o.opis,
                        OpremaNaPopustu = o.oprema_na_popustu,
                        Proizvodjac = o.proizvodjac,
                        Slika = o.slika,
                        SlikaOriginalPutanja = o.slikaOriginalPutanja,
                        IzabranaKolicina = 1,
                        DaliMozeJosDaseDoda = o.kolicina_na_lageru > 0,
                        TmpIzabranaKolicina = o.kolicinaURezervacijama
                    });

                    for (int k = 0; k < o.ListaParametara.Length; k++)
                    {
                        DbItemParametri p = o.ListaParametara[k];
                        r.Oprema[j].ListaParametara.Add(new Parametri(null)
                        {
                            DefaultVrednost = p.default_vrednost,
                            IdParametri = p.id_parametri,
                            IdTipOpreme = p.id_tip_opreme,
                            VrednostParametra = p.vrednost_parametra,
                            Name = p.naziv_parametra
                        });
                    }
                }
                this.Rezervacije.Add(r);
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

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

    }
}
