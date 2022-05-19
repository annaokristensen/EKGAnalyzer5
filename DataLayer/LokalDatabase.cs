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
