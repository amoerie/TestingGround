using System.Collections.Generic;
using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Domain.Fitness.Models
{
    public class GymMember: Entity
    {
        private ICollection<Workout> _workouts;
        public virtual string Name { get; set; }

        public  ICollection<Workout> Workouts
        {
            get { return _workouts ?? (_workouts = new List<Workout>()); }
            set { _workouts = value; }
        }
    }
}
