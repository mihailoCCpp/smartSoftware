using SmartSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.ViewModel
{
    public class SmartSoftwareDocumentManagerVM : INotifyPropertyChanged
    {


        private SmartSoftwareDocumentVM currentDocumentVM;

        public SmartSoftwareDocumentVM CurrentDocumentVM
        {
            get { return currentDocumentVM; }
            set { SetAndNotify(ref currentDocumentVM, value); }
        }

        public SmartSoftwareDocumentManagerVM()
        {
            this.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());
            
        }




        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { SetAndNotify(ref korpa, value); }
        }

        private ObservableCollection<SmartSoftwareGlavnaOblast> rezervacije = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Rezervacije
        {
            get { return rezervacije; }
            set { SetAndNotify(ref rezervacije, value); }
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
