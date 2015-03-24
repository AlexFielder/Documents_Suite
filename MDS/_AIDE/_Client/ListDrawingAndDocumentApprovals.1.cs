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
    public partial class ListDrawingAndDocumentApprovals
    {
        partial void PrintNewDocument_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            nd.GenerateFromAddNew("DDA", this.DandDAs.SelectedItem, this.DataWorkspace.MDSData.DandADocs);
        }
    }
}
