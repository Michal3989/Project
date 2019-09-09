using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
  public  class TableDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public byte NumOfPeople { get; set; }
        public bool Male { get; set; }
        public string Title { get; set; }
        public Nullable<byte> CategoryCode { get; set; }
        public int Amount { get; set; }
    }
}
