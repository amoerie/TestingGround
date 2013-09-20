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
        private readonly IEntityLoader<GymMember> _workoutsLoader 
            = EntityLoader<GymMember>.Include(g => g.Workouts.Where(w => w.Id == 2));
        private readonly IEntityFilter<GymMember> _alexFilter = EntityFilter<GymMember>.Where(g => g.Name.Equals("Alex"));
        [Test]
        public void TestThatNumberOfGymMembersIs1()
        {
            var gymMembers = GymMembers.List(loader: _workoutsLoader);
            Assert.That(gymMembers.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TestThatNumberOfWorkoutsOnAlexIs1()
        {
            var alex = GymMembers.Single(_alexFilter, _workoutsLoader);
            Assert.That(alex.Workouts.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestThatNumberOfWorkoutExercisesOnTheWorkoutIs0()
        {
            var alex = GymMembers.Single(_alexFilter, _workoutsLoader);
            var firstWorkout = alex.Workouts.Single();
            Assert.That(firstWorkout.WorkoutExercises.Count, Is.EqualTo(0));
        }
    }
}
