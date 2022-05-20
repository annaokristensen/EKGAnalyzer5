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
        private OffentligDatabase offentligDatabase;

        public EKGController()
        {
            //lokalDatabase = new LokalDatabase();
            algoritme = new Algoritme();
            EKGresult = new LokalDatabase();
            offentligDatabase = new OffentligDatabase();

        }

        //public void CPRTyped(int CPRNumber)
        //{
        //    bool EKGresult = algoritme.Analyze(lokalDatabase.GetEKG());

        //}

        public List<DateTime> CPRTyped(string cpr)
        {
            if (EKGresult.isUserRegistered(cpr))
            {
                return EKGresult.GetDateTimes(cpr);
            }
            else
            {
                return null;
            }
        }

        public EKG GetEKG(string cpr, DateTime dt)
        {
            EKG ekg = EKGresult.GetEKG(cpr, dt);

            return ekg;
        }

        public bool AnalyzeEKG(string cpr, DateTime dt)
        {
            EKG ekg = EKGresult.GetEKG(cpr, dt);

            return algoritme.Analyze(ekg);
        }

        public void SendEKG(EKG ekg, Læge læge)
        {
            offentligDatabase.SendToDatabase(ekg,læge);

        }


    }
}
