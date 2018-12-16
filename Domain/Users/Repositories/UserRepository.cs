using Microsoft.Extensions.Configuration;
using WorkDayLog.Core;

namespace WorkDayLog.Domain.Users.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        protected UserRepository(IConfiguration config) 
            : base(config, "Users") {}
    }
}