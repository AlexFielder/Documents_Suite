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
    public partial class CreateNewTask
    {

        partial void CreateNewTask_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.TaskProperty = new Task();
        }

        partial void IsProjectTask_Changed()
        {
            if (IsProjectTask)
            {
                this.FindControl("Project").IsVisible = IsProjectTask;
                this.FindControl("SelectedCustomer").IsVisible = IsProjectTask;
                this.FindControl("SelectedCustomer").Focus();
            }
            else
            {
                this.FindControl("Project").IsVisible = IsProjectTask;
                this.FindControl("SelectedCustomer").IsVisible = IsProjectTask;
                this.TaskProperty.User = this.Application.User.FullName;
                this.FindControl("TaskType").Focus();
                //var Task = (from Task a in this.DataWorkspace.MDSData.Tasks
                //            where a.TaskType == this.TaskProperty.TaskType
                //            select a).AsEnumerable();
                //string TaskId = Convert.ToString(Task.Count() + 1);
                //char pad = '0';
                //string str = TaskId.PadLeft(5, pad);
                //this.TaskProperty.TaskNumber = str;
            }
            
        }

        partial void CreateNewTask_Saved()
        {
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.TaskProperty);
        }

        partial void AddNewTaskType_Execute()
        {
            Application.Current.ShowCreateNewTaskType(false);
        }

    }
}
