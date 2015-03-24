using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class TQDetail
    {
        partial void Project_Changed()
        {
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            //TQId = Project.ProjectTitle + Id;
            UserName = this.Application.User.FullName;
            CustomerProjectCode = Project.CustomerProjectCode;
            MatrixProjectCode = Project.MatrixProjectCode;
            QueryDate = DateTime.Today.ToString("ddd d MMM yyyy");
            int ProjectTQsCount = Project.TQDetails.Count();
            string TQsId = Convert.ToString(ProjectTQsCount);
            string str = Project.CustomerOrderNumber + "-" + Project.CustomerProjectCode + "-TQ";
            //string str = Project.CustomerProjectCode + "-TQ";
            char pad = '0';
            TQNumber = str + TQsId.PadLeft(5, pad);
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

        partial void ResponseSatisfactory_Changed()
        {
            if (ResponseSatisfactory != "True" | ResponseSatisfactory != "False")
            {
                ResponseSatisfactory = " ";
            }
        }
    }
}
