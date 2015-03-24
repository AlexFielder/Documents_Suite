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
    public partial class ListTechnicalQueriesByProject
    {
        partial void GetTQsByProjectEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void GetTQsByProjectEditSelected_Execute()
        {
            Application.Current.ShowTQDetailDetail(this.GetTQsByProject.SelectedItem.Id);
        }
    }
}
