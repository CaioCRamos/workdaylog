using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WorkDayLog.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        IEnumerable<User> GetBy(Expression<Func<User, bool>> search);

        User GetById(Guid id);
    }
}