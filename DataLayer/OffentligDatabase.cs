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
                var con = new SqlConnection($@"Data Source=LAPTOP-S4DIHSKB\SQLEXPRESS;Initial Catalog=offentlig;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                con.Open();
                return con;
            }
        }
        public void SendToDatabase(EKG ekg, Læge læge)
        {
            string insertStringParam2 =
                @"INSERT INTO EKGMAELING (dato, antalmaalinger, sfp_ansvrmedarbjnr, sfp_ans_org, borger_cprnr) OUTPUT INSERTED.ekgmaaleid VALUES(@dato, 1, @Id, @org, @cpr)";
            DateTime dato = ekg.MeasurementTime;
            string cpr = ekg.CPR;
            string org = læge.Organisation;
            string Id = læge.ID;

            using (SqlCommand cmd = new SqlCommand(insertStringParam2, OpenConnectionST))
            {
                cmd.Parameters.AddWithValue("@dato", dato);
                cmd.Parameters.AddWithValue("@org", org);
                cmd.Parameters.AddWithValue("@cpr", cpr);
                cmd.Parameters.AddWithValue("@Id", Id);
                long id = (long)cmd.ExecuteScalar();
            }


            List<double> data = ekg.EKGsamples;
            DateTime starttid = ekg.MeasurementTime;
            float sampleRate = (float)ekg.SampleRate;
            int intervalSec = ekg.IntervalSec;
            string dataFormat = ekg.DataFormat;
            string binEllerTekst = ekg.BinEllerTekst;
            string maaleformatType = ekg.Maaleformat_type;

            string insertStringParam = @"INSERT INTO dbo.EKGDATA (raa_data, samplerate_hz, interval_sec, data_format, bin_eller_tekst, maaleformat_type, start_tid) OUTPUT INSERTED.ekgdataid  VALUES(@data, @sampleRate, @intervalSec, @dataformat, @binEllerTekst, @maalformatType, @starttid)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnectionST))
            {
                cmd.Parameters.AddWithValue("@starttid", starttid);
                cmd.Parameters.AddWithValue("@sampleRate", sampleRate);
                cmd.Parameters.AddWithValue("@intervalSec", intervalSec);
                cmd.Parameters.AddWithValue("@dataformat", dataFormat);
                cmd.Parameters.AddWithValue("@binEllerTekst", binEllerTekst);
                cmd.Parameters.AddWithValue("@maalformatType", maaleformatType);

                cmd.Parameters.AddWithValue("@data", data.ToArray().SelectMany(value => BitConverter.GetBytes(value)).ToArray());
                int id1 = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record 64 bit/8 bytes
                //HVad gør det her?
            }

        }

    }
}
