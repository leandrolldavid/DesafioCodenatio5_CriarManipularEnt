using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private readonly CodenationContext _context;

        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return  _context.Candidates
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.Acceleration)
                .Distinct().ToList();
        }

        public Acceleration FindById(int id)
        {
            return _context.Accelerations.Where(x => x.Id == id).FirstOrDefault();

        }

        public Acceleration Save(Acceleration acceleration)
        {
            Acceleration resultAcceleration = acceleration;
           // if (acceleration.Id.Equals(0))
             if(!_context.Accelerations.Any(x => x.Id == acceleration.Id))
            {
                _context.Accelerations.Add(acceleration);
            }
            else
            {
                _context.Accelerations.Update(acceleration);
            }
            _context.SaveChanges();
            return resultAcceleration;
        }
    }
}
