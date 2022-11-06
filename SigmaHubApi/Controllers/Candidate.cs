using Microsoft.AspNetCore.Mvc;
using SigmaHubApi.Interface;
using SigmaHubApi.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Cif Number should not be empty" });
                }
                else if (string.IsNullOrWhiteSpace(candidate.LastName))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Cif Number should not be empty" });
                }
                else if (string.IsNullOrWhiteSpace(candidate.Email))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Cif Number should not be empty" });
                }
                else if (string.IsNullOrWhiteSpace(candidate.FreeTextComment))
                {
                    return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Cif Number should not be empty" });
                }               
                else
                {
                    var result = await _JobCandidate.EnrolCandicateToCsv(candidate);
                    if (result == 0)
                    {
                        return Ok(new Response { Code = ResponseCode.Existance, Message = "Fail to add Information" });
                    }
                    if (result == 1)
                    {
                        return Ok(new ResponseData { Code = ResponseCode.Successfull, Message = "successfully", Result = result });
                    }
                    else
                    {
                        return Ok(new Response { Code = ResponseCode.InvalidInput, Message = "Sorry, Fail to Modify Customer " });
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
