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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FPWindow findPatientW;
        private EKGController ekgObject;
        private CMWindow chooseMeassurementW;
        private string cpr;
        private string laegehus;
        public bool PatientOK { get; set; }

        //public bool SeveralDateTimes { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ekgObject = new EKGController();
            findPatientW = new FPWindow(this, ekgObject);
            chooseMeassurementW = new CMWindow();

            TBCPR2.Text = cpr;
            TBLaegehus2.Text = laegehus;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public string Cpr
        {
            get { return cpr; }
            set {
                cpr = value;
                TBCPR2.Text= value;

            }
        }

        public string Laegehus
        {
            get { return laegehus; }
            set { laegehus = value;
                TBLaegehus2.Text = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            findPatientW.ShowDialog();
            //den følgende kode er blot for at teste vores loginvindue. Vi skal senere ændre det til, at patienten er blevet fundet i tabellen.

            //if (SeveralDateTimes == true)
            {
                if (PatientOK == true)
                {
                    
                    chooseMeassurementW.ShowDialog();
                }
                else
                {
                    
                    chooseMeassurementW.Close();
                }
            }
            //else
            //{
            //    if(PatientOK == true)
            //    {
            //        this.Show();

            //    }

            //    else 
            //    { 
            //        Close(); 
                
            //    }
            //}

            
           
        }
    }
}
