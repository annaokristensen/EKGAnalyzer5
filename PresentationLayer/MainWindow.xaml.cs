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
            EKGLine.LineSmoothness = 0;
            EKGLine.PointGeometry = null;

            //MainWindow skjules
            this.Hide();
            findPatientW = new FPWindow(this, ekgObject);
            findPatientW.ShowDialog();

            if (PatientOK == true)
                //hvis CPR-nummeret er fundet i den lokale database, åbner chooseMeassurementvinduet op.
            {
                chooseMeassurementW = new CMWindow(this, ekgObject);
                chooseMeassurementW.ShowDialog(); //CMWindow vises.
                if (string.IsNullOrEmpty(Dato))
                {
                    Close();
                }
            }
           
            ekg = ekgObject.GetEKG(Cpr, Convert.ToDateTime(Dato)); //det valgte EKG i CMWindow og udfra CPR hentes.

            labelformatter = x => (x / ekg.SampleRate + Slider.Value).ToString();
            labelformatter1 = x => x.ToString("F1");
            //akserne på LiveChart defineres. 

            double[] testsignal = new double[TEST_SIGNAL_LENGTH]; //Test signalet, som skal vises på EKG dannes.

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


            double[] specifikMaaling = new double[ekg.EKGsamples.Count];
            
            for (int i = 0; i < specifikMaaling.Length; i++)
            {
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

            //Test signalet og den specifikke EKG-måling lægges ind på i grafens værdier. 
            
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            if(dato != null) //Hvis man vælger at lukke programmet i CMWindow eller FPWindow skal MainWindow ikke vises. 
            {
                Show();
            }
        }

        public string Cpr
        {
            get { return cpr; }
            set
            {
                cpr = value;
                TBCPR2.Text = value; //TB skal vise CPR-nummer i mainwindow

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
                TBMaaling.Text = value; //TB skal vise Dato i mainwindow
            }
        }
        public string Laegehus
        {
            get { return laegehus; }
            set
            {
                laegehus = value;
                TBLaegehus2.Text = value; //TB skal vise lægehuset i mainwindow
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TBCPR2.Text = cpr;
            TBLaegehus2.Text = laegehus;


            //EKG-analyze udføres på EKG-objektet. Ud fra udfaldet vises analysesvaret i TB. 
            if (ekgObject.AnalyzeEKG(ekg.CPR, ekg.MeasurementTime))
            {
                TBAnalyse.Text = "Tegn på \n atrieflimren";
            }
            else
            {
                TBAnalyse.Text = "Ikke tegn på \n atrieflimren";
            }

            //GRID til EKG bliver lavet
            EKGAnalyzer.AxisX[0].MinValue = 0;
            EKGAnalyzer.AxisX[1].MinValue = 0;
            EKGAnalyzer.AxisX[0].MaxValue = ekg.SampleRate*5;
            EKGAnalyzer.AxisX[1].MaxValue = ekg.SampleRate*5;

            EKGAnalyzer.AxisX[0].Separator.Step = 0.04 / (1 / ekg.SampleRate);
            EKGAnalyzer.AxisX[1].Separator.Step = 0.2 / (1 / ekg.SampleRate);

            EKGAnalyzer.AxisY[0].MinValue = -1;
            EKGAnalyzer.AxisY[1].MinValue = -1;
            EKGAnalyzer.AxisY[0].MaxValue = 2;
            EKGAnalyzer.AxisY[1].MaxValue = 2;
            EKGAnalyzer.AxisY[0].Separator.Step = 0.1;
            EKGAnalyzer.AxisY[1].Separator.Step = 0.5;
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            //DTO-objekt for lægen oprettes.
            Læge læge = new Læge(Laegehus, Medarbejdernummer);

            //EKG sendes til offentlig database gennem logik laget
            ekgObject.SendEKG(ekg,læge);

            //En tekstbox vises, at målingen er sendt korrekt
            MessageBox.Show("Måling er sendt til offentlig database");
        }

        private void Slider_ValueChanged(object sender, DragCompletedEventArgs e)
        {
            //Udfra sliderens værdier ændres værdierne på x-aksen på LiveChart
            EKGAnalyzer.AxisX[0].MinValue = Slider.Value * ekg.SampleRate;

            EKGAnalyzer.AxisX[0].MaxValue = (Slider.Value+2) * ekg.SampleRate;

            EKGAnalyzer.AxisX[1].MinValue = Slider.Value * ekg.SampleRate;

            EKGAnalyzer.AxisX[1].MaxValue = (Slider.Value+2) * ekg.SampleRate;
        }

       
    }
}
