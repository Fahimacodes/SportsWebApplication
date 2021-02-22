using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEvents.Models
{
    public class Sport
    {
        public List<Membership> memberships { get; set; }
        public Sport() {
            memberships = new List<Membership>();
        }
        public int SportID { get; set; }
        [DisplayName("Sport: ")]
        public string SportName { get; set; }
        [DisplayName("Event: ")]
        public string Event { get; set; }
        [DisplayName("Description: ")]
        public string Description { get; set; }
        [DisplayName("Date: ")]
        public string Date { get; set; }
        [DisplayName("Time: ")]
        public string Time { get; set; }
        [DisplayName("Location: ")]
        public string Location { get; set; }

    }
}
