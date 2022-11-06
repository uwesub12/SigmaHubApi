using Dapper;
using SigmaHubApi.Helper;
using SigmaHubApi.Interface;
using SigmaHubApi.Modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SigmaHubApi.Repository
{
    public class JobCandidate: IJobCandidate
    {
        private readonly IDataAccessManager _Database;
        public JobCandidate(IDataAccessManager Database)
        {
            _Database = Database;
        }
        public async Task<int> EnrolCandicateToCsv([FromBody]CandidateInfor candidateInfor)
        {
            try
            {
                string separator = ",";
                StringBuilder outString = new StringBuilder();
                string fileLocation = AppDomain.CurrentDomain.BaseDirectory + "StoredCandidate.csv";                
                string[] newLine = { candidateInfor.FirstName, candidateInfor.LastName,
                                    candidateInfor.PhoneNumber, candidateInfor.Email, candidateInfor.TimeInterval.ToString(),
                                    candidateInfor.LinkedInURL,candidateInfor.GitHubURL,candidateInfor.FreeTextComment};
                outString.AppendLine(string.Join(separator, newLine));
                await Task.Run(() => { File.AppendAllText(fileLocation, outString.ToString()); });
                return 1;
            }
            catch (Exception ex)
            {
                Logs.Writelog("EnrolCandicate : " + ex.ToString());
                return 0;
            }
        }
        public async Task<List<ExecuteResponse>> EnrolCandicateToDb(CandidateInfor candidateInfor)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Parameter.FirstName, candidateInfor.FirstName, DbType.String);
                param.Add(Parameter.LastName, candidateInfor.LastName, DbType.String);
                param.Add(Parameter.PhoneNumber, candidateInfor.PhoneNumber, DbType.String);
                param.Add(Parameter.Email, candidateInfor.Email, DbType.String);
                param.Add(Parameter.TimeInterval, candidateInfor.TimeInterval, DbType.String);
                param.Add(Parameter.LinkedInURL, candidateInfor.LinkedInURL, DbType.String);
                param.Add(Parameter.GitHubURL, candidateInfor.GitHubURL, DbType.String);
                param.Add(Parameter.FreeTextComment, candidateInfor.FreeTextComment, DbType.String);
                var result = await Task.FromResult(_Database.ExecuteStoredProcedure<ExecuteResponse>(Parameter.Candidate, param));
                return result;
            }
            catch (Exception ex)
            {
                Logs.Writelog("EnrolCandicate : " + ex.ToString());
                return null;
            }
        }
    }
}
