using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Domain.Fitness.Models
{
    public class WorkoutExercise: Entity
    {
        public virtual Exercise Exercise { get; set; }
        public virtual int ExerciseId { get; set; }

        public virtual Workout Workout { get; set; }
        public virtual int WorkoutId { get; set; }

        public virtual int Repetitions { get; set; }
        public virtual double Calories { get; set; }

        public WorkoutExercise()
        {
        }

        public WorkoutExercise(Exercise exercise, double calories, int repetitions)
        {
            Exercise = exercise;
            Repetitions = repetitions;
            Calories = calories;
        }
    }
}
