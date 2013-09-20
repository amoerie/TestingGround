using System.Linq;
using NUnit.Framework;
using TestingGround.Core.Domain.Fitness.Models;
using TestingGround.Core.Domain.Internal.Contracts;
using TestingGround.Core.Infrastructure.Utilities.Filtering;

namespace TestingGround.Tests
{
    public class TestsForLazyCollectionLoading: TestBase
    {
        private readonly IEntityFilter<GymMember> _alexFilter = EntityFilter<GymMember>.Where(g => g.Name.Equals("Alex"));
        [Test]
        public void TestThatNumberOfGymMembersIs1()
        {
            var gymMembers = GymMembers.List();
            Assert.That(gymMembers.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TestThatNumberOfWorkoutsOnAlexIs2()
        {
            var alex = GymMembers.Single(_alexFilter);
            Assert.That(alex.Workouts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestThatNumberOfWorkoutExercisesOnTheFirstWorkoutIs3()
        {
            var alex = GymMembers.Single(_alexFilter);
            var firstWorkout = alex.Workouts.OrderBy(w => w.Date).First();
            Assert.That(firstWorkout.WorkoutExercises.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestThatNumberOfWorkoutExercisesOnTheLastWorkoutIs0()
        {
            var alex = GymMembers.Single(_alexFilter);
            var lastWorkout = alex.Workouts.OrderBy(w => w.Date).Last();
            Assert.That(lastWorkout.WorkoutExercises.Count, Is.EqualTo(0));
        }
    }
}
