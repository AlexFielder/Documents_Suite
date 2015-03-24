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
    public partial class CreateNewTransmittal
    {
        partial void CreateNewTransmittal_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.TransmittalProperty = new Transmittal();
        }
        partial void CreateNewTransmittal_Saved()
        {
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.TransmittalProperty);
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("DT", this.TransmittalProperty, this.DataWorkspace.MDSData.TransmittalDocs);
        }

        partial void Method_Execute()
        {
            Application.Current.ShowCreateNewClientStaffMember(false);
        }
    }
}
