//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMSDBLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Intervention
    {
        public System.Guid Id { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public Nullable<decimal> Costs { get; set; }
        public Nullable<int> LifeRemaining { get; set; }
        public string Comments { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.DateTime> DateFinish { get; set; }
        public Nullable<System.DateTime> DateRecentVisit { get; set; }
        public Nullable<System.Guid> InterventionTypeId { get; set; }
        public Nullable<System.Guid> ClientId { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> ApprovedBy { get; set; }
    }
}