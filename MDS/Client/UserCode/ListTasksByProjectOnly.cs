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
    public partial class ListTasksByProjectOnly
    {

        partial void GenerateNewDocument_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("COPT", copt, this.DataWorkspace.MDSData.Tasks,true,true);
        }

        partial void ListTasksByProjectOnly_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.copt = new CADOfficeProjT();
        }

        partial void SelectedProject_Changed()
        {
            copt.Project = SelectedProject;
            copt.Customer = SelectedProject.Customer.CustomerName;
            copt.CustomerOrderNo = SelectedProject.CustomerOrderNumber;
            copt.MatrixSalesOrderNo = SelectedProject.MatrixSalesOrderNumber;
            copt.CustomerProjectCode = SelectedProject.CustomerProjectCode;
            copt.MatrixProjectCode = SelectedProject.MatrixProjectCode;
            int ProjectCOPTCount = SelectedProject.CADOfficeProjTS.Count();
            string COPTId = Convert.ToString(ProjectCOPTCount);
            string str = SelectedProject.CustomerProjectCode + "-COPT";
            char pad = '0';
            string shortMonth = StartDate.ToString("MMMyyyy");
            copt.COPTNumber = shortMonth
                        + "-"
                        + str + COPTId.PadLeft(5, pad)
                        + "-"
                        + copt.Project;
        }

        partial void StartDate_Changed()
        {
            if (copt != null)
            {
                copt.StartDate = StartDate;
                copt.Month = StartDate.Month;
                copt.Year = StartDate.Year;
            }
        }

        partial void EndDate_Changed()
        {
            copt.EndMonth = EndDate.Value.Month;
            copt.EndYear = EndDate.Value.Year;
            copt.EndDate = EndDate;
        }

        partial void GetTasksByProjectOnlyEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void GetTasksByProjectOnlyEditSelected_Execute()
        {
            Application.Current.ShowTaskDetail(this.GetTasksByProjectOnly.SelectedItem.Id);
        }
    }
}
