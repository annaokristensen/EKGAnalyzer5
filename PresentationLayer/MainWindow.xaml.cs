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
using LiveCharts.Wpf.Charts.Base;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EKGController ekgObject;
        private FPWindow findPatientW;
        private CMWindow chooseMeassurementW;
        public SeriesCollection MyCollection { get; set; }
        private LineSeries EKGLine;
        private EKG ekg;
        private string cpr;
        private string laegehus;
        private string dato;
        const int TEST_SIGNAL_LENGTH = 140;

        private string medarbejdernummer;

        public bool PatientOK { get; set; }
        public Func<double, string> labelformatter { get; set; }
        public Func<double, string> labelformatter1 { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();
            ekgObject = new EKGController();
            MyCollection = new SeriesCollection();
            EKGLine = new LineSeries();
            EKGLine.Fill = System.Windows.Media.Brushes.Transparent;
            EKGLine.PointGeometry = null;

            this.Hide();
            findPatientW = new FPWindow(this, ekgObject);
            findPatientW.ShowDialog();

            //den følgende kode er blot for at teste vores loginvindue. Vi skal senere ændre det til, at patienten er blevet fundet i tabellen.
            if (PatientOK == true)
            {
                chooseMeassurementW = new CMWindow(this, ekgObject);
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

            labelformatter = x => (x / ekg.SampleRate + Slider.Value).ToString();
            labelformatter1 = x => x.ToString("F1");

            double[] testsignal = new double[TEST_SIGNAL_LENGTH];

            for (int i = 0; i < 20; i++)
            {
                testsignal[i] = 1.65;
            }
            for (int i = 20; i < 120; i++)
            {
                testsignal[i] = 2.65;
            }
            for (int i = 120; i < testsignal.Length; i++)
            {
                testsignal[i] = 1.65;
            }


            double[] specifikMaaling = new double[ekg.EKGsamples.Count];

            //double[] specifikMaaling = new double[5000];

            for (int i = 0; i < specifikMaaling.Length; i++)
            {
                //specifikMaaling[i] = (i * 0.01) % 1;

                specifikMaaling[i] = ekg.EKGsamples[i];
            }

            EKGLine.Values = new ChartValues<double> { };
            MyCollection.Add(EKGLine);

            for (int i = 0; i < testsignal.Length; i++)
            {
                EKGLine.Values.Add(testsignal[i]);
            }

            for (int i = 0; i < specifikMaaling.Length; i++)
            {
                EKGLine.Values.Add(specifikMaaling[i]);
            }
            
            DataContext = this;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Show();
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
            TBCPR2.Text = cpr;
            TBLaegehus2.Text = laegehus;


            //Styr på Analyze ekg
            if (ekgObject.AnalyzeEKG(ekg.CPR, ekg.MeasurementTime))
            {
                TBAnalyse.Text = "Atrieflimren er \n påvist";
            }
            else
            {
                TBAnalyse.Text = "Atrieflimren er \n ikke påvist";
            }

            //GRID til EKG bliver lavet
            EKGAnalyzer.AxisX[0].MinValue = 0;
            EKGAnalyzer.AxisX[1].MinValue = 0;
            EKGAnalyzer.AxisX[0].MaxValue = ekg.SampleRate*5;
            EKGAnalyzer.AxisX[1].MaxValue = ekg.SampleRate*5;

            EKGAnalyzer.AxisX[0].Separator.Step = 0.04 / (1 / ekg.SampleRate);
            EKGAnalyzer.AxisX[1].Separator.Step = 0.2 / (1 / ekg.SampleRate);

            EKGAnalyzer.AxisY[0].MinValue = 0;
            EKGAnalyzer.AxisY[1].MinValue = 0;
            //Skal indstilles til 1.5 mV, eller hvad der cirka passer. Skal justeres og tilpasses senere 
            //når vi har målinger vi kan teste på
            EKGAnalyzer.AxisY[0].MaxValue = 3.3;
            EKGAnalyzer.AxisY[1].MaxValue = 3.3;
            EKGAnalyzer.AxisY[0].Separator.Step = 0.1;
            EKGAnalyzer.AxisY[1].Separator.Step = 0.5;
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

            EKGAnalyzer.AxisX[0].MaxValue = (Slider.Value+2) * ekg.SampleRate;

            EKGAnalyzer.AxisX[1].MinValue = Slider.Value * ekg.SampleRate;

            EKGAnalyzer.AxisX[1].MaxValue = (Slider.Value+2) * ekg.SampleRate;
        }

       
    }
}
