using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigmaHubApi.Helper
{
    public static class Parameter
    {
        //Database Parameter
        public static string FirstName = "FirstName";
        public static string LastName = "LastName";
        public static string PhoneNumber = "PhoneNumber";
        public static string Email = "Email";
        public static string TimeInterval = "TimeInterval";
        public static string LinkedInURL = "LinkedInURL";
        public static string GitHubURL = "GitHubURL";
        public static string FreeTextComment = "FreeTextComment";

        //Database Stored Procedure
        public static string Candidate = "usp_Create_Candidate";
    }
}
