using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class DandDA
    {
        partial void Project_Changed()
        {
            ProjectTitle = Project.ProjectTitle;
            CustomerProjectCode = Project.CustomerProjectCode;
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            MatrixProjectCode = Project.MatrixProjectCode;
            UserName = this.Application.User.FullName;
            DDADate = DateTime.Today.ToString("ddd d MMM yyyy");
            int? ProjectDDACount = Project.DandDAs.Count();
            string DDAId = Convert.ToString(ProjectDDACount);
            string str = Project.CustomerProjectCode + "-DDA";
            char pad = '0';
            DDANumber = str + DDAId.PadLeft(5, pad);
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
