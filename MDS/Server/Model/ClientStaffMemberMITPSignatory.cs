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
    
    public partial class ClientStaffMemberMITPSignatory
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int ClientStaffMemberMITPSignatory_ClientStaffMember { get; set; }
        public int ClientStaffMemberMITPSignatory_MITPDetail { get; set; }
    
        public virtual ClientStaffMember ClientStaffMember { get; set; }
        public virtual MITPDetail MITPDetail { get; set; }
    }
}