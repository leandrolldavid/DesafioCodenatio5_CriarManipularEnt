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
            return _context.Candidates.Where(x => x.UserId == userId && x.AccelerationId == accelerationId && x.CompanyId == companyId).FirstOrDefault();
        }

        public Candidate Save(Candidate candidate)
        {
            Candidate saveCandidate;
            if (candidate.UserId.Equals(0) || candidate.AccelerationId.Equals(0) || candidate.CompanyId.Equals(0))
            {
                _context.Candidates.Add(candidate);
                saveCandidate = candidate;
            }
            else
            {
                _context.Candidates.Update(candidate);
                saveCandidate = candidate;
            }
            _context.SaveChanges();
            return saveCandidate;
        }
    }
}
