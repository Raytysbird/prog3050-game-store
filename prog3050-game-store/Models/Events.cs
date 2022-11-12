using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime? StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public ICollection<UserEvent> UserEvent { get; set; }
    }
}
