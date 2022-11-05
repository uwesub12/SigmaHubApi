using System;
using System.ComponentModel.DataAnnotations;

namespace SigmaHubApi.Modal
{
    public class Candidate
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public string LinkedInURL { get; set; }
        public string GitHubURL { get; set; }
        [Required]
        public string FreeTextComment { get; set; }
    }
}
