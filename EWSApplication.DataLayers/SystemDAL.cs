using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWSApplication.DataLayers.Common;
using EWSApplication.Entities;
using EWSApplication.Entities.DBContext;

namespace EWSApplication.DataLayers
{
    public class SystemDAL
    {
        EWSDbContext db = new EWSDbContext();
        public UserAccount Login(string userName, string password)
        {
            UserAccount data = null;
            data = db.UserAccounts.Where(x => (x.username == userName && x.password == password) ).FirstOrDefault<UserAccount>();
            return data;
        }
        public bool CreateNewAccount(UserAccount acc)
        {
            try
            {
                UserAccount newacc = new UserAccount()
                {
                   email = acc.email,
                   password  = acc.password,
                   username = acc.username,
                   roleid = acc.roleid,
                   Departmentid = acc.Departmentid,
                   opentime = DateTime.Now
                };
                db.UserAccounts.Add(newacc);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeteleNewAccount(int userid)
        {
            try
            {
                UserAccount acc= db.UserAccounts.Where(x => x.userid == userid).SingleOrDefault();
                db.UserAccounts.Remove(acc);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<StructureAccountToRender> GetListInfoAccount()
        {
            List<Comment> cmt = new List<Comment>();
            //cmt = db.Co mments.Where(x => x.postid == postId).ToList();
            var list =  (from r in db.Roles
                        join u in db.UserAccounts on r.roleid equals u.roleid
                        join f in db.Department on u.Departmentid equals f.Departmentid
                         select new
                        {
                            u.userid,
                            u.username,
                            u.email,
                            r.rolename,
                            f.Departmentname
                        }).ToList();
            List<StructureAccountToRender> data = new List<StructureAccountToRender>();
            foreach (var c in list)
            {
                data.Add(new StructureAccountToRender
                {
                    userid = c.userid,
                    username = c.username,
                    email = c.email,
                    rolename = c.rolename,
                    Departmentname = c.Departmentname
                });
            }
            return data;

        }
        public bool UpdateOpenTime(string opentime)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(opentime);
                var accs = db.UserAccounts.ToList();
                accs.ForEach(acc => acc.opentime = dt);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetDepartment(int id)
        {
            string Departmentname = db.Department.Find(id).Departmentname;
            return Departmentname;
        }

    }
}
