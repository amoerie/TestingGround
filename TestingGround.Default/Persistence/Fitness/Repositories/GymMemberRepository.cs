using System.Data.Entity;
using TestingGround.Core.Domain.Fitness;
using TestingGround.Core.Domain.Fitness.Models;
using TestingGround.Core.Domain.Fitness.Repositories;
using TestingGround.Default.Persistence.Internal.Repositories;

namespace TestingGround.Default.Persistence.Fitness.Repositories
{
    public class GymMemberRepository: Repository<GymMember>, IGymMemberRepository
    {
        public GymMemberRepository(DbContext context)
            : base(context)
        {
        }
    }
}
