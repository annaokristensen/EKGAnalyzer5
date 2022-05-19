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
        public event EventHandler Click;
        public object SelectedItem { get; set; }
        private MainWindow mainW;
        private EKGController eKGcontrole;
        private FPWindow findPatientW;
        
        public bool PatientOK { get; set; }
        public CMWindow()
        {
            InitializeComponent();
            this.eKGcontrole = eKGcontrole;
            this.mainW = mainW;
           
            
            

            
        }

        private void addToListbox()
        {
            //ListBox cm = new ListboxCM();
            //cm.Items.Add("item");
            
        }

        private void ButtonCMHent_Click(object sender, RoutedEventArgs e)
        {

            //mainW.ShowDialog();
            //string item = ListboxCM.SelectedItem.ToString();

            // Tjekker om vi har valgt en måling, før vi lukker dette window.

            // SKal mainWindow mainW have noget props som ændres på baggrund af nogle valg fra denne side.
            // Hvis dette er korrekt, så kalder vi Close();
            // Nu åbnes main window igen.



            //når hent button trykkes, åbnes den valgte måling
            //this.Hide();
            //mainW = new MainWindow();
            //mainW.ShowDialog();
            Close();
        }

        private void CMWindow_Loaded(object sender, RoutedEventArgs e)
        {

            ListboxCM.Items.Add("item1");
            ListboxCM.Items.Add("item2");
            
        }
    }
}
