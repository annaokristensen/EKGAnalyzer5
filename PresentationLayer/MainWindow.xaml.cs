using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private int signal_length;
        const int TEST_SIGNAL_LENGTH = 140;

        private string medarbejdernummer;

        public bool PatientOK { get; set; }
        public Func<double, string> labelformatter { get; set; }
        public Func<double, string> labelformatter1 { get; set; }
        public LineSeries Maaling { get; set; }

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

            double[] testsignal = new double[TEST_SIGNAL_LENGTH];

            for (int i = 0; i < 20; i++)
            {
                testsignal[i] = 0;
            }
            for (int i = 20; i < 120; i++)
            {
                testsignal[i] = 1;
            }
            for (int i = 120; i < testsignal.Length; i++)
            {
                testsignal[i] = 0;
            }


            double[] specifikMaaling = new double[ekg.IntervalSec * (int)ekg.SampleRate];

            for (int i = 0; i < specifikMaaling.Length; i++)
            {
                specifikMaaling[i] = ekg.EKGsamples[i];
            }

            for (int i = 0; i < testsignal.Length; i++)
            {
                EKGLine.Values.Add(testsignal[i]);
            }

            for (int i = 0; i <specifikMaaling.Length; i++)
            {
               EKGLine.Values.Add(specifikMaaling[i]);
            }

            MyCollection.Add(EKGLine);

            
            //Styr på Analyze ekg


            //if (ekgObject.AnalyzeEKG(ekg.CPR, ekg.MeasurementTime) )
            //{
            //    TBAnalyse.Text = "Atrieflimren er påvist";
            //}
            //else
            //{
            //    TBAnalyse.Text = "Atrieflimren er ikke påvist";
            //}


            //GRID til EKG bliver lavet
            EKGAnalyzer.AxisX[0].MinValue = 0;
            EKGAnalyzer.AxisX[1].MinValue = 0;
            EKGAnalyzer.AxisX[0].MaxValue = ekg.SampleRate;
            EKGAnalyzer.AxisX[1].MaxValue = ekg.SampleRate;

            EKGAnalyzer.AxisX[0].Separator.Step = 0.04 / (1 / ekg.SampleRate);
            EKGAnalyzer.AxisX[1].Separator.Step = 0.2 / (1 / ekg.SampleRate);

            EKGAnalyzer.AxisY[0].MinValue = -1;
            EKGAnalyzer.AxisY[1].MinValue = -1;
            //Skal indstilles til 1.5 mV, eller hvad der cirka passer. Skal justeres og tilpasses senere 
            //når vi har målinger vi kan teste på
            EKGAnalyzer.AxisY[0].MaxValue = 1.5;
            EKGAnalyzer.AxisY[1].MaxValue = 1.5;
            EKGAnalyzer.AxisY[0].Separator.Step = 0.1;
            EKGAnalyzer.AxisY[1].Separator.Step = 0.5;

            Show();

        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            Læge læge = new Læge(Laegehus, Medarbejdernummer);
            ekgObject.SendEKG(ekg,læge);

            MessageBox.Show("Måling er sendt til offentlig database");
        }

        private void Slider_ValueChanged(object sender, DragCompletedEventArgs e)
        {
            EKGAnalyzer.AxisX[0].MinValue = Slider.Value * ekg.SampleRate;

            EKGAnalyzer.AxisX[0].MaxValue = (Slider.Value + 1) * ekg.SampleRate;

            EKGAnalyzer.AxisX[1].MinValue = Slider.Value * ekg.SampleRate;

            EKGAnalyzer.AxisX[1].MaxValue = (Slider.Value + 1) * ekg.SampleRate;
        }
    }
}
