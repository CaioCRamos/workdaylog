using System;

namespace WorkDayLog.Domain.Users.Services
{
    public interface IUserService
    {
        User GetById(Guid id);

        User Authenticate(string email, string password);

        bool Save(User user);
    }
}