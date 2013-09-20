using System.Data;
using System.Linq;
using NUnit.Framework;
using TestingGround.Core.Domain.Fitness.Models;
using TestingGround.Core.Domain.Internal.Contracts;
using TestingGround.Core.Infrastructure.Utilities.Filtering;
using TestingGround.Core.Infrastructure.Utilities.Loading;

namespace TestingGround.Tests
{
    [TestFixture]
    public class TestsForEagerEnumerableLoading: TestBase
    {
        private readonly IEntityLoader<GymMember> _workoutsLoader = EntityLoader<GymMember>.Include(g => g.Workouts.Where(w => w.Deleted == false));
        private readonly IEntityFilter<GymMember> _alexFilter = EntityFilter<GymMember>.Where(g => g.Name.Equals("Alex"));
        [Test]
        public void TestThatNumberOfGymMembersIs1()
        {
            var gymMembers = GymMembers.List(loader: _workoutsLoader);
            Assert.That(gymMembers.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TestThatNumberOfWorkoutsOnAlexIs2()
        {
            var alex = GymMembers.Single(_alexFilter, _workoutsLoader);
            Assert.That(alex.Workouts.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestThatNumberOfWorkoutExercisesOnTheFirstWorkoutIs3()
        {
            var alex = GymMembers.Single(_alexFilter, _workoutsLoader);
            var firstWorkout = alex.Workouts.OrderBy(w => w.Date).First();
            Assert.That(firstWorkout.WorkoutExercises.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestThatNumberOfWorkoutExercisesOnTheLastWorkoutIs0()
        {
            var alex = GymMembers.Single(_alexFilter, _workoutsLoader);
            var lastWorkout = alex.Workouts.OrderBy(w => w.Date).Last();
            Assert.That(lastWorkout.WorkoutExercises.Count, Is.EqualTo(0));
        }
    }
}
