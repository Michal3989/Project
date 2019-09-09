using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
   public class EventDto
    {
        public int Id { get; set; }
        public int IdEventOwner { get; set; }
        public byte EventTypeCode { get; set; }
        public System.DateTime Date { get; set; }
        public string Picture { get; set; }
        public string FreeText { get; set; }
        public string Name { get; set; }
}

}

