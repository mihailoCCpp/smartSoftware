using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace SmartSoftware.Model
{
    public class Oprema : SmartSoftwareGlavnaOblast
    {

        

        private int idTipOpreme;

        public int IdTipOpreme
        {
            get { return idTipOpreme; }
            set { SetAndNotify(ref idTipOpreme, value); }
        }

        private int idOprema;

        public int IdOprema
        {
            get { return idOprema; }
            set { SetAndNotify(ref idOprema, value); }
        }

        private int izabranaKolicina = 0;

        public int IzabranaKolicina
        {
            get { return izabranaKolicina; }
            set { SetAndNotify(ref izabranaKolicina, value);}
        }

        private int? tmpIzabranaKolicina = 1;

        public int? TmpIzabranaKolicina
        {
            get { return tmpIzabranaKolicina; }
            set { SetAndNotify(ref tmpIzabranaKolicina, value); }
        }

        private bool daliMozeJosDaSeDoda = true;

        public bool DaliMozeJosDaseDoda
        {
            get
            {
                return daliMozeJosDaSeDoda;
            }
            set
            {
                SetAndNotify(ref daliMozeJosDaSeDoda, value);
            }
        }

        private int? kolikoPutajeProdata;

        public int? KolikoPutajeProdata
        {
            get { return kolikoPutajeProdata; }
            set { SetAndNotify(ref kolikoPutajeProdata, value); }
        }

        private double sumCena;

        public double SumCena
        {
            get {
                SumCena = this.izabranaKolicina * this.cena;
                return sumCena; 
            }
            set {
                SetAndNotify(ref sumCena, value);
            }
        }

        public string VratiTextZaDugme
        {
            get
            {
                if(this.DeletedItem)
                {
                    return "Restoruj ovu opremu";
                }
                else
                {
                    return "Izbrisi ovu opremu";
                }
            }
        }


        private string proizvodjac;

        public string Proizvodjac
        {
            get { return proizvodjac; }
            set { SetAndNotify(ref proizvodjac, value); }
        }

        private double cena;

        public double Cena
        {
            get { return cena; }
            set { SetAndNotify(ref cena, value); }
        }

        private string opis;

        public string Opis
        {
            get { return opis; }
            set { SetAndNotify(ref opis, value); }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { SetAndNotify(ref model, value); }
        }


        private bool? lager;

        public bool? Lager
        {
            get { return lager; }
            set { SetAndNotify(ref lager, value); }
        }

        private int? kolicinaURezervi;

        public int? KolicinaURezervi
        {
            get { return kolicinaURezervi; }
            set { SetAndNotify(ref kolicinaURezervi, value); }
        }


        private int? kolicinaNaLageru;

        public int? KolicinaNaLageru
        {
            get { return kolicinaNaLageru; }
            set { SetAndNotify(ref kolicinaNaLageru, value); }
        }


        private int? tmpKolicinaNaLageru;

        public int? TmpKolicinaNaLageru
        {
            get { return tmpKolicinaNaLageru; }
            set { SetAndNotify(ref tmpKolicinaNaLageru, value); }
        }

        private int? tmp2KolicinaNaLageru;

        public int? Tmp2KolicinaNaLageru
        {
            get { return tmp2KolicinaNaLageru; }
            set { SetAndNotify(ref tmp2KolicinaNaLageru, value); }
        }


        private int? opremaNaPopustu;

        public int? OpremaNaPopustu
        {
            get { return opremaNaPopustu; }
            set { SetAndNotify(ref opremaNaPopustu, value); }
        }


        private string slika;

        public string Slika
        {
            get { return slika; }
            set { SetAndNotify(ref slika, value); }
        }

        private string slikaOriginalPutanja;

        public string SlikaOriginalPutanja
        {
            get { return slikaOriginalPutanja; }
            set { SetAndNotify(ref slikaOriginalPutanja, value); }
        }


        private int izabranTipOpreme;

        public int IzabranTipOpreme
        {
            get { return izabranTipOpreme; }
            set { SetAndNotify(ref izabranTipOpreme, value); }
        }

      

        public Oprema(OblastiOpreme parent)
            : base(parent)
        {

        }

        private bool daLiJeSlikaOpremePromenjena = false;

        public bool DaLiJeSlikaOpremePromenjena
        {
            get { return daLiJeSlikaOpremePromenjena; }
            set { daLiJeSlikaOpremePromenjena = value; }
        }


        private ObservableCollection<Parametri> listaParametara = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaParametara
        {
            get { return listaParametara; }
            //set { SetAndNotify(ref listaParametara, value); }
        }


        private ObservableCollection<Oprema> kolekcijaOpreme = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> KolekcijaOpreme
        {
            get { return kolekcijaOpreme; }
            set { kolekcijaOpreme = value; }
        }

    }

    public class KupljenaOprema : Oprema
    {
        private int? prodataKolicina;

        public int? ProdataKolicina
        {
            get { return prodataKolicina; }
            set { SetAndNotify(ref prodataKolicina, value); }
        }

        private double? cena_opreme_kad_je_prodata;

        public double? Cena_opreme_kad_je_prodata
        {
            get { return cena_opreme_kad_je_prodata; }
            set { SetAndNotify(ref cena_opreme_kad_je_prodata, value); }
        }

        private double? popust_na_cenu;

        public double? Popust_na_cenu
        {
            get { return popust_na_cenu; }
            set { SetAndNotify(ref popust_na_cenu, value); }
        }

        private int? ukupna_cena_artikla;

        public int? Ukupna_cena_artikla
        {
            get { return ukupna_cena_artikla; }
            set { SetAndNotify(ref ukupna_cena_artikla, value); }
        }

        private int id_istorija_kupovine;

        public int Id_istorija_kupovine
        {
            get { return id_istorija_kupovine; }
            set { SetAndNotify(ref id_istorija_kupovine, value); }
        }

        public KupljenaOprema(OblastiOpreme parent) : base(parent)
        {

        }
    }




    public class Narudzbina : INotifyPropertyChanged
    {
        private Oprema oprema;

        public Oprema Oprema
        {
            get { return oprema; }
            set { SetAndNotify(ref oprema, value); }
        }


        private DateTime? datumNarudzbine;

        public DateTime? DatumNarudzbine
        {
            get { return datumNarudzbine; }
            set { SetAndNotify(ref datumNarudzbine, value); }
        }
        private int narucenaKolicina;

        public int NarucenaKolicina
        {
            get { return narucenaKolicina; }
            set { SetAndNotify(ref narucenaKolicina, value); }
        }
        private Korisnici prodavac;

        public Korisnici Prodavac
        {
            get { return prodavac; }
            set { SetAndNotify(ref prodavac, value); }
        }

        private int redniBrojNarudzbine;

        public int RedniBrojNarudzbine
        {
            get { return redniBrojNarudzbine; }
            set { SetAndNotify(ref redniBrojNarudzbine, value); }
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

    public class GrupaOgranicenja : INotifyPropertyChanged
    {

        private int id_grupe_ogranicenja;

        public int Id_grupe_ogranicenja
        {
            get { return id_grupe_ogranicenja; }
            set { SetAndNotify(ref id_grupe_ogranicenja, value); }
        }
        private int id_tip_opreme1;

        public int Id_tip_opreme1
        {
            get { return id_tip_opreme1; }
            set { SetAndNotify(ref id_tip_opreme1, value); }
        }

        private string nazivTipaOpreme1;

        public string NazivTipaOpreme1
        {
            get { return nazivTipaOpreme1; }
            set { SetAndNotify(ref nazivTipaOpreme1, value); }
        }

        private int id_parametra1;

        public int Id_parametra1
        {
            get { return id_parametra1; }
            set { SetAndNotify(ref id_parametra1, value); }
        }
        
        private string nazivParametra1;

        public string NazivParametra1
        {
            get { return nazivParametra1; }
            set { SetAndNotify(ref nazivParametra1, value); }
        }


        private int? id_tip_opreme2;

        public int? Id_tip_opreme2
        {
            get { return id_tip_opreme2; }
            set { SetAndNotify(ref id_tip_opreme2, value); }
        }

        private string nazivTipaOpreme2;

        public string NazivTipaOpreme2
        {
            get { return nazivTipaOpreme2; }
            set { SetAndNotify(ref nazivTipaOpreme2, value); }
        }

        private int? id_parametra2;

        public int? Id_parametra2
        {
            get { return id_parametra2; }
            set { SetAndNotify(ref id_parametra2, value); }
        }

        private string nazivParametra2;
                                                    
        public string NazivParametra2
        {
            get { return nazivParametra2; }
            set { SetAndNotify(ref nazivParametra2, value); }
        }
        private string tipProvere;

        public string TipProvere
        {
            get { return tipProvere; }
            set { SetAndNotify(ref tipProvere, value); }
        }


        private int id_tip_opreme_kolekcije;

        public int Id_tip_opreme_kolekcije
        {
            get { return id_tip_opreme_kolekcije; }
            set { SetAndNotify(ref id_tip_opreme_kolekcije, value); }
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
