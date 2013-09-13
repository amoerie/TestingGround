using System;
using System.Collections.Generic;
using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Domain.Fitness.Models
{
    public class Workout: Entity
    {
        private ICollection<WorkoutExercise> _workoutExercises;

        public DateTime Date { get; set; }

        public ICollection<WorkoutExercise> WorkoutExercises
        {
            get { return _workoutExercises ?? (_workoutExercises = new List<WorkoutExercise>()); }
            set { _workoutExercises = value; }
        }
    }
}
