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
    public partial class CreateNewDandDA
    {

        partial void CreateNewDandDA_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.DandDAProperty = new DandDA();
        }

        partial void CreateNewDandDA_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.DandDAProperty);
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("DDA", this.DandDAProperty, this.DataWorkspace.MDSData.DandADocs);
        }

    }
}
