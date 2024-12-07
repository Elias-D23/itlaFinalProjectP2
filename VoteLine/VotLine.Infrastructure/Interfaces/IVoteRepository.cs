using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteLine.Domain.Entities;

namespace VotLine.Infrastructure.Interfaces
{
    public interface IVoteRepository
    {
        Task<bool> SubmitVote(Vote model);

        Task<List<object>> GetResults();
    }
}
