using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.CustomValidation;
using System.Xml.Linq;

namespace GameStore.Models
{
    public partial class Events
    {
        public Events()
        {
            UserEvent = new HashSet<UserEvent>();
        }

        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [PastDateValidation]
        public DateTime? StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
         [PastDateValidation]
        public DateTime? EndDate { get; set; }

        public ICollection<UserEvent> UserEvent { get; set; }
    }
}
