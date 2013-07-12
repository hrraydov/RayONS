using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public class UsersDAL
    {
        /// <summary>
        /// Gets the user's id.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Guid getUserId(string username)
        {
            RayONSEntities db = new RayONSEntities();
            return (Guid)db.Users.Where(u => u.UserName == username).Select(u => u.UserId).First();
        }
        /// <summary>
        /// Gets the name of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string getUserName(Guid userId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Users.Where(u => u.UserId == userId).Select(u => u.UserName).First();
        }
    }
}
