using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Transmittal
    {
        partial void Project_Changed()
        {
            Customer = Project.Customer.CustomerName;
            CustomerOrderNo = Project.CustomerOrderNumber;
            MatrixSalesOrderNo = Project.MatrixSalesOrderNumber;
            //TQId = Project.ProjectTitle + Id;
            UserName = this.Application.User.FullName;
            ProjectTitle = Project.ProjectTitle;
            CustomerProjectCode = Project.CustomerProjectCode;
            MatrixProjectCode = Project.MatrixProjectCode;
            QueryDate = DateTime.Today.ToString("ddd d MMM yyyy");
            try
            {
                int? ProjectTransmittalsCount = Project.Transmittals.Count();
                string TransmittalsId = Convert.ToString(ProjectTransmittalsCount); //is always the next number as we're creating a new one at this point!
                if (Project.CustomerProjectCode == "N/A")
                {//use the customer order number instead.
                    string str = Project.CustomerOrderNumber + "-TR";
                    char pad = '0';
                    TransmittalNumber = str + TransmittalsId.PadLeft(5, pad);
                }
                else
                {
                    string str = Project.CustomerProjectCode + "-TR";
                    char pad = '0';
                    TransmittalNumber = str + TransmittalsId.PadLeft(5, pad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
