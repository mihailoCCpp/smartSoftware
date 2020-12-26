using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.Model
{
    public class Korisnici : INotifyPropertyChanged
    {

        private int idKorisnici;

        public int IdKorisnici
        {
            get { return idKorisnici; }
            set { SetAndNotify(ref idKorisnici, value); }
        }

        private string imeKorisnika;

        public string ImeKorisnika
        {
            get { return imeKorisnika; }
            set { SetAndNotify(ref imeKorisnika, value); }
        }

        private string prezimeKorisnika;

        public string PrezimeKorisnika
        {
            get { return prezimeKorisnika; }
            set { SetAndNotify(ref prezimeKorisnika, value); }
        }


        private int izabranaUloga;

        public int IzabranaUloga
        {
            get { return izabranaUloga; }
            set { SetAndNotify(ref izabranaUloga, value); }
        }

        private string mejlKorisnika;

        public string MejlKorisnika
        {
            get { return mejlKorisnika; }
            set { SetAndNotify(ref mejlKorisnika, value); }
        }



        private string brojTelefonaKorisnika;

        public string BrojTelefonaKorisnika
        {
            get { return brojTelefonaKorisnika; }
            set { SetAndNotify(ref brojTelefonaKorisnika, value); }
        }

        private double brojOstvarenihPoena;

        public double BrojOstvarenihPoena
        {
            get { return brojOstvarenihPoena; }
            set { SetAndNotify(ref brojOstvarenihPoena, value); }
        }


        private string username;

        public string Username
        {
            get { return username; }
            set { SetAndNotify(ref username, value); }
        }

        private string lozinka;

        public string Lozinka
        {
            get { return lozinka; }
            set { SetAndNotify(ref lozinka, value); }
        }

        private int brojPoenaZaPopust;

        public int BrojPoenaZaPopust
        {
            get { return brojPoenaZaPopust; }
            set { SetAndNotify(ref brojPoenaZaPopust, value); }
        }
        private int izabranBrojPoenaZaPopust;

        public int IzabranBrojPoenaZaPopust
        {
            get { return izabranBrojPoenaZaPopust; }
            set { SetAndNotify(ref izabranBrojPoenaZaPopust, value); }
        }

        private string nazivUloge;

        public string NazivUloge
        {
            get { return nazivUloge; }
            set { SetAndNotify(ref nazivUloge, value); }
        }

        private int idUloge;

        public int IdUloge
        {
            get { return idUloge; }
            set { SetAndNotify(ref idUloge, value); }
        }

        private bool deletedItem;

        public bool DeletedItem
        {
            get { return deletedItem; }
            set { SetAndNotify(ref deletedItem, value); }
        }

        public string VratiTextZaDugme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return "Restoruj ovu opremu";
                }
                else
                {
                    return "Izbrisi ovu opremu";
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
    }


    public class Kupci : Korisnici
    {
        private ObservableCollection<IstorijaKupovine> listaIstorijeKupovine = new ObservableCollection<IstorijaKupovine>();

        public ObservableCollection<IstorijaKupovine> ListaIstorijeKupovine
        {
            get { return listaIstorijeKupovine; }
            set { SetAndNotify(ref listaIstorijeKupovine, value); }
        }


        private double? najvisePotrosio;

        public double? NajvisePotrosio
        {
            get { return najvisePotrosio; }
            set { SetAndNotify(ref najvisePotrosio, value); }
        }


        private int? brojKupovina;

        public int? BrojKupovina
        {
            get { return brojKupovina; }
            set { SetAndNotify(ref brojKupovina, value); }
        }

        private int? brojKupljeneOpreme;

        public int? BrojKupljeneOpreme
        {
            get { return brojKupljeneOpreme; }
            set { brojKupljeneOpreme = value; }
        }


    }
    
}
