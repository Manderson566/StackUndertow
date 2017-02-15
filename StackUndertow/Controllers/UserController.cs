using StackUndertow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackUndertow.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User


        [Route("U/{userName}")]
        public ActionResult Index(string UserName)
        {
            ApplicationUser userInstance = db.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            return View(userInstance);
        }
    }
}