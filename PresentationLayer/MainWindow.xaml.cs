using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using DTO;
using LiveCharts;
using LiveCharts.Wpf;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private FPWindow findPatientW;
        private EKGController ekgObject;
        /*rivate CMWindow chooseMeassurementW;*/
        public SeriesCollection MyCollection { get; set; }
        private LineSeries EKGLine;
        private EKG ekg;
        private string cpr;
        private string laegehus;
        private string dato;

        private string medarbejdernummer;

        public bool PatientOK { get; set; }

        //public bool SeveralDateTimes { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ekgObject = new EKGController();
            //findPatientW = new FPWindow(this, ekgObject);
            //chooseMeassurementW = new CMWindow();

            MyCollection = new SeriesCollection();
            EKGLine = new LineSeries();
            EKGLine.Values = new ChartValues<double> { };
            EKGLine.Fill = Brushes.Transparent;
            EKGLine.PointGeometry = null;

            MyCollection.Add(EKGLine);

            TBCPR2.Text = cpr;
            TBLaegehus2.Text = laegehus;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = this;

        }

        public string Cpr
        {
            get { return cpr; }
            set
            {
                cpr = value;
                TBCPR2.Text = value;

            }
        }

        public string Medarbejdernummer
        {
            get { return medarbejdernummer; }
            set
            {
                medarbejdernummer = value;
            }
        }

        public string Dato
        {
            get => dato; set
            {
                dato = value;
                TBMaaling.Text = value;
            }
        }
        public string Laegehus
        {
            get { return laegehus; }
            set
            {
                laegehus = value;
                TBLaegehus2.Text = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var findPatientW = new FPWindow(this, ekgObject);
            findPatientW.ShowDialog();

            //den følgende kode er blot for at teste vores loginvindue. Vi skal senere ændre det til, at patienten er blevet fundet i tabellen.


            if (PatientOK == true)
            {
                var chooseMeassurementW = new CMWindow(this, ekgObject);

                chooseMeassurementW.ShowDialog();
                if (string.IsNullOrEmpty(Dato))
                {
                    Close();
                }

            }
            else
            {
                Close();
            }

            ekg = ekgObject.GetEKG(Cpr, Convert.ToDateTime(Dato));

            
            //HUSK AT ÆNDRE DET NÅR VI FÅR RIGTIGE INTERVALLER OG SAMPLERATES

            for (int i = 0; i <ekg.EKGsamples.Count /*Convert.ToInt32(ekg.IntervalSec) * Convert.ToInt32(ekg.SampleRate)*/; i++)
            {
                double sample = ekg.EKGsamples[i];
                EKGLine.Values.Add(sample);
            }

            

            //this.ShowDialog();

            if (ekgObject.AnalyzeEKG(ekg.CPR, ekg.MeasurementTime))
            {
                TBAnalyse.Text = "Atrieflimren er påvist";
            }
            else
            {
                TBAnalyse.Text = "Atrieflimren er ikke påvist";
            }

            Show();
            //chooseMeassurementW.ShowDialog();
            //den følgende kode er blot for at teste vores loginvindue. Vi skal senere ændre det til, at patienten er blevet fundet i tabellen.


            //

            //List<double> EKGsamples = ekgObject.GetEKG(cpr, Convert.ToDateTime(dato));

            //foreach(var maaling in EKGsamples )
            
            //{

            //}



        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            Læge læge = new Læge(Laegehus, Medarbejdernummer);
            ekgObject.SendEKG(ekg,læge);

            MessageBox.Show("Måling er sendt til offentlig database");
        }
    }
}
