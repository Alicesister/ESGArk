using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ESGArk.Models;

namespace ESGArk.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        ESGArkEntities db = new ESGArkEntities();
        // GET: Member
        public ActionResult Index()
        {
            return View("../Home/Index2", "_LayoutMember");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult PBA()
        {
            return View();
        }
    }
}