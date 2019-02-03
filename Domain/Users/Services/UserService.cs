using System;
using System.Linq;
using WorkDayLog.Domain.Users.Repositories;

namespace WorkDayLog.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            => _userRepository = userRepository;

        public User Authenticate(string email, string password)
            => _userRepository.GetBy(
                u => 
                    u.Email.Address.Equals(email) && 
                    u.Password.Value.Equals(password)
                ).FirstOrDefault();

        public User GetById(Guid id)
            => _userRepository.GetById(id);

        public bool Save(User user)
        {
            //Validate if the e-mail address is unique

            _userRepository.Add(user);
            return true;
        }
    }
}