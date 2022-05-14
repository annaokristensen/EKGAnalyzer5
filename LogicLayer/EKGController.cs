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
        //private LokalDatabase lokalDatabase;
        private Algoritme algoritme;
        private LokalDatabase EKGresult;

        public EKGController()
        {
            //lokalDatabase = new LokalDatabase();
            algoritme = new Algoritme();
            EKGresult = new LokalDatabase();
            
        }

        //public void CPRTyped(int CPRNumber)
        //{
        //    bool EKGresult = algoritme.Analyze(lokalDatabase.GetEKG());

        //}

        public bool CPRTyped(string cpr)
        {
            return EKGresult.isUserRegistered(cpr);
        }

    }
}
