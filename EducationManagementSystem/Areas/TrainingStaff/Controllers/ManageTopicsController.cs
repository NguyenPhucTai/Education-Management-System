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
    public class ManageTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string RoleCondition = "Trainer";

        // GET: TrainingStaff/ManageTopics
        public ActionResult Index(int? classId)
        {
            var topics = db.Topics.Include(t => t.Class).Include(t => t.Trainer);
            ViewBag.ClassId = classId;
            ViewBag.ClassCode = db.Classes.Where(c => c.Id == classId).First().Code;
            return View(topics.Where(x => x.ClassId == classId).ToList());
           
        }

        // GET: TrainingStaff/ManageTopics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: TrainingStaff/ManageTopics/Create
        public ActionResult Create(string classCode)
        {
            ViewBag.ClassId = db.Classes.Where(x => x.Code == classCode).First().Id;
            ViewBag.ApplicationUserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleCondition)).ToList(), "Id", "UserName");
            return View();
        }

     
        // POST: TrainingStaff/ManageTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ApplicationUserId,ClassId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index", new { classId = topic.ClassId });
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Code", topic.ClassId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", topic.ApplicationUserId);
            return View(topic);
        }


        //
        // GET: TrainingStaff/ManageTopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleCondition)).ToList(), "Id", "UserName", topic.ApplicationUserId);
            return View(topic);
        }

        // POST: TrainingStaff/ManageTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ApplicationUserId,ClassId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {classId = topic.ClassId });
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Code", topic.ClassId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", topic.ApplicationUserId);
            return View(topic);
        }


        // GET: TrainingStaff/ManageTopics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: TrainingStaff/ManageTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Topic topic = db.Topics.Find(id);
            var ClassId = topic.ClassId;
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index", new { classId = ClassId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
