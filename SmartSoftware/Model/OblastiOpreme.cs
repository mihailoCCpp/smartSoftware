using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Media;

namespace SmartSoftware.Model
{
    //[DataContract]
    //[Serializable]
    public class OblastiOpreme : SmartSoftwareGlavnaOblast
    {
        //[DataMember]
        private ObservableCollection<SmartSoftwareGlavnaOblast> items = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Items
        {
            get { return items; }
            set { SetAndNotify(ref items, value); }
        }

        public OblastiOpreme(OblastiOpreme parent)
            : base(parent)
        {

        }

        private string slikaOriginalPutanja;

        public string SlikaOriginalPutanja
        {
            get { return slikaOriginalPutanja; }
            set { SetAndNotify(ref slikaOriginalPutanja, value); }
        }


        private bool daliMozeDaSeAzurira = false;

        public bool DaliMozeDaSeAzurira
        {
            get { return daliMozeDaSeAzurira; }
            set
            {
                SetAndNotify(ref daliMozeDaSeAzurira, value);
                
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
        
        private string nazivOblastiOpreme;
        //[DataMember(Name = "NazivOblastiOpreme", IsRequired = false)]
        public string NazivOblastiOpreme
        {
            get { return nazivOblastiOpreme; }
            set { SetAndNotify(ref nazivOblastiOpreme, value); }
        }

        private int idOblastiOpreme;
        //[DataMember(Name = "IdOblastiOpreme", IsRequired = false)]
        public int IdOblastiOpreme
        {
            get { return idOblastiOpreme; }
            set { SetAndNotify(ref idOblastiOpreme, value); }
        }

    }
}
