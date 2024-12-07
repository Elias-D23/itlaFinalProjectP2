using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteLine.Common.Dtos;
using VoteLine.Common.Requests;
using VoteLine.Common.Responses;
using VoteLine.Domain.Entities;
using VoteLine.Persistence;
using VotLine.Common.Responses;
using VotLine.Infrastructure.Interfaces;

namespace VotLine.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly VoteLineDbContext _context;

        public CandidateRepository(VoteLineDbContext context)
        {
            _context = context;
        }

        public async Task<List<CandidateDto>> GetCandidates()
        {
            var candidates = await _context.Candidates.ToListAsync();
            var candidatesToReturn = new List<CandidateDto>();
            foreach (var canidateDb in candidates)
            {
                candidatesToReturn.Add(FromCanidateToDto(canidateDb));
            }
            return candidatesToReturn;
        }

        public async Task<CandidateDto> GetCandidate(int id)
        {
            var candidateDb = await _context.Candidates.FindAsync(id);
            if (candidateDb == null)
            {
                throw new Exception("No data Found");
            }

            return FromCanidateToDto(candidateDb);
        }

        public async Task<NewCandidateResponse> AddCandidate(NewCandidateRequest request)
        {

            var candidateDb = FromCandidateDtoToCandidate(request);

            _context.Candidates.Add(candidateDb);
            await _context.SaveChangesAsync();
            return new NewCandidateResponse { CandidateId = candidateDb.CandidateId };


        }

        public async Task<bool> UpdateCandidate(int id, NewCandidateRequest request)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return false;
            }

            candidate.FullName = request.FullName;
            candidate.Party = request.Party;
            candidate.Position = request.Position;
            candidate.Picture = request.Picture;

            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return false;
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return true;
        }

        private CandidateDto FromCanidateToDto(Candidate candidateDb)
        {
            return new CandidateDto
            {
                CandidateId = candidateDb.CandidateId,
                FullName = candidateDb.FullName,
                Position = candidateDb.Position,
                Party = candidateDb.Party,
                Picture = candidateDb.Picture,
            };
        }

        private Candidate FromCandidateDtoToCandidate(NewCandidateRequest dto)
        {
            return new Candidate
            {
                FullName = dto.FullName,
                Position = dto.Position,
                Party = dto.Party,
                Picture = dto.Picture,
            };
        }
        private Candidate FromCandidateDtoToCandidate(CandidateDto dto)
        {
            return new Candidate
            {
                FullName = dto.FullName,
                Position = dto.Position,
                Party = dto.Party,
                Picture = dto.Picture,
            };
        }
    
    }
}
    

