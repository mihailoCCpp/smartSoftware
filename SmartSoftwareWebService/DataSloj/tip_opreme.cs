//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartSoftwareWebService.DataSloj
{
    using System;
    using System.Collections.Generic;
    
    public partial class tip_opreme
    {
        public tip_opreme()
        {
            this.parametris = new HashSet<parametri>();
        }
    
        public int id_tip_opreme { get; set; }
        public string naziv_tipa { get; set; }
        public int id_oblasti_opreme { get; set; }
        public string slika_tipa { get; set; }
        public bool deletedField { get; set; }
    
        public virtual ICollection<parametri> parametris { get; set; }
        public virtual oblasti_opreme oblasti_opreme { get; set; }
    }
}