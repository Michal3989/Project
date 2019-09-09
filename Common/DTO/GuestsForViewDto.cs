using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class GuestsForViewDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string DegreeBefore { get; set; }
        public string DegreeAfter { get; set; }
        public bool StatusApproval { get; set; }
        public int NumOfMale { get; set; }
        public int NumOfFemale { get; set; }





    }
}
