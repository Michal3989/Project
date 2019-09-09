using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
 public class GuestDto
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public string DegreeBefore { get; set; }
        public string DegreeAfter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte CategoryCode { get; set; }
    }
}
