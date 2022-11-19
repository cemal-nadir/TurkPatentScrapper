using Microsoft.AspNetCore.Identity;
using System;

namespace TPHunter.WebServices.Identity.API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public int County { get; set; }
        public Guid MainLanguage { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public Gender Gender { get; set; }
    }
}
