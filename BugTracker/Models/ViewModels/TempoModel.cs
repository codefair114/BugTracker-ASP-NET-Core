using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.ViewModels
{
    public class TempoModel
    {
        public Tempo Tempo { get; set; }
        public IEnumerable<SelectListItem> Issue { get; set; }
    }
}
