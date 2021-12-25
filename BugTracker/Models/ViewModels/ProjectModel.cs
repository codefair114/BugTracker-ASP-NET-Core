using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.ViewModels
{
    public class ProjectModel
    {
        public Project Project { get; set; }
        public IEnumerable<SelectListItem> Priority { get; set; }

    }
}
