using OnlineVotingSystem.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models.Interfaces
{
    public interface IResultService
    {
        Task<List<ElectionViewModel>> GetElectionList();
        Task<List<ResultViewModel>> GetCandidateList(int electionID);
        Task<string> GetElectionName(int electionID);
    }
}
