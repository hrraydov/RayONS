using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public static class TrainerQueryDAL
    {
        /// <summary>
        /// Gets all queries for trainer.
        /// </summary>
        /// <returns></returns>
        public static string[] getAllQueries()
        {
            RayONSEntities db = new RayONSEntities();
            Guid[] ids = db.TrainerQueries.Select(s => s.UserId).ToArray();
            string[] whys=db.TrainerQueries.Select(s=>s.Why).ToArray();
            string[] userNames = new string[2];
            foreach (var item in ids)
            {
                userNames[0]=UsersDAL.getUserName(item);
            }
            foreach (var item in whys) {
                userNames[1] = item;
            }
            return userNames;
        }
        /// <summary>
        /// Checks if query exists.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool queryExist(string userName)
        {
            RayONSEntities db = new RayONSEntities();
            Guid userId = UsersDAL.getUserId(userName);
            if (db.TrainerQueries.Where(s => s.UserId == userId).Count() > 0) return true;
            else return false;
        }
    }
}
