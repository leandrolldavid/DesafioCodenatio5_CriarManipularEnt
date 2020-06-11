using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly CodenationContext _context;
        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            return _context.Candidates
                .Where(x => x.AccelerationId == accelerationId && x.UserId == userId )
                .Select(x => x.Acceleration.Challenge).Distinct().ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            Models.Challenge saveChallenge = challenge;
            // if (challenge.Id.Equals(0))
            if (!_context.Challenges.Any(x => x.Id == challenge.Id ))

            {
                _context.Challenges.Add(challenge);
            }
            else
            {
                _context.Challenges.Update(challenge);
            }
            _context.SaveChanges();
            return saveChallenge;
        }
    }
}