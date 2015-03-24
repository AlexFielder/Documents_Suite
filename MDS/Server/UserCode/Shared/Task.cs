using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Task
    {
        partial void Day_Changed()
        {
            TaskDate = Day.Date.ToShortDateString();
        }

        //partial void Summary_Compute(ref string result)
        //{
        //    result = Project.CustomerProjectCode + ", " + TaskNumber + ", " + TaskDate;

        //}
        partial void Project_Changed()
        {
            User = this.Application.User.FullName;
        }

        partial void TaskType_Changed()
        {
            User = this.Application.User.FullName;
        }
    }
}
