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
            return (from tbU in _context.Users
                    join tbC in _context.Candidates on tbU.Id equals tbC.UserId
                    join tbS in _context.Submissions on tbU.Id equals tbS.UserId
                    where tbC.AccelerationId == accelerationId && tbS.ChallengeId == challengeId
                    select tbS).Distinct().ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return  _context.Submissions.
            Where(x => x.ChallengeId == challengeId).Max(x => x.Score);
        }

        public Submission Save(Submission submission)
        {
            Submission saveSubmission;
            if (submission.ChallengeId.Equals(0) &&
                submission.UserId.Equals(0))
                {
                _context.Submissions.Add(submission);
                saveSubmission = submission;
            }
            else
            {
                _context.Submissions.Update(submission);
                saveSubmission = submission;
            }
            _context.SaveChanges();
            return saveSubmission;
        }
    }
}
