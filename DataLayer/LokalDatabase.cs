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
            return new EKG(new List<double>(), 12, "1111111", DateTime.Now);
        }

        public bool isUserRegistered(string cpr)
        {
            bool result = false;

            if(cpr == "123456789")
            {
                result = true;
            }

            return result;
        }
    }
}
