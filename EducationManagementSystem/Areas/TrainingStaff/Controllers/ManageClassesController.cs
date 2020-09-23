using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducationManagementSystem.Models;

namespace EducationManagementSystem.Areas.TrainingStaff.Controllers
{
    [Authorize(Roles = "TrainingStaff")]
    public class ManageClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrainingStaff/ManageClasses
        public ActionResult Index()
        {
            var classes = db.Classes.Include(c => c.Course);
            return View(classes.ToList());
        }

        // GET: TrainingStaff/ManageClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: TrainingStaff/ManageClasses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            return View();
        }

        // POST: TrainingStaff/ManageClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,StartDate,EndDate,CourseId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", @class.CourseId);
            return View(@class);
        }

        // GET: TrainingStaff/ManageClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", @class.CourseId);
            return View(@class);
        }

        // POST: TrainingStaff/ManageClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,StartDate,EndDate,CourseId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", @class.CourseId);
            return View(@class);
        }

        // GET: TrainingStaff/ManageClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: TrainingStaff/ManageClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // AssignTrainee
        public ActionResult AssignTrainee(int? Id)
        {
            Class currentClass = db.Classes.Find(Id);
            ICollection<ApplicationUser> TraineeList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("Trainee")).ToList();
            ViewBag.TraineeId = new SelectList(TraineeList.Where(item => currentClass.TraineeClasses.FirstOrDefault(r => r.ApplicationUserId == item.Id) == null).ToList(), "Id", "UserName");
            return View(currentClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToClass(int classId, string[] traineeId)
        {
            if (traineeId != null && traineeId.Count() > 0)
            {
                foreach (string item in traineeId)
                {
                    db.TraineeClasses.Add(new TraineeClass() { ClassId = classId, ApplicationUserId = item });
                }
                db.SaveChanges();
            }
            Class currentClass = db.Classes.Find(classId);
            ICollection<ApplicationUser> TraineeList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("Trainee")).ToList();
            ViewBag.TraineeId = new SelectList(TraineeList.Where(item => currentClass.TraineeClasses.FirstOrDefault(r => r.ApplicationUserId == item.Id) == null).ToList(), "Id", "UserName");
            return RedirectToAction("AssignTrainee", new { Id = classId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTraineeFromClass(int classId, string traineeId)
        {
            Class currentClass = db.Classes.Find(classId);
            ICollection<ApplicationUser> TraineeList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("Trainee")).ToList();
            db.TraineeClasses.Remove(currentClass.TraineeClasses.Single(m => m.ApplicationUserId == traineeId));
            db.SaveChanges();
            ViewBag.TraineeId = new SelectList(TraineeList.Where(item => currentClass.TraineeClasses.FirstOrDefault(r => r.ApplicationUserId == item.Id) == null).ToList(), "Id", "UserName");
            return RedirectToAction("AssignTrainee", new { Id = classId });
        }
    }
}
