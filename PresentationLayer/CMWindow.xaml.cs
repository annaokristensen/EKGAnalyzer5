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
       
        public object SelectedItem { get; set; }
       
        private EKGController eKGcontrole;
       
        private MainWindow mainWindow;
        private EKGController ekgObject;

        public bool PatientOK { get; set; }
      

        public CMWindow(MainWindow mainWindow, EKGController ekgObject)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.ekgObject = ekgObject;
        }

        private void addToListbox()
        {
            //ListBox cm = new ListboxCM();
            //cm.Items.Add("item");
            
        }

        private void ButtonCMHent_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Dato =
            (string)ListboxCM.SelectedValue;

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
            //Vi kalder metoden fra vores logiklag (en list med datoerne for målingerne på patienten). Laver en for løkke, hvor vi adder datorerme. for loop som tilføjer en dato af typen string

            List <DateTime> liste = eKGcontrole.getListDateTime(mainWindow.Cpr);
            
            foreach(DateTime dato in liste)
            {
                ListboxCM.Items.Add(dato);
            }

            //For loop, hvor vi går gennem listen. Konvertere til string og ligger dem ind i list boxen. 
            //ListboxCM.Items.Add("item1");
            //ListboxCM.Items.Add("item2");
            
        }
    }
}
