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

            EKG ekg = lokalDatabase.GetEKG("1234567890", new DateTime(1900, 01, 01));

            //EKG ekg1 = new EKG(liste, 2, 2, "x", "x", "x", "1234567890", new DateTime(2000, 1, 1));

            Console.WriteLine(ekg.CPR);

            Console.WriteLine(ekg.DataFormat);

            Console.WriteLine();

            foreach (var dt in lokalDatabase.GetDateTimes(ekg.CPR))
            {
                Console.WriteLine(Convert.ToString(dt));
            }
            Console.ReadLine();
            //Læge læge = new Læge("Lægerne", "1234");

            //OffentligDatabase offentligDatabase = new OffentligDatabase();

            //offentligDatabase.SendToDatabase(ekg1, læge);


        }
    }
}