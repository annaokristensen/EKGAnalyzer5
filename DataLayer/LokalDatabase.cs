using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;

namespace DataLayer
{
    public class LokalDatabase
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private const string db = "EKG_Lokal";
        public LokalDatabase()
        {
            
        }
        private SqlConnection OpenConnectionST
        {
            get
            {
                var con = new SqlConnection($@"Data Source=BBLAP18\SQLEXPRESS;Initial Catalog=EKG_Lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                con.Open(); 
                return con;
            }
        }
        public EKG GetEKG(string CPR,DateTime dt)
        {
            EKG ekg = new EKG();

            //CPR
            ekg.CPR = CPR;
            ekg.MeasurementTime = dt;


            //BLOB laves til liste
            byte[] bytesArr = new byte[800];
            List<double> list = new List<double>();
            SqlDataReader rdr;
            string selectString = "Select * from dbo.EKGLokal where cpr_borger = " + CPR + " AND start_tid = '1 Jan 1900'"; //datetime er ikke rigtigt
            using (SqlCommand cmd = new SqlCommand(selectString, OpenConnectionST))
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    bytesArr = (byte[])rdr["raa_data"];
                }
                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                    list.Add(BitConverter.ToDouble(bytesArr, i));
            }

            ekg.EKGsamples = list;

            //Resten af informationerne udtrækkes
            using (SqlCommand cmd = new SqlCommand(selectString, OpenConnectionST))
            {
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ekg.BinEllerTekst = Convert.ToString(rdr["bin_eller_tekst"].ToString());
                    ekg.IntervalSec = Convert.ToInt32(rdr["interval_sec"]);
                    ekg.SampleRate = Convert.ToDouble(rdr["samplerate_hz"]);
                    ekg.DataFormat = Convert.ToString(rdr["data_format"].ToString());
                    ekg.Maaleformat_type = Convert.ToString(rdr["maaleformat_type"].ToString());
                    ekg.MeasurementTime = Convert.ToDateTime((DateTime)rdr["start_tid"]);
                }

            }
            return ekg;
        }

        public List<DateTime> GetDateTimes(string cpr)
        {
            return new List<DateTime>();
        }
        public bool isUserRegistered(string cpr)
        {

            //SqlConnection er typen til variablen conn. Er fundet ved at skrive var conn, inden. 
            SqlConnection con = connection; //tager fat i connection under proppertyen, så vi holder fast i den samme connection. Vi sender den connection ind til vores Sqlcommand.
            command = new SqlCommand($"Select * from EKGLokal Where cpr_borger = '{cpr}' ", con); //Her ligger min forespørgelse. $ er en template streng, der gør at vi kan skrive midt i strengen.
            bool isUserFound = false; // bool, fordi det er retur værdien i en metoden ovenfor. 
            
            reader = command.ExecuteReader();
            if (reader.Read()) { //Read metoden returnere selv om den er true eller false. 
                isUserFound = true;
            }

            con.Close();
            return isUserFound;

            //bool result = false;

            //if (cpr == "123456789") {
            //    result = true;
            //}

            //return result;
        }

        //select Dato 
    }
}
