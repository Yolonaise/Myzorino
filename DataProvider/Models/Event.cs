//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataProvider.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int id { get; set; }
        public System.DateTime created_date { get; set; }
        public int creator_id { get; set; }
        public System.DateTime end_date { get; set; }
        public System.DateTime start_date { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
    }
}
