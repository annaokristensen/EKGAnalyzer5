using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DTO;

namespace LogicLayer
{
    public class EKGController
    {
        private LokalDatabase lokalDatabase;
        private Algoritme algoritme;

        public EKGController()
        {
            lokalDatabase = new LokalDatabase();
            algoritme = new Algoritme();
        }

        public void CPRTyped(int CPRNumber)
        {
            bool EKGresult = algoritme.Analyze(lokalDatabase.GetEKG());

        }

    }
}
