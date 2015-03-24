using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class DandDC
    {
        partial void Project_Changed()
        {
            ProjectTitle = Project.ProjectTitle;
            CustomerProjectCode = Project.CustomerProjectCode;
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            UserName = this.Application.User.FullName;
            DandDCStartDate = DateTime.Today.ToString("ddd d MMM yyyy");
            MatrixProjectCode = Project.MatrixProjectCode;
            DDCNumber = CustomerProjectCode + "-" + MatrixProjectCode + "-DDC";
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
