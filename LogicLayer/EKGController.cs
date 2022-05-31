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
        private Algoritme algoritme;
        private LokalDatabase EKGresult;
        private OffentligDatabase offentligDatabase;
        public EKGController()
        {
            algoritme = new Algoritme();
            EKGresult = new LokalDatabase();
            offentligDatabase = new OffentligDatabase();
        }
        public bool CPRTyped(string cpr)
            //returnerer om det indtastede CPR-nummer er true eller false
        {
            return EKGresult.isUserRegistered(cpr);
            
        }

        public List<DateTime> getListDateTime(string cpr)
            //returner en liste af datetimes
        {
            return EKGresult.GetDateTimes(cpr);
        }

        public EKG GetEKG(string cpr, DateTime dt)
            //returner måledata for et bestemt CPR-nummer og dato
        {
            EKG ekg = EKGresult.GetEKG(cpr, dt);

            return ekg;
        }

        public bool AnalyzeEKG(string cpr, DateTime dt)
            //retunerer algoritmens analyse
        {
            EKG ekg = EKGresult.GetEKG(cpr, dt);

            return algoritme.Analyze(ekg);
        }

        public void SendEKG(EKG ekg, Læge læge)
            //Ud fra de to DTO-klasser der er parametere, sendes målingen til datalaget, hvor den sendes til den offentlige database.
        {
            offentligDatabase.SendToDatabase(ekg,læge);

        }
    }
}
