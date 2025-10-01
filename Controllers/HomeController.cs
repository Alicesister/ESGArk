using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ESGArk.Models;

namespace ESGArk.Controllers
{
    public class HomeController : Controller
    {
        ESGArkEntities db=new ESGArkEntities();
        public ActionResult Food()
        {
            return View();
        }
        public ActionResult Energy()
        {
            return View();
        }
        public ActionResult Traffic()
        {
            return View();
        }
        public ActionResult Life()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Index()
        {
            var list = db.Members.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult Edit(string fName)
        {
            var mem = db.Members.
                Where(m => m.Name == fName)
                .FirstOrDefault();
            return View(mem);
        }
        [HttpPost]
        public ActionResult Edit(Member member)
        {
            var tmem=db.Members.
                Where(m=>m.UserId==member.UserId)
                .FirstOrDefault();
            tmem.Name = member.Name;
            tmem.Email = member.Email;
            tmem.Pwd = member.Pwd;
            tmem.CreatedAt = member.CreatedAt;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string fName)
        {
            var member = db.Members.
                Where(m=> m.Name == fName)
                .FirstOrDefault();
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Member pmember)
        {
            if(ModelState.IsValid == false)
            {
                ViewBag.Message = "請輸入帳密";
                return View();
            }
            var member = db.Members.
                Where(m=>m.Email==pmember.Email)
                .FirstOrDefault();
            if (member == null)
            {
                db.Members.Add(pmember);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號已有人使用";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pwd)
        {
            var member=db.Members.
                Where(m=>m.Email== email && m.Pwd==pwd)
                .FirstOrDefault();
            if (member == null) 
            {
                ViewBag.Message = "帳密錯誤";
                return View();
            }
            Session["Welcome"] = member.Name + "會員歡迎";
            FormsAuthentication.RedirectFromLoginPage(email, true);
            return RedirectToAction("Index", "member");
        }
        public ActionResult ForgetPwd()
        {
            return View();
        }
    }
}
