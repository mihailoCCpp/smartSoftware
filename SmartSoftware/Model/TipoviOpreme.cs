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
    public class TipoviOpreme : SmartSoftwareGlavnaOblast
    {

        private string nazivTipa;

        public string NazivTipa
        {
            get { return nazivTipa; }
            set { SetAndNotify(ref nazivTipa, value); }
        }

        private int idTipOpreme;

        public int IdTipOpreme
        {
            get { return idTipOpreme; }
            set { SetAndNotify(ref idTipOpreme, value); }
        }

        private int? idOblastiOpreme;

        public int? IdOblastiOpreme
        {
            get { return idOblastiOpreme; }
            set { SetAndNotify(ref idOblastiOpreme, value); }
        }

        private int tipSlika;

        public int TipSlika
        {
            get { return tipSlika; }
            set { SetAndNotify(ref tipSlika, value); }
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

        private string slikaOriginalPutanja;

        public string SlikaOriginalPutanja
        {
            get { return slikaOriginalPutanja; }
            set { SetAndNotify(ref slikaOriginalPutanja, value); }
        }

        private string staroImeTipa;

        public string StaroImeTipa
        {
            get { return staroImeTipa; }
            set { SetAndNotify(ref staroImeTipa, value); }
        }

        private bool daliMozeTipDaSeAzurira = false;

        public bool DaliMozeTipDaSeAzurira
        {
            get { return daliMozeTipDaSeAzurira; }
            set { SetAndNotify(ref daliMozeTipDaSeAzurira, value); }
        }



        private bool daLiJeSlikaTipaPromenjena = false;

        public bool DaLiJeSlikaTipaPromenjena
        {
            get { return daLiJeSlikaTipaPromenjena; }
            set { SetAndNotify(ref daLiJeSlikaTipaPromenjena, value); }
        }
        private bool daLiJeOblastPromenjena = false;

        public bool DaLiJeOblastPromenjena1
        {
            get { return daLiJeOblastPromenjena; }
            set { SetAndNotify(ref daLiJeOblastPromenjena, value); }
        }
        private bool daLiJeTekstTipaOpremePromenjen = false;

        public bool DaLiJeTekstTipaOpremePromenjen
        {
            get { return daLiJeTekstTipaOpremePromenjen; }
            set { SetAndNotify(ref daLiJeTekstTipaOpremePromenjen, value); }
        }

        public bool DaLiJeOblastPromenjena
        {
            get { return daLiJeOblastPromenjena; }
            set { SetAndNotify(ref daLiJeOblastPromenjena, value); }
        }

        private string nazivOblastiOpreme;

        public string NazivOblastiOpreme
        {
            get { return nazivOblastiOpreme; }
            set { SetAndNotify(ref nazivOblastiOpreme, value); }
        }

        private int izabranTipOpreme;

        public int IzabranTipOpreme
        {
            get { return izabranTipOpreme; }
            set { SetAndNotify(ref izabranTipOpreme, value); }
        }




        public TipoviOpreme(OblastiOpreme parent)
            : base(parent)
        {

        }



        private ObservableCollection<Oprema> opremaKolekcija = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> OpremaKolekcija
        {
            get { return opremaKolekcija; }
        }

        


        
    }

    public class TipoviKofiguracije : TipoviOpreme
    {

        public TipoviKofiguracije(OblastiOpreme parent)
            : base(parent)
        {

        }

        private bool? izabranZaKonfiguraciju;

        public bool? IzabranZaKonfiguraciju
        {
            get { return izabranZaKonfiguraciju; }
            set { SetAndNotify(ref izabranZaKonfiguraciju, value); }
        }
        private int? idTipOpremeKolekcije;

        public int? IdTipOpremeKolekcije
        {
            get { return idTipOpremeKolekcije; }
            set { SetAndNotify(ref idTipOpremeKolekcije, value); }
        }
        private int? redosledPrikazivanja;

        public int? RedosledPrikazivanja
        {
            get { return redosledPrikazivanja; }
            set { SetAndNotify(ref redosledPrikazivanja, value); }
        }
        private int? izabranRedosled;

        public int? IzabranRedosled
        {
            get { return izabranRedosled; }
            set { SetAndNotify(ref izabranRedosled, value); }
        }

        private int mogucaKolicinaUnosa;

        public int MogucaKolicinaUnosa
        {
            get { return mogucaKolicinaUnosa; }
            set { SetAndNotify(ref mogucaKolicinaUnosa, value); }
        }

        private ObservableCollection<string> listaBrojevaZaRedosled = new ObservableCollection<string>();

        public ObservableCollection<string> ListaBrojevaZaRedosled
        {
            get { return listaBrojevaZaRedosled; }
            set { SetAndNotify(ref listaBrojevaZaRedosled, value); }
        }

    }
}
