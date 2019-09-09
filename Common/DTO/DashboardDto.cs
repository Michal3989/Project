using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
   public class DashboardDto
    {
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string EventType { get; set; }
        public string CelebratorsName { get; set; }
        public int NumDaysForEvent { get; set; }
        //public StatusApprovalsDto StatusApprovals { get; set; }


    }
}
