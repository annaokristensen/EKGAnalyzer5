using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Læge
    {
        public string Organisation { get; set; }
        public string ID { get; set; }

        public Læge(string organisation, string id)
        {
            Organisation = organisation;
            ID = id;
        }
    }
}
