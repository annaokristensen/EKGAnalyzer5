using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Patient
    {
        public string CPRNumber { get; set; }
        public string Name { get; set; }

        public Patient(string cpr, string name)
        {
            CPRNumber = cpr;
            Name = name;
        }
    }
}
