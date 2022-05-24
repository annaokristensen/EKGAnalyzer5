using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataLayer;

namespace TestPRogram
{
    class Program
    {
        static void Main(string[] args)
        {

            List<double> liste = new List<double>();
            liste.Add(1);
            liste.Add(1);
            liste.Add(1); 
            liste.Add(1); 
            liste.Add(1);

            LokalDatabase lokalDatabase = new LokalDatabase();

            

           
            Læge læge = new Læge("Lægerne", "1234");

            OffentligDatabase offentligDatabase = new OffentligDatabase();

            //offentligDatabase.SendToDatabase(ekg1, læge);

            List<double> list = new List<double>();
            for (int i = 0; i < 5000; i++)
            {
                list.Add((i * 0.01) % 1);
            }

            EKG ekg = new EKG(list,500,10,"data","b","test","1234567890",new DateTime(2000,12,12));
            ekg.SampleRate = 500;
            ekg.EKGsamples = list;
            ekg.IntervalSec = 10;

            
            offentligDatabase.SendToDatabase(ekg,læge);
        }
    }
}