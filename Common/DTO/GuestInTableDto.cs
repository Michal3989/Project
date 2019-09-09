using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class TablesWithGuestsDto
    {
        public int TableId { get; set; }
        public string Title { get; set; }
        public bool Male { get; set; }
        public List<string> GuestsFullName { get; set;}
    }
}
