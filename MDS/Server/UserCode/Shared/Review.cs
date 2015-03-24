using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Review
    {
        partial void ReviewDate_Changed()
        {
            ReviewDateStr = ReviewDate.Date.ToShortDateString();
        }
        partial void Project_Changed()
        {
            Reviewer = this.Application.User.FullName;
            int? ProjectReviewsCount = Project.Reviews.Count();
            string ReviewId = Convert.ToString(ProjectReviewsCount);
            string str = Project.CustomerProjectCode + "-Review";
            char pad = '0';
            ReviewNumber = str + ReviewId.PadLeft(5, pad);
        }
    }
}
