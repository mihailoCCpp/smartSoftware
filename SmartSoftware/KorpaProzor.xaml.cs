using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Microsoft.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for KorpaProzor.xaml
    /// </summary>
    public partial class KorpaProzor : Window, INotifyPropertyChanged
    {

        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { korpa = value; }
        }


        private ObservableCollection<SmartSoftwareGlavnaOblast> korpaProvera = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> KorpaProvera
        {
            get { return korpaProvera; }
            set { korpaProvera = value; }
        }

        private double ukupnaCenaUKorpi = 0;

        public double UkupnaCenaUKorpi
        {
            get {
                double suma = 0;

                foreach (var item in this.Korpa)
                {
                    suma += (item as Oprema).SumCena;
                }

                SetAndNotify(ref ukupnaCenaUKorpi, suma);
                return this.ukupnaCenaUKorpi;
            }

            //set
            //{
            //    SetAndNotify(ref ukupnaCenaUKorpi, value);
            //}
            
        }

        

        private Oprema tmpEditObj;

        public Oprema TmpEditObj
        {
            get { return tmpEditObj; }
            set { tmpEditObj = value; }
        }

        
        public KorpaProzor()
        {
            InitializeComponent();
            //this.pera.ItemsSource = this.Korpa;
            this.DataContext = this;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect();
            this.PopuniKorpu(oprema);
        }




        private void PopuniKorpu(DbItemOpremaSaParametrima[] oprema)
        {

          
            this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

            for (int i = 0; i < oprema.Length; i++)
            {
                this.Korpa.Add(new Oprema(null)
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
                    IzabranaKolicina = oprema[i].kolicinaUKorpi,
                    SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena

                });

                this.KorpaProvera.Add(new Oprema(null)
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
                    IzabranaKolicina = oprema[i].kolicinaUKorpi,
                    SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena
                });


                

                for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
                {
                    (this.Korpa[i] as Oprema).ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                        IdParametri = oprema[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                        Name = oprema[i].ListaParametara[j].naziv_parametra
                    });

                    (this.KorpaProvera[i] as Oprema).ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                        IdParametri = oprema[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                        Name = oprema[i].ListaParametara[j].naziv_parametra
                    });

                }

            }


        }


       

        private void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Oprema o1 = tmpEditObj as Oprema;
            StackPanel s = sender as StackPanel;
            Xceed.Wpf.Toolkit.MessageBox.Show(sender.GetType().ToString());


            


            //MessageBox.Show(s.Children[0].ToString());
            Grid g = s.Children[0] as Grid;
            //MessageBox.Show(g.FindName("skrivenId").GetType().ToString());
            TextBlock t = g.FindName("skrivenId") as TextBlock;
            //MessageBox.Show(t.Text);

            List<Oprema> o = new List<Oprema>();
            for (int i = 0; i < Korpa.Count; i++)
            {
                if ((Korpa[i] as Oprema).IdOprema == Int32.Parse(t.Text))
                {
                  //  o.Add(lista[i] as Oprema);
                    this.TmpEditObj = (Korpa[i] as Oprema);
                    break;
                }
            }
        }

        private void SacuvajIzmeneUKorpi()
        {

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect();

            for (int i = 0; i < Korpa.Count; i++)
            {
                if ((this.Korpa[i] as Oprema).IzabranaKolicina != (this.KorpaProvera[i] as Oprema).IzabranaKolicina)
                {
                    service.KorpaUpdate((Korpa[i] as Oprema).IdOprema, (int)(Korpa[i] as Oprema).IzabranaKolicina);
                    //(Korpa[i] as Oprema).Tmp2KolicinaNaLageru = (Korpa[i] as Oprema).KolicinaNaLageru - (Korpa[i] as Oprema).IzabranaKolicina;
                    (Korpa[i] as Oprema).TmpIzabranaKolicina = 1;
                    //(Korpa[i] as Oprema).DaliMozeJosDaseDoda = (Korpa[i] as Oprema).IzabranaKolicina != (Korpa[i] as Oprema).KolicinaNaLageru;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.SacuvajIzmeneUKorpi();
        }

        private void kolicina_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            Xceed.Wpf.Toolkit.IntegerUpDown updown = sender as Xceed.Wpf.Toolkit.IntegerUpDown;
            //MessageBox.Show(updown.Parent.GetType().ToString());
            Grid grid = updown.Parent as Grid;
            int id = 0;
            TextBlock t = grid.FindName("skrivenId") as TextBlock;
            id = Convert.ToInt32(t.Text);
            for (int i = 0; i < this.Korpa.Count; i++)
            {
                if((this.Korpa[i] as Oprema).IdOprema == id)
                {
                    (this.Korpa[i] as Oprema).SumCena = (this.Korpa[i] as Oprema).IzabranaKolicina * (this.Korpa[i] as Oprema).Cena;
                }
            }
            //pera.ItemsSource = this.Korpa;
        }

        private void kolicina_MouseDown(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {


            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBlock txtBox = (TextBlock)spinner.Content;

            int value = String.IsNullOrEmpty(txtBox.Text) ? 0 : Convert.ToInt32(txtBox.Text);
            if (e.Direction == SpinDirection.Increase )
                value++;
            else
                value--;
            txtBox.Text = value.ToString();

            Grid grid = spinner.Parent as Grid;
            Grid grid1 = grid.Parent as Grid;
            int id = 0;
            TextBlock t = grid1.FindName("skrivenId") as TextBlock;
            id = Convert.ToInt32(t.Text);
            for (int i = 0; i < this.korpa.Count; i++)
            {
                if ((this.korpa[i] as Oprema).IdOprema == id)
                {
                    
                    Oprema tmp = (this.korpa[i] as Oprema);
                    tmp.IzabranaKolicina = value;
                    tmp.SumCena = (this.korpa[i] as Oprema).IzabranaKolicina * (this.korpa[i] as Oprema).Cena;
                   // this.ukupnaCenaUKorpi += tmp.SumCena; 
                    if (value == tmp.KolicinaNaLageru)
                    {
                        spinner.ValidSpinDirection = ValidSpinDirections.Decrease;
                    }
                    else if (value == 1)
                    {
                        spinner.ValidSpinDirection = ValidSpinDirections.Increase;
                    }
                    else
                    {
                        ButtonSpinner b = new ButtonSpinner();
                        spinner.ValidSpinDirection = b.ValidSpinDirection;
                        b = null;
                    }
                }
            }


            //foreach (var item in this.Korpa)
            //{
            //    UkupnaCenaUKorpi += (item as Oprema).SumCena;
            //}
            this.refreshujUkupnuCenu();
            //Xceed.Wpf.Toolkit.MessageBox.Show(this.ukupnaCenaUKorpi.ToString());


            //TextBlock textBlockUkunaCena = this.gridDugmici.FindName("txbUkupnaCena") as TextBlock;
            //textBlockUkunaCena.Text = "Ukupna cena u korpi: " + this.UkupnaCenaUKorpi.ToString();
        }

        public void refreshujUkupnuCenu()
        {
            this.ukupnaCenaUKorpi = this.UkupnaCenaUKorpi;
        }

        private void btnObrisiCeluKorpu_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(null);
            pera.ItemsSource = null;
            this.korpa = this.korpaProvera = new ObservableCollection<SmartSoftwareGlavnaOblast>();
            refreshujUkupnuCenu();
        }

        private void btnObrisiIzKorpe_Click(object sender, RoutedEventArgs e)
        {

            Button b = sender as Button;
            Grid g = b.Parent as Grid;
            Grid g1 = g.Parent as Grid;
            TextBlock t = g1.FindName("skrivenId") as TextBlock;
            int id = Convert.ToInt32(t.Text);

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(id);
            pera.ItemsSource = null;
            this.PopuniKorpu(rez);
            pera.ItemsSource = this.Korpa;
            refreshujUkupnuCenu();
        }

        private void btnKupi_Click(object sender, RoutedEventArgs e)
        {
            this.SacuvajIzmeneUKorpi();
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect();
            this.PopuniKorpu(oprema);
            this.Close();
            KupovinaProzor kupovinaProzor = new KupovinaProzor();
            kupovinaProzor.UkupnaCena = this.UkupnaCenaUKorpi;
            kupovinaProzor.Korpa = this.Korpa;
            kupovinaProzor.ShowDialog();
            this.Korpa = kupovinaProzor.Korpa;
            this.Close();
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
