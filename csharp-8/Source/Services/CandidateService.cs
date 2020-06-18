using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CodenationContext _context;
        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates
               .Where(x => x.AccelerationId == accelerationId).ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Candidates
                .Where(x => x.CompanyId == companyId).ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates
                .Where(x => x.UserId == userId && x.AccelerationId == accelerationId && x.CompanyId == companyId)
                .FirstOrDefault();
        }

        public Candidate Save(Candidate candidate)
        {
            var resultCandidate = FindById(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);

            if (resultCandidate is null)
            {
                _context.Candidates.Add(candidate);
            }
            else
            {
                resultCandidate.Status = candidate.Status;
                _context.Candidates.Update(resultCandidate);
            }
            _context.SaveChanges();
            return candidate;
        }
    }
}
