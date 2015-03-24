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
    public partial class ListCADOfficeTimesheets
    {
        partial void CreateReport_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            if (this.CADOfficeProjTS.SelectedItem.Project  != null)
            {
                nd.GenerateFromAddNew("COPT", this.CADOfficeProjTS.SelectedItem, this.DataWorkspace.MDSData.Tasks);
            }
            else
            {
                nd.GenerateFromAddNew("COPT", this.CADOfficeProjTS.SelectedItem, this.DataWorkspace.MDSData.Tasks,false);
            }
            
        }

        partial void CADOfficeProjTListEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void CADOfficeProjTListEditSelected_Execute()
        {
            Application.Current.ShowCADOfficeProjTDetail(this.CADOfficeProjTS.SelectedItem.Id);
        }
    }
}
