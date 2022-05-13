using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataLayer
{
    public class LokalDatabase
    {
        public EKG GetEKG()
        {
            return new EKG(new List<double>(), 12, new Patient("1111111", "Ida"), DateTime.Now);
        }
    }
}
