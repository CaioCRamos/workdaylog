using System;
using WorkDayLog.Domain.Users.ValueObjects;

namespace WorkDayLog.Domain.Users
{
    public class User
    {
        public static User New(string email, string password)
            => new User
            {
                Id = Guid.NewGuid(),
                Email = new Email(email),
                Password = new Password(email)
            };

        public static User Load(Guid id, string email, string password)
            => new User
            {
                Id = id,
                Email = new Email(email),
                Password = new Password(password)
            };

        public Guid Id { get; private set; }

        public Email Email { get; private set; }

        public Password Password { get; private set; }
    }
}