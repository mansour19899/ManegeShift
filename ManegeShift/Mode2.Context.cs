﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ManageShiftEntities1 : DbContext
    {
        public ManageShiftEntities1()
            : base("name=ManageShiftEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DailyWeek> DailyWeeks { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<ShiftDay> ShiftDays { get; set; }
        public virtual DbSet<Status> Status { get; set; }
    }
}
