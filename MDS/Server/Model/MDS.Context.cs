﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MDSEntities : DbContext
    {
        public MDSEntities()
            : base("name=MDSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<CADOfficeProjT> CADOfficeProjTS { get; set; }
        public DbSet<ClientStaffMemberMITPSignatory> ClientStaffMemberMITPSignatories { get; set; }
        public DbSet<ClientStaffMemberProject> ClientStaffMemberProjects { get; set; }
        public DbSet<ClientStaffMemberReviewer> ClientStaffMemberReviewers { get; set; }
        public DbSet<ClientStaffMemberRFIRecipient> ClientStaffMemberRFIRecipients { get; set; }
        public DbSet<ClientStaffMemberRFIRespondent> ClientStaffMemberRFIRespondents { get; set; }
        public DbSet<ClientStaffMember> ClientStaffMembers { get; set; }
        public DbSet<ClientStaffMemberValidationAttendee> ClientStaffMemberValidationAttendees { get; set; }
        public DbSet<ClientStaffMemberVerificationAttendee> ClientStaffMemberVerificationAttendees { get; set; }
        public DbSet<CofCDoc> CofCDocs { get; set; }
        public DbSet<CofC> CofCs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DandADoc> DandADocs { get; set; }
        public DbSet<DandDA> DandDAs { get; set; }
        public DbSet<DandDC> DandDCs { get; set; }
        public DbSet<DrawingPrefix> DrawingPrefixes { get; set; }
        public DbSet<DrawingRegister> DrawingRegisters { get; set; }
        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<MatrixSalesOrderNumber> MatrixSalesOrderNumbers { get; set; }
        public DbSet<MatrixStaffMemberMITPSignatory> MatrixStaffMemberMITPSignatories { get; set; }
        public DbSet<MatrixStaffMemberProject> MatrixStaffMemberProjects { get; set; }
        public DbSet<MatrixStaffMemberReviewer> MatrixStaffMemberReviewers { get; set; }
        public DbSet<MatrixStaffMemberRFIRespondent> MatrixStaffMemberRFIRespondents { get; set; }
        public DbSet<MatrixStaffMember> MatrixStaffMembers { get; set; }
        public DbSet<MatrixStaffMemberValidationAttendee> MatrixStaffMemberValidationAttendees { get; set; }
        public DbSet<MatrixStaffMemberVerificationAttendee> MatrixStaffMemberVerificationAttendees { get; set; }
        public DbSet<MITPDetail> MITPDetails { get; set; }
        public DbSet<MITP> MITPs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RFI> RFIs { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TQDetail> TQDetails { get; set; }
        public DbSet<TransmittalDocReasonsForIssue> TransmittalDocReasonsForIssues { get; set; }
        public DbSet<TransmittalDoc> TransmittalDocs { get; set; }
        public DbSet<Transmittal> Transmittals { get; set; }
    }
}