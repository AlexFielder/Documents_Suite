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
    public partial class CreateNewCofC
    {
        partial void CreateNewCofC_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Write your code here.
            this.CofCProperty = new CofC();
        }

        partial void CreateNewCofC_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.CofCProperty);
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("CofC", this.CofCProperty, this.DataWorkspace.MDSData.CofCDocs);
        }
    }
}