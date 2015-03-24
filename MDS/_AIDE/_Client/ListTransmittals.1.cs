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
    public partial class ListTransmittals
    {
        partial void PrintNewDocument_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("DT", this.Transmittals.SelectedItem, this.DataWorkspace.MDSData.TransmittalDocs);
        }

        partial void TransmittalListEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void TransmittalListEditSelected_Execute()
        {
            // Write your code here.
            Application.Current.ShowTransmittalDetail(this.Transmittals.SelectedItem.Id);
        }
    }
}
