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
    /// Interaction logic for FPWindow.xaml
    /// </summary>
    public partial class FPWindow : Window
    {
        private MainWindow mainW;
        private EKGController eKGcontrole;

        public FPWindow(MainWindow mainW, EKGController eKGcontrole)
        {
            InitializeComponent();

            this.mainW = mainW;
            this.eKGcontrole = eKGcontrole;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void ButtonHent_Click(object sender, RoutedEventArgs e)
        {
            bool patient = eKGcontrole.CPRTyped(TBCPR1.Text);

            if (patient == true)
            {
                mainW.PatientOK = true;
                mainW.Cpr = TBCPR1.Text;
                mainW.Laegehus = TBLaegehus1.Text;
                Close();
            }

        }
    }
}
