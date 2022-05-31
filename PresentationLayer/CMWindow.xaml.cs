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

        private EKGController eKGcontrole;
       
        private MainWindow mainWindow;


        public CMWindow(MainWindow mainWindow, EKGController ekgControle)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.eKGcontrole = ekgControle;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        

        private void ButtonCMHent_Click(object sender, RoutedEventArgs e)
            //hvis der er blevet valgt en måling, lukkker vinduet. Hvis ikke, bliver der vist en messagebox.
        {
            if (ListboxCM.SelectedValue == null)
            {
                MessageBox.Show("Vælg hvilken dato målingen skal være fra");
            }
            else
            {
                mainWindow.Dato =
                    Convert.ToString(ListboxCM.SelectedValue);
                Close();
            }
        }

        private void CMWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Vi kalder metoden fra vores logiklag (en list med datoerne for målingerne på patienten). Genererer  en for-loop som tilføjer en dato af typen string

            TBCPRCM.Text = mainWindow.Cpr;

            List <DateTime> liste = eKGcontrole.getListDateTime(mainWindow.Cpr);
            
            foreach(DateTime dato in liste)
            {
                ListboxCM.Items.Add(dato);
            }
            
        }

    }
}
