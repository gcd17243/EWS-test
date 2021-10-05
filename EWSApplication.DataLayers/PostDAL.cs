using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EWSApplication.DataLayers.Common;
using EWSApplication.Entities.DBContext;
namespace EWSApplication.DataLayers
{
    public class PostDAL
    {
        EWSDbContext db = new EWSDbContext();
        /// <summary>
        /// Chế đọ xem của Guest
        /// </summary>
        /// <returns></returns>
        public List<StructurePostToRender> GetAllPost_Guest(int page , int pageSize)
        {
            int startPos = (page - 1) * pageSize + 1;
            int endPos = startPos + pageSize - 1;
            SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\EWS.mdf;");
            SqlCommand command = new SqlCommand();
            command.CommandText = "select * from (select p.*,u.username,u.facultyid, ROW_NUMBER() OVER(ORDER BY postid ASC) AS RowNumber from Post as p INNER JOIN UserAccount as u on u.userid = p.userid) as t where (t.RowNumber BETWEEN @StartPos AND @EndPos ) and isActive = 1";
            command.CommandType = CommandType.Text;
            command.Connection = connect;
            connect.Open(); // mở kết nối
            command.Parameters.AddWithValue("@StartPos", startPos);
            command.Parameters.AddWithValue("@EndPos", endPos);
            SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<StructurePostToRender> data = new List<StructurePostToRender>();
            while (read.Read())
            {
                data.Add(new StructurePostToRender
                {
                    postid = Convert.ToInt32(read["postid"]),
                    title = Convert.ToString(read["title"]),
                    anonymous = Convert.ToBoolean(read["anonymous"]),
                    tag = Convert.ToString(read["tag"]),
                    userid = Convert.ToInt32(read["userid"]),
                    content = Convert.ToString(read["content"]),
                    view = Convert.ToInt32(read["view"]),
                    like = Convert.ToInt32(read["like"]),
                    dislike = Convert.ToInt32(read["dislike"]),
                    datetimepost = Convert.ToDateTime(read["datetimepost"]),
                    filePath = Convert.ToString(read["filePath"]),
                    username = Convert.ToString(read["username"])
                });
            }
            return data;
        }
        /// <summary>
        /// lấy danh sách tất cả bài post để hiển thị trên Home
        /// </summary>
        /// <returns></returns>
        public List<StructurePostToRender> GetAllPost(int page , int pageSize,int facultyid)
        {
            #region use LinQ
            //var list = (from p in db.Posts
            //         join u in db.Users
            //         on p.userid equals u.userid
            //         select new
            //         {
            //             postid = p.postid,
            //             title = p.title,
            //             anonymous = p.anonymous,
            //             tag = p.tag,
            //             userid = p.userid,
            //             content = p.content,
            //             view = p.view,
            //             like = p.like,
            //             dislike = p.dislike,
            //             datetimepost = p.datetimepost,
            //             filepath = p.filePath,
            //             username = u.username
            //         }).ToList();
            #endregion
            int startPos = (page - 1) * pageSize + 1;
            int endPos = startPos + pageSize - 1;

            SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\EWS.mdf;");
            SqlCommand command = new SqlCommand();
            command.CommandText = "select * from (select p.*,u.username,u.facultyid, ROW_NUMBER() OVER(ORDER BY postid ASC) AS RowNumber from Post as p INNER JOIN UserAccount as u on u.userid = p.userid) as t where (t.RowNumber BETWEEN @StartPos AND @EndPos ) and isActive = 1 and t.facultyid = @facultyid";
            command.CommandType = CommandType.Text;
            command.Connection = connect;
            connect.Open(); // mở kết nối
            command.Parameters.AddWithValue("@StartPos", startPos);
            command.Parameters.AddWithValue("@EndPos", endPos);
            command.Parameters.AddWithValue("@facultyid", facultyid);
            SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<StructurePostToRender> data = new List<StructurePostToRender>();
            while (read.Read())
            {
                data.Add(new StructurePostToRender
                {
                    postid = Convert.ToInt32(read["postid"]),
                    title = Convert.ToString(read["title"]),
                    anonymous = Convert.ToBoolean(read["anonymous"]),
                    tag = Convert.ToString(read["tag"]),
                    userid = Convert.ToInt32(read["userid"]),
                    content = Convert.ToString(read["content"]),
                    view = Convert.ToInt32(read["view"]),
                    like = Convert.ToInt32(read["like"]),
                    dislike = Convert.ToInt32(read["dislike"]),
                    datetimepost = Convert.ToDateTime(read["datetimepost"]),
                    filePath = Convert.ToString(read["filePath"]),
                    username = Convert.ToString(read["username"])
                });
            }
            return data;
        }
        /// <summary>
        /// Chi tiết bài post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Post GetDetailsPost(int postId)
        {
            Post pst = new Post();
            pst = db.Posts.Where(x => x.postid == postId).SingleOrDefault();
            UpdateViewPost(postId);
            return pst;
        }
        /// <summary>
        /// lấy top 5 bài post phổ biến
        /// </summary>
        /// <returns></returns>
        public List<StructurePostToRender> GetTopPopularPost(int facultyid_temp)
        {
            List<StructurePostToRender> lst = new List<StructurePostToRender>();
            //lst = db.Posts.OrderByDescending(x => x.like).Take(5).ToList();
            var list = (from p in db.Posts
                        join u in db.UserAccounts
                        on p.userid equals u.userid
                        where u.Departmentid == facultyid_temp
                        orderby p.like descending
                        select new
                        {
                            postid = p.postid,
                            title = p.title,
                            anonymous = p.anonymous,
                            tag = p.tag,
                            userid = p.userid,
                            content = p.content,
                            view = p.view,
                            like = p.like,
                            dislike = p.dislike,
                            datetimepost = p.datetimepost,
                            filepath = p.filePath,
                            username = u.username
                        }).Take(5).ToList();
            foreach(var item in list)
            {
                lst.Add(new StructurePostToRender
                {
                    postid = item.postid,
                    title = item.title,
                    anonymous = item.anonymous,
                    tag = item.tag,
                    userid = item.userid,
                    content = item.content,
                    view = item.view,
                    like = item.like,
                    dislike = item.dislike,
                    datetimepost = item.datetimepost,
                    filePath = item.filepath,
                    username = item.username
                });
            }
            return lst;
        }
        /// <summary>
        /// lấy top 5 bài post nhiều view nhất
        /// </summary>
        /// <returns></returns>
        public List<StructurePostToRender> GetTopViewPost(int facultyid_temp)
        {
            List<StructurePostToRender> lst = new List<StructurePostToRender>();
            //lst = db.Posts.OrderByDescending(x => x.like).Take(5).ToList();
            var list = (from p in db.Posts
                        join u in db.UserAccounts
                        on p.userid equals u.userid
                        where u.Departmentid == facultyid_temp
                        orderby p.view descending
                        select new
                        {
                            postid = p.postid,
                            title = p.title,
                            anonymous = p.anonymous,
                            tag = p.tag,
                            userid = p.userid,
                            content = p.content,
                            view = p.view,
                            like = p.like,
                            dislike = p.dislike,
                            datetimepost = p.datetimepost,
                            filepath = p.filePath,
                            username = u.username
                        }).Take(5).ToList();
            foreach (var item in list)
            {
                lst.Add(new StructurePostToRender
                {
                    postid = item.postid,
                    title = item.title,
                    anonymous = item.anonymous,
                    tag = item.tag,
                    userid = item.userid,
                    content = item.content,
                    view = item.view,
                    like = item.like,
                    dislike = item.dislike,
                    datetimepost = item.datetimepost,
                    filePath = item.filepath,
                    username = item.username
                });
            }
            return lst;

        }
        /// <summary>
        /// lấy top bài post lastest
        /// </summary>
        /// <returns></returns>
        public List<Post> GetTopLastPost()
        {
            DateTime date = DateTime.Now;
            // creating object of TimeSpan 
            TimeSpan ts = new TimeSpan(10, 0, 0, 0);

            // getting ShortTime from  
            // subtracting DateTime and TimeSpan 
            // using Subtract() method; 
            DateTime dateFrom = date.Subtract(ts);
            List<Post> lst = new List<Post>();
            lst = db.Posts.Where(t => t.datetimepost > dateFrom && t.datetimepost < DateTime.Now).ToList();
            return lst;
        }

