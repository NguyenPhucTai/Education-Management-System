﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducationManagementSystem.Models;

namespace EducationManagementSystem.Areas.Trainee.Controllers
{
    [Authorize(Roles = "Trainee")]
    public class ViewTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainee/ViewTopics
        public ActionResult Index(int? classId)
        {
            var topics = db.Topics.Include(t => t.Class).Include(t => t.Trainer);
            return View(topics.Where(x => x.ClassId == classId).ToList());
        }

        // GET: Trainee/ManageTopics/Details/5
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
    }
}