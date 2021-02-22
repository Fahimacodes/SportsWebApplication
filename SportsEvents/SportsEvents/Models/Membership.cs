using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEvents.Models
{
    public class Membership
    {
        [Key]
        public int UserRegisteredID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TelephoneNo { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string HouseNo { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string WorkLocation { get; set; }
        public int SportID { get; set; }
        public virtual Sport Sport { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public string Skills { get; set; }
    }
}
