using EWSApplication.DataLayers.Common;
using EWSApplication.Entities.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWSApplication.DataLayers
{
    public class ManagerDAL
    {
        EWSDbContext db = new EWSDbContext();
        #region Tags
        /// <summary>
        /// Tao Tag moi
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public bool CreateNewTag(string tagName,string description)
        {
            try
            {
                var tag = new Tag()
                {
                    tagname = tagName,
                    description = description
                };
                db.Tags.Add(tag);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Xoa tag
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public bool DeleteTag(int tagID)
        {
            try
            {
                var tag = db.Tags.Where(x => x.tagid == tagID).SingleOrDefault();
                db.Tags.Remove(tag);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Tag> GetListTag()
        {
            List<Tag> data = new List<Tag>();
            data = db.Tags.ToList();
            return data;
        }
        #endregion
        #region download tệp đính kèm
        public List<string> GetAllFileToDownload()
        {
            List<string> lstFiles = new List<string>();
            var listPath = (from s in db.Posts
                           where s.filePath != ""
                           select s.filePath).ToList();
            foreach (string strfile in listPath)
            {
                string temp = Path.GetFileName(strfile);
                lstFiles.Add(temp);
            }
            return lstFiles;
        }
        
        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "Text Document";
                case ".jpg":
                case ".png":
                    return "Image";
                default:
                    return "Unknown";
            }
        }
        #endregion
        #region phân tích thống kê

        #endregion
        #region analysis and statistics
        public List<Analysis> Analysis()
        {
            SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\EWS.mdf;");
            SqlCommand command = new SqlCommand();
            command.CommandText = "select f.facultyname, count(*) as amount from Post as p inner join UserAccount as u on p.userid = u.userid inner join Faculty as f on u.facultyid = f.facultyid group by f.facultyname";
            command.CommandType = CommandType.Text;
            command.Connection = connect;
            connect.Open(); // mở kết nối
            SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Analysis> data = new List<Analysis>();
            while (read.Read())
            {
                data.Add(new Analysis
                {
                    facultyname = Convert.ToString(read["facultyname"]),
                    amount = Convert.ToInt32(read["title"]),                 
                });
            }
            return data;
        }
        #endregion
        public List<PostWaitingActive> GetPostWaitingActive(int facultyid)
        {
            SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\EWS.mdf;");
            SqlCommand command = new SqlCommand();
            command.CommandText = "select * from (select p.*,u.username,u.facultyid from Post as p INNER JOIN UserAccount as u on u.userid = p.userid) as t where isActive = 0 and t.facultyid = @facultyid";
            command.CommandType = CommandType.Text;
            command.Connection = connect;
            connect.Open(); // mở kết nối
            command.Parameters.AddWithValue("@facultyid", facultyid);
            SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<PostWaitingActive> data = new List<PostWaitingActive>();
            while (read.Read())
            {
                data.Add(new PostWaitingActive
                {
                    postid = Convert.ToInt32(read["postid"]),
                    username = Convert.ToString(read["username"]),
                    title = Convert.ToString(read["title"])
                });
            }
            return data;
        }
        public bool ActivePost(int postid)
        {
            try
            {
                var pst = db.Posts.Where(x => x.postid == postid).SingleOrDefault();
                pst.isActive = true;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
