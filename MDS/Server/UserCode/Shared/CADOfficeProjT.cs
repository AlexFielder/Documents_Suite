using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class CADOfficeProjT
    {
        partial void Project_Changed()
        {
            UserName = this.Application.User.FullName;
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            //Year = DateTime.Today.Year;
            //Month = DateTime.Today.Month;
            //Month = DateTime.Today.ToString("MMM yyyy");
            CustomerProjectCode = Project.CustomerProjectCode;
            MatrixProjectCode = Project.MatrixProjectCode;
            int ProjectCOPTCount = Project.CADOfficeProjTS.Count();
            string COPTId = Convert.ToString(ProjectCOPTCount);
            string str = Project.CustomerProjectCode + "-COPT";
            char pad = '0';
            COPTNumber = str + COPTId.PadLeft(5, pad);
        }

        partial void COPTNumber_Validate(EntityValidationResultsBuilder results)
        {
            // results.AddPropertyError("<Error-Message>");

        }
    }
}
