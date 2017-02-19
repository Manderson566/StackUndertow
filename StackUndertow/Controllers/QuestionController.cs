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
    public class QuestionController : Controller
    {
        public Question question;

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Question
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Owner);
            return View(questions.ToList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id, int? Aid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QAnswers = db.Answers.Where(a => a.QuestionId == id).ToList();
            string currentUser = User.Identity.GetUserId();
    
            return View(question);
        }


        [Authorize]
        [HttpPost]
        public ActionResult UpVote(int Aid, int? id)
        {
           

            Vote vote = new Vote();
       
            vote.UpVote = 1;
            vote.OwnerId = User.Identity.GetUserId();
            vote.AnswerId = Aid;
            vote.Created = DateTime.Now;
            db.Votes.Add(vote);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id, Aid = Aid });

        }
        [Authorize]
        [HttpPost]
        public ActionResult DownVote(int Aid, int? id)
        {
            

            Vote vote = new Vote();
          
            vote.DownVote = 1;
            vote.OwnerId = User.Identity.GetUserId();
            vote.AnswerId = Aid;
            vote.Created = DateTime.Now;
            db.Votes.Add(vote);
            db.SaveChanges();


            return RedirectToAction("Details", new { id = id });
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,QuestionText")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Created = DateTime.Now;
                question.OwnerId = User.Identity.GetUserId();
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", question.OwnerId);
            return View(question);
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", question.OwnerId);
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,QuestionText,OwnerId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", question.OwnerId);
            return View(question);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
