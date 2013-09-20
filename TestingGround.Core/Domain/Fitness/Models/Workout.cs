using System;
using System.Collections.Generic;
using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Domain.Fitness.Models
{
    public class Workout: Entity
    {
        public DateTime Date { get; set; }
        public virtual GymMember GymMember { get; set; }
        public virtual int GymMemberId { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
