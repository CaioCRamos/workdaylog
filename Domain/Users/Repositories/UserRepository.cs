using Microsoft.Extensions.Configuration;
using WorkDayLog.Core;

namespace WorkDayLog.Domain.Users.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly string _connectionString;

        protected UserRepository(IConfiguration config) 
            : base(config, "Users") {}
    }
}