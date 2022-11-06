using NUnit.Framework;
using System.Threading.Tasks;

namespace SigmaHubApiUnitTesting
{
    public class Candidate
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task<int> Create_Candidate_Profile(string CandidateDetail)
        {
            return await Task.Run(() => { Save_data_to_csv(CandidateDetail); });
        }
        [Test]
        public int Save_data_to_csv(string CandidateDetail)
        {
            return 0;
        }
    }
}