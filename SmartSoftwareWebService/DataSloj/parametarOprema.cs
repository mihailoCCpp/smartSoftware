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
    
    public partial class parametarOprema
    {
        public int id_parametri { get; set; }
        public int id_oprema { get; set; }
        public string vrednost_parametra { get; set; }
    
        public virtual parametri parametri { get; set; }
        public virtual oprema oprema { get; set; }
    }
}