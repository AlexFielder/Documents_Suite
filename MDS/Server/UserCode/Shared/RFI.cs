using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class RFI
    {
        partial void Project_Changed()
        {
            ProjectTitle = Project.ProjectTitle;
            CustomerProjectCode = Project.CustomerProjectCode;
            MatrixProjectCode = Project.MatrixProjectCode;
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            UserName = this.Application.User.FullName;
            RFIDate = DateTime.Today.ToString("ddd d MMM yyyy");
            int? ProjectRFICount = Project.RFIs.Count();
            string RFIId = Convert.ToString(ProjectRFICount);
            string str = Project.CustomerOrderNumber + "-" + Project.CustomerProjectCode + "-RFI";
            //string str = Project.CustomerProjectCode + "-RFI";
            char pad = '0';
            RFINumber = str + RFIId.PadLeft(5, pad);
        }

        partial void UserName_Changed()
        {
            //Add more users here as appropriate!
            if (this.UserName.Contains("Alex") | this.UserName.Contains("Nick") | this.UserName.Contains("Simon"))
            {
                UserPosition = "Design Engineer";
            }
            else if (this.UserName.Contains("Julian"))
            {
                UserPosition = "Managing Director";
            }
            else
            {
                UserPosition = "Admin";
            }
        }
    }
}
