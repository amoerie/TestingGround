using System.Collections.Generic;
using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Domain.Fitness.Models
{
    public class GymMember: Entity
    {
        public virtual string Name { get; set; }

        public ICollection<Workout> Workouts { get; set; }
    }
}
