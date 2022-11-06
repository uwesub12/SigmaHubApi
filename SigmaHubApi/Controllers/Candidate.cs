using Microsoft.AspNetCore.Mvc;
using SigmaHubApi.Helper;
using SigmaHubApi.Interface;
using SigmaHubApi.Modal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace SigmaHubApi.Controllers
{
    public class Candidate : ControllerBase
    {
        private readonly IJobCandidate _JobCandidate;
        public Candidate(IJobCandidate jobcandidate)
        {
            _JobCandidate = jobcandidate;
        }
        #region Job Candidate
        [Route("JobCandidate")]
        [HttpPost]
        [ResponseType(typeof(List<ResponseModel>))]
        public async Task<IActionResult> JobCandidate([FromBody] CandidateInfor candidate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(candidate.FirstName))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, First Name should not be empty" });
                }
                else if (Helpers.CheckCharacter(candidate.FirstName) == false)
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, First Name should Contain letters" });
                }
                else if (string.IsNullOrWhiteSpace(candidate.LastName))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Last Name should not be empty" });
                }
                else if (Helpers.CheckCharacter(candidate.LastName) == false)
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Last Name should Contain letters" });
                }
                else if (candidate.PhoneNumber.Length != 10)
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Invalid Length of Phone Number (Should be 10 {0XXXXXXXXX})" });
                }
                else if (Helpers.CheckDigit(candidate.PhoneNumber) == false)
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Phone Number allow only Digits" });
                }
                else if (string.IsNullOrWhiteSpace(candidate.Email))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Email should not be Empty" });
                }
                else if (Helpers.validateEmail(candidate.Email) == false)
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Invalid Email Address" });
                }
                else if (string.IsNullOrWhiteSpace(candidate.FreeTextComment))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, FreeTextComment should not be empty" });
                }
                else
                {
                    var result = await _JobCandidate.EnrolCandicateToCsv(candidate);
                    if (result == 0)
                    {
                        return Ok(new Response { Code = ResponseCode.Existance, Message = "Fail to add Profile" });
                    }
                    else if (result == 1)
                    {
                        return Ok(new ResponseData { Code = ResponseCode.Successfull, Message = "successfully Updated", Result = result });
                    }
                    else if (result == 2)
                    {
                        return Ok(new ResponseData { Code = ResponseCode.Successfull, Message = "successfully Added", Result = result });
                    }
                    else
                    {
                        return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Fail to Modify Profile " });
                    }                   
                }
            }
            catch (Exception ex)
            {
                Logs.Writelog("JobCandidate : " + ex.ToString());
                return Ok(new Response { Code = ResponseCode.Existance, Message = "Sorry, Check out your input or Network Connection" });
            }
        }
        #endregion
    }
}
