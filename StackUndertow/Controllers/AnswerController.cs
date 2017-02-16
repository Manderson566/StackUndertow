using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StackUndertow.Models;
using Microsoft.AspNet.Identity;

namespace StackUndertow.Controllers
{
    public class AnswerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answer
        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.Owner).Include(a => a.Question);
            return View(answers.ToList());
        }

        // GET: Answer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answer/Create
        public ActionResult Create(int? id)
        {
          ViewBag.UserQuestionA = db.Questions.Where(q => q.Id == id).ToList();
            ViewBag.QuestionId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuestionId,OwnerId,QAnswer")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                int aid = answer.QuestionId;
                ViewBag.UserQuestionA = db.Questions.Where(q => q.Id == aid).ToList();
                db.Answers.Add(answer);
                answer.Created = DateTime.Now;
                answer.OwnerId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", answer.OwnerId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", answer.QuestionId);
            return View(answer);
        }

        // GET: Answer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", answer.OwnerId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", answer.QuestionId);
            return View(answer);
        }

        // POST: Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuestionId,OwnerId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", answer.OwnerId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", answer.QuestionId);
            return View(answer);
        }

        // GET: Answer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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
    }
}
