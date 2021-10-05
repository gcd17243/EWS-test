using EWSApplication.BussinessLayers;
using EWSApplication.DataLayers.Common;
using EWSApplication.Entities.DBContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWSApplication.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            List<StructureAccountToRender> obj = SystemBLL.System_GetListInfoAccount();
            return View(obj);
        }
        
        public ActionResult Tag()
        {
            List<Tag> obj = ManagerBLL.Manager_GetListTag();
            return View(obj);
        }
        public ActionResult DeleteTag(int id)
        {
            ManagerBLL.Manager_DeleteTag(id);
            return RedirectToAction("Tag", "Manager");
        }
        [HttpPost]
        public ActionResult CreateTag(string tagName,string description)
        {
            ManagerBLL.Manager_CreateNewTag(tagName, description);
            return RedirectToAction("Tag", "Manager");
        }
        public ActionResult DeleteAccount(int id)
        {
            SystemBLL.System_DeteleNewAccount(id);
            return RedirectToAction("Index", "Manager");
        }
        public ActionResult Statistics()
        {
            //List<Tag> obj = ManagerBLL.Manager_GetListTag();
            return View(/*obj*/);
        }
        [HttpGet]
        public ActionResult Review(int facultyid)
        {
            List<PostWaitingActive> obj = ManagerBLL.Manager_GetPostWaitingActive(facultyid);
            return View(obj);
        }
        [HttpGet]
        public ActionResult Accept(int postid)
        {
            ManagerBLL.Manager_ActivePost(postid);
            return RedirectToAction("Review", new { facultyid = Convert.ToInt32(Session["ufacultyid"]) });
        }

        public ActionResult Download()
        {
            List<string> data = ManagerBLL.Manager_GetAllFileToDownload();
            return View(data);
        }
        public FileResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/Files");
            string fullPath = Path.Combine(path, fileName);
            return File(fullPath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult Analysis()
        {
            List<Analysis> lst = ManagerBLL.Manager_Analysis();
            return View(lst);
        }

    }
}