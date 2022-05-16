using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO;

namespace DataLayer
{
    public class OffentligDatabase
    {
        private SqlConnection connection;
        private SqlCommand command;
        private const string db = "EKG_Offentlig";

        public OffentligDatabase()
        {
           
        }

        private SqlConnection OpenConnectionST
        {
            get
            {
                var con = new SqlConnection(@"Data Source=BBLAP18\SQLEXPRESS;Initial Catalog=EKG_Offentlig;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                con.Open();
                return con;
            }
        }
        public void SendToDatabase(EKG ekg)
        {

            string insertStringParam = @"INSERT INTO EKGDATA (raa_data, ekgmaaleid, samplerate_hz, interval_sec, data_format, bin_eller_tekst, maaleformat_type, start_tid) OUTPUT INSERTED.ekgdataid VALUES(@data, 164, 400, 3600, N'2015-04-27', '1', N'double', CONVERT(DATETIME, @starttid, 102))"; //Datetime, 102???
            
            List<double> data = ekg.EKGsamples;
            DateTime starttid = ekg.MeasurementTime;


            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnectionST))
            {
                cmd.Parameters.AddWithValue("@starttid", starttid); //ER DET HER RIGTIGT??!!
                // Get your parameters ready
                cmd.Parameters.AddWithValue("@data", data.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
                long id = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record 64 bit/8 bytes
            }
        }

    }
}
