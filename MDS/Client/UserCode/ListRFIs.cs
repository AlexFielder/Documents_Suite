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
    public partial class ListRFIs
    {
        partial void AddNewStaffRespondent_Execute()
        {
            Application.Current.ShowCreateNewClientStaffMember(false);
        }

        partial void AddNewMatrixRespondent_Execute()
        {
            
        }

        partial void RFIListEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void RFIListEditSelected_Execute()
        {
            Application.Current.ShowRFIDetail(this.RFIs.SelectedItem.Id);
        }
    }
}
