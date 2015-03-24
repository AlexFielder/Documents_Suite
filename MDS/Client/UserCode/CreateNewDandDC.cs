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
    public partial class CreateNewDandDC
    {
        partial void CreateNewDandDC_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.DandDCProperty = new DandDC();
        }

        partial void CreateNewDandDC_Saved()
        {
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.DandDCProperty);
            CreateNewDocument nd = new CreateNewDocument();
            //need to update the list of users if it has changed.
            nd.UpdateUserStrings("DDC", this.DandDCProperty);
            //and then generate our new document.
            nd.GenerateFromAddNew("DDC", this.DandDCProperty, this.DataWorkspace.MDSData.Reviews);
        }

        partial void CreateNewDandDC_Saving(ref bool handled)
        {
            // Write your code here.
            var cvma = from c in this.ClientStaffMemberVerificationAttendees
                       where c.ClientStaffMember.Name != null
                       select c.ClientStaffMember.Name;
            this.DandDCProperty.ClientVerificationMeetingAttendees = String.Join(", ", cvma);
            var mvma = from c in this.MatrixStaffMemberVerificationAttendees
                       where c.MatrixStaffMember.Name != null
                       select c.MatrixStaffMember.Name;
            this.DandDCProperty.MatrixVerificationMeetingAttendees = String.Join(", ", mvma);
            var cvalma = from c in this.ClientStaffMemberValidationAttendees
                         where c.ClientStaffMember.Name != null
                         select c.ClientStaffMember.Name;
            this.DandDCProperty.ClientValidationMeetingAttendees = String.Join(", ", cvalma);
            var mvalma = from c in this.MatrixStaffMemberValidationAttendees
                         where c.MatrixStaffMember.Name != null
                         select c.MatrixStaffMember.Name;
            this.DandDCProperty.MatrixValidationMeetingAttendees = String.Join(",", mvalma);
        }

        partial void AddClientStaffMember_Execute()
        {
            Application.Current.ShowCreateNewClientStaffMember(false);
        }
    }
}