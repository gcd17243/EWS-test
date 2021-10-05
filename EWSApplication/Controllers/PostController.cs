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
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int postId)
        {
            //xử lí dữ liệu post
            // dữ liệu post gồm:......    
            TempData["postIdFromDetails"] = postId;
            Post postData = new Post();
            postData= PostBLL.Post_GetDetailsPost(postId);
            ViewBag.uroleid = Session["uroleid"];
            ViewBag.ListComt = PostBLL.Post_GetListCommentOfPost(postId);
            ViewBag.opentime = Session["opentime"];
            return View(postData);
        }

        [HttpPost]
        public ActionResult NewComment(StructureComment cmtData)
        {
            cmtData.userid = Convert.ToInt32(Session["uid"]);
            cmtData.postid = Convert.ToInt32(TempData["postIdFromDetails"]);
            //gồm nội dung comment và id người gửi + id bài post...
            PostBLL.Post_CreateNewComment(cmtData);
            return RedirectToAction("Detail", new { postId = Convert.ToInt32(TempData["postIdFromDetails"]) });
        }

        [HttpPost]
        public ActionResult Create(StructurePost data , ObjFile doc)
        {
            var filePath = "";
            if(Session["uid"] != null)
            {
                data.userid = Session["uid"].ToString();
            }
            foreach (var file in doc.files)
            {

                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    filePath = Path.Combine(Server.MapPath("/Files"),fileName);
                    file.SaveAs(filePath);
                }
                else
                {
                    filePath = "";
                }
            }

            PostBLL.Post_CreateNewPost(data, filePath);
            return RedirectToAction("Index","Home");
        }
    }
}