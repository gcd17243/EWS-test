using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EWSApplication.BussinessLayers;
using EWSApplication.DataLayers.Common;
using EWSApplication.Entities.DBContext;

namespace EWSApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public ActionResult Index(string mode = "all", int page = 1)
        {
            List<StructurePostToRender> lst = new List<StructurePostToRender>();
            EWSDbContext db = new EWSDbContext();
            
            ViewBag.userId = Session["uid"];
            ViewBag.userName = Session["uname"];
            ViewBag.ufacultyid = Session["ufacultyid"];
            ViewBag.uroleid = Session["uroleid"];
            ViewBag.facultyname = Session["facultyname"];
            ViewBag.opentime = Session["opentime"];

            int pageSize = 5;
            int rowCount = (from s in db.Posts where s.isActive == true select s).Count();
            int pageCount = rowCount / pageSize;
            ViewBag.pageCount = rowCount / pageSize;
            //ViewBag.pageSize = pageSize;
            ViewBag.pageCur = page;
            //ViewBag.mode = mode;
            if (rowCount % pageSize > 0)
            {
                ViewBag.pageCount = rowCount / pageSize + 1;
                pageCount = rowCount / pageSize + 1;
            }
            if (page > pageCount)
            {
                page = pageCount;
            }
            if (page <= 0)
            {
                page = 1;
            }
            int roleid_temp = Int32.Parse(Session["uroleid"].ToString());
            int facultyid_temp = Int32.Parse(Session["ufacultyid"].ToString());
            if (roleid_temp == 1 || roleid_temp == 5)
            {
                lst = PostBLL.Post_GetAllPost_Guest(page,pageSize);
            }
            else
            {               
                if (mode == "all")
                {

                    lst = PostBLL.Post_GetAllPost(page, pageSize, Int32.Parse(Session["ufacultyid"].ToString()));
                }
                if (mode == "popular")
                {
                    lst = PostBLL.Post_GetTopPopularPost(facultyid_temp);
                }
                if (mode == "topview")
                {
                    lst = PostBLL.Post_GetTopViewPost(facultyid_temp);
                }
            }
            return View(lst);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MostView()
        {
            int facultyid_temp = Int32.Parse(Session["ufacultyid"].ToString());
            List<StructurePostToRender> lst = new List<StructurePostToRender>();
            lst = PostBLL.Post_GetTopViewPost(facultyid_temp);
            return View(lst);
        }
        public ActionResult Popular()
        {
            int facultyid_temp = Int32.Parse(Session["ufacultyid"].ToString());
            List<StructurePostToRender> lst = new List<StructurePostToRender>();
            lst = PostBLL.Post_GetTopPopularPost(facultyid_temp);
            return View(lst);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult settime(FormCollection cl)
        {
            string dt = cl["opentime"];
            if(SystemBLL.System_UpdateOpenTime(dt))
            Session["opentime"] = dt;
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            var userInfo = SystemBLL.System_Login(userName, password);
            string facultyName = SystemBLL.System_GetFaculty(userInfo.Departmentid);
            if (userInfo == null)
            {
                ModelState.AddModelError("", "Login Failed!");
                return View();
            }
            Session["uid"] = userInfo.userid;
            Session["uname"] = userInfo.username;
            Session["ufacultyid"] = userInfo.Departmentid;
            Session["uroleid"] = userInfo.roleid;
            Session["opentime"] = userInfo.opentime.ToString();
            Session["facultyname"] = facultyName;
            FormsAuthentication.SetAuthCookie("isLogin", false);
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Register(UserAccount acc)
       {
            SystemBLL.System_CreateNewAccount(acc);
            return RedirectToAction("Index", "manager");
        }
    }
}