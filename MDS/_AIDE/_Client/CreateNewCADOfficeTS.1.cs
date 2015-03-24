using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Specialized;

namespace LightSwitchApplication
{
    public partial class CreateNewCADOfficeTS
    {

        partial void CreateNewCADOfficeTS_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            
            this.DesignLogProperty = new CADOfficeProjT();
        }

        partial void CreateNewCADOfficeTS_Saved()
        {
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.DesignLogProperty);
            CreateNewDocument nd = new CreateNewDocument();
            if (DisplayProjectTasksOnly)
            {
                nd.GenerateFromAddNew("COPT", this.DesignLogProperty, this.DataWorkspace.MDSData.Tasks, true);
            }
            else
            {
                nd.GenerateFromAddNew("COPT", this.DesignLogProperty, this.DataWorkspace.MDSData.Tasks, false);
            }
        }

        partial void CreateNewCADOfficeTS_Activated()
        {
            DisplayProjectTasksOnly = true;
        }

        partial void GenerateNewDocument_Execute()
        {
            CreateNewDocument nd = new CreateNewDocument();
            if (DisplayProjectTasksOnly)
            {
                nd.GenerateFromAddNew("COPT", this.DesignLogProperty, this.DataWorkspace.MDSData.Tasks, true);
            }
            else
            {
                nd.GenerateFromAddNew("COPT", this.DesignLogProperty, this.DataWorkspace.MDSData.Tasks, false);
            }
        }

        partial void DisplayProjectTasksOnly_Changed()
        {
            if (DisplayProjectTasksOnly)
            {
                this.FindControl("GetTasksByMonth").IsVisible = !DisplayProjectTasksOnly;
                this.FindControl("GetTasksByProject").IsVisible = DisplayProjectTasksOnly;
                this.FindControl("SelectedCustomer").IsVisible = DisplayProjectTasksOnly;
                this.FindControl("DesignLogProperty_Project").IsVisible = DisplayProjectTasksOnly;
            }
            else
            {
                this.FindControl("GetTasksByMonth").IsVisible = !DisplayProjectTasksOnly;
                this.FindControl("GetTasksByProject").IsVisible = DisplayProjectTasksOnly;
                this.FindControl("SelectedCustomer").IsVisible = DisplayProjectTasksOnly;
                this.FindControl("DesignLogProperty_Project").IsVisible = DisplayProjectTasksOnly;
            }
        }

        partial void CreateNewCADOfficeTS_Saving(ref bool handled)
        {

        }

        partial void StartDate_Changed()
        {
            if (this.DesignLogProperty != null)
            {
                if (DisplayProjectTasksOnly)
                {
                    this.DesignLogProperty.UserName = this.Application.User.FullName;
                    this.DesignLogProperty.StartDate = StartDate.Value;
                    this.DesignLogProperty.Month = StartDate.Value.Month;
                    this.DesignLogProperty.Year = StartDate.Value.Year;
                    int ProjectCOPTCount = this.DesignLogProperty.Project.CADOfficeProjTS.Count();
                    string COPTId = Convert.ToString(ProjectCOPTCount);
                    string str = this.DesignLogProperty.Project.CustomerProjectCode + "-COPT";
                    char pad = '0';
                    string shortMonth = StartDate.Value.ToString("MMMyyyy");
                    this.DesignLogProperty.COPTNumber = shortMonth 
                        + "-" 
                        + str + COPTId.PadLeft(5, pad)
                        + "-" 
                        + this.DesignLogProperty.UserName;
                }
                else
                {
                    this.DesignLogProperty.UserName = this.Application.User.FullName;
                    this.DesignLogProperty.StartDate = StartDate.Value;
                    this.DesignLogProperty.Month = StartDate.Value.Month;
                    this.DesignLogProperty.Year = StartDate.Value.Year;
                    string shortMonth = StartDate.Value.ToString("MMMyyyy");
                    this.DesignLogProperty.COPTNumber = shortMonth + "-" + this.DesignLogProperty.UserName;
                }
                
            }
        }

        partial void ProjectsByCustomer_Changed(NotifyCollectionChangedEventArgs e)
        {
            
        }

        partial void EndDate_Changed()
        {
            this.DesignLogProperty.EndDate = EndDate.Value;
        }

        partial void MatrixStaffMember_Changed()
        {
            this.DesignLogProperty.UserName = MatrixStaffMember.Name;
        }
    }
}
