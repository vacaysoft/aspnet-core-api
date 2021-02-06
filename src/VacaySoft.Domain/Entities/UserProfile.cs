using Microsoft.AspNetCore.Identity;
using System;

namespace VacaySoft.Domain.Entities
{
    public class UserProfile : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TemporaryEmailAddress { get; set; }
        public string ProfileUrl { get; set; }
    }
}
