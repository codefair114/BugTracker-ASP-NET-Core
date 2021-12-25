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
    public class Tempo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Summary")]
        public string Summary { get; set; }

        [Required]
        [MaxLength(1000)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Hours")]
        public float Hours { get; set; }

        [Required]
        [ForeignKey("userName")]
        [DisplayName("Assignee")]
        public string? AssigneeName { get; set; }
        public virtual IdentityUser Assignee { get; set; }

        [Required]
        [ForeignKey("Id")]
        [DisplayName("Issue")]
        public int? IssueId { get; set; }
        public virtual Issue Issue { get; set; }

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
