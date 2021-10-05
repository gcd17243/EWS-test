using EWSApplication.DataLayers;
using EWSApplication.DataLayers.Common;
using EWSApplication.Entities.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EWSApplication.BussinessLayers
{
    public class ManagerBLL
    {
        private static ManagerDAL ManagerDAL = new ManagerDAL();
        public static bool Manager_CreateNewTag(string tagName ,string description)
        {
            return ManagerDAL.CreateNewTag(tagName, description);
        }
        public static bool Manager_DeleteTag(int tagID)
        {
            return ManagerDAL.DeleteTag(tagID);
        }
        public static List<string> Manager_GetAllFileToDownload()
        {
            return ManagerDAL.GetAllFileToDownload();
        }
        public static List<Tag> Manager_GetListTag()
        {
            return ManagerDAL.GetListTag();
        }
        public static List<Analysis> Manager_Analysis()
        {
            return ManagerDAL.Analysis();
        }
        public static List<PostWaitingActive> Manager_GetPostWaitingActive(int facultyid)
        {
            return ManagerDAL.GetPostWaitingActive(facultyid);
        }
        public static bool Manager_ActivePost(int postid)
        {
            return ManagerDAL.ActivePost(postid);
        }
    }
}
