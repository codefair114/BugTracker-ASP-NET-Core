using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.ViewModels
{
    public class IssueModel
    {
        public Issue Issue { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }
        public IEnumerable<SelectListItem> IssueType { get; set; }
        public IEnumerable<SelectListItem> Assignee { get; set; }
        public IEnumerable<SelectListItem> Priority { get; set; }


    }
}
