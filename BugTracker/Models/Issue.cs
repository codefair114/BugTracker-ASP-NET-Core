using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Models
{
    public class Issue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id")]
        [DisplayName("Project")]

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("Id")]
        [DisplayName("IssueType")]

        public int? IssueTypeId { get; set; }
        public virtual IssueType IssueType { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Summary")]
        public string Summary { get; set; }
        
        [Required]
        [MaxLength(1000)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ForeignKey("userName")]
        [DisplayName("Reporter")]

        public string? ReporterName { get; set; }
        public virtual IdentityUser Reporter { get; set; }

        [ForeignKey("userName")]
        [DisplayName("Assignee")]

        public string? AssigneeName { get; set; }
        public virtual IdentityUser Assignee { get; set; }
        
        [ForeignKey("Id")]
        [DisplayName("Priority")]
        public int? PriorityId { get; set; }
        public virtual Priority Priority { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        [DataType(DataType.Date)]
        public DateTime ModifiedAt { get; set; }

    }
}
