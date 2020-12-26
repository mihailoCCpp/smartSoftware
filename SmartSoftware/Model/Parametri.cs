using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartSoftware.Model
{
    public class Parametri : SmartSoftwareGlavnaOblast
    {

        public Parametri(OblastiOpreme oblastiOpreme)
            :base(oblastiOpreme)
        {

        }
       
        private int idTipOpreme;

        public int IdTipOpreme
        {
            get { return idTipOpreme; }
            set { SetAndNotify(ref idTipOpreme, value); }
        }


        private int izabranTipOpreme;

        public int IzabranTipOpreme
        {
            get { return izabranTipOpreme; }
            set { SetAndNotify(ref izabranTipOpreme, value); }
        }

        private bool parametarJeIFilter;

        public bool ParametarJeIFilter
        {
            get { return parametarJeIFilter; }
            set { SetAndNotify(ref parametarJeIFilter, value); }
        }


        private int idParametri;

        public int IdParametri
        {
            get { return idParametri; }
            set { SetAndNotify(ref idParametri, value); }
        }

        private string defaultVrednost;

        public string DefaultVrednost
        {
            get { return defaultVrednost; }
            set { SetAndNotify(ref defaultVrednost, value); }
        }

        private string vrednostParametra;

        public string VrednostParametra
        {
            get { return vrednostParametra; }
            set { SetAndNotify(ref vrednostParametra, value); }
        }

        private ObservableCollection<VrednostiFiltera> kolekcijaVrednostiZaFilter = new ObservableCollection<VrednostiFiltera>();

        public ObservableCollection<VrednostiFiltera> KolekcijaVrednostiZaFilter
        {
            get { return kolekcijaVrednostiZaFilter; }
        }

        private bool odabranParametarZaFiltere = false;

        public bool OdabranParametarZaFiltere
        {
            get { return odabranParametarZaFiltere; }
            set { odabranParametarZaFiltere = value; }
        }


        private bool izmenjenaVrednostParamertra;

        public bool IzmenjenaVrednostParamertra
        {
            get { return izmenjenaVrednostParamertra; }
            set {
                bool vrednost = this.VrednostParametra.Length > 0 ? true : false;
                SetAndNotify(ref izmenjenaVrednostParamertra, vrednost);
            }
        }

        private string tipParametra;

        public string TipParametra
        {
            get { return tipParametra; }
            set { SetAndNotify(ref tipParametra, value); }
        }

       
        private SolidColorBrush prikaziBojom;

        public SolidColorBrush PrikaziBojom
        {
            get { return prikaziBojom; }
            set {
                SolidColorBrush vrednost = this.izmenjenaVrednostParamertra == true ? Brushes.White : Brushes.Gray;
                SetAndNotify(ref prikaziBojom, vrednost);
            }
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
    }

    

    public class VrednostiFiltera
    {
        public string VrednostFiltera { get; set; }
        public int idVrednostiFiltera { get; set; }
        
        private bool odabranaVrednostZaFiltere = false;        
        public bool OdabranaVrednostZaFiltere
        {
            get { return odabranaVrednostZaFiltere; }
            set { odabranaVrednostZaFiltere = value; }
        }
    }
}
