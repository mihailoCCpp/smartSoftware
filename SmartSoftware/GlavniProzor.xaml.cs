using Microsoft.Win32;
using SmartSoftware;
using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using SmartSoftware.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
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
using System.Xml;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window, INotifyPropertyChanged
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        bool dalisepozivabaza = false;
        private SmartSoftwareDocumentManagerVM documentManagerVM = new SmartSoftwareDocumentManagerVM();
        private List<Oprema> lista = new List<Oprema>();

        public List<Oprema> Lista
        {
            get { return lista; }
            set { lista = value; }
        }

        private SmartSoftwareGlavnaOblast tmpEditObj;

        public SmartSoftwareGlavnaOblast TmpEditObj
        {
            get { return tmpEditObj; }
            set { tmpEditObj = value; }
        }

        private bool aktivnoRezervisanje = false;

        public bool AktivnoRezervisanje
        {
            get { return aktivnoRezervisanje; }
            set { SetAndNotify(ref aktivnoRezervisanje, value); }
        }

        private TrenutnaListaRezervacija rezDodavanje = new TrenutnaListaRezervacija();

        public TrenutnaListaRezervacija RezDodavanje
        {
            get { return rezDodavanje; }
            set { SetAndNotify(ref rezDodavanje, value); }
        }

        private int idUloge;

        public GlavniProzor(int idUloge)
        {
            InitializeComponent();
            this.DataContext = documentManagerVM;

            if (idUloge == 1) AdminPanelKlik.Visibility = Visibility.Visible;
            else AdminPanelKlik.Visibility = Visibility.Hidden;
            


            //if(TipoviOpreme.Id = 1)
            //{
            //    slikaZaTipove.Source = 
            //}




            //OVDJE TESTIRANJE
            /*
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrimaStatistika[] nizFilteri = service.IstorijaKupovineNajprodavanijaOprema(true);


            int rezProdataOprema = service.IstorijaKupovineUkupanBrojProdateOpreme();

            DateTime danas = DateTime.Now;
            DateTime sutra = danas.AddDays(1);

            //ovo dole ne radi
            int rezProdataOpremaDanas = service.IstorijaKupovineUkupanBrojProdateOpremeZaDanas(danas, sutra);

            double rezZaradjeno = service.IstorijaKupovineUkupnoZaradjeno();

            double rezZaradjenoDanas = service.IstorijaKupovineZaradjenoDanas(danas, sutra);

            SmartSoftwareServiceReference.DbItemKupci[] nizNajcesciKupci = service.IstorijaKupovineNajcesciKupci(true);

            SmartSoftwareServiceReference.DbItemKupci[] nizVelikiPotrosaci = service.IstorijaKupovineKupciKojiNajviseKupuju(true);

            SmartSoftwareServiceReference.DbItemKupci[] nizKupciPotrosnja = service.IstorijaKupovineKupciKojiNajviseTrose(true);


            DbItemNarudzbine narudzbina1 = new DbItemNarudzbine()
            {
                datum_narudzbine = DateTime.Now,
                id_narudzbine = 8,
                id_oprema = 1,
                id_prodavca = 14
                kolicina = 1
            };

            bool rezultatNarudzbineInsert = service.OpNarudzbineInsert(narudzbina1);


            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbine = service.OpNarudzbineSelect();

            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbine1 = service.OpNarudzbinePrihvatiNarudzbinu(narudzbina1);

            DbItemNarudzbine narudzbina2 = new DbItemNarudzbine()
            {
                datum_narudzbine = DateTime.Now,
                id_narudzbine = 9,
                id_oprema = 6,
                id_prodavca = 14,
                kolicina = 3
            };
            bool rezultatNarudzbineInsert2 = service.OpNarudzbineInsert(narudzbina2);
            
            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbine2 = service.OpNarudzbineOdbijNarudzinu(narudzbina2);

            //kolekcija opreme
            SmartSoftwareServiceReference.DbItemKolekcijaOpreme[] nizKolekcijeOpreme = service.OpKolekcijaOpremeSelect();
            DbItemKolekcijaOpreme kolekcijaOpreme = new DbItemKolekcijaOpreme()
            {  
                cena = 10000,
                DeletedField = false,
                id_oprema = 0,
                id_tip_opreme = 13,
                kolicinaUKorpi = 0,
                kolicinaURezervacijama = 0,
                lager = true,
                kolicina_na_lageru = 10,
                kolicina_u_rezervi = 10,
                model = "nk3000",
                ListaParametara = new DbItemParametri[0],
                kolekcijaOpremeIdjevi = new int[2] { 1, 6 },
                KolekcijaOpreme = null,
                naslov = "nova kolekcija opreme",
                oprema_na_popustu = 0,
                opis = "znaci najnoviji komp mi ga sklapali najaci je master Marko i padawan Mihailo su sklapali tako da ono zna se sta valja",
                proizvodjac = "SmartSoftware prodavniica",
                slika = null,
                slikaOriginalPutanja = null,
                zaPretragu = ""
            };
            SmartSoftwareServiceReference.DbItemKolekcijaOpreme[] nizKolekcijeOpreme1 = service.OpKolekcijaOpremeInsert(kolekcijaOpreme);

            */
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

        private void trivju_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.documentManagerVM.CurrentDocumentVM.CurrentSmartSoftwareGlavnaOblastVM = e.NewValue as SmartSoftwareGlavnaOblastVM;



            pera.ItemsSource = null;
            pera2.Content = null;
            skrolVjuverEdit.Visibility = System.Windows.Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;

            TreeView t = sender as TreeView;
            TipoviOpremeVM t1 = t.SelectedItem as TipoviOpremeVM;
            ListaFiltera = null;
            if (t1 != null)
            {

                TipoviOpreme t3 = t1.Model as TipoviOpreme;
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima(t3.IdTipOpreme);
                this.popuniListuOprema(niz);
                pera.ItemsSource = Lista;
                this.popuniListuFiltera(t3.IdTipOpreme);

            }
            if (ListaFiltera != null)
            {
                if (ListaFiltera.Count > 0)
                {
                    grdFilteri.Visibility = Visibility.Visible;
                    grdFilteri.Height = 120;
                }
                else
                {
                    grdFilteri.Visibility = Visibility.Hidden;
                    grdFilteri.Height = 0;
                }
            }
            else
            {
                grdFilteri.Visibility = Visibility.Hidden;
                grdFilteri.Height = 0;
            }
        }

        private ObservableCollection<Parametri> listaFiltera = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaFiltera
        {
            get { return listaFiltera; }
            set { SetAndNotify(ref listaFiltera, value); }
        }


        private void popuniListuFiltera(int idTipOpreme)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizFilteri = service.PrikaziFiltere(idTipOpreme);
            this.ListaFiltera = new ObservableCollection<Parametri>();
            foreach (var item in nizFilteri)
            {
                Parametri p = new Parametri(null)
                {
                    DefaultVrednost = item.default_vrednost,
                    IdParametri = item.id_parametri,
                    IdTipOpreme = item.id_tip_opreme,
                    Name = item.naziv_parametra,
                    VrednostParametra = item.vrednost_parametra
                };
                for (int i = 0; i < item.ListaVrednostiZaFiltere.Length; i++)
                {
                    p.KolekcijaVrednostiZaFilter.Add(new VrednostiFiltera()
                    {
                        VrednostFiltera = item.ListaVrednostiZaFiltere[i],
                        idVrednostiFiltera = i + 1,
                        OdabranaVrednostZaFiltere = false
                    });
                }

                listaFiltera.Add(p);
            }
            filteriPrikaz.ItemsSource = listaFiltera;
        }

        private void popuniListuOprema(DbItemOpremaSaParametrima[] nizOpremaSaParametrima)
        {
            List<Oprema> Listaa = new List<Oprema>();
            for (int i = 0; i < nizOpremaSaParametrima.Length; i++)
            {
                Listaa.Add(new Oprema(null)
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
                    DaliMozeJosDaseDoda = nizOpremaSaParametrima[i].kolicina_na_lageru > 0
                });

                for (int j = 0; j < nizOpremaSaParametrima[i].ListaParametara.Length; j++)
                {
                    Parametri p = new Parametri(null)
                    {
                        DefaultVrednost = nizOpremaSaParametrima[i].ListaParametara[j].default_vrednost,
                        IdParametri = nizOpremaSaParametrima[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = nizOpremaSaParametrima[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra == null
                        || nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra == "" ?
                        KlasaKonstante.NEMA :
                        nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra,
                        Name = nizOpremaSaParametrima[i].ListaParametara[j].naziv_parametra
                    };

                    if (p.VrednostParametra.ToUpper() == "TRUE")
                    {
                        p.VrednostParametra = KlasaKonstante.IMA;
                    }
                    else if (p.VrednostParametra.ToUpper() == "FALSE")
                    {
                        p.VrednostParametra = KlasaKonstante.NEMA;
                    }

                    Listaa[i].ListaParametara.Add(p);
                }
            }
            this.Lista = Listaa;
        }

        private void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pera2.Visibility = Visibility.Visible;
            Grid g = sender as Grid;
            //MessageBox.Show(s.Children[0].ToString());

            //MessageBox.Show(g.FindName("skrivenId").GetType().ToString());
            TextBlock t = g.FindName("skrivenId") as TextBlock;
            //MessageBox.Show(t.Text);

            List<Oprema> o = new List<Oprema>();
            for (int i = 0; i < Lista.Count; i++)
            {
                if (lista[i].IdOprema == Int32.Parse(t.Text))
                {
                    o.Add(lista[i]);
                    this.TmpEditObj = this.lista[i];
                    break;
                }
            }

            Oprema o1 = tmpEditObj as Oprema;
            o1.TmpKolicinaNaLageru = o1.KolicinaNaLageru;
            o1.Tmp2KolicinaNaLageru = o1.KolicinaNaLageru;


            ////pera2Edit.Content = null;
            skrolVjuverEdit.Visibility = System.Windows.Visibility.Hidden;
            pera2.Content = null;

            if (dalisepozivabaza == true)
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.OpremeSaParametrima((this.tmpEditObj as Oprema).IdTipOpreme);
                popuniListuOprema(rez);
                this.dalisepozivabaza = false;
            }

            pera2.Content = o;
            //pera2.Visibility = System.Windows.Visibility.Visible;
            //pera.Visibility = System.Windows.Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = Visibility.Visible;

        }

        private void btnIzmeniArtikal_Click(object sender, RoutedEventArgs e)
        {
            skrolVjuverEdit.Visibility = Visibility.Visible;
            skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;

            List<SmartSoftwareGlavnaOblast> t = new List<SmartSoftwareGlavnaOblast>();

            t.Add(this.tmpEditObj);
            pera2.Content = null;
            pera2Edit.Content = null;
            pera2Edit.Content = t;
            dalisepozivabaza = true;




        }

        private void btnIzmeniISacuvaj_Click(object sender, RoutedEventArgs e)
        {

            // Korisnik dijalogom zadaje fajl iz koga se čita dokument
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Presentation file (*.jpg)|*.jpg"; // Ekstenzija ppp je ekstenzija dokumenta aplikacije Presentation
            //if (ofd.ShowDialog() ?? false == true)
            //{
            //    MessageBox.Show(ofd.FileName);
            //    MessageBox.Show(ofd.SafeFileName);

            //    string a = (tmpEditObj as Oprema).SlikaOriginalPutanja;
            //    int pozicija = a.LastIndexOf("/");
            //    string putanjaPrefix = a.Substring(0, pozicija + 1);
            //    System.IO.FileInfo fileInfo =
            //           new System.IO.FileInfo(ofd.FileName);
            //    SmartSoftwareServiceInterfaceClient clientUpload =
            //            new SmartSoftwareServiceInterfaceClient();
            //    SmartSoftwareServiceReference.RemoteFileInfo
            //           uploadRequestInfo = new RemoteFileInfo();

            //    using (System.IO.FileStream stream =
            //           new System.IO.FileStream(ofd.FileName,
            //           System.IO.FileMode.Open, System.IO.FileAccess.Read))
            //    {
            //        uploadRequestInfo.FileName = ofd.SafeFileName;
            //        uploadRequestInfo.Length = fileInfo.Length;
            //        uploadRequestInfo.FileByteStream = stream;
            //        clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);

            //        //clientUpload.UploadFile(stream);

            //        string zika = putanjaPrefix + ofd.SafeFileName;
            //        (tmpEditObj as Oprema).SlikaOriginalPutanja = zika;

            //    }
            //}





            Oprema o = tmpEditObj as Oprema;
            DbItemOpremaSaParametrima oprema = new DbItemOpremaSaParametrima()
            {
                cena = o.Cena,
                id_oprema = o.IdOprema,
                opis = o.Opis,
                id_tip_opreme = o.IdTipOpreme,
                kolicina_na_lageru = o.KolicinaNaLageru,
                kolicina_u_rezervi = o.KolicinaURezervi,
                lager = o.Lager,

                naslov = o.Name,
                oprema_na_popustu = o.OpremaNaPopustu,
                proizvodjac = o.Proizvodjac,
                slika = o.Slika,
                slikaOriginalPutanja = o.SlikaOriginalPutanja,
                model = o.Model

            };

            List<DbItemParametri> parametri = new List<DbItemParametri>();


            foreach (var item in o.ListaParametara)
            {
                parametri.Add(new DbItemParametri() { default_vrednost = item.DefaultVrednost, id_parametri = item.IdParametri, id_tip_opreme = item.IdTipOpreme, naziv_parametra = item.Name, vrednost_parametra = item.VrednostParametra });
            }

            oprema.ListaParametara = parametri.ToArray();
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            DbItemOpremaSaParametrima[] nizOpremaSaParametrima = service.UpdateOpreme(oprema);
            this.popuniListuOprema(nizOpremaSaParametrima);


            skrolVjuverEdit.Visibility = System.Windows.Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Visible;
            // pera2.ItemsSource = null;
            //pera
            List<Oprema> ponovniPrikazObjekta = new List<Oprema>();
            int idOprema = (this.tmpEditObj as Oprema).IdOprema;
            Oprema tmpO = null;
            foreach (var item in this.Lista)
            {
                if (item.IdOprema == idOprema)
                {
                    tmpO = item;
                    break;
                }
            }
            this.tmpEditObj = tmpO;

            ponovniPrikazObjekta.Add(tmpO);
            pera2.Content = ponovniPrikazObjekta;


            if (dalisepozivabaza == true)
            {
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.OpremeSaParametrima((this.tmpEditObj as Oprema).IdTipOpreme);
                popuniListuOprema(rez);
                this.dalisepozivabaza = false;
            }

            pera.ItemsSource = null;
            pera.ItemsSource = this.Lista;
        }

        private void btnOtkaziIzmene_Click(object sender, RoutedEventArgs e)
        {
            skrolVjuverEdit.Visibility = Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = Visibility.Visible;
            // pera2.ItemsSource = null;
            if (dalisepozivabaza == true)
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((this.tmpEditObj as Oprema).IdTipOpreme);
                popuniListuOprema(niz);
                this.dalisepozivabaza = false;
            }

            List<Oprema> ponovniPrikazObjekta = new List<Oprema>();
            int idOprema = (this.tmpEditObj as Oprema).IdOprema;
            Oprema tmpO = null;
            foreach (var item in this.Lista)
            {
                if (item.IdOprema == idOprema)
                {
                    tmpO = item;
                    break;
                }
            }
            //ovo si prosli put izmenio da bi radilo za text a za sliku ne radi i dalje
            this.tmpEditObj = tmpO;
            //mozda je neka fora sto se slika menja za stari objekat
            ponovniPrikazObjekta.Add(tmpO);
            pera2.Content = ponovniPrikazObjekta;
        }

        private void btnProdajArtikal_Click(object sender, RoutedEventArgs e)
        {

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



            int brojac = 0;
            int duzina = this.documentManagerVM.Korpa.Count;
            for (brojac = 0; brojac < duzina; brojac++)
            {
                if ((this.documentManagerVM.Korpa[brojac] as Oprema).IdOprema == (this.tmpEditObj as Oprema).IdOprema)
                {
                    break;
                }
            }
            if (brojac == duzina)
            {
                //this.documentManagerVM.Korpa.Add(tmpEditObj);
                if ((tmpEditObj as Oprema).IzabranaKolicina <= (tmpEditObj as Oprema).KolicinaNaLageru)
                {

                    SmartSoftwareServiceReference.OperationObject rez = service.KorpaInsert((tmpEditObj as Oprema).IdOprema, (tmpEditObj as Oprema).IzabranaKolicina);
                }
            }
            else
            {
                //onda samo povecaj ili smanji broj 
                if ((tmpEditObj as Oprema).IzabranaKolicina + (documentManagerVM.Korpa[brojac] as Oprema).IzabranaKolicina <= (documentManagerVM.Korpa[brojac] as Oprema).KolicinaNaLageru)
                {
                    SmartSoftwareServiceReference.OperationObject rez = service.KorpaUpdate((tmpEditObj as Oprema).IdOprema, (tmpEditObj as Oprema).IzabranaKolicina + ((documentManagerVM.Korpa[brojac] as Oprema).IzabranaKolicina));
                }
            }
            Oprema op = TmpEditObj as Oprema;

            //  op.IzabranaKolicina += op.TmpIzabranaKolicina;
            //op.Tmp2KolicinaNaLageru = op.KolicinaNaLageru - op.IzabranaKolicina;
            //op.TmpIzabranaKolicina = 1;
            //op.DaliMozeJosDaseDoda = op.IzabranaKolicina != op.KolicinaNaLageru;
            //if (!op.DaliMozeJosDaseDoda)
            //{
            //    op.IzabranaKolicina = 1;
            //}

            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect();



            this.documentManagerVM.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

            for (int i = 0; i < oprema.Length; i++)
            {
                this.documentManagerVM.Korpa.Add(new Oprema(null)
                {
                    Cena = oprema[i].cena,
                    IdOprema = oprema[i].id_oprema,
                    IdTipOpreme = oprema[i].id_tip_opreme,
                    KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
                    KolicinaURezervi = oprema[i].kolicina_u_rezervi,
                    Lager = oprema[i].lager,
                    Model = oprema[i].model,
                    Name = oprema[i].naslov,
                    Opis = oprema[i].opis,
                    OpremaNaPopustu = oprema[i].oprema_na_popustu,
                    Proizvodjac = oprema[i].proizvodjac,
                    Slika = oprema[i].slika,
                    SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
                    IzabranaKolicina = oprema[i].kolicinaUKorpi

                });



                for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
                {
                    (this.documentManagerVM.Korpa[i] as Oprema).ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                        IdParametri = oprema[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                        Name = oprema[i].ListaParametara[j].naziv_parametra
                    });

                }
            }


            this.documentManagerVM.Rezervacije = this.documentManagerVM.Korpa;


            int id = (tmpEditObj as Oprema).IdOprema;

            if (tmpEditObj != null)
            {
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((tmpEditObj as Oprema).IdTipOpreme);
                this.popuniListuOprema(niz);
                List<Oprema> o = new List<Oprema>();
                for (int i = 0; i < this.Lista.Count; i++)
                {
                    if (this.Lista[i].IdOprema == id)
                    {
                        o.Add(this.Lista[i]);
                    }
                }

                pera.ItemsSource = null;
                pera.ItemsSource = this.Lista;

                pera2.Content = null;
                pera2.Content = o;
                tmpEditObj = o[0];
            }


        }

        private void btnSkiniSaLageraArtikal_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            if (this.TmpEditObj != null)
            {
                this.popuniListuOprema(service.UpdateOpremeSkiniOpremuSaLagera((tmpEditObj as Oprema).IdOprema));
            }
        }

        private void btnRezervisiArtikal_Click(object sender, RoutedEventArgs e)
        {

            if (!this.aktivnoRezervisanje)
            {
                this.AktivnoRezervisanje = true;
                this.rezDodavanje = new TrenutnaListaRezervacija();
                rezDodavanje.ShowDialog();
            }





            int brojac = 0;
            int duzina = rezDodavanje.Rezervacije.Count;
            for (brojac = 0; brojac < duzina; brojac++)
            {
                if ((rezDodavanje.Rezervacije[brojac] as Oprema).IdOprema == (this.tmpEditObj as Oprema).IdOprema)
                {
                    break;
                }
            }
            if (brojac == duzina)
            {
                //this.documentManagerVM.Korpa.Add(tmpEditObj);
                if ((tmpEditObj as Oprema).IzabranaKolicina <= (tmpEditObj as Oprema).KolicinaNaLageru)
                {

                    rezDodavanje.Rezervacije.Add(tmpEditObj);
                    (tmpEditObj as Oprema).TmpIzabranaKolicina = 1;
                }
            }
            else
            {
                (rezDodavanje.Rezervacije[brojac] as Oprema).TmpIzabranaKolicina += 1;
            }


            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



            //int brojac = 0;
            //int duzina = this.documentManagerVM.Rezervacije.Count;
            //for (brojac = 0; brojac < duzina; brojac++)
            //{
            //    if ((this.documentManagerVM.Rezervacije[brojac] as Oprema).IdOprema == (this.tmpEditObj as Oprema).IdOprema)
            //    {
            //        break;
            //    }
            //}
            //if (brojac == duzina)
            //{
            //    //this.documentManagerVM.Korpa.Add(tmpEditObj);
            //    if ((tmpEditObj as Oprema).IzabranaKolicina <= (tmpEditObj as Oprema).KolicinaNaLageru)
            //    {

            //        SmartSoftwareServiceReference.OperationObject rez = service.RezervacijeInsert((tmpEditObj as Oprema).IdOprema, (tmpEditObj as Oprema).IzabranaKolicina,"ime");
            //    }
            //}
            //else
            //{
            //    //onda samo povecaj ili smanji broj 
            //    if ((tmpEditObj as Oprema).IzabranaKolicina + (documentManagerVM.Rezervacije[brojac] as Oprema).IzabranaKolicina <= (documentManagerVM.Rezervacije[brojac] as Oprema).KolicinaNaLageru)
            //    {
            //        SmartSoftwareServiceReference.OperationObject rez = service.RezervacijeUpdate((tmpEditObj as Oprema).IdOprema, (tmpEditObj as Oprema).IzabranaKolicina + ((documentManagerVM.Rezervacije[brojac] as Oprema).IzabranaKolicina));
            //    }
            //}
            //Oprema op = TmpEditObj as Oprema;

            ////  op.IzabranaKolicina += op.TmpIzabranaKolicina;
            ////op.Tmp2KolicinaNaLageru = op.KolicinaNaLageru - op.IzabranaKolicina;
            ////op.TmpIzabranaKolicina = 1;
            ////op.DaliMozeJosDaseDoda = op.IzabranaKolicina != op.KolicinaNaLageru;
            ////if (!op.DaliMozeJosDaseDoda)
            ////{
            ////    op.IzabranaKolicina = 1;
            ////}

            //SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.RezervacijeSelect();



            //this.documentManagerVM.Rezervacije = new ObservableCollection<SmartSoftwareGlavnaOblast>();

            //for (int i = 0; i < oprema.Length; i++)
            //{
            //    this.documentManagerVM.Rezervacije.Add(new Oprema(null)
            //    {
            //        Cena = oprema[i].cena,
            //        IdOprema = oprema[i].id_oprema,
            //        IdTipOpreme = oprema[i].id_tip_opreme,
            //        KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
            //        KolicinaURezervi = oprema[i].kolicina_u_rezervi,
            //        Lager = oprema[i].lager,
            //        Model = oprema[i].model,
            //        Name = oprema[i].naslov,
            //        Opis = oprema[i].opis,
            //        OpremaNaPopustu = oprema[i].oprema_na_popustu,
            //        Proizvodjac = oprema[i].proizvodjac,
            //        Slika = oprema[i].slika,
            //        SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
            //        IzabranaKolicina = oprema[i].kolicinaUKorpi

            //    });



            //    for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
            //    {
            //        (this.documentManagerVM.Rezervacije[i] as Oprema).ListaParametara.Add(new Parametri(null)

            //        {
            //            DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
            //            IdParametri = oprema[i].ListaParametara[j].id_parametri,
            //            IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
            //            VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
            //            Name = oprema[i].ListaParametara[j].naziv_parametra
            //        });

            //    }
            //}


            //this.documentManagerVM.Rezervacije = this.documentManagerVM.Rezervacije;


            //int id = (tmpEditObj as Oprema).IdOprema;

            //if (tmpEditObj != null)
            //{
            //    this.popuniListuOprema((tmpEditObj as Oprema).IdTipOpreme, null);
            //    List<Oprema> o = new List<Oprema>();
            //    for (int i = 0; i < this.Lista.Count; i++)
            //    {
            //        if (this.Lista[i].IdOprema == id)
            //        {
            //            o.Add(this.Lista[i]);
            //        }
            //    }

            //    pera.ItemsSource = null;
            //    pera.ItemsSource = this.Lista;

            //    pera2.ItemsSource = null;
            //    pera2.ItemsSource = o;
            //    tmpEditObj = o[0];
            //}
        }

        private void btnPopustArtikal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNaruciArtikal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void korpaklik_MouseDown(object sender, RoutedEventArgs e)
        {
            KorpaProzor prozor = new KorpaProzor();
            //prozor.Korpa = null;
            //prozor.Korpa = this.documentManagerVM.Korpa;

            prozor.ShowDialog();


            //vidite ovo dole da li treba
            this.documentManagerVM.Korpa = prozor.Korpa;
            //ovo u sredini







            if (tmpEditObj != null)
            {
                int id = (tmpEditObj as Oprema).IdOprema;

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((tmpEditObj as Oprema).IdTipOpreme);
                this.popuniListuOprema(niz);
                List<Oprema> o = new List<Oprema>();
                for (int i = 0; i < this.Lista.Count; i++)
                {
                    if (this.Lista[i].IdOprema == id)
                    {
                        o.Add(this.Lista[i]);
                    }
                }

                pera.ItemsSource = null;
                pera.ItemsSource = this.Lista;

                pera2.Content = null;
                pera2.Content = o;
                tmpEditObj = o[0];
            }

        }

        private void perakloz(object sender, EventArgs e)
        {
            this.documentManagerVM.Korpa = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(null);


            Application.Current.Shutdown();

        }

        private void RezervacijeKlik_MouseDown(object sender, RoutedEventArgs e)
        {
            RezervacijePregledProzor prozor = new RezervacijePregledProzor();
            prozor.ShowDialog();


            //this.documentManagerVM.Rezervacije = prozor.Rezervacije;







            if (tmpEditObj != null)
            {
                int id = (tmpEditObj as Oprema).IdOprema;

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((tmpEditObj as Oprema).IdTipOpreme);

                this.popuniListuOprema(niz);
                List<Oprema> o = new List<Oprema>();
                for (int i = 0; i < this.Lista.Count; i++)
                {
                    if (this.Lista[i].IdOprema == id)
                    {
                        o.Add(this.Lista[i]);
                    }
                }

                pera.ItemsSource = null;
                pera.ItemsSource = this.Lista;

                pera2.Content = null;
                pera2.Content = o;
                tmpEditObj = o[0];
            }
        }

        private void btnRezervisiArtikal_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void tbPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            string zaPretragu = t.Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.PretragaOpreme(zaPretragu, false);
            this.popuniListuOprema(niz);
            pera.ItemsSource = null;
            pera.ItemsSource = this.Lista;
        }

        private void chbFilteri_Checked(object sender, RoutedEventArgs e)
        {

            ICollectionView checkboxovi = CollectionViewSource.GetDefaultView(this.ListaFiltera);
            Parametri p = checkboxovi.CurrentItem as Parametri;

            CheckBox ch = sender as CheckBox;
            TextBlock textZaId = (ch.Parent as StackPanel).FindName("skrivenId") as TextBlock;
            int skrivenId = Convert.ToInt32(textZaId.Text);

            p.OdabranParametarZaFiltere = true;
            foreach (var item in p.KolekcijaVrednostiZaFilter)
            {
                if (item.idVrednostiFiltera == skrivenId)
                {
                    item.OdabranaVrednostZaFiltere = true;
                }
            }



            //ICollectionView vrednostiCheckboxova = CollectionViewSource.GetDefaultView(p.KolekcijaVrednostiZaFilter);
            //VrednostiFiltera stikliranaVrednost = vrednostiCheckboxova.CurrentItem as VrednostiFiltera;

            this.PrikaziOpremuNaOsnovuIzabranihFiltera();

        }


        private void PrikaziOpremuNaOsnovuIzabranihFiltera()
        {



            //sastavljanje uslova upita
            //rade stavke grane ALI NE RADE TJ NE KUPI
            //SVE GRANE proverite ovo dole sto ne kupi sve lepo 
            // ana vveb servisu rai lepo onaj upit marko car !!! :D 
            List<DbItemParametri> listaZaUpit = new List<DbItemParametri>();
            foreach (var grana in this.listaFiltera)
            {
                if (grana.OdabranParametarZaFiltere)
                {
                    DbItemParametri v = new DbItemParametri();
                    v.naziv_parametra = grana.Name;
                    List<string> s = new List<string>();
                    foreach (var stavka in grana.KolekcijaVrednostiZaFilter)
                    {
                        if (stavka.OdabranaVrednostZaFiltere)
                        {
                            s.Add(stavka.VrednostFiltera);
                        }
                    }
                    v.ListaVrednostiZaFiltere = s.ToArray();
                    listaZaUpit.Add(v);
                }
            }

            //servis da se pozove

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.PrikaziOpremuPoFilterima(1, listaZaUpit.ToArray());
            this.popuniListuOprema(niz);
            pera.ItemsSource = null;
            pera.ItemsSource = this.Lista;
        }

        private void chbFilteri_Unchecked(object sender, RoutedEventArgs e)
        {
            ICollectionView checkboxovi = CollectionViewSource.GetDefaultView(this.ListaFiltera);
            Parametri p = checkboxovi.CurrentItem as Parametri;

            CheckBox ch = sender as CheckBox;
            TextBlock textZaId = (ch.Parent as StackPanel).FindName("skrivenId") as TextBlock;
            int skrivenId = Convert.ToInt32(textZaId.Text);

            int brojac = 0;

            foreach (var item in p.KolekcijaVrednostiZaFilter)
            {
                if (item.idVrednostiFiltera == skrivenId && item.OdabranaVrednostZaFiltere)
                {
                    item.OdabranaVrednostZaFiltere = false;
                    break;
                }
            }

            foreach (var item in p.KolekcijaVrednostiZaFilter)
            {
                if (item.OdabranaVrednostZaFiltere)
                {
                    brojac++;
                }
            }

            if (brojac == 0)
            {
                p.OdabranParametarZaFiltere = false;
            }



            //ICollectionView vrednostiCheckboxova = CollectionViewSource.GetDefaultView(p.KolekcijaVrednostiZaFilter);
            //VrednostiFiltera stikliranaVrednost = vrednostiCheckboxova.CurrentItem as VrednostiFiltera;

            this.PrikaziOpremuNaOsnovuIzabranihFiltera();

        }


        private void AdminPanelKlik_Click(object sender, RoutedEventArgs e)
        {
            AdminPanelProzor adminPanel = new AdminPanelProzor();
            adminPanel.ShowDialog();
            this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());
        }

        private void probaDugme_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("aaaa");
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void dugmeZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dugmeMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
            else this.WindowState = System.Windows.WindowState.Normal;
        }

        private void dugmeMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnUcitajPonovoIzBaze_Click(object sender, RoutedEventArgs e)
        {
            this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());
        }


    }
}