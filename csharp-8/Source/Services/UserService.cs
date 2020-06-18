using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private readonly CodenationContext _context;
        public UserService(CodenationContext context)
        {
                _context = context;
        }
        public IList<User> FindByAccelerationName(string name)
        {
            return (from tbA in _context.Accelerations
                    join tbC in _context.Candidates on tbA.Id equals tbC.AccelerationId
                    join tbU in _context.Users on tbC.UserId equals tbU.Id
                    where tbA.Name == name
                    select tbU
                    ).Distinct().ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
           return  (from tbC in _context.Candidates
                             where tbC.CompanyId.Equals(companyId)
                             select tbC.User).Distinct().ToList();
        }

        public User FindById(int id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User Save(User user)
        {
            var resultUser = FindById(user.Id);
            if ( resultUser is null)
            {
                _context.Users.Add(user);
            }
            else
            {
                resultUser.FullName = user.FullName;
                resultUser.Email = user.Email;
                resultUser.Nickname = user.Nickname;
                resultUser.Password = user.Password;
                _context.Users.Update(resultUser);
            }
            _context.SaveChanges();
            return user;
        }
    }
}