        /// <summary>
        /// Load tất cả comment của bài post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public List<StructureCommentToRender> GetListCommentOfPost(int postId)
        {
            //List<Comment> cmt = new List<Comment>();
            //cmt = db.Comments.Where(x => x.postid == postId).ToList();
            var list = (from c in db.Comments
                        join u in db.UserAccounts
                        on c.userid equals u.userid
                        where c.postid == postId
                        select new
                        {
                            commentid = c.commentid,
                            anonymous = c.anonymous,
                            Date = c.Date,
                            Content = c.Content,
                            postid = c.postid,
                            userid = c.userid,
                            username = u.username,
                            roleid = u.roleid
                        }).ToList();
            List<StructureCommentToRender> data = new List<StructureCommentToRender>();
            foreach (var c in list)
            {
                data.Add(new StructureCommentToRender
                {
                            commentid = c.commentid,
                            anonymous = c.anonymous,
                            Date = c.Date,
                            Content = c.Content,
                            postid = c.postid,
                            userid = c.userid,
                            username = c.username,
                            roleid = c.roleid
                });
            }
            return data;
        }
        /// <summary>
        /// tăng view cho bài post
        /// </summary>
        /// <param name="postId"></param>
        public void UpdateViewPost(int postId)
        {
            var pst = db.Posts.Where(x => x.postid == postId).SingleOrDefault();
            pst.view = pst.view+1;
            db.SaveChanges();
        }
        /// <summary>
        /// Tạo mới bài post
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CreateNewPost(StructurePost data, string filePath)
        {
            try
            {
                Post pst = new Post()
                {
                    //postid = 0,
                    title = data.title,
                    anonymous = data.anonymous=="on",
                    tag = data.tag,
                    userid = Convert.ToInt32(data.userid),
                    content = data.content,
                    view = 0,
                    like = 0,
                    dislike = 0,
                    datetimepost = DateTime.Now,
                    filePath = filePath
                };             
                db.Posts.Add(pst);
                db.SaveChanges();
                Email(data.content, "", "");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool CreateNewComment(StructureComment cmtData)
        {
            try
            {
                Comment cmt = new Comment()
                {
                    anonymous = cmtData.anonymous=="on",
                    Date = DateTime.Now,
                    Content= cmtData.Content,
                    postid = cmtData.postid,
                    userid = cmtData.userid
                };
                db.Comments.Add(cmt);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// gửi email về admin khi có bài post mới vào hệ thống 
        /// </summary>
        /// <param name="htmlString"></param>
        public static void Email(string content , string email , string pass)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("thanhtngcd17282@fpt.edu.vn");//mail he thong
                message.To.Add(new MailAddress("chantran.kam@gmail.com"));//mail admin
                message.Subject = "Test";
                message.IsBodyHtml = false; //to make message body as html (true)
                message.Body = content;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("thanhtngcd17282@fpt.edu.vn", "0543811497");//tk mk cua mail he thong
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
