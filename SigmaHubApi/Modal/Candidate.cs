using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SigmaHubApi.Modal
{
    public class CandidateInfor
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string TimeInterval { get; set; }
        public string LinkedInURL { get; set; }
        public string GitHubURL { get; set; }
        [Required]
        public string FreeTextComment { get; set; }
    }
    public class Response
    {
        public ResponseCode Code;
        public string Message;
    }
    public class ExecuteResponse
    {
        public string Result;
    }
    public class ResponseData
    {
        public ResponseCode Code;
        public string Message;
        public object Result;
    }
    public class ResponseModel
    {
        public int Response { set; get; }
    }
    public enum ResponseCode
    {
        Successfull = 400,
        Existance = 401,
        InvalidLength = 402,
        InvalidInput = 403
    }
}
