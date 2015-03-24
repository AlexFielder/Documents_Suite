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
    public partial class ListReviews
    {
        partial void ReviewListEditSelected_CanExecute(ref bool result)
        {
            // Write your code here.

        }

        partial void ReviewListEditSelected_Execute()
        {
            Application.Current.ShowReviewDetail(this.Reviews.SelectedItem.Id);
        }
    }
}
