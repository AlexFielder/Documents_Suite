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
    public partial class ListTasksByMonth
    {
        partial void ListTasksByMonth_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Write your code here.
            this.copt = this.DataWorkspace.MDSData.CADOfficeProjTS.AddNew();
            //this.copt = new CADOfficeProjT();
            // this.newCopt = new CADOfficeProjT(this.DataWorkspace.MDSData.CADOfficeProjTS);
        }

        /// <summary>
        /// May not be needed in order to have the COPT save correctly.
        /// </summary>
        partial void PrintSelectedDocument_Execute()
        {
            //add a new COPT if needed.
            IEnumerable<CADOfficeProjT> foundCOPT = (from CADOfficeProjT a in this.DataWorkspace.MDSData.CADOfficeProjTS
                                                     where a.COPTNumber == copt.COPTNumber
                                                     select a).AsEnumerable();
            if (foundCOPT.Count<CADOfficeProjT>() == 0)
            {
                //CADOfficeProjT newCopt = this.DataWorkspace.MDSData.CADOfficeProjTS.AddNew();
                //newCopt.Customer = copt.Customer;
                //newCopt.Project = copt.Project;
                //newCopt.Month = copt.Month;
                //newCopt.COPTNumber = copt.COPTNumber;
                //newCopt.Year = copt.Year;
                //newCopt.StartDate = copt.StartDate;
                //newCopt.UserName = copt.UserName;
                this.DataWorkspace.MDSData.SaveChanges();
            }
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("COPT", copt, this.DataWorkspace.MDSData.Tasks, false);
        }

        partial void TaskStartDate_Changed()
        {
            if (copt != null)
            {
                copt.UserName = this.Application.User.FullName;
                copt.StartDate = TaskStartDate;
                copt.Month = TaskStartDate.Month;
                copt.Year = TaskStartDate.Year;
                string shortMonth = TaskStartDate.ToString("MMMyyyy");
                copt.COPTNumber = shortMonth + "-" + copt.UserName;
            }
        }

        partial void TaskEndDate_Changed()
        {
            copt.EndMonth = TaskEndDate.Value.Month;
            copt.EndYear = TaskEndDate.Value.Year;
            copt.EndDate = TaskEndDate;
        }

        partial void TaskListEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void TaskListEditSelected_Execute()
        {
            Application.Current.ShowTaskDetail(this.GetTasksByMonth.SelectedItem.Id);
        }

        partial void ListTasksByMonth_Saving(ref bool handled)
        {
            //add a new COPT if needed.
            IEnumerable<CADOfficeProjT> foundCOPT = (from CADOfficeProjT a in this.DataWorkspace.MDSData.CADOfficeProjTS
                                                     where a.COPTNumber == copt.COPTNumber
                                                     select a).AsEnumerable();
            if (foundCOPT.Count<CADOfficeProjT>() == 0)
            {
                //CADOfficeProjT newCopt = this.DataWorkspace.MDSData.CADOfficeProjTS.AddNew();
                //newCopt.Customer = copt.Customer;
                //newCopt.Project = copt.Project;
                //newCopt.Month = copt.Month;
                //newCopt.COPTNumber = copt.COPTNumber;
                //newCopt.Year = copt.Year;
                //newCopt.StartDate = copt.StartDate;
                //newCopt.UserName = copt.UserName;
                this.DataWorkspace.MDSData.SaveChanges();
            }
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("COPT", copt, this.DataWorkspace.MDSData.Tasks, false);
        }
    }
}
