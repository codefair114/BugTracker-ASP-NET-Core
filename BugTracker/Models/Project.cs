using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Project
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("userName")]
        [DisplayName("Reporter")]

        public string? CreatorName { get; set; }
        public virtual IdentityUser Creator { get; set; }

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
