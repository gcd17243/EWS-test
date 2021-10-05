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
    public class PostBLL
    {
        public static PostDAL pstDAL = new PostDAL();
        public static List<StructurePostToRender> Post_GetAllPost_Guest(int page , int pageSize)
        {
            return pstDAL.GetAllPost_Guest(page, pageSize);
        }
        public static List<StructurePostToRender> Post_GetAllPost(int page, int pageSize , int facultyid)
        {
            return pstDAL.GetAllPost( page,  pageSize, facultyid);
        }
        /// <summary>
        /// Chi tiết bài post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Post Post_GetDetailsPost(int postId)
        {
            return pstDAL.GetDetailsPost(postId);
        }
        /// <summary>
        /// lấy top 5 bài post phổ biến
        /// </summary>
        /// <returns></returns>
        public static List<StructurePostToRender> Post_GetTopPopularPost(int facultyid_temp)
        {
            return pstDAL.GetTopPopularPost(facultyid_temp);
        }
        /// <summary>
        /// lấy top 5 bài post nhiều view nhất
        /// </summary>
        /// <returns></returns>
        public static List<StructurePostToRender> Post_GetTopViewPost(int facultyid_temp)
        {
            return pstDAL.GetTopViewPost(facultyid_temp);
        }
        /// <summary>
        /// lấy top bài post lastest
        /// </summary>
        /// <returns></returns>
        public static List<Post> Post_GetTopLastPost()
        {
            return pstDAL.GetTopLastPost();
        }

        /// <summary>
        /// Load tất cả comment của bài post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static List<StructureCommentToRender> Post_GetListCommentOfPost(int postId)
        {
            return pstDAL.GetListCommentOfPost(postId);
        }
        public static bool Post_CreateNewComment(StructureComment cmtData)
        {
            return pstDAL.CreateNewComment(cmtData);
        }
        /// <summary>
        /// Tạo mới bài post
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Post_CreateNewPost(StructurePost data,string filePath)
        {
            return pstDAL.CreateNewPost(data, filePath);
        }
        
    }
}
