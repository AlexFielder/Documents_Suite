//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TQDetail
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string TQNumber { get; set; }
        public string CustomerProjectCode { get; set; }
        public string Customer { get; set; }
        public string CustomerOrderNo { get; set; }
        public string MatrixProjectCode { get; set; }
        public string MatrixSalesOrderNo { get; set; }
        public string DrawingRevision { get; set; }
        public string DrawingNumber { get; set; }
        public string QueryText { get; set; }
        public string UserName { get; set; }
        public string UserPosition { get; set; }
        public string QueryDate { get; set; }
        public string ResponseText { get; set; }
        public string ResponseEngineer { get; set; }
        public string ResponseEngineerPosition { get; set; }
        public Nullable<System.DateTime> ResponseDate { get; set; }
        public string ResponseEngineerEmail { get; set; }
        public Nullable<System.DateTime> TQReturnDate { get; set; }
        public string ResponseSatisfactory { get; set; }
        public Nullable<int> TQDetail_Project { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
