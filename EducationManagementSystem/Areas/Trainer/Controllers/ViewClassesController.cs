using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducationManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace EducationManagementSystem.Areas.Trainer.Controllers
{
    public class ViewClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainer/ViewClasses
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var classes = db.Classes.Include(c => c.Course).Include(c => c.Course.Category);
            return View(classes.Where(x => x.Topics.Select(y => y.ApplicationUserId).Contains(userID)).ToList());
        }

        // GET: Trainer/ViewClasses/Details/5
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
    }
}