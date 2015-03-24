using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
namespace LightSwitchApplication
{
    public partial class ListTasksByProjectUser
    {
        partial void PrintSelectedDocument_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("COPT", copt, this.DataWorkspace.MDSData.Tasks);
        }

        partial void ListTasksByProjectUser_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.copt = new CADOfficeProjT();
        }

        partial void MatrixStaffMember_Changed()
        {
            copt.Project = SelectedProject;
            copt.UserName = this.Application.User.FullName;
            copt.Customer = SelectedProject.Customer.CustomerName;
            copt.CustomerOrderNo = SelectedProject.CustomerOrderNumber;
            copt.MatrixSalesOrderNo = SelectedProject.MatrixSalesOrderNumber;
            copt.CustomerProjectCode = SelectedProject.CustomerProjectCode;
            copt.MatrixProjectCode = SelectedProject.MatrixProjectCode;
            int ProjectCOPTCount = SelectedProject.CADOfficeProjTS.Count();
            string COPTId = Convert.ToString(ProjectCOPTCount);
            string str = SelectedProject.CustomerProjectCode + "-COPT";
            char pad = '0';
            string shortMonth = TaskStartDate.ToString("MMMyyyy");
            copt.COPTNumber = shortMonth 
                        + "-" 
                        + str + COPTId.PadLeft(5, pad)
                        + "-" 
                        + copt.UserName;
        }

        partial void TaskStartDate_Changed()
        {
            if (copt != null)
            {
                copt.StartDate = TaskStartDate;
                copt.Month = TaskStartDate.Month;
                copt.Year = TaskStartDate.Year;
            }

        }

        partial void TaskEndDate_Changed()
        {
            copt.EndMonth = TaskEndDate.Value.Month;
            copt.EndYear = TaskEndDate.Value.Year;
            copt.EndDate = TaskEndDate;
        }

        partial void GetTasksByProjectEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void GetTasksByProjectEditSelected_Execute()
        {
            Application.Current.ShowTaskDetail(this.GetTasksByProject.SelectedItem.Id);
        }
    }
}
