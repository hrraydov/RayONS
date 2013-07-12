using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public class MembershipDAL
    {

        public static int getFailedAttempts(Guid UserId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Memberships.Where(m => m.UserId == UserId).Select(m => m.FailedPasswordAttemptCount).First();
        }
    }
}
