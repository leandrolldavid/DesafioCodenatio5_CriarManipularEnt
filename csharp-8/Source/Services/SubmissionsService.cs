using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly CodenationContext _context;
        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return (from tbC in _context.Candidates
                    join tbU in _context.Users on tbC.UserId equals tbU.Id
                    join tbS in _context.Submissions on tbU.Id equals tbS.UserId
                    where tbC.AccelerationId == accelerationId && tbS.ChallengeId == challengeId
                    select tbS).Distinct().ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {//edt
            return _context.Submissions
                .Where(x => x.ChallengeId == challengeId)
                .OrderByDescending(x => x.ChallengeId)
                .LastOrDefault().Score;
        }

        public Submission Save(Submission submission)
        {
            Submission saveSubmission = submission;
         // test   if (_context.Submissions.FirstOrDefault(x => x.UserId == submission.UserId || x.ChallengeId == submission.ChallengeId) == null)
            if (!_context.Submissions.Any(x => x.UserId == submission.UserId || x.ChallengeId == submission.ChallengeId))
            {
                _context.Submissions.Add(submission);
            }
            else
            {
                _context.Submissions.Update(submission);
            }
            _context.SaveChanges();
            return saveSubmission;
        }
    }
}
