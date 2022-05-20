using System;
using System.Collections.Generic;
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
        private EKG ekg;
        private string cpr;
        private string laegehus;
        private string dato;

        public bool PatientOK { get; set; }

        //public bool SeveralDateTimes { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ekgObject = new EKGController();
            //findPatientW = new FPWindow(this, ekgObject);
            //chooseMeassurementW = new CMWindow();

            TBCPR2.Text = cpr;
            TBLaegehus2.Text = laegehus;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

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
            //this.ShowDialog();
            Show();
            //chooseMeassurementW.ShowDialog();
            //den følgende kode er blot for at teste vores loginvindue. Vi skal senere ændre det til, at patienten er blevet fundet i tabellen.


            //

            ekg = ekgObject.GetEKG(cpr,Convert.ToDateTime(dato));

            ekgObject.AnalyzeEKG(ekg.CPR, ekg.MeasurementTime);



        }
    }
}
