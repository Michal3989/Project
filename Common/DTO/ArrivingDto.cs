using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ArrivingDto
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public bool Male { get; set; }
        public Nullable<int> TableId { get; set; }
    }
}
