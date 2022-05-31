using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace DataLayer
{
    public class LokalDatabase
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private const string db = "EKG_Lokal";
        private List<DateTime> listDT;
        private List<int> listid;

        public LokalDatabase()
        {
            listDT = new List<DateTime>();
            listid = new List<int>();
        }
        private SqlConnection OpenConnectionST
            //oprettes forbindelsen til databasen
        {
            get
            {
                //var con = new SqlConnection($@"Data Source=BBLAP18\SQLEXPRESS;Initial Catalog=EKG_Lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                SqlConnection con = new SqlConnection($@"Data Source=172.20.10.4\SQLEXPRESS;Initial Catalog=testprojekt;User ID =Login; Password=1234;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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

           
            int id=0;

            for (int i = 0; i < listDT.Count; i++)
            {
                if (listDT[i].ToString() == dt.ToString())
                {
                    id = listid[i];
                    break;
                }
            }

            //string selectString = "Select * from dbo.EKGLokal where cpr_borger = '" + CPR + "' AND start_tid = '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'"; //datetime er ikke rigtigt
            //string selectString = "Select * from dbo.EKGLokal where cpr_borger = '" + CPR + "' AND start_tid = '20 May 2022'"; //datetime er ikke rigtigt

            string selectString = "Select * from dbo.EKGLokal where ekgid = "+ id; 

            using (SqlCommand cmd = new SqlCommand(selectString, OpenConnectionST))
            {
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    bytesArr = (byte[])rdr["raa_data"];
                }
                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                    list.Add(BitConverter.ToDouble(bytesArr, i));
                //konverter måledata fra byte til double
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
//trækker dato ud af den lokale database og gemmer dem i en liste
        {
            SqlDataReader rdr;
            string selectString = "Select * from EKGLokal Where cpr_borger = '" + cpr +"'";
            //string selectString = "Select * from EKGLokal Where ekgid = " + 15;
            using (SqlCommand cmd = new SqlCommand(selectString, OpenConnectionST))
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listDT.Add(Convert.ToDateTime((DateTime)rdr["start_tid"]));
                    listid.Add((int)rdr["ekgid"]);
                }

            }
            return listDT;
        }
        public bool isUserRegistered(string cpr)
            //checker om der forligger data for det indtastede CPR-nummer
        {
            SqlDataReader rdr;
          
     
            string selectString = "Select * from EKGLokal Where cpr_borger = '" + cpr+"'"; //Her ligger forespørgelsen i databasen.
            //string selectString = "Select * from EKGLokal Where ekgid = " + 15;
            bool isUserFound = false; 

            using (SqlCommand cmd = new SqlCommand(selectString, OpenConnectionST))
            {
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {  
                    isUserFound = true;
                }
            }
            
            return isUserFound;


        }
        
    }
}
