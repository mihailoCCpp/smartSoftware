using Microsoft.Win32;
using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using SmartSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SmartSoftware
{
        
 

    public enum TipParametara
    {
        //comboBox1.DataSource = Enum.GetValues(typeof(BlahEnum));
        STRING,
        BIT,
        TEXT,
        INT,
    }

    public partial class AdminPanelProzor : Window, INotifyPropertyChanged
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private bool ucitanProzorPoPrviPut = false;
        ICollectionView oblasti;



        private ObservableCollection<String> listaUloga = new ObservableCollection<String>() { "Administrator", "Prodavac", "Kupac" };

        public ObservableCollection<String> ListaUloga
        {
            get { return listaUloga; }
            set { listaUloga = value; }
        }


        OblastiOpreme oo;
        Oprema o;
        TipoviOpreme t;
        Korisnici k;
        IstorijaKupovine i;
        Kupci ku;


        private ObservableCollection<String> listaUpitaZaKupca = new ObservableCollection<string>() { "IstorijaKupovineInicijalniPrikazKupaca", "IstorijaKupovineNajcesciKupci", "IstorijaKupovineKupciKojiNajviseKupuju", "IstorijaKupovineKupciKojiNajviseTrose" };

        public ObservableCollection<String> ListaUpitaZaKupca
        {
            get { return listaUpitaZaKupca; }
            //set { listaUpitaZaKupca = value; }
        }


        private int? izabranaOblastOpreme;
        public int? IzabranaOblastOpreme
        {
            get { return izabranaOblastOpreme; }
            set { SetAndNotify(ref izabranaOblastOpreme, value); }
        }

        private Parametri currentParametri;

        public Parametri CurrentParametri
        {
            get { return currentParametri; }
            set { SetAndNotify(ref currentParametri, value); }
        }

        ObservableCollection<OblastiOpreme> listaOblastiOpreme = new ObservableCollection<OblastiOpreme>();

        public ObservableCollection<OblastiOpreme> ListaOblastiOpreme
        {
            get { return listaOblastiOpreme; }
            set { SetAndNotify(ref listaOblastiOpreme, value); }
        }

        ObservableCollection<TipoviOpreme> listaTipovaOpreme = new ObservableCollection<TipoviOpreme>();

        public ObservableCollection<TipoviOpreme> ListaTipovaOpreme
        {
            get { return listaTipovaOpreme; }
            set { SetAndNotify(ref listaTipovaOpreme, value); }
        }

        private ObservableCollection<TipoviKofiguracije> listaTipovaZaKonfiguraciju = new ObservableCollection<TipoviKofiguracije>();

        public ObservableCollection<TipoviKofiguracije> ListaTipovaZaKonfiguraciju
        {
          get { return listaTipovaZaKonfiguraciju; }
          set { SetAndNotify(ref listaTipovaZaKonfiguraciju, value); }
        }

        ObservableCollection<Parametri> listaParametara = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaParametara
        {
            get { return listaParametara; }
            set { SetAndNotify(ref listaParametara, value); }
        }


        ObservableCollection<String> listaTipovaProvere = new ObservableCollection<String>()
            {
                "Upoređivanje", "Prikaz" 
            };

        public ObservableCollection<String> ListaTipovaProvere
        {
          get { return listaTipovaProvere; }
          set { SetAndNotify(ref listaTipovaProvere, value); }
        }   
        

        ObservableCollection<Parametri> listaParametaraPomocno = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaParametaraPomocno
        {
            get { return listaParametaraPomocno; }
            set { SetAndNotify(ref listaParametaraPomocno, value); }
        }
        
        ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            set { SetAndNotify(ref listaKorisnika, value); }
        }

        ObservableCollection<IstorijaKupovine> listaIstorijeKupovine = new ObservableCollection<IstorijaKupovine>();

        public ObservableCollection<IstorijaKupovine> ListaIstorijeKupovine
        {
            get { return listaIstorijeKupovine; }
            set { SetAndNotify(ref listaIstorijeKupovine, value); }
        }

        private ObservableCollection<Narudzbina> listaNarudzbina = new ObservableCollection<Narudzbina>();

        public ObservableCollection<Narudzbina> ListaNarudzbina
        {
            get { return listaNarudzbina; }
            set { SetAndNotify(ref listaNarudzbina, value); }
        }

        private ObservableCollection<GrupaOgranicenja> listaOgranicenja = new ObservableCollection<GrupaOgranicenja>();

        public ObservableCollection<GrupaOgranicenja> ListaOgranicenja
        {
            get { return listaOgranicenja; }
            set { SetAndNotify(ref listaOgranicenja, value); }
        }

        private ObservableCollection<GrupaOgranicenja> listaTrenutnaGrupeOgranicenjaZaTipOpreme = new ObservableCollection<GrupaOgranicenja>();

        public ObservableCollection<GrupaOgranicenja> ListaTrenutnaGrupeOgranicenjaZaTipOpreme
        {
            get { return listaTrenutnaGrupeOgranicenjaZaTipOpreme; }
            set { SetAndNotify(ref listaTrenutnaGrupeOgranicenjaZaTipOpreme, value); }
        }

        private GrupaOgranicenja currentOgranicenjeKolekcije = new GrupaOgranicenja();
        
        public GrupaOgranicenja CurrentOgranicenjeKolekcije
        {
            get { return currentOgranicenjeKolekcije; }
            set { SetAndNotify(ref currentOgranicenjeKolekcije, value); }
        }


        ObservableCollection<Oprema> listaOpreme = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> ListaOpreme
        {
            get { return listaOpreme; }
            set { SetAndNotify(ref listaOpreme, value); }
        }

        ObservableCollection<Kupci> listaKupaca = new ObservableCollection<Kupci>();

        public ObservableCollection<Kupci> ListaKupaca
        {
            get { return listaKupaca; }
            set { SetAndNotify(ref listaKupaca, value); }
        }
        

        private OblastiOpreme currentOblastiOpreme;

        public OblastiOpreme CurrentOblastiOpreme
        {
            get { return currentOblastiOpreme; }
            set { SetAndNotify(ref currentOblastiOpreme,value); }
        }

        private TipoviOpreme currentTipoviOpreme;

        public TipoviOpreme CurrentTipoviOpreme
        {
            get { return currentTipoviOpreme; }
            set { SetAndNotify(ref currentTipoviOpreme, value); }
        }

        private Korisnici currentKorisnici;

        public Korisnici CurrentKorisnici
        {
            get { return currentKorisnici; }
            set { SetAndNotify(ref currentKorisnici, value); }
        }

        private Oprema currentOprema;

        public Oprema CurrentOprema
        {
            get { return currentOprema; }
            set { SetAndNotify(ref currentOprema, value); }
        }

        private Kupci currentKupci;

        public Kupci CurrentKupci
        {
            get { return currentKupci; }
            set { SetAndNotify(ref currentKupci, value); }
        }

        private bool daLiJeSlikaPromenjena = false;

        public bool DaLiJeSlikaPromenjena
        {
            get { return daLiJeSlikaPromenjena; }
            set { daLiJeSlikaPromenjena = value; }
        }
        //private int vreme = 5;

        //private DispatcherTimer timer;
        private int odakleKrece = 0;
        private int dokleIde = 4;



        public AdminPanelProzor()
        {

            InitializeComponent();
            this.DataContext = this;

            btnOblastiOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");
            
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpreme();
            this.popuniListuOblasti(nizOblasti);
           

            
        }

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    if (vreme > 0) {
        //        vreme--;

        //    }
        //    else
        //    {
        //        timer.Stop();
                
        //    }
        //}

        
        private void btnIzaberiOblastOpreme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GridPrikazRezultataOblastiOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosOblastiOpreme = template.FindName("ctmUnosOblastiOpreme", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosOblastiOpreme.Visibility = Visibility.Hidden;

            oblasti = CollectionViewSource.GetDefaultView(this.ListaOblastiOpreme);
            oo = oblasti.CurrentItem as OblastiOpreme;
            this.CurrentOblastiOpreme = oo;
            this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = false;
            
            template = null;

        }

        private void btnOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in grdDugmici.Children)
            {
               (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnOblastiOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");
            
            ListaOblastiOpreme = null;
            ListaOblastiOpreme = new ObservableCollection<OblastiOpreme>();

           
           
            
               Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeOblastimaOpreme");
               SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
               SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpreme();
               this.popuniListuOblasti(nizOblasti);


               Sadrzaj.Visibility = Visibility.Visible;
               grdIstorijaKupovine.Visibility = Visibility.Hidden;

            
        }

        private void popuniListuOblasti(DbItemOblastiOpreme[] nizOblasti)
        {
            ListaOblastiOpreme = null;
            ListaOblastiOpreme = new ObservableCollection<OblastiOpreme>();
            for (int i = 0; i < nizOblasti.Length; i++)
            {
                ListaOblastiOpreme.Add(new OblastiOpreme(null)
                {
                    Name = nizOblasti[i].naziv_oblasti_opreme,
                    IdOblastiOpreme = nizOblasti[i].id_oblasti_opreme,
                    Picture = nizOblasti[i].picture,
                    SlikaOriginalPutanja = nizOblasti[i].SlikaOriginalPutanja,
                    NazivOblastiOpreme = nizOblasti[i].naziv_oblasti_opreme,
                    DeletedItem = nizOblasti[i].DeletedField
                });
            }
        }

        private void popuniListuTipovaOpreme(DbItemTipOpreme[] nizTipovaOpreme)
        {
            ListaTipovaOpreme = null;
            ListaTipovaOpreme = new ObservableCollection<TipoviOpreme>();
            for (int i = 0; i < nizTipovaOpreme.Length; i++)
            {
                ListaTipovaOpreme.Add(new TipoviOpreme(null)
                {
                    Name = nizTipovaOpreme[i].naziv_tipa,
                    IdOblastiOpreme = nizTipovaOpreme[i].id_oblasti_opreme,
                    IdTipOpreme = nizTipovaOpreme[i].id_tip_opreme,
                    Picture = nizTipovaOpreme[i].slika_tipa,
                    SlikaOriginalPutanja = nizTipovaOpreme[i].SlikaOriginalPutanja,
                    StaroImeTipa = nizTipovaOpreme[i].naziv_tipa,
                    NazivOblastiOpreme = nizTipovaOpreme[i].naziv_oblasti_opreme,
                    DeletedItem = nizTipovaOpreme[i].DeletedField
                });
                
                
            }
        }

        private void popuniListuKorisnici(DbItemKorisnici[] nizKorisnika)
        {
            ListaKorisnika = null;
            ListaKorisnika = new ObservableCollection<Korisnici>();
            for (int i = 0; i < nizKorisnika.Length; i++)
            {
                ListaKorisnika.Add(new Korisnici()
                {
                    IdKorisnici = nizKorisnika[i].id_korisnici,
                    IdUloge = nizKorisnika[i].id_uloge,
                    IzabranaUloga = nizKorisnika[i].id_uloge - 1,
                    ImeKorisnika = nizKorisnika[i].ime,
                    PrezimeKorisnika = nizKorisnika[i].prezime,
                    MejlKorisnika = nizKorisnika[i].mejl,
                    BrojTelefonaKorisnika = nizKorisnika[i].broj_telefona,
                    BrojOstvarenihPoena = Convert.ToDouble(nizKorisnika[i].brojOstvarenihPoena),
                    Username = nizKorisnika[i].username,
                    Lozinka = nizKorisnika[i].lozinka,
                    NazivUloge = nizKorisnika[i].naziv_uloge,
                    DeletedItem = nizKorisnika[i].deletedField
                });
            }
        }

        private void popuniListuNarudzbina(DbItemNarudzbine[] nizNarudzbina)
        {
            ListaNarudzbina = new ObservableCollection<Narudzbina>();
            ObservableCollection<Narudzbina> TrenutnaListaNarudzbina = new ObservableCollection<Narudzbina>();
            odakleKrece = 0;
            dokleIde = 4;

            for (int i = odakleKrece; i < dokleIde; i++)
            {
                Narudzbina n = new Narudzbina()
                    {
                        RedniBrojNarudzbine = i+1,
                        DatumNarudzbine = nizNarudzbina[i].datum_narudzbine,
                        NarucenaKolicina = nizNarudzbina[i].kolicina,
                        Prodavac = new Korisnici()
                        {
                            IdKorisnici = nizNarudzbina[i].prodavac.id_korisnici,
                            IdUloge = nizNarudzbina[i].prodavac.id_uloge,
                            IzabranaUloga = nizNarudzbina[i].prodavac.id_uloge - 1,
                            ImeKorisnika = nizNarudzbina[i].prodavac.ime,
                            PrezimeKorisnika = nizNarudzbina[i].prodavac.prezime,
                            MejlKorisnika = nizNarudzbina[i].prodavac.mejl,
                            BrojTelefonaKorisnika = nizNarudzbina[i].prodavac.broj_telefona,
                            BrojOstvarenihPoena = Convert.ToDouble(nizNarudzbina[i].prodavac.brojOstvarenihPoena),
                            Username = nizNarudzbina[i].prodavac.username,
                            Lozinka = nizNarudzbina[i].prodavac.lozinka,
                            NazivUloge = nizNarudzbina[i].prodavac.naziv_uloge,
                            DeletedItem = nizNarudzbina[i].prodavac.deletedField
                        },
                        Oprema = new Oprema(null)
                        {
                            Cena = nizNarudzbina[i].narucenaOprema.cena,
                            IdOprema = nizNarudzbina[i].narucenaOprema.id_oprema,
                            IdTipOpreme = nizNarudzbina[i].narucenaOprema.id_tip_opreme,
                            KolicinaNaLageru = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru,
                            KolicinaURezervi = nizNarudzbina[i].narucenaOprema.kolicina_u_rezervi,
                            Lager = nizNarudzbina[i].narucenaOprema.lager,
                            Model = nizNarudzbina[i].narucenaOprema.model,
                            Name = nizNarudzbina[i].narucenaOprema.naslov,
                            Opis = nizNarudzbina[i].narucenaOprema.opis,
                            OpremaNaPopustu = nizNarudzbina[i].narucenaOprema.oprema_na_popustu,
                            Proizvodjac = nizNarudzbina[i].narucenaOprema.proizvodjac,
                            Picture = nizNarudzbina[i].narucenaOprema.slika,
                            Slika = nizNarudzbina[i].narucenaOprema.slika,
                            SlikaOriginalPutanja = nizNarudzbina[i].narucenaOprema.slikaOriginalPutanja,
                            IzabranaKolicina = 1,
                            DaliMozeJosDaseDoda = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru > 0,
                            DeletedItem = nizNarudzbina[i].narucenaOprema.DeletedField,

                        }

                    };
                TrenutnaListaNarudzbina.Add(n);
            }

            ListaNarudzbina = TrenutnaListaNarudzbina;



            //timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 1);
            //timer.Tick += timer_Tick;
            //timer.Start();

            //foreach (var item in nizNarudzbina)
            //{
            //    Narudzbina n = new Narudzbina()
            //        {

            //            DatumNarudzbine = item.datum_narudzbine,
            //            NarucenaKolicina = item.kolicina,
            //            Prodavac = new Korisnici()
            //            {
            //                IdKorisnici = item.prodavac.id_korisnici,
            //                IdUloge = item.prodavac.id_uloge,
            //                IzabranaUloga = item.prodavac.id_uloge - 1,
            //                ImeKorisnika = item.prodavac.ime,
            //                PrezimeKorisnika = item.prodavac.prezime,
            //                MejlKorisnika = item.prodavac.mejl,
            //                BrojTelefonaKorisnika = item.prodavac.broj_telefona,
            //                BrojOstvarenihPoena = Convert.ToDouble(item.prodavac.brojOstvarenihPoena),
            //                Username = item.prodavac.username,
            //                Lozinka = item.prodavac.lozinka,
            //                NazivUloge = item.prodavac.naziv_uloge,
            //                DeletedItem = item.prodavac.deletedField
            //            },
            //            Oprema = new Oprema(null)
            //            {
            //                Cena = item.narucenaOprema.cena,
            //                IdOprema = item.narucenaOprema.id_oprema,
            //                IdTipOpreme = item.narucenaOprema.id_tip_opreme,
            //                KolicinaNaLageru = item.narucenaOprema.kolicina_na_lageru,
            //                KolicinaURezervi = item.narucenaOprema.kolicina_u_rezervi,
            //                Lager = item.narucenaOprema.lager,
            //                Model = item.narucenaOprema.model,
            //                Name = item.narucenaOprema.naslov,
            //                Opis = item.narucenaOprema.opis,
            //                OpremaNaPopustu = item.narucenaOprema.oprema_na_popustu,
            //                Proizvodjac = item.narucenaOprema.proizvodjac,
            //                Picture = item.narucenaOprema.slika,
            //                Slika = item.narucenaOprema.slika,
            //                SlikaOriginalPutanja = item.narucenaOprema.slikaOriginalPutanja,
            //                IzabranaKolicina = 1,
            //                DaliMozeJosDaseDoda = item.narucenaOprema.kolicina_na_lageru > 0,
            //                DeletedItem = item.narucenaOprema.DeletedField,

            //            }

            //        };
            //        ListaNarudzbina.Add(n);

            //}
        }

        private void popuniListuNarudzbinaOpet(DbItemNarudzbine[] nizNarudzbina, int odakleKrece, int dokleIde)
        {

            int broj = nizNarudzbina.Length;
            if (broj < dokleIde)
            {
                dokleIde = broj;
            }
            for (int i = odakleKrece; i < dokleIde; i++)
            {
                Narudzbina n = new Narudzbina()
                    {
                        RedniBrojNarudzbine = i+1,
                        DatumNarudzbine = nizNarudzbina[i].datum_narudzbine,
                        NarucenaKolicina = nizNarudzbina[i].kolicina,
                        Prodavac = new Korisnici()
                        {
                            IdKorisnici = nizNarudzbina[i].prodavac.id_korisnici,
                            IdUloge = nizNarudzbina[i].prodavac.id_uloge,
                            IzabranaUloga = nizNarudzbina[i].prodavac.id_uloge - 1,
                            ImeKorisnika = nizNarudzbina[i].prodavac.ime,
                            PrezimeKorisnika = nizNarudzbina[i].prodavac.prezime,
                            MejlKorisnika = nizNarudzbina[i].prodavac.mejl,
                            BrojTelefonaKorisnika = nizNarudzbina[i].prodavac.broj_telefona,
                            BrojOstvarenihPoena = Convert.ToDouble(nizNarudzbina[i].prodavac.brojOstvarenihPoena),
                            Username = nizNarudzbina[i].prodavac.username,
                            Lozinka = nizNarudzbina[i].prodavac.lozinka,
                            NazivUloge = nizNarudzbina[i].prodavac.naziv_uloge,
                            DeletedItem = nizNarudzbina[i].prodavac.deletedField
                        },
                        Oprema = new Oprema(null)
                        {
                            Cena = nizNarudzbina[i].narucenaOprema.cena,
                            IdOprema = nizNarudzbina[i].narucenaOprema.id_oprema,
                            IdTipOpreme = nizNarudzbina[i].narucenaOprema.id_tip_opreme,
                            KolicinaNaLageru = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru,
                            KolicinaURezervi = nizNarudzbina[i].narucenaOprema.kolicina_u_rezervi,
                            Lager = nizNarudzbina[i].narucenaOprema.lager,
                            Model = nizNarudzbina[i].narucenaOprema.model,
                            Name = nizNarudzbina[i].narucenaOprema.naslov,
                            Opis = nizNarudzbina[i].narucenaOprema.opis,
                            OpremaNaPopustu = nizNarudzbina[i].narucenaOprema.oprema_na_popustu,
                            Proizvodjac = nizNarudzbina[i].narucenaOprema.proizvodjac,
                            Picture = nizNarudzbina[i].narucenaOprema.slika,
                            Slika = nizNarudzbina[i].narucenaOprema.slika,
                            SlikaOriginalPutanja = nizNarudzbina[i].narucenaOprema.slikaOriginalPutanja,
                            IzabranaKolicina = 1,
                            DaliMozeJosDaseDoda = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru > 0,
                            DeletedItem = nizNarudzbina[i].narucenaOprema.DeletedField,

                        }

                    };
                ListaNarudzbina.Add(n);
            }
            if (broj != dokleIde)
            {
                

                //vreme = 5;
                //timer = new DispatcherTimer();
                //timer.Interval = new TimeSpan(0, 0, 1);
                //timer.Tick += timer_Tick;
                //timer.Start();
            }
            else
            {
                ControlTemplate template = Sadrzaj.Template;
                Button dugmeUcitajJos = template.FindName("btnUcitajJosNarudzbina", Sadrzaj) as Button;
                dugmeUcitajJos.Visibility= Visibility.Hidden;
                odakleKrece = 0;
                dokleIde = 4;
            }
            
            
        }

        private void popuniListuOgranicenja(DbItemGrupeOgranicenja[] nizOgranicenja)
        {
            if(nizOgranicenja.Length > 0)
            {
                this.CurrentTipoviOpreme = new TipoviOpreme(null)
                {
                    IdTipOpreme = nizOgranicenja[0].id_tip_opreme_kolekcije
                };
            }
            else
            {
                return;
            }

            ListaOgranicenja = new ObservableCollection<GrupaOgranicenja>();
            foreach (var item in nizOgranicenja)
            {
                GrupaOgranicenja grupaOgranicenja = new GrupaOgranicenja()
                {
                    Id_grupe_ogranicenja = item.Id_grupe_ogranicenja,
                    Id_tip_opreme_kolekcije = item.id_tip_opreme_kolekcije,
                    Id_parametra1 = item.id_parametra1,
                    Id_parametra2 = item.id_parametra2,
                    Id_tip_opreme1 = item.id_tip_opreme1,
                    Id_tip_opreme2 = item.id_tip_opreme2,
                    TipProvere = item.tipProvere,
                    NazivParametra1 = item.nazivParametra1,
                    NazivParametra2 = item.nazivParametra2,
                    NazivTipaOpreme1 = item.nazivTipaOpreme1,
                    NazivTipaOpreme2 = item.nazivTipaOpreme2
                };
                ListaOgranicenja.Add(grupaOgranicenja);
            }
        }

        private void popuniListuOpremeSaParametrima(DbItemOpremaSaParametrima[] nizOpremaSaParametrima)
        {
            ListaOpreme = null;
            ListaOpreme = new ObservableCollection<Oprema>();
            for (int i = 0; i < nizOpremaSaParametrima.Length; i++)
            {
                if (nizOpremaSaParametrima[i] != null) 
                {
                    
                }

                Oprema o = new Oprema(null)
                {
                    Cena = nizOpremaSaParametrima[i].cena,
                    IdOprema = nizOpremaSaParametrima[i].id_oprema,
                    IdTipOpreme = nizOpremaSaParametrima[i].id_tip_opreme,
                    KolicinaNaLageru = nizOpremaSaParametrima[i].kolicina_na_lageru,
                    KolicinaURezervi = nizOpremaSaParametrima[i].kolicina_u_rezervi,
                    Lager = nizOpremaSaParametrima[i].lager,
                    Model = nizOpremaSaParametrima[i].model,
                    Name = nizOpremaSaParametrima[i].naslov,
                    Opis = nizOpremaSaParametrima[i].opis,
                    OpremaNaPopustu = nizOpremaSaParametrima[i].oprema_na_popustu,
                    Proizvodjac = nizOpremaSaParametrima[i].proizvodjac,
                    Picture = nizOpremaSaParametrima[i].slika,
                    Slika = nizOpremaSaParametrima[i].slika,
                    SlikaOriginalPutanja = nizOpremaSaParametrima[i].slikaOriginalPutanja,
                    IzabranaKolicina = 1,
                    DaliMozeJosDaseDoda = nizOpremaSaParametrima[i].kolicina_na_lageru > 0,
                    DeletedItem = nizOpremaSaParametrima[i].DeletedField,
                };

                DbItemOpremaSaParametrimaStatistika opremaStatistika = (nizOpremaSaParametrima[i] as DbItemOpremaSaParametrimaStatistika);

                if(opremaStatistika != null)
                {
                    o.KolikoPutajeProdata = opremaStatistika.kolkoPutaJeProdavata;
                }

                ListaOpreme.Add(o);

                for (int j = 0; j < nizOpremaSaParametrima[i].ListaParametara.Length; j++)
                {
                    ListaOpreme[i].ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = nizOpremaSaParametrima[i].ListaParametara[j].default_vrednost,
                        IdParametri = nizOpremaSaParametrima[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = nizOpremaSaParametrima[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra,
                        Name = nizOpremaSaParametrima[i].ListaParametara[j].naziv_parametra,
                        TipParametra = nizOpremaSaParametrima[i].ListaParametara[j].tipParametra
                    });
                }
                
            }
        }

        private void popuniListuIstorijeKupovine(DbItemIstorijaKupovine[] nizIstorijeKupovine)
        {
            ListaIstorijeKupovine = null;
            ListaIstorijeKupovine = new ObservableCollection<IstorijaKupovine>();
            
            for (int i = 0; i < nizIstorijeKupovine.Length; i++)
            {

                
                


                ListaIstorijeKupovine.Add(new IstorijaKupovine()
                {
                    Datum_prodaje = nizIstorijeKupovine[i].datum_prodaje,
                    IdIstorijaKupovine = nizIstorijeKupovine[i].id_istorija_kupovine,
                    Broj_iskoriscenih_popust_poena = nizIstorijeKupovine[i].broj_iskoriscenih_popust_poena,
                    Kupac = nizIstorijeKupovine[i].Kupac == null? null : new Korisnici(){
                        
                        BrojTelefonaKorisnika = nizIstorijeKupovine[i].Kupac.broj_telefona,
                        IdKorisnici = nizIstorijeKupovine[i].Kupac.id_korisnici,
                        ImeKorisnika = nizIstorijeKupovine[i].Kupac.ime,
                        PrezimeKorisnika = nizIstorijeKupovine[i].Kupac.prezime,
                        MejlKorisnika = nizIstorijeKupovine[i].Kupac.mejl
                        
                    },
                    Prodavac = nizIstorijeKupovine[i].prodavac == null ? null : new Korisnici()
                    {

                        BrojTelefonaKorisnika = nizIstorijeKupovine[i].Kupac.broj_telefona,
                        IdKorisnici = nizIstorijeKupovine[i].Kupac.id_korisnici,
                        ImeKorisnika = nizIstorijeKupovine[i].Kupac.ime,
                        PrezimeKorisnika = nizIstorijeKupovine[i].Kupac.prezime,
                        MejlKorisnika = nizIstorijeKupovine[i].Kupac.mejl,
                        IdUloge = nizIstorijeKupovine[i].Kupac.id_uloge,
                        NazivUloge = nizIstorijeKupovine[i].Kupac.naziv_uloge,
                        Username = nizIstorijeKupovine[i].Kupac.username,
                        Lozinka = nizIstorijeKupovine[i].Kupac.lozinka
                    },
                    
                    Ukupna_cena_kupovine = nizIstorijeKupovine[i].ukupna_cena_kupovine
                   
                });


                for (int j = 0; j < nizIstorijeKupovine[i].ListaKupljeneOpreme.Length; j++)
                {
                    //ListaIstorijeKupovine[i].ListaKupljeneOpreme = new ObservableCollection<KupljenaOprema>();

                    KupljenaOprema o = new KupljenaOprema(null)
                    {
                        Cena = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].cena,
                        IdOprema = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].id_oprema,
                        IdTipOpreme = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].id_tip_opreme,
                        KolicinaNaLageru = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicina_na_lageru + nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicinaUKorpi,
                        KolicinaURezervi = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicina_u_rezervi,
                        Lager = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].lager,
                        Model = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].model,
                        Name = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].naslov,
                        Opis = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].opis,
                        OpremaNaPopustu = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].oprema_na_popustu,
                        Proizvodjac = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].proizvodjac,
                        Slika = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].slika,
                        SlikaOriginalPutanja = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].slikaOriginalPutanja,
                        IzabranaKolicina = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicinaUKorpi,
                        ProdataKolicina = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].prodataKolicina,
                        Popust_na_cenu = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].popust_na_cenu,
                        Cena_opreme_kad_je_prodata = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].cena_opreme_kad_je_prodata

                    };

                    for (int k = 0; k < nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara.Length; k++)
                    {
                        o.ListaParametara.Add(new Parametri(null)

                        {
                            DefaultVrednost = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].default_vrednost,
                            IdParametri = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].id_parametri,
                            IdTipOpreme = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].id_tip_opreme,
                            VrednostParametra = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].vrednost_parametra,
                            Name = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].naziv_parametra
                        });

                    }

                    ListaIstorijeKupovine[i].ListaKupljeneOpreme.Add(o);
                }
            }
        }
        

        private void popuniListuKupaca(DbItemKupci[] nizKupaca)
        {
            
            ListaKupaca = new ObservableCollection<Kupci>();
            Kupci k;
            foreach (var kupac in nizKupaca)
	        {
                k = new Kupci()
                {
                    ImeKorisnika = kupac.ime,
                    PrezimeKorisnika = kupac.prezime,
                    NajvisePotrosio = kupac.ukupnoPotrosio,
                    BrojKupovina = kupac.brojKupovina,
                    BrojKupljeneOpreme = kupac.brojKupovina
                };
                foreach (var item in kupac.ListaKupovina)
	            {
                    KupljenaOprema ku;
                    IstorijaKupovine ik = new IstorijaKupovine()
                    {
                        Broj_iskoriscenih_popust_poena = item.broj_iskoriscenih_popust_poena,
                        IdIstorijaKupovine = item.id_istorija_kupovine,
                        Datum_prodaje = item.datum_prodaje,
                        Ukupna_cena_kupovine = item.ukupna_cena_kupovine
                    };
                    foreach (var jednaKupljenaOprema in item.ListaKupljeneOpreme)
	                {
                        Parametri p ;

                        ku = new KupljenaOprema(null)
                        {
                            Cena = jednaKupljenaOprema.cena,
                            Cena_opreme_kad_je_prodata = jednaKupljenaOprema.cena_opreme_kad_je_prodata,
                            DeletedItem = jednaKupljenaOprema.DeletedField,
                            Picture = jednaKupljenaOprema.slika,
                            Slika = jednaKupljenaOprema.slika,
                            Model = jednaKupljenaOprema.model,
                            Opis = jednaKupljenaOprema.opis,
                            Name= jednaKupljenaOprema.naslov,
                            Popust_na_cenu = jednaKupljenaOprema.popust_na_cenu,
                            Proizvodjac = jednaKupljenaOprema.proizvodjac,
                            IdOprema = jednaKupljenaOprema.id_oprema,
                            Id_istorija_kupovine = jednaKupljenaOprema.id_istorija_kupovine,
                            IdTipOpreme = jednaKupljenaOprema.id_tip_opreme,
                            IzabranaKolicina = Convert.ToInt32(jednaKupljenaOprema.prodataKolicina)
                        };

                        foreach (var parametar in jednaKupljenaOprema.ListaParametara)
	                    {
                            p =  new Parametri(null){
                                            

                                DefaultVrednost = parametar.default_vrednost,
                                DeletedItem = parametar.deletedField,
                                IdParametri = parametar.id_parametri,
                                IdTipOpreme= parametar.id_tip_opreme,
                                IzabranTipOpreme = parametar.id_tip_opreme,
                                Name = parametar.naziv_parametra,
                                ParametarJeIFilter = parametar.za_filter,
                                TipParametra = parametar.tipParametra,
                                VrednostParametra = parametar.vrednost_parametra
                                            
                            };
                            ku.ListaParametara.Add(p);         
	                    }



                        ik.ListaKupljeneOpreme.Add(ku);
	                }

                    k.ListaIstorijeKupovine.Add(ik);
                    
                    
                }
                ListaKupaca.Add(k);
                
	        }

        }       

        private void popuniListuTipovaOpremeZaKonfiguracijuOgranicenja(DbItemTipoviZaKonfiguraciju[] nizTipovaOpremeZaKonfiguraciju)
        {
            ListaTipovaZaKonfiguraciju = new ObservableCollection<TipoviKofiguracije>();

            int duzinaNiza = nizTipovaOpremeZaKonfiguraciju.Length;
            ObservableCollection<string> ListaBrojevaZaRedosled = new ObservableCollection<string>();
            for (int i = 0; i <duzinaNiza ; i++)
            {
                ListaBrojevaZaRedosled.Add((i + 1).ToString());
            }
            

            for (int i = 0; i < nizTipovaOpremeZaKonfiguraciju.Length; i++)
			{
                TipoviKofiguracije tk = new TipoviKofiguracije(null)
                {
                    IdTipOpremeKolekcije = nizTipovaOpremeZaKonfiguraciju[i].idTipOpremeKolekcije,
                    Name = nizTipovaOpremeZaKonfiguraciju[i].naziv_tipa,
                    IzabranZaKonfiguraciju = nizTipovaOpremeZaKonfiguraciju[i].izabranZaKonfiguraciju,
                    IdTipOpreme = nizTipovaOpremeZaKonfiguraciju[i].id_tip_opreme,
                    RedosledPrikazivanja = nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja,
                    IzabranRedosled = nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja == null || nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja == 0 ?
                    0 : nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja - 1,
                    ListaBrojevaZaRedosled = ListaBrojevaZaRedosled,
                    MogucaKolicinaUnosa = nizTipovaOpremeZaKonfiguraciju[i].moguca_kolicina_unosa
                };
                ListaTipovaZaKonfiguraciju.Add(tk);
			}

        }

        


        private void btnUpravljanjeTipovimaOpreme_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljanjeTipovimaOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeTipovimaOpreme");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);
            Sadrzaj.Visibility = Visibility.Visible;
            grdIstorijaKupovine.Visibility = Visibility.Hidden;

        }

        private void btnUpravljanjeOpremom_Click(object sender, RoutedEventArgs e)
        {
            
            Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeOpremom");
            

            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljanjeOpremom.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanel();
            this.popuniListuOpremeSaParametrima(nizOpreme);
            Sadrzaj.Visibility = Visibility.Visible;
            grdIstorijaKupovine.Visibility = Visibility.Hidden;
           
        }
        
            
            
            
            
        
        private void btnUpravljanjeKorisnici_Click(object sender, RoutedEventArgs e)
        {
            Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeKorisnicima");
            ControlTemplate template = Sadrzaj.Template;

            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljanjeKorisnici.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");
           
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnici = service.PrikaziZaposleneKorisnike(null);
            this.popuniListuKorisnici(nizKorisnici);
            Sadrzaj.Visibility = Visibility.Visible;
            grdIstorijaKupovine.Visibility = Visibility.Hidden;
        }


        private void btnUpravljanjeFilterima_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljanjeFilterima.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeParametrima");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.SelectAdminPanelParametri();
            this.popuniListuParametara(nizParametara);
            Sadrzaj.Visibility = Visibility.Visible;
            grdIstorijaKupovine.Visibility = Visibility.Hidden;

        }

        private void btnUpravljenjeIstorijomKupovine_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljenjeIstorijomKupovine.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            ControlTemplate template =  Sadrzaj.Template;
            //Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeIstorijomKupovine");
            Sadrzaj.Visibility = Visibility.Hidden;
            grdIstorijaKupovine.Visibility = Visibility.Visible;
            



           SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
           SmartSoftwareServiceReference.DbItemIstorijaKupovine[] nizIstorijeKupovine = service.IstorijaKupovineSelect();
           this.popuniListuIstorijeKupovine(nizIstorijeKupovine);

           SmartSoftwareServiceReference.DbItemOpremaSaParametrimaStatistika[] nizOpremeZaIstorijuKupovine = service.IstorijaKupovineNajprodavanijaOprema(false);
           this.popuniListuOpremeSaParametrima(nizOpremeZaIstorijuKupovine);

            

        }

        private void btnUnosOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosOblastiOpreme = template.FindName("ctmUnosOblastiOpreme", Sadrzaj) as ContentControl;

            ContentControl ctmlevaStranaSadrzaj = template.FindName("ctmlevaStranaSadrzaj", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            ctmUnosOblastiOpreme.Visibility = Visibility.Visible;


            this.CurrentOblastiOpreme= new OblastiOpreme(null);


        }

        

        

        private void btnDodajSliku_Click(object sender, RoutedEventArgs e)
        {

            // Korisnik dijalogom zadaje fajl iz koga se čita dokument
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpg)|*.jpg"; // Ekstenzija ppp je ekstenzija dokumenta aplikacije Presentation
            if (ofd.ShowDialog() ?? false == true)
            {
                this.currentOblastiOpreme.Picture = ofd.FileName;
                
            }
        }


        private void btnUnesiOblastOpreme_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOblastiOpreme = template.FindName("ctmUnosOblastiOpreme", Sadrzaj) as ContentControl;

            

            int pozicija = 0;
            string nazivSlike = null;
            string putanjaZaBazu = null;

            if (this.CurrentOblastiOpreme != null && this.CurrentOblastiOpreme.Name != null && this.CurrentOblastiOpreme.Name != "")
            {
                if (this.CurrentOblastiOpreme.Picture != null)
                {
                    pozicija = this.CurrentOblastiOpreme.Picture.LastIndexOf("\\");
                    nazivSlike = this.CurrentOblastiOpreme.Picture.Substring(pozicija + 1);

                    string putanjaPrefix = "\\slike\\oblasti_opreme\\";
                    System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentOblastiOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOblastiOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    putanjaZaBazu = putanjaPrefix + nazivSlike;
                }

                DbItemOblastiOpreme oblast = new DbItemOblastiOpreme()
                {
                    naziv_oblasti_opreme = currentOblastiOpreme.Name,
                    picture = putanjaZaBazu == null ? null : putanjaZaBazu
                };

                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeInsert(oblast);
                ctmUnosOblastiOpreme.Visibility = Visibility.Hidden;
                this.popuniListuOblasti(nizOblasti);
            }
            else
            {
                MessageBox.Show("Unesite naziv oblasti");
            }
        }


        private void btnAzurirajSlikuOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentOblastiOpreme.Picture = ofd.FileName;
                this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = true;
                ContentControl ctmPrikazDetaljaSadrzaj = Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                
                this.daLiJeSlikaPromenjena = true;
            }
        }

        private void btnAzurirajOblastOpreme_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            string celaPutanjaDoslike = this.CurrentOblastiOpreme.Picture;
            int pozicija = celaPutanjaDoslike.LastIndexOf("\\");
            string slika = celaPutanjaDoslike.Substring(pozicija+1);
            string bla = this.CurrentOblastiOpreme.SlikaOriginalPutanja;
            int pozicijaZaPrefix = bla.LastIndexOf("/");
            string slikaPrefix = bla.Substring(0, pozicijaZaPrefix + 1);


            System.IO.FileInfo fileInfo =
                      new System.IO.FileInfo(celaPutanjaDoslike);
            SmartSoftwareServiceInterfaceClient clientUpload =
                    new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.RemoteFileInfo
                   uploadRequestInfo = new RemoteFileInfo();

            using (System.IO.FileStream stream =
                   new System.IO.FileStream(celaPutanjaDoslike,
                   System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                uploadRequestInfo.FileName = slika;
                uploadRequestInfo.Length = fileInfo.Length;
                uploadRequestInfo.FileByteStream = stream;
                clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, slikaPrefix, uploadRequestInfo.FileByteStream);
            }

            DbItemOblastiOpreme o = new DbItemOblastiOpreme()
            {
                id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme,
                naziv_oblasti_opreme = this.CurrentOblastiOpreme.Name,
                picture = slikaPrefix + slika
            };
            if (o != null || o.id_oblasti_opreme != 0)
            {
                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeUpdate(o);
                ctmPrikazDetalja.Visibility = Visibility.Hidden; 
                this.popuniListuOblasti(nizOblasti);

            }
        }

        private void btnObrisiOblastOpreme_Click(object sender, RoutedEventArgs e)
        {

            string pera = (sender as Button).Content.ToString();
            if (pera == "Restoruj ovu opremu")
            {

                string poruka = "Prodavci će videti ovu opremu. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                    ControlTemplate template = Sadrzaj.Template;
                    ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            
                    DbItemOblastiOpreme o = new DbItemOblastiOpreme()
                    {
                        id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme
                    };

                    if (o != null || o.id_oblasti_opreme != 0)
                    {
                        SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OpOblastiOpremeRestore(o);
                        ctmPrikazDetalja.Visibility = Visibility.Hidden; 
                        this.popuniListuOblasti(nizOblasti);
                    }

                    break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Da li zaista želite da uklonite ovu oblast i njene tipove?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            
                        DbItemOblastiOpreme o = new DbItemOblastiOpreme()
                        {
                            id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme
                        };

                        if (o != null || o.id_oblasti_opreme != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OpOblastiOpremeDelete(o);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden; 
                            this.popuniListuOblasti(nizOblasti);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
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

        private void tbImeOblastiOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            if ((this.CurrentOblastiOpreme.DaliMozeDaSeAzurira == false) || (this.daLiJeSlikaPromenjena == false))
            {
                ContentControl ctmPrikazDetaljaSadrzaj = Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                TextBox tekstBoks = sender as TextBox;
                string tekst = tekstBoks.Text;
                string tekstTrimovan = tekst.Trim();

                if (tekst == this.CurrentOblastiOpreme.Name || tekstTrimovan == ""){
                    this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = false;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.White;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat2", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.White;
                }
                else
                {
                    this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = true;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.LightGreen;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat2", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.LightGreen;
                }
            }
        }

        private void btnUnosTipoviOpreme_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentTipoviOpreme = new TipoviOpreme(null);
            (Sadrzaj.Template.FindName("ctmUnosTipaOpreme", Sadrzaj) as ContentControl).Visibility = Visibility.Visible;
            this.IzabranaOblastOpreme = 0;
        }

        private void GridPrikazRezultataTipovaOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ListaTipovaOpreme = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            //this.popuniListuTipovaOpreme(nizTipovaOblasti);


            if (this.CurrentTipoviOpreme != null)
            {
                ContentControl ctmPrikazDetaljaSadrzaj = (Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl);
                string tekstIme = (ctmPrikazDetaljaSadrzaj.Template.FindName("tbImeTipoviOpreme", ctmPrikazDetaljaSadrzaj) as TextBox).Text;
                this.CurrentTipoviOpreme.Name = this.CurrentTipoviOpreme.StaroImeTipa;
                
                if (!this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena && tekstIme != this.CurrentTipoviOpreme.StaroImeTipa)
                {
                    this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = false;
                    this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen = false;
                    this.CurrentTipoviOpreme.DaLiJeOblastPromenjena = false;

                }
            }
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosTipaOpreme = template.FindName("ctmUnosTipaOpreme", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosTipaOpreme.Visibility = Visibility.Hidden;

            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpreme();
            this.popuniListuOblasti(nizOblasti);

            



            oblasti = CollectionViewSource.GetDefaultView(this.ListaTipovaOpreme);
            t = oblasti.CurrentItem as TipoviOpreme;
            this.CurrentTipoviOpreme = t;

            for (int i = 0; i < this.ListaOblastiOpreme.Count; i++)
            {
                if (this.ListaOblastiOpreme[i].IdOblastiOpreme == this.CurrentTipoviOpreme.IdOblastiOpreme)
                {
                    IzabranaOblastOpreme = i;
                }
            }
            
            template = null;
        }

        private void GridPrikazRezultataKorisnika_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            Grid grdDodatniGrid = template.FindName("grdDodatniGrid", Sadrzaj) as Grid;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosKorisnici.Visibility = Visibility.Hidden;
            grdDodatniGrid.Visibility = Visibility.Visible;

            oblasti = CollectionViewSource.GetDefaultView(this.ListaKorisnika);
            k = oblasti.CurrentItem as Korisnici;
            this.CurrentKorisnici = k;
        }

        private void GridPrikazRezultataOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosOpreme.Visibility = Visibility.Hidden;
            slucajnoUProlazu = false;

            ListaTipovaOpreme = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            ControlTemplate opremaTemplate = ctmPrikazDetalja.Template;
            ItemsControl listaVrednostiParametara = opremaTemplate.FindName("listaVrednostiParametara", ctmPrikazDetalja) as ItemsControl;
            ItemsControl icListaParametaraLevo = opremaTemplate.FindName("icListaParametaraLevo", ctmPrikazDetalja) as ItemsControl;
            

            
            oblasti = CollectionViewSource.GetDefaultView(this.ListaOpreme);
            o = oblasti.CurrentItem as Oprema;
            this.CurrentOprema = o;

            for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            {
                if (this.CurrentOprema.IdTipOpreme == listaTipovaOpreme[i].IdTipOpreme)
                {
                    this.CurrentOprema.IzabranTipOpreme = i;
                }
            }
            icListaParametaraLevo.Items.Clear();
            listaVrednostiParametara.Items.Clear();

            if(this.CurrentOprema.KolekcijaOpreme != null || this.CurrentOprema.KolekcijaOpreme.Count > 0)
            {
                DbItemTipOpreme[] t = service.TipOpreme();

                foreach (var item in this.CurrentOprema.KolekcijaOpreme)
                {


                }
            }



            foreach (var item in this.CurrentOprema.ListaParametara)
            {
                
                    
                        StackPanel stpDesno = new StackPanel();
                        Separator separatorDesno = new Separator();
                        Style stilDesno = separatorDesno.Style;
                        ToolBar tulbarDesno = new ToolBar();
                        stilDesno = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                        separatorDesno.Background = Brushes.LightGray;
                        stpDesno.Children.Add(separatorDesno);

                        StackPanel stpLevo = new StackPanel();
                        Separator separatorLevo = new Separator();
                        Style stilLevo = separatorLevo.Style;
                        ToolBar tulbarLevo = new ToolBar();
                        stilLevo = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                        separatorLevo.Background = Brushes.LightGray;
                        stpLevo.Children.Add(separatorLevo);


                        



                        if (item.TipParametra == "bit")
                        {

                            Binding binding = new Binding();
                            binding.Path = new PropertyPath("VrednostParametra");
                            binding.Source = item;

                            


                            CheckBox chb = new CheckBox();
                            //if (parametar.VrednostParametra == "0")
                            //    chb.IsChecked = true;
                            //else chb.IsChecked = false;
                            chb.HorizontalAlignment = HorizontalAlignment.Center;

                            BindingOperations.SetBinding(chb, CheckBox.IsCheckedProperty, binding);

                            Label lbl = new Label();
                            lbl.Content = item.Name;

                            Grid grdZaListu = new Grid();
                            Grid grdZaListuLevo = new Grid();

                            RowDefinition redDesno1 = new RowDefinition();
                            redDesno1.Height = new GridLength(5);
                            RowDefinition redDesno2 = new RowDefinition();
                            redDesno2.Height = new GridLength(30);
                            grdZaListu.RowDefinitions.Add(redDesno1);
                            grdZaListu.RowDefinitions.Add(redDesno2);

                            Grid.SetRow(stpDesno, 0);
                            Grid.SetRow(chb, 1);

                            RowDefinition redLevo1 = new RowDefinition();
                            redLevo1.Height = new GridLength(5);
                            RowDefinition redLevo2 = new RowDefinition();
                            redLevo2.Height = new GridLength(30);

                            grdZaListuLevo.RowDefinitions.Add(redLevo1);
                            grdZaListuLevo.RowDefinitions.Add(redLevo2);

                            Grid.SetRow(stpLevo, 0);
                            Grid.SetRow(lbl, 1);

                            icListaParametaraLevo.Items.Add(grdZaListuLevo);
                            listaVrednostiParametara.Items.Add(grdZaListu);

                            grdZaListu.Children.Add(stpDesno);
                            grdZaListu.Children.Add(chb);

                            grdZaListuLevo.Children.Add(stpLevo);
                            grdZaListuLevo.Children.Add(lbl);
                            
                        }
                        else if (item.TipParametra == "text")
                        {
                            Binding binding = new Binding();
                            binding.Path = new PropertyPath("VrednostParametra");
                            binding.Source = item;

                            TextBox tb = new TextBox();
                            //tb.Text = item.VrednostParametra;

                            BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);

                            Label lbl = new Label();
                            lbl.Content = item.Name;

                            Grid grdZaListu = new Grid();
                            Grid grdZaListuLevo = new Grid();

                            RowDefinition redDesno1 = new RowDefinition();
                            redDesno1.Height = new GridLength(5);
                            RowDefinition redDesno2 = new RowDefinition();
                            redDesno2.Height = new GridLength(120);
                            grdZaListu.RowDefinitions.Add(redDesno1);
                            grdZaListu.RowDefinitions.Add(redDesno2);

                            Grid.SetRow(stpDesno, 0);
                            Grid.SetRow(tb, 1);

                            RowDefinition redLevo1 = new RowDefinition();
                            redLevo1.Height = new GridLength(5);
                            RowDefinition redLevo2 = new RowDefinition();
                            redLevo2.Height = new GridLength(120);

                            grdZaListuLevo.RowDefinitions.Add(redLevo1);
                            grdZaListuLevo.RowDefinitions.Add(redLevo2);

                            Grid.SetRow(stpLevo, 0);
                            Grid.SetRow(lbl, 1);

                            listaVrednostiParametara.Items.Add(grdZaListu);
                            icListaParametaraLevo.Items.Add(grdZaListuLevo);



                            grdZaListu.Children.Add(stpDesno);
                            grdZaListu.Children.Add(tb);

                            grdZaListuLevo.Children.Add(stpLevo);
                            grdZaListuLevo.Children.Add(lbl);
                        }
                        else 
                        {
                            Binding bindingg = new Binding();
                            bindingg.Path = new PropertyPath("VrednostParametra");
                            bindingg.Source = item;
                            TextBox tb = new TextBox();
                            //tb.Text = item.VrednostParametra;

                            BindingOperations.SetBinding(tb, TextBox.TextProperty, bindingg);

                            Label lbl = new Label();
                            lbl.Content = item.Name;

                            Grid grdZaListu = new Grid();
                            Grid grdZaListuLevo = new Grid();


                            RowDefinition redDesno1 = new RowDefinition();
                            redDesno1.Height = new GridLength(5);
                            RowDefinition redDesno2 = new RowDefinition();
                            redDesno2.Height = new GridLength(30);
                            grdZaListu.RowDefinitions.Add(redDesno1);
                            grdZaListu.RowDefinitions.Add(redDesno2);

                            Grid.SetRow(stpDesno, 0);
                            Grid.SetRow(tb, 1);

                            RowDefinition redLevo1 = new RowDefinition();
                            redLevo1.Height = new GridLength(5);
                            RowDefinition redLevo2 = new RowDefinition();
                            redLevo2.Height = new GridLength(30);

                            grdZaListuLevo.RowDefinitions.Add(redLevo1);
                            grdZaListuLevo.RowDefinitions.Add(redLevo2);

                            Grid.SetRow(stpLevo, 0);
                            Grid.SetRow(lbl, 1);

                            listaVrednostiParametara.Items.Add(grdZaListu);
                            icListaParametaraLevo.Items.Add(grdZaListuLevo);

                            grdZaListu.Children.Add(stpDesno);
                            grdZaListu.Children.Add(tb);

                            grdZaListuLevo.Children.Add(stpLevo);
                            grdZaListuLevo.Children.Add(lbl);
                        }
                    }



            slucajnoUProlazu = false;

        }

        private void btnUnesiTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentTipoviOpreme != null && this.CurrentTipoviOpreme.IdOblastiOpreme != -1 && this.CurrentTipoviOpreme.Name != null && this.CurrentTipoviOpreme.Name != "")
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                int pozicija = 0;
                string nazivSlike = null;
                string putanjaZaBazu = null;

                
                    if (this.CurrentTipoviOpreme.Picture != null)
                    {
                        pozicija = this.CurrentTipoviOpreme.Picture.LastIndexOf("\\");
                        nazivSlike = this.CurrentTipoviOpreme.Picture.Substring(pozicija + 1);

                        string putanjaPrefix = "\\slike\\tipovi_opreme\\";
                        System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentTipoviOpreme.Picture);
                        SmartSoftwareServiceInterfaceClient clientUpload =
                                new SmartSoftwareServiceInterfaceClient();
                        SmartSoftwareServiceReference.RemoteFileInfo
                               uploadRequestInfo = new RemoteFileInfo();

                        using (System.IO.FileStream stream =
                               new System.IO.FileStream(this.CurrentTipoviOpreme.Picture,
                               System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            uploadRequestInfo.FileName = nazivSlike;
                            uploadRequestInfo.Length = fileInfo.Length;
                            uploadRequestInfo.FileByteStream = stream;
                            clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                        }

                        putanjaZaBazu = putanjaPrefix + nazivSlike;
                    }

                    DbItemTipOpreme tip = new DbItemTipOpreme()
                    {
                        naziv_tipa = CurrentTipoviOpreme.Name,
                        slika_tipa = putanjaZaBazu == null ? null : putanjaZaBazu,
                        id_oblasti_opreme = IzabranaOblastOpreme + 1
                    };

                    SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeInsert(tip);
                    (Sadrzaj.Template.FindName("ctmUnosTipaOpreme", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                    (Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                    this.popuniListuTipovaOpreme(nizTipova);
            }
                else
                {
                    MessageBox.Show("Unesite naziv tipa");
                }
            
        }

        private void btnDodajSlikuTipaOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpg)|*.jpg"; // Ekstenzija ppp je ekstenzija dokumenta aplikacije Presentation
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentTipoviOpreme.Picture = ofd.FileName;
                this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena = true;
            }
        }

        private void btnObrisiTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            string pera = (sender as Button).Content.ToString();
            if (pera == "Restoruj ovu opremu")
            {

                string poruka = "Prodavci će videti ovaj tip opreme. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);
                
                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                         SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                    ControlTemplate template = Sadrzaj.Template;
                    ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                    DbItemTipOpreme t = new DbItemTipOpreme()
                    {
                        id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme
                    };

                    if (t != null || t.id_tip_opreme != 0)
                    {
                        SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeRestore(t);
                        ctmPrikazDetalja.Visibility = Visibility.Hidden;
                        this.popuniListuTipovaOpreme(nizTipova);
                    }

                    break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Da li zaista želite da uklonite ovaj tip opreme i njenu opremu?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                         SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                    ControlTemplate template = Sadrzaj.Template;
                    ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                    DbItemTipOpreme t = new DbItemTipOpreme()
                    {
                        id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme
                    };

                    if (t != null || t.id_tip_opreme != 0)
                    {
                        SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeDelete(t);
                        ctmPrikazDetalja.Visibility = Visibility.Hidden;
                        this.popuniListuTipovaOpreme(nizTipova);
                    }

                    break;
                    case MessageBoxResult.No:
                        break;



                }

            }
        }

        private void btnAzurirajTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            
            string celaPutanjaDoslike = this.CurrentTipoviOpreme.Picture;
            int pozicija = celaPutanjaDoslike.LastIndexOf("\\");
            string slika = celaPutanjaDoslike.Substring(pozicija + 1);
            string bla = this.CurrentTipoviOpreme.SlikaOriginalPutanja;

            //ovo je ispravka za sliku ako nije upisana kad se radio insert
            if(bla == null)
            {
                //string putanjaPrefix = 
                bla = "\\slike\\tipovi_opreme\\";
            }
            int pozicijaZaPrefix = bla.LastIndexOf("\\");
            string slikaPrefix = bla.Substring(0, pozicijaZaPrefix + 1);
            string promenjanaSlika = slikaPrefix + slika;


            //ovdje je ovo dje smo 

            if (this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == true)
            {
                System.IO.FileInfo fileInfo =
                            new System.IO.FileInfo(celaPutanjaDoslike);
                SmartSoftwareServiceInterfaceClient clientUpload =
                        new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.RemoteFileInfo
                        uploadRequestInfo = new RemoteFileInfo();

                using (System.IO.FileStream stream =
                        new System.IO.FileStream(celaPutanjaDoslike,
                        System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    uploadRequestInfo.FileName = slika;
                    uploadRequestInfo.Length = fileInfo.Length;
                    uploadRequestInfo.FileByteStream = stream;
                    clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, slikaPrefix, uploadRequestInfo.FileByteStream);
                }
            }

            DbItemTipOpreme t = new DbItemTipOpreme()
            {
                id_oblasti_opreme = this.CurrentTipoviOpreme.IdOblastiOpreme,
                naziv_tipa = this.CurrentTipoviOpreme.Name,
                slika_tipa = promenjanaSlika,
                id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme
            };
            if (t != null || t.id_tip_opreme != 0)
            {
                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeUpdate(t);
                ctmPrikazDetalja.Visibility = Visibility.Hidden;
                this.popuniListuTipovaOpreme(nizTipova);

            }
        }

        private void tbImeTipoviOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            if ((this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira == false) || (this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == false) || (this.CurrentTipoviOpreme.DaLiJeOblastPromenjena == false))
            {
                TextBox tekstBoks = sender as TextBox;
                string tekst = tekstBoks.Text;
                string tekstTrimovan = tekst.Trim();

                if (tekst == this.CurrentTipoviOpreme.StaroImeTipa || tekstTrimovan == "")
                {
                    this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen = false;
                    if (this.CurrentTipoviOpreme.DaLiJeOblastPromenjena == false && this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == false)
                        this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = false;
                }
                else
                {
                    this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen = true;
                    this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = true;
                }
            }
        }

        private void btnAzurirajSlikuTipaOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentTipoviOpreme.Picture = ofd.FileName;
                this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = true;
                this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena = true;
            }
        }

        private void cmbListaOblastiOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((this.IzabranaOblastOpreme + 1) == this.CurrentTipoviOpreme.IdOblastiOpreme)
            {
                this.CurrentTipoviOpreme.DaLiJeOblastPromenjena = false;
                if (this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == true || this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen == true)
                    this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = true;
                else this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = false;
            }
            else
            {
                this.CurrentTipoviOpreme.DaLiJeOblastPromenjena = true;
                this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = true;
            }
        }

        private void tbPretragaOblastiOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] oblastiOpreme = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                oblastiOpreme = service.OblastiOpreme();
            }
            else
            {
                oblastiOpreme = service.PretragaOblastiOpreme(zaPretragu);

            }
            this.popuniListuOblasti(oblastiOpreme);
        }

        private void tbPretragaTipovaOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] tipoviOpreme = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                tipoviOpreme = service.TipOpreme();
            }
            else
            {
                tipoviOpreme = service.PretragaTipovaOpreme(zaPretragu);

            }
            this.popuniListuTipovaOpreme(tipoviOpreme);
        }

        private void btnAzurirajKorisnika_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


            DbItemKorisnici korisniciAzuriranje = new DbItemKorisnici()
            {
                id_korisnici = this.CurrentKorisnici.IdKorisnici,
                id_uloge = this.CurrentKorisnici.IzabranaUloga + 1,
                ime = this.CurrentKorisnici.ImeKorisnika,
                prezime = this.CurrentKorisnici.PrezimeKorisnika,
                mejl = this.CurrentKorisnici.MejlKorisnika,
                broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                brojOstvarenihPoena = Convert.ToInt32(this.CurrentKorisnici.BrojOstvarenihPoena),
                username = this.CurrentKorisnici.Username,
                lozinka = this.CurrentKorisnici.Lozinka,
                //naziv_uloge = this.CurrentKorisnici.NazivUloge,
                //deletedField = this.CurrentKorisnici.DeletedItem
            };

            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.ZaposleniKorisniciUpdate(korisniciAzuriranje);
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            this.popuniListuKorisnici(nizKorisnika);           
        }

        

        private void btnObrisiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            string pera = (sender as Button).Content.ToString();
            if (pera == "Restoruj ovu opremu")
            {

                string poruka = "Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                        int id = this.CurrentKorisnici.IdKorisnici;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.KorisniciRestore(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuKorisnici(nizKorisnika);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Da li zaista želite da obrišete ovog korisnika?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        int id = this.CurrentKorisnici.IdKorisnici;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.KorisniciDelete(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuKorisnici(nizKorisnika);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }
        }

        private void btnUnosKorisnici_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;

            ContentControl ctmlevaStranaSadrzaj = template.FindName("ctmlevaStranaSadrzaj", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            ctmUnosKorisnici.Visibility = Visibility.Visible;


            this.CurrentKorisnici = new Korisnici();
        }

        private void btnUnesiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentKorisnici != null )
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemKorisnici korisnik = new DbItemKorisnici()
                {
                    ime = this.CurrentKorisnici.ImeKorisnika,
                    prezime= this.CurrentKorisnici.PrezimeKorisnika,
                    id_uloge = this.CurrentKorisnici.IzabranaUloga + 1,
                    mejl = this.CurrentKorisnici.MejlKorisnika,
                    broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                    brojOstvarenihPoena = (int)(this.CurrentKorisnici.BrojOstvarenihPoena),
                    username = this.CurrentKorisnici.Username,
                    lozinka = this.CurrentKorisnici.Lozinka
                    //naziv_uloge = this.CurrentKorisnici.NazivUloge
                };

                SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.ZaposleniKorisniciInsert(korisnik);
                (Sadrzaj.Template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                (Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                this.popuniListuKorisnici(nizKorisnika);
            }
            else
            {
                MessageBox.Show("Popunite sva polja");
            }
        }

        private void btnAzurirajOpremu_Click(object sender, RoutedEventArgs e)
        {
          
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;



            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            

            string celaPutanjaDoslike = this.CurrentOprema.Picture;
            int pozicija = celaPutanjaDoslike.LastIndexOf("\\");
            string slikaa = celaPutanjaDoslike.Substring(pozicija + 1);
            string bla = this.CurrentOprema.SlikaOriginalPutanja;
            
            if (bla == null)
            {
                //string putanjaPrefix = 
                bla = "\\slike\\oprema\\";
            }
            
            int pozicijaZaPrefix = bla.LastIndexOf("\\");
            string slikaPrefix = bla.Substring(0, pozicijaZaPrefix + 1);



            if (this.CurrentOprema.DaLiJeSlikaOpremePromenjena == true)
            {

                System.IO.FileInfo fileInfo =
                          new System.IO.FileInfo(celaPutanjaDoslike);
                SmartSoftwareServiceInterfaceClient clientUpload =
                        new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.RemoteFileInfo
                       uploadRequestInfo = new RemoteFileInfo();

                using (System.IO.FileStream stream =
                       new System.IO.FileStream(celaPutanjaDoslike,
                       System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    uploadRequestInfo.FileName = slikaa;
                    uploadRequestInfo.Length = fileInfo.Length;
                    uploadRequestInfo.FileByteStream = stream;
                    clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, slikaPrefix, uploadRequestInfo.FileByteStream);
                }

            }


            DbItemOpremaSaParametrima opremaZaAzuriranje = new DbItemOpremaSaParametrima()
            {
                cena = this.CurrentOprema.Cena,
                id_oprema = this.CurrentOprema.IdOprema,
                id_tip_opreme = this.CurrentOprema.IdTipOpreme,
                kolicina_na_lageru = this.CurrentOprema.KolicinaNaLageru,
                kolicina_u_rezervi = this.CurrentOprema.KolicinaURezervi,
                kolicinaUKorpi = (int)this.CurrentOprema.TmpIzabranaKolicina,
                kolicinaURezervacijama = this.CurrentOprema.IzabranaKolicina,
                lager = this.CurrentOprema.Lager,
                model = this.CurrentOprema.Model,
                naslov = this.CurrentOprema.Name,
                opis = this.CurrentOprema.Opis,
                oprema_na_popustu = this.CurrentOprema.OpremaNaPopustu,
                proizvodjac = this.CurrentOprema.Proizvodjac,
                slika = slikaPrefix + slikaa
            };

            List<DbItemParametri> parametri = new List<DbItemParametri>();

            foreach (var item in this.CurrentOprema.ListaParametara)
            {
                parametri.Add(new DbItemParametri()
                {
                    default_vrednost = item.DefaultVrednost,
                    id_parametri = item.IdParametri,
                    id_tip_opreme = item.IdTipOpreme,
                    naziv_parametra = item.Name,
                    tipParametra = item.TipParametra,
                    vrednost_parametra = item.VrednostParametra
                });
            }

            opremaZaAzuriranje.ListaParametara = parametri.ToArray();




            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanelUpdate(opremaZaAzuriranje);
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            this.popuniListuOpremeSaParametrima(nizOpreme);
        }

        private void btnAzurirajSlikuOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentOprema.Picture = ofd.FileName;
                this.CurrentOprema.DaLiJeSlikaOpremePromenjena = true;
            }
        }

        private void btnUnosOpreme_Click(object sender, RoutedEventArgs e)
        {
            ucitaloSE = false;
            idiNaCMB = false;

            this.CurrentOprema = new Oprema(null);
            (Sadrzaj.Template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl).Visibility = Visibility.Visible;
            this.CurrentOprema.IzabranTipOpreme = 0;


            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;

            ListaTipovaOpreme = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            ControlTemplate opremaTemplate = ctmUnosOpreme.Template;
            ItemsControl listaVrednostiParametara = opremaTemplate.FindName("listaVrednostiParametara", ctmUnosOpreme) as ItemsControl;
            ItemsControl icListaParametaraLevo = opremaTemplate.FindName("icListaParametaraLevo", ctmUnosOpreme) as ItemsControl;


            icListaParametaraLevo.Items.Clear();
            listaVrednostiParametara.Items.Clear();



            //for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            //{
            //    if (this.CurrentOprema.IdTipOpreme == listaTipovaOpreme[i].IdTipOpreme)
            //    {
            //        this.CurrentOprema.IzabranTipOpreme = i;
            //    }
            //}
            

            
            //for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            //{
            //    if (this.CurrentOprema.IzabranTipOpreme == i)
            //    {
            //        this.CurrentOprema.IdTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
            //    }
            //}

            this.CurrentOprema.IzabranTipOpreme = 1;

            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(1);

            this.CurrentOprema = new Oprema(null);



            //ovdje treba veb servis

            
            for (int i = 0; i < nizParametara.Length; i++)
            {
                this.CurrentOprema.ListaParametara.Add(new Parametri(null)
                {
                    DefaultVrednost = nizParametara[i].default_vrednost,
                    IdParametri = nizParametara[i].id_parametri,
                    IdTipOpreme = nizParametara[i].id_tip_opreme,
                    VrednostParametra = nizParametara[i].vrednost_parametra,
                    Name = nizParametara[i].naziv_parametra,
                    TipParametra = nizParametara[i].tipParametra
                });
            }



            foreach (var item in this.CurrentOprema.ListaParametara)
            {


                StackPanel stpDesno = new StackPanel();
                Separator separatorDesno = new Separator();
                Style stilDesno = separatorDesno.Style;
                ToolBar tulbarDesno = new ToolBar();
                stilDesno = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorDesno.Background = Brushes.LightGray;
                stpDesno.Children.Add(separatorDesno);

                StackPanel stpLevo = new StackPanel();
                Separator separatorLevo = new Separator();
                Style stilLevo = separatorLevo.Style;
                ToolBar tulbarLevo = new ToolBar();
                stilLevo = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorLevo.Background = Brushes.LightGray;
                stpLevo.Children.Add(separatorLevo);






                if (item.TipParametra == "bit")
                {

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;




                    CheckBox chb = new CheckBox();
                    //if (parametar.VrednostParametra == "0")
                    //    chb.IsChecked = true;
                    //else chb.IsChecked = false;
                    chb.HorizontalAlignment = HorizontalAlignment.Center;

                    BindingOperations.SetBinding(chb, CheckBox.IsCheckedProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(chb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    icListaParametaraLevo.Items.Add(grdZaListuLevo);
                    listaVrednostiParametara.Items.Add(grdZaListu);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(chb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);

                }
                else if (item.TipParametra == "text")
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;

                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(120);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(120);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);



                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
                else
                {
                    Binding bindingg = new Binding();
                    bindingg.Path = new PropertyPath("VrednostParametra");
                    bindingg.Source = item;
                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, bindingg);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();


                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
            }



            ucitaloSE = true;
            idiNaCMB = true;
            slucajnoUProlazu = true;
        }

        private bool idiNaCMB = false;
        private bool ucitaloSE = false;
        private bool slucajnoUProlazu = false;

        private void cmbListaTipovaOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ucitaloSE && !idiNaCMB || !slucajnoUProlazu)
            {
                return;
            }

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;

            

            if(ctmUnosOpreme == null)
            {
                return;
            }
            ControlTemplate opremaTemplate = ctmUnosOpreme.Template;
            ItemsControl listaVrednostiParametara = opremaTemplate.FindName("listaVrednostiParametara", ctmUnosOpreme) as ItemsControl;
            ItemsControl icListaParametaraLevo = opremaTemplate.FindName("icListaParametaraLevo", ctmUnosOpreme) as ItemsControl;


            icListaParametaraLevo.Items.Clear();
            listaVrednostiParametara.Items.Clear();



            if (ListaTipovaOpreme == null)
            {
                this.CurrentOprema.IdTipOpreme = 1;

            }
            else
            {


                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (this.CurrentOprema.IzabranTipOpreme == i)
                    {
                        this.CurrentOprema.IdTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                    }
                }
            }
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(this.CurrentOprema.IdTipOpreme);

            //this.CurrentOprema = new Oprema(null);



            //ovdje treba veb servis

            this.CurrentOprema.ListaParametara.Clear();
            for (int i = 0; i < nizParametara.Length; i++)
            {
                this.CurrentOprema.ListaParametara.Add(new Parametri(null)
                {
                    DefaultVrednost = nizParametara[i].default_vrednost,
                    IdParametri = nizParametara[i].id_parametri,
                    IdTipOpreme = nizParametara[i].id_tip_opreme,
                    VrednostParametra = nizParametara[i].vrednost_parametra,
                    Name = nizParametara[i].naziv_parametra,
                    TipParametra = nizParametara[i].tipParametra
                });
            }



            foreach (var item in this.CurrentOprema.ListaParametara)
            {


                StackPanel stpDesno = new StackPanel();
                Separator separatorDesno = new Separator();
                Style stilDesno = separatorDesno.Style;
                ToolBar tulbarDesno = new ToolBar();
                stilDesno = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorDesno.Background = Brushes.LightGray;
                stpDesno.Children.Add(separatorDesno);

                StackPanel stpLevo = new StackPanel();
                Separator separatorLevo = new Separator();
                Style stilLevo = separatorLevo.Style;
                ToolBar tulbarLevo = new ToolBar();
                stilLevo = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorLevo.Background = Brushes.LightGray;
                stpLevo.Children.Add(separatorLevo);






                if (item.TipParametra == "bit")
                {

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;




                    CheckBox chb = new CheckBox();
                    //if (parametar.VrednostParametra == "0")
                    //    chb.IsChecked = true;
                    //else chb.IsChecked = false;
                    chb.HorizontalAlignment = HorizontalAlignment.Center;

                    BindingOperations.SetBinding(chb, CheckBox.IsCheckedProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(chb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    icListaParametaraLevo.Items.Add(grdZaListuLevo);
                    listaVrednostiParametara.Items.Add(grdZaListu);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(chb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);

                }
                else if (item.TipParametra == "text")
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;

                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(120);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(120);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);



                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
                else
                {
                    Binding bindingg = new Binding();
                    bindingg.Path = new PropertyPath("VrednostParametra");
                    bindingg.Source = item;
                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, bindingg);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();


                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }


            }
           
        }

        private void btnUnesiOpremu_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;

            int pozicija = 0;
            string nazivSlike = null;
            string putanjaZaBazu = null;

            if (this.CurrentOprema != null && this.CurrentOprema.Name != null && this.CurrentOprema.Name != "")
            {
                if (this.CurrentOprema.Picture != null)
                {
                    pozicija = this.CurrentOprema.Picture.LastIndexOf("\\");
                    nazivSlike = this.CurrentOprema.Picture.Substring(pozicija + 1);





                    //ovde treba da se radi ona zajebancija sa putanjama



                    //string putanjaPrefix = "\\slike\\oblasti_opreme\\";


                    //treba valjda dinamicki da se ubacuje u foldere

                    string putanjaPrefix = "\\slike\\oprema\\probaFolder\\";




                    System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentOprema.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOprema.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    putanjaZaBazu = putanjaPrefix + nazivSlike;
                }


                DbItemOpremaSaParametrima opremaZaInsert = new DbItemOpremaSaParametrima()
                {
                    cena = this.CurrentOprema.Cena,
                    id_oprema = this.CurrentOprema.IdOprema,
                    id_tip_opreme = this.CurrentOprema.IdTipOpreme,
                    kolicina_na_lageru = this.CurrentOprema.KolicinaNaLageru,
                    kolicina_u_rezervi = this.CurrentOprema.KolicinaURezervi,
                    kolicinaUKorpi = (int)this.CurrentOprema.TmpIzabranaKolicina,
                    kolicinaURezervacijama = this.CurrentOprema.IzabranaKolicina,
                    lager = this.CurrentOprema.Lager,
                    model = this.CurrentOprema.Model,
                    naslov = this.CurrentOprema.Name,
                    opis = this.CurrentOprema.Opis,
                    oprema_na_popustu = this.CurrentOprema.OpremaNaPopustu,
                    proizvodjac = this.CurrentOprema.Proizvodjac,
                    slika = putanjaZaBazu
                };
                
                List<DbItemParametri> parametri = new List<DbItemParametri>();

                foreach (var item in this.CurrentOprema.ListaParametara)
                {
                    parametri.Add(new DbItemParametri()
                    {
                        default_vrednost = item.DefaultVrednost,
                        id_parametri = item.IdParametri,
                        id_tip_opreme = item.IdTipOpreme,
                        naziv_parametra = item.Name,
                        tipParametra = item.TipParametra,
                        vrednost_parametra = item.VrednostParametra
                    });
                }

                opremaZaInsert.ListaParametara = parametri.ToArray();


                //ovde dole trba druga opreracija tj za insert opreme

                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanelInsert(opremaZaInsert);
                ctmUnosOpreme.Visibility = Visibility.Hidden;
                this.popuniListuOpremeSaParametrima(nizOpreme);

               
            }
            else
            {
                MessageBox.Show("Unesite naziv oblasti");
            }
        }

        private void btnDodajSlikuOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentOprema.Picture = ofd.FileName;
                this.CurrentOprema.DaLiJeSlikaOpremePromenjena = true;
            }
        }

        private void btnObrisiOpremu_Click(object sender, RoutedEventArgs e)
        {
            string pera = (sender as Button).Content.ToString();
            if (pera == "Restoruj ovu opremu")
            {

                string poruka = "Prodavci će videti ovu opremu. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                        int id = this.CurrentOprema.IdOprema;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaRestore(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuOpremeSaParametrima(nizOpreme);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Ovime ćete ovu opremu samo sakriti za prodavca. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        int id = this.CurrentOprema.IdOprema;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaDelete(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuOpremeSaParametrima(nizOpreme);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }

        }



        private void GridPrikazRezultataParametara_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosKorisnici.Visibility = Visibility.Hidden;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            oblasti = CollectionViewSource.GetDefaultView(this.ListaParametara);
            Parametri p = oblasti.CurrentItem as Parametri;
            this.CurrentParametri = p;

            ControlTemplate template2 = ctmPrikazDetalja.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmPrikazDetalja) as ComboBox;
            cmbUnosParametara.ItemsSource = Enum.GetValues(typeof(TipParametara));
            
            cmbUnosParametara.SelectedIndex = 0;


            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if (this.ListaTipovaOpreme[i].IdTipOpreme == this.CurrentParametri.IdTipOpreme)
                {
                    this.CurrentParametri.IzabranTipOpreme = i;
                }
            }

        }

        //private TipParametara VratiTipParametaraZaParametar(Parametri p)
        //{
        //    switch(p.TipParametra)
        //    {
        //        case ""if (val.Equals("astringvalue", StringComparison.InvariantCultureIgnoreCase))
        //    } //e izvini zvao me brat nismo se culi 100 godina evo sad cu da prekinem samo ti polako
        //}
       

        private void btnUnosParametara_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;

            ContentControl ctmlevaStranaSadrzaj = template.FindName("ctmlevaStranaSadrzaj", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            ctmUnosKorisnici.Visibility = Visibility.Visible;


            ControlTemplate template2 = ctmUnosKorisnici.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmUnosKorisnici) as ComboBox;
            cmbUnosParametara.ItemsSource = Enum.GetValues(typeof(TipParametara));
            cmbUnosParametara.SelectedIndex = 0;


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            this.CurrentParametri = new Parametri(null);


            

        }

        

        private void popuniListuParametara(DbItemParametri[] nizParametara)
        {
            
            this.ListaParametara = new ObservableCollection<Parametri>();
            for (int i = 0; i < nizParametara.Length; i++)
            {
                this.ListaParametara.Add(new Parametri(null)
                 {
                     DefaultVrednost = nizParametara[i].default_vrednost,
                     IdParametri = nizParametara[i].id_parametri,
                     IdTipOpreme = nizParametara[i].id_tip_opreme,
                     VrednostParametra = nizParametara[i].vrednost_parametra,
                     Name = nizParametara[i].naziv_parametra,
                     TipParametra = nizParametara[i].tipParametra,
                     ParametarJeIFilter = nizParametara[i].za_filter,
                     DeletedItem = nizParametara[i].deletedField
                 });
            }
        }

        private void popuniListuParametaraPomocno(DbItemParametri[] nizParametara)
        {

            this.ListaParametaraPomocno = new ObservableCollection<Parametri>();
            for (int i = 0; i < nizParametara.Length; i++)
            {
                this.ListaParametaraPomocno.Add(new Parametri(null)
                {
                    DefaultVrednost = nizParametara[i].default_vrednost,
                    IdParametri = nizParametara[i].id_parametri,
                    IdTipOpreme = nizParametara[i].id_tip_opreme,
                    VrednostParametra = nizParametara[i].vrednost_parametra,
                    Name = nizParametara[i].naziv_parametra,
                    TipParametra = nizParametara[i].tipParametra,
                    ParametarJeIFilter = nizParametara[i].za_filter,
                    DeletedItem = nizParametara[i].deletedField
                });
            }
        }


        private void btnUnesiParametar_Click(object sender, RoutedEventArgs e)
        {
            if(this.currentParametri == null)
            {
                return;
            }

            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if(i == this.currentParametri.IzabranTipOpreme)
                {
                    this.currentParametri.IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme;
                    break;
                }
            }

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;

            ControlTemplate template2 = ctmUnosKorisnici.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmUnosKorisnici) as ComboBox;
            //MessageBox.Show(cmbUnosParametara.SelectedValue.ToString());
            this.currentParametri.TipParametra = cmbUnosParametara.SelectedValue.ToString();

            
            DbItemParametri p = new DbItemParametri()
            {
                default_vrednost = this.currentParametri.DefaultVrednost,
                id_parametri = this.currentParametri.IdParametri,
                id_tip_opreme = this.currentParametri.IdTipOpreme,
                naziv_parametra = this.currentParametri.Name,
                vrednost_parametra = this.currentParametri.VrednostParametra,
                za_filter = this.currentParametri.ParametarJeIFilter,
                tipParametra = this.currentParametri.TipParametra
            };

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriInsert(p);
            this.popuniListuParametara(nizParametara);
        }

        private void btnAzurirajParametar_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentParametri == null)
            {
                return;
            }

            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if (i == this.currentParametri.IzabranTipOpreme)
                {
                    this.currentParametri.IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme;
                    break;
                }
            }

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            ControlTemplate template2 = ctmPrikazDetalja.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmPrikazDetalja) as ComboBox;
            //MessageBox.Show(cmbUnosParametara.SelectedValue.ToString());
            this.currentParametri.TipParametra = cmbUnosParametara.SelectedValue.ToString();




            DbItemParametri p = new DbItemParametri()
            {
                default_vrednost = this.currentParametri.DefaultVrednost,
                id_parametri = this.currentParametri.IdParametri,
                id_tip_opreme = this.currentParametri.IdTipOpreme,
                naziv_parametra = this.currentParametri.Name,
                vrednost_parametra = this.currentParametri.VrednostParametra,
                za_filter = this.currentParametri.ParametarJeIFilter,
                tipParametra = this.currentParametri.TipParametra
            };

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriUpdate(p);
            this.popuniListuParametara(nizParametara);

            ctmPrikazDetalja.Visibility = Visibility.Hidden;

        }

        private void btnObrisiParametar_Click(object sender, RoutedEventArgs e)
        {
            string pera = (sender as Button).Content.ToString();
            if (pera == "Restoruj ovu opremu")
            {

                string poruka = "Prodavci će videti ovaj parametar. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                        int id = this.CurrentParametri.IdParametri;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriRestore(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuParametara(nizParametara);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Ovime ćete ovaj parametar samo sakriti za prodavca. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        int id = this.CurrentParametri.IdParametri;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriDelete(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuParametara(nizParametara);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void dugmeMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
            else this.WindowState = System.Windows.WindowState.Normal;
        }

        private void dugmeZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dugmeMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void tbPretragaParametara_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] parametri = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                parametri = service.SelectAdminPanelParametri();
            }
            else
            {
                parametri = service.ParametriPretraga(zaPretragu);
            }
            this.popuniListuParametara(parametri);
        }

        private void cmbListaUloga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.currentKorisnici.IdUloge = (sender as ComboBox).SelectedIndex + 1;
        }

        private void tbPretragaKorisnika_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                korisnici = service.PrikaziZaposleneKorisnike(null);
            }
            else
            {
                korisnici = service.PretragaKorisnika(zaPretragu);
            }
            this.popuniListuKorisnici(korisnici);
        }

        private void tbPretragaOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                oprema = service.OpremaSaParametrimaAdminPanel();
            }
            else
            {
                oprema = service.PretragaOpreme(zaPretragu,true);
            }
            this.popuniListuOpremeSaParametrima(oprema);
        }

        private void grdPrikazKupacaIstorijeKupovine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            oblasti = CollectionViewSource.GetDefaultView(this.ListaKupaca);
            ku = oblasti.CurrentItem as Kupci;
            this.CurrentKupci = ku;
            
        }

        private void cmbListaUpitaZaKupce_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            switch((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    this.popuniListuKupaca(service.IstorijaKupovineInicijalniPrikazKupaca());
                    (grdIstorijaKupovine.FindName("listaKupacaGrupisanihPo") as ContentControl).SetResourceReference(TemplateProperty, "listaKupacaInit");
                    break;
                case 1:
                    this.popuniListuKupaca(service.IstorijaKupovineNajcesciKupci(false));

                    (grdIstorijaKupovine.FindName("listaKupacaGrupisanihPo") as ContentControl).SetResourceReference(TemplateProperty, "kupciNajcesciKupci");
                    break;
                case 2:
                    this.popuniListuKupaca(service.IstorijaKupovineKupciKojiNajviseKupuju(false));
                    (grdIstorijaKupovine.FindName("listaKupacaGrupisanihPo") as ContentControl).SetResourceReference(TemplateProperty, "kupciNajviseKupljeneRobe");
                    break;
                case 3:
                    this.popuniListuKupaca(service.IstorijaKupovineKupciKojiNajviseTrose(false));
                    (grdIstorijaKupovine.FindName("listaKupacaGrupisanihPo") as ContentControl).SetResourceReference(TemplateProperty,"kupciNajvisePotroseno");
                    
                    break;
            }

            
        }



        private void element_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ContentControl ctl = grdIstorijaKupovine.FindName("listaOpremeGrupisanihPo") as ContentControl;
            ControlTemplate template = ctl.Template;
            ContentControl pera2 = template.FindName("pera2", ctl) as ContentControl;

            oblasti = CollectionViewSource.GetDefaultView(this.ListaOpreme);
            o = oblasti.CurrentItem as Oprema;
            this.CurrentOprema= o;
            pera2.Visibility = Visibility.Visible;


            //ContentControl ctl = grdIstorijaKupovine.FindName("prikazIstorijeKupovineGrupisanePoOpremi") as ContentControl;
            //ControlTemplate template = ctl.Template;
            //ContentControl pera2 = template.FindName("pera2", ctl) as ContentControl;

            ////pera2.Visibility = Visibility.Visible;
            //Grid g = sender as Grid;
            
            //TextBlock t = g.FindName("skrivenId") as TextBlock;
            

            


            //pera2.Content = null;


            
            //    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.OpremeSaParametrima((this.tmpEditObj as Oprema).IdTipOpreme);
            //    popuniListuOprema(rez);
            //    this.dalisepozivabaza = false;
            


            //pera2.Content = ;
            

        }

        private void btnUpravljanjeNarudzbinama_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljanjeNarudzbinama.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeNarudzbinama");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbina = service.OpNarudzbineSelect();

            


            this.popuniListuNarudzbina(nizNarudzbina);



            




        }

        private void btnOdbijNarudzbinu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrihvatiNarudzbinu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpravljanjeOgranicenjima_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in grdDugmici.Children)
            {
                (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            }
            btnUpravljanjeOgranicenjima.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeOgranicenjima");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOpreme = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOpreme);





        }

        

        private void cmbListaTipovaOpremeOgranicenjaGlavno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranTipOpreme = -1;
            string nazivIzabranogTipaOpreme = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                        nazivIzabranogTipaOpreme = ListaTipovaOpreme[i].Name;
                        break;
                    }
                }
                this.CurrentOgranicenjeKolekcije.Id_tip_opreme1 = izabranTipOpreme;
                this.CurrentOgranicenjeKolekcije.NazivTipaOpreme1 = nazivIzabranogTipaOpreme;

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(izabranTipOpreme);
                this.popuniListuParametara(nizParametara);
            }
        }

        private void cmbListaTipovaOpremeOgranicenjaPomocno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranTipOpreme = -1;
            string nazivIzabranogTipaOpreme = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                        nazivIzabranogTipaOpreme = ListaTipovaOpreme[i].Name;
                        break;
                    }
                }

                this.CurrentOgranicenjeKolekcije.Id_tip_opreme2 = izabranTipOpreme;
                this.CurrentOgranicenjeKolekcije.NazivTipaOpreme2 = nazivIzabranogTipaOpreme;


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(izabranTipOpreme);
                this.popuniListuParametaraPomocno(nizParametara);
            }
        }

        private void cmbListaParametaraOgranicenja1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranIdParametra = -1;
            string nazivIzabranogParametra = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaParametara.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranIdParametra = ListaParametara[i].IdParametri;
                        nazivIzabranogParametra = ListaParametara[i].Name;
                        break;
                    }
                }

                this.CurrentOgranicenjeKolekcije.Id_parametra1 = izabranIdParametra;
                this.CurrentOgranicenjeKolekcije.NazivParametra1 = nazivIzabranogParametra;

            }
        }

        private void cmbListaParametaraOgranicenja2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranIdParametra = -1;
            string nazivIzabranogParametra = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaParametaraPomocno.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranIdParametra = ListaParametaraPomocno[i].IdParametri;
                        nazivIzabranogParametra = ListaParametaraPomocno[i].Name;
                        break;
                    }
                }

                this.CurrentOgranicenjeKolekcije.Id_parametra2 = izabranIdParametra;
                this.CurrentOgranicenjeKolekcije.NazivParametra2 = nazivIzabranogParametra;

            }
        }

        private void cmbListaTipovaOpremeKonfiguracije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranTipOpreme = -1;

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                        break;
                    }
                }

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                SmartSoftwareServiceReference.DbItemTipoviZaKonfiguraciju[] nizTipovaOpremeZaKonfiguraciju = service.TipoviZaNovuKonfiguraciju(izabranTipOpreme);
                
                this.popuniListuTipovaOpremeZaKonfiguracijuOgranicenja(nizTipovaOpremeZaKonfiguraciju);

                int selektovaniIndex = cmb.SelectedIndex;

                for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
                {
                    if(selektovaniIndex == i)
                    {
                        this.currentTipoviOpreme = new TipoviOpreme(null)
                        {
                            IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme
                            
                        };
                    break;
                    }
                }
            }
        }


        int IzabranIdTipaOpremeZaKonfiguraciju = 0;
        private void btnIdiNaDruguStranu_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;
            
            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ContentControl ctlDugmiciUnosOgranicenja = ctmUnosOgranicenjaTemplate.FindName("ctmDugmiciUnosOgranicenja", ctmUnosOgranicenja) as ContentControl;



            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



            DbItemTipoviZaKonfiguraciju[] kolekcijaKonfiguracije = new DbItemTipoviZaKonfiguraciju[this.ListaTipovaZaKonfiguraciju.Count];
            for (int i = 0; i < this.ListaTipovaZaKonfiguraciju.Count; i++)
            {
                kolekcijaKonfiguracije[i] = new DbItemTipoviZaKonfiguraciju()
                {
                    naziv_tipa = ListaTipovaZaKonfiguraciju[i].Name,
                    id_tip_opreme = ListaTipovaZaKonfiguraciju[i].IdTipOpreme,
                    idTipOpremeKolekcije = ListaTipovaZaKonfiguraciju[i].IdTipOpremeKolekcije,
                    idTipOpremeDeoKolekcije = ListaTipovaZaKonfiguraciju[i].IdTipOpreme,
                    redosledPrikazivanja = ListaTipovaZaKonfiguraciju[i].IzabranRedosled + 1,
                    izabranZaKonfiguraciju = ListaTipovaZaKonfiguraciju[i].IzabranZaKonfiguraciju
                };
            }



            IzabranIdTipaOpremeZaKonfiguraciju = this.CurrentTipoviOpreme.IdTipOpreme;


            SmartSoftwareServiceReference.DbItemTipoviZaKonfiguraciju[] nizTipovaZaKonfiguraciju = service.TipoviZaNovuKonfiguracijuInsertUpdate( this.CurrentTipoviOpreme.IdTipOpreme, kolekcijaKonfiguracije);

            this.popuniListuTipovaOpreme(nizTipovaZaKonfiguraciju);
            
            ctlUnosOgranicenjaSredina.SetResourceReference(TemplateProperty, "ctpUnosOgranicenjaDrugaStrana");
            ctlDugmiciUnosOgranicenja.SetResourceReference(TemplateProperty, "ctpDugmiciUnosOgranicenjaDrugaStrana");


            DbItemGrupeOgranicenja[] nizOgranicenja = service.OgranicenjaSelect(IzabranIdTipaOpremeZaKonfiguraciju);
            this.popuniListuOgranicenja(nizOgranicenja);

        }

        private void btnUnesiOgranicenja_Click(object sender, RoutedEventArgs e)
        {
            int idTipOpremeKolekcije = this.CurrentTipoviOpreme.IdTipOpreme;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            DbItemGrupeOgranicenja ogranicenje = new DbItemGrupeOgranicenja()
            {
                id_tip_opreme_kolekcije = this.CurrentTipoviOpreme.IdTipOpreme,
                Id_grupe_ogranicenja = this.CurrentOgranicenjeKolekcije.Id_grupe_ogranicenja,
                id_parametra1 = this.CurrentOgranicenjeKolekcije.Id_parametra1,
                id_parametra2 = this.CurrentOgranicenjeKolekcije.Id_parametra2,
                id_tip_opreme1 = this.CurrentOgranicenjeKolekcije.Id_tip_opreme1,
                id_tip_opreme2 = this.CurrentOgranicenjeKolekcije.Id_tip_opreme2,
                tipProvere =  this.CurrentOgranicenjeKolekcije.TipProvere
            };
            this.CurrentOgranicenjeKolekcije = new GrupaOgranicenja();
            this.CurrentTipoviOpreme = new TipoviOpreme(null);
            DbItemGrupeOgranicenja[] nizOgranicenja = service.OgranicenjaInsert(ogranicenje);
            this.popuniListuOgranicenja(nizOgranicenja);
        }

        private void btnVratiSeNaPrethodnuStranu_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ContentControl ctlDugmiciUnosOgranicenja = ctmUnosOgranicenjaTemplate.FindName("ctmDugmiciUnosOgranicenja", ctmUnosOgranicenja) as ContentControl;



            


            ctlUnosOgranicenjaSredina.SetResourceReference(TemplateProperty, "ctpUnosOgranicenjaPrvaStrana");
            ctlDugmiciUnosOgranicenja.SetResourceReference(TemplateProperty, "ctpDugmiciUnosOgranicenjaPrvaStrana");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOpreme = service.TipOpreme();
            this.popuniListuTipovaOpreme(nizTipovaOpreme);

            for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            {
                if (ListaTipovaOpreme[i].IdTipOpreme == IzabranIdTipaOpremeZaKonfiguraciju)
                {
                    this.CurrentTipoviOpreme.IzabranTipOpreme = i;
                    break;
                }
            }
        }

        private void btnUcitajJosJednuGrupu_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;

            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ControlTemplate ctlUnosOgranicenjaSredinaTemplate = ctlUnosOgranicenjaSredina.Template;

            ContentControl ctlGrupaOgranicenja = ctlUnosOgranicenjaSredinaTemplate.FindName("ctlGrupaOgranicenja", ctlUnosOgranicenjaSredina) as ContentControl;
            ctlGrupaOgranicenja.Visibility = Visibility.Visible;

            Button jos = sender as Button;
            jos.Visibility = Visibility.Hidden;

           
            
        }

       

        private void btnUnesiOgranicenja_Click_1(object sender, RoutedEventArgs e)
        {
           ControlTemplate template = Sadrzaj.Template;

            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ControlTemplate ctlUnosOgranicenjaSredinaTemplate = ctlUnosOgranicenjaSredina.Template;

            ContentControl ctlGrupaOgranicenja = ctlUnosOgranicenjaSredinaTemplate.FindName("ctlGrupaOgranicenja", ctlUnosOgranicenjaSredina) as ContentControl;

            
        }

        private void cmbListaTipaProvere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            string nazivIzabranogTipaProvere = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaProvere.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        nazivIzabranogTipaProvere = ListaTipovaProvere[i];
                        break;
                    }
                }


                this.CurrentOgranicenjeKolekcije.TipProvere = nazivIzabranogTipaProvere;
            }
        }

        private void btnUcitajJosNarudzbina_Click(object sender, RoutedEventArgs e)
        {
            odakleKrece = dokleIde;
            dokleIde += 5;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbina = service.OpNarudzbineSelect();
            this.popuniListuNarudzbinaOpet(nizNarudzbina, odakleKrece, dokleIde);
        }

      

    }
}
