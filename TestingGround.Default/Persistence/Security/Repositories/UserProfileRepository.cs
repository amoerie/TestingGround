using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingGround.Core.Domain.Security;
using TestingGround.Core.Domain.Security.Repositories;
using TestingGround.Default.Database;
using TestingGround.Default.Persistence.Internal.Repositories;

namespace TestingGround.Default.Persistence.Security.Repositories
{
    public class UserProfileRepository: Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(TestingContext context) : base(context)
        {
        }
    }
}
