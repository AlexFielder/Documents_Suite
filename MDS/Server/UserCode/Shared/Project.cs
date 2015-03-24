using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Project
    {

        partial void CustomerOrderNumber_Validate(EntityValidationResultsBuilder results)
        {
            if (this.CustomerOrderNumber.Contains("/") | this.CustomerOrderNumber.Contains("\\"))
            {
                results.AddPropertyError("CustomerOrderNumber cannot contain / or \\ characters as it is used for filename generation!");
            }
        }
    }
}
