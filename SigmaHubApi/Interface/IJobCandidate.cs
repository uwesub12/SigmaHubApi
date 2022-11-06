using SigmaHubApi.Modal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SigmaHubApi.Interface
{
    public interface IJobCandidate
    {
        public Task<int> EnrolCandicateToCsv(CandidateInfor candidateInfor);
        public Task<List<ExecuteResponse>> EnrolCandicateToDb(CandidateInfor candidateInfor);
    }
}
