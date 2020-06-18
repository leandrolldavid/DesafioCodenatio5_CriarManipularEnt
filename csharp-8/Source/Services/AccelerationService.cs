using System.Collections.Generic;
using System.Linq;
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
            var resultAcceleration = FindById(acceleration.Id);
             if (resultAcceleration is null)
            {
                _context.Accelerations.Add(acceleration);
            }
            else
            {
                resultAcceleration.Name = acceleration.Name;
                resultAcceleration.Slug = acceleration.Slug;
                _context.Accelerations.Update(resultAcceleration);
            }
            _context.SaveChanges();
            return acceleration;
        }
    }
}
