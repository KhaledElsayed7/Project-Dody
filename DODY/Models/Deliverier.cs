//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DODY.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deliverier
    {
        public int Did { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> Birth_Date { get; set; }
        public string Image { get; set; }
        public int Ono { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
