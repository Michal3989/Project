using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using DAL;

namespace BL
{
    public static class OwnerBL
    {
        public static OwnerDto Login(string email, string password)
        {
            var user = OwnerDAL.Login(email, password);
            return user;
        }


        public static int AddOwner(OwnerDto newOwner)
        {
            return OwnerDAL.AddOwner(newOwner);
        }
    }
}
