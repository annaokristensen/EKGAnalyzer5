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
            LokalDatabase lokalDatabase = new LokalDatabase();

            EKG ekg1 = lokalDatabase.GetEKG("1234567890", new DateTime(2021, 12, 1));

            Console.WriteLine(ekg1.CPR);
            Console.WriteLine(ekg1.IntervalSec);



            List<double> liste = new List<double>();
            liste.Add(1);
            liste.Add(1);
            liste.Add(1); 
            liste.Add(1); 
            liste.Add(1);
            EKG ekg = new EKG(liste, 500, 10, "data", "b", "test", "1234567890", new DateTime(2000, 12, 12));

            Læge læge = new Læge("Lægerne", "1234");

            OffentligDatabase offentligDatabase = new OffentligDatabase();

            offentligDatabase.SendToDatabase(ekg, læge);

            Console.ReadLine();

        }
    }
}