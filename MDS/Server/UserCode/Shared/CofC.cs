using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class CofC
    {
        partial void Project_Changed()
        {
            //ProjectTitle = Project.ProjectTitle;
            CustomerProjectCode = Project.CustomerProjectCode;
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            //MatrixProjectCode = Project.MatrixProjectCode;
            UserName = this.Application.User.FullName;
            CofCDate = DateTime.Today.ToShortDateString();
        }

        partial void CertificateNo_Validate(EntityValidationResultsBuilder results)
        {
            var val = (from a in this.DataWorkspace.MDSData.CofCs
                       where a.CertificateNo == this.CertificateNo
                       select a).SingleOrDefault();
            if (val != null)
            {
                results.AddPropertyResult("The Certificate Number must be unique!", ValidationSeverity.Error);
            }

        }
    }
}
