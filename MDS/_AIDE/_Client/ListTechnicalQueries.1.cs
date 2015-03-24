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
    public partial class ListTechnicalQueries
    {
        partial void TQDetailListEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void TQDetailListEditSelected_Execute()
        {
            Application.Current.ShowTQDetailDetail(this.TQDetails.SelectedItem.Id);
        }
    }
}
