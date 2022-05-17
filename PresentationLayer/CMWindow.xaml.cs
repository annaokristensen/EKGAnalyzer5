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
using System.Windows.Shapes;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for CMWindow.xaml
    /// </summary>
    public partial class CMWindow : Window
    {
        private MainWindow mainW;
        private EKGController eKGcontrole;
        private FPWindow findPatientW;
        public bool PatientOK { get; set; }
        public CMWindow()
        {
            InitializeComponent();
            this.eKGcontrole = eKGcontrole;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //når hent button trykkes, åbnes den valgte måling
            this.Hide();
            mainW = new MainWindow();
            mainW.ShowDialog();
            Show();

            
        }
    }
}
