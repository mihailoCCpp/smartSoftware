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
    
    public partial class korisnici
    {
        public int id_korisnici { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string mejl { get; set; }
        public string broj_telefona { get; set; }
        public string username { get; set; }
        public string lozinka { get; set; }
        public int broj_ostvareni_poena { get; set; }
        public int id_uloge { get; set; }
        public bool deletedField { get; set; }
    }
}
