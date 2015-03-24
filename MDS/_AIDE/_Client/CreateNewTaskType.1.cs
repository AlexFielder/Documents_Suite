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
    public partial class CreateNewTaskType
    {
        partial void CreateNewTaskType_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            // Write your code here.
            this.TaskTypeProperty = new TaskType();
        }

        partial void CreateNewTaskType_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.TaskTypeProperty);
        }
    }
}