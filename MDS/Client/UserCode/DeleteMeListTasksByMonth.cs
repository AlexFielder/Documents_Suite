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
    public partial class DeleteMeListTasksByMonth
    {
        /// <summary>
        /// We only need to new this object, not add it to the database.
        /// </summary>
        public dynamic newCopt; 

        /// <summary>
        /// Creates a newly printed document based on the selection made by the user.
        /// </summary>
        partial void PrintSelectedDocument_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("COPT", newCopt, this.DataWorkspace.MDSData.Tasks,false);
        }

        partial void DeleteMeListTasksByMonth_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.Details.Dispatcher.BeginInvoke(() =>
                {
                    this.newCopt = new CADOfficeProjT(this.DataWorkspace.MDSData.CADOfficeProjTS);
                });
            
        }

        /// <summary>
        /// Captures the change in TaskStartDate
        /// </summary>
        partial void TaskStartDate_Changed()
        {
            //newCopt = this.DataWorkspace.MDSData.CADOfficeProjTS.AddNew();
            newCopt.UserName = this.Application.User.FullName;
            newCopt.StartDate = TaskStartDate;
            newCopt.Month = TaskStartDate.Month;
            newCopt.Year = TaskStartDate.Year;
            string shortMonth = TaskStartDate.ToString("MMMyyyy");
            newCopt.COPTNumber = shortMonth + "-" + newCopt.UserName;
        }

        partial void TaskEndDate_Changed()
        {
            newCopt.EndMonth = TaskEndDate.Value.Month;
            newCopt.EndYear = TaskEndDate.Value.Year;
            newCopt.EndDate = TaskEndDate;
        }

        partial void DeleteMeListTasksByMonth_Activated()
        {
            // Write your code here.

        }
    }
}
