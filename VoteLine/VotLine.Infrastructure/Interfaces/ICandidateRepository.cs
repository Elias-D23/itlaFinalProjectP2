using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteLine.Common.Dtos;
using VoteLine.Common.Requests;
using VoteLine.Common.Responses;

namespace VotLine.Infrastructure.Interfaces
{
    public interface ICandidateRepository
    {
        Task<List<CandidateDto>> GetCandidates();

        Task<CandidateDto> GetCandidate(int id);

        Task<NewCandidateResponse> AddCandidate(NewCandidateRequest request);

        Task<bool> UpdateCandidate(int id, NewCandidateRequest request);

        Task<bool> DeleteCandidate(int id);
       
    }
}
