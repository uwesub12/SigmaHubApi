using CsvHelper;
using Dapper;
using SigmaHubApi.Helper;
using SigmaHubApi.Interface;
using SigmaHubApi.Modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                bool Found = false;
                string fileLocation = AppDomain.CurrentDomain.BaseDirectory + "StoredCandidate.csv";
                string[] dataList = File.ReadAllLines(fileLocation);
                int col = 0;
                foreach (var Data in dataList)
                {
                    if (Data.Contains(candidateInfor.Email))
                    {
                        Found = true;
                        break;
                    }
                    else
                    {
                        col++;
                    }
                }
                if (Found == true)
                {
                    dataList[col] = dataList[col].Split(',')[0] + candidateInfor.FirstName + "," + candidateInfor.LastName + "," 
                        + candidateInfor.PhoneNumber + "," + candidateInfor.Email + "," + candidateInfor.TimeInterval + "," 
                        + candidateInfor.LinkedInURL + "," + candidateInfor.GitHubURL + "," + candidateInfor.FreeTextComment;
                    File.WriteAllLines(fileLocation , dataList);
                    return 1;
                }
                else
                {
                    string separator = ",";
                    StringBuilder outString = new StringBuilder();
                    string[] newLine = { candidateInfor.FirstName, candidateInfor.LastName,
                                    candidateInfor.PhoneNumber, candidateInfor.Email, candidateInfor.TimeInterval.ToString(),
                                    candidateInfor.LinkedInURL,candidateInfor.GitHubURL,candidateInfor.FreeTextComment};
                    outString.AppendLine(string.Join(separator, newLine));
                    await Task.Run(() => { File.AppendAllText(fileLocation, outString.ToString()); });
                    return 2;
                }                
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
