using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TestingGround.Core.Domain.Fitness.Models;
using TestingGround.Core.Domain.Fitness.Repositories;
using TestingGround.Core.Infrastructure.Utilities.Including;

namespace TestingGround.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGymMemberRepository _gymMembers;

        public HomeController(IGymMemberRepository gymMembers)
        {
            _gymMembers = gymMembers;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var includer = EntityIncluder<GymMember>.Include(g => g.Workouts)
                .And(g => g.Workouts.Select(w => w.WorkoutExercises))
                .And(g => g.Workouts.Select(w => w.WorkoutExercises.Select(we => we.Exercise)));
            var gymMembers = _gymMembers.List(includer: includer).ToList();
            return View(gymMembers);
        }

        public ActionResult AddWorkout()
        {
            var gymMember = _gymMembers.Find(1);
            gymMember.Workouts.Add(new Workout
            {
                Date = DateTime.Now,
                WorkoutExercises = new List<WorkoutExercise>
                {
                    new WorkoutExercise
                    {
                        Calories = 500,
                        Repetitions = 10,
                        Exercise = new Exercise
                        {
                            Name = "Crunching!"
                        }
                    }
                }
            });
            _gymMembers.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveWorkout()
        {
            var gymMember = _gymMembers.Find(1);
            var workout = gymMember.Workouts.Last();
            workout.Deleted = false;
            _gymMembers.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
