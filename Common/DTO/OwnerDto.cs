using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAuthorized { get; set; }
        public string ErrorMessage { get; set; }
    }
}
