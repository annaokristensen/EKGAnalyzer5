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
        private string cpr;
        public bool PatientOK { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ekgObject = new EKGController();
            findPatientW = new FPWindow(this, ekgObject);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public string CPRNumber
        {
            get { return cpr; }
            set { cpr = value;
                TBCPR.Text = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            findPatientW.ShowDialog();
            //den følgende kode er blot for at teste vores loginvindue. Vi skal senere ændre det til, at patienten er blevet fundet i tabellen.
            if (PatientOK == true)
            {
                this.Show();
            }
            else
            {
                Close();
            }
        }
    }
}
