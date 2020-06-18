using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly CodenationContext _context;
        public CompanyService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return (from tbCa in _context.Candidates
                    join tbCo in _context.Companies on tbCa.CompanyId equals tbCo.Id
                    where tbCa.AccelerationId == accelerationId 
                    select tbCo
                ).Distinct().ToList();
        }
        public Company FindById(int id)
        {
            return _context.Companies.Where(x => x.Id == id).FirstOrDefault();
        }
        public IList<Company> FindByUserId(int userId)
        {
            return _context.Candidates.Where(x => x.UserId == userId)
                .Select(x => x.Company).Distinct().ToList();
        }
        public Company Save(Company company)
        {
            var resultCompany = FindById(company.Id);
            if (resultCompany is null)
            {
                _context.Companies.Add(company);
            }
            else
            {
                resultCompany.Name = company.Name;
                resultCompany.Slug = company.Slug;
                _context.Companies.Update(resultCompany);
            }
            _context.SaveChanges();
            return company;
        }
    }
}