//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManegeShift
{
    using System;
    using System.Collections.Generic;
    
    public partial class DailyWeek
    {
        public int Id { get; set; }
        public int IdDay { get; set; }
        public int Person_fk { get; set; }
        public int Status_fk { get; set; }
        public string Mid { get; set; }
    }
}
