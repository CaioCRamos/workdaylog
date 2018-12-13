using System;
using WorkDayLog.Core;
using WorkDayLog.Domain.Users.ValueObjects;

namespace WorkDayLog.Domain.Users
{
    public class User : EntityBase
    {
        public static User New(string name, string email, string password)
            => new User
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Email = new Email(email),
                Password = new Password(password)
            };

        public string Name { get; private set; }

        public Email Email { get; private set; }

        public Password Password { get; private set; }
    }
}