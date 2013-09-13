using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingGround.Core.Domain.Internal.Repositories;

namespace TestingGround.Core.Domain.Security.Repositories
{
    public interface IUserProfileRepository: IRepository<UserProfile>
    {
    }
}
