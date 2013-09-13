using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingGround.Core.Domain.Fitness.Models;

namespace TestingGround.Default.Database
{
    public class TestingInitializer: DropCreateDatabaseIfModelChanges<TestingContext>
    {
        protected override void Seed(TestingContext context)
        {
            var pushups = new Exercise {Name = "Pushups"};
            var crunches = new Exercise {Name = "Crunches"};
            var situps = new Exercise {Name = "Situps"};
            var alex = new GymMember {Name = "Alex"};
            alex.Workouts = new List<Workout>
            {
                new Workout
                {
                    Date = DateTime.Now.AddDays(-7 * 5),
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Exercise = pushups,
                            Calories = 500,
                            Repetitions = 50
                        },
                        new WorkoutExercise
                        {
                            Exercise = situps,
                            Calories = 300,
                            Repetitions = 75
                        },
                        new WorkoutExercise
                        {
                            Exercise = crunches,
                            Calories = 120,
                            Repetitions = 15,
                            Deleted = true
                        },
                    }
                },
                new Workout
                {
                    Date = DateTime.Now.AddDays(-7 * 4),
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Exercise = pushups,
                            Calories = 500,
                            Repetitions = 50
                        },
                        new WorkoutExercise
                        {
                            Exercise = situps,
                            Calories = 300,
                            Repetitions = 75
                        },
                        new WorkoutExercise
                        {
                            Exercise = crunches,
                            Calories = 120,
                            Repetitions = 15,
                            Deleted = true
                        },
                    }
                },
                new Workout
                {
                    Date = DateTime.Now.AddDays(-7 * 3),
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Exercise = pushups,
                            Calories = 500,
                            Repetitions = 50
                        },
                        new WorkoutExercise
                        {
                            Exercise = situps,
                            Calories = 300,
                            Repetitions = 75
                        },
                        new WorkoutExercise
                        {
                            Exercise = crunches,
                            Calories = 120,
                            Repetitions = 15,
                            Deleted = true
                        },
                    }
                },
                new Workout
                {
                    Date = DateTime.Now.AddDays(-7 * 2),
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Exercise = pushups,
                            Calories = 500,
                            Repetitions = 50
                        },
                        new WorkoutExercise
                        {
                            Exercise = situps,
                            Calories = 300,
                            Repetitions = 75
                        },
                        new WorkoutExercise
                        {
                            Exercise = crunches,
                            Calories = 120,
                            Repetitions = 15,
                            Deleted = true
                        },
                    }
                },
                new Workout
                {
                    Date = DateTime.Now.AddDays(-7 * 1),
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Exercise = pushups,
                            Calories = 500,
                            Repetitions = 50
                        },
                        new WorkoutExercise
                        {
                            Exercise = situps,
                            Calories = 300,
                            Repetitions = 75
                        },
                        new WorkoutExercise
                        {
                            Exercise = crunches,
                            Calories = 120,
                            Repetitions = 15,
                            Deleted = true
                        },
                    }
                },
                new Workout
                {
                    Date = DateTime.Now.AddDays(-7 * 1),
                    WorkoutExercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise
                        {
                            Exercise = pushups,
                            Calories = 500,
                            Repetitions = 50
                        },
                        new WorkoutExercise
                        {
                            Exercise = situps,
                            Calories = 300,
                            Repetitions = 75
                        },
                        new WorkoutExercise
                        {
                            Exercise = crunches,
                            Calories = 120,
                            Repetitions = 15,
                            Deleted = true
                        },
                    }
                },
            };

            context.GymMembers.Add(alex);
            context.SaveChanges();
        }
    }
}
