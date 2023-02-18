using System.Windows;
using ST10092081POEBudgetApp.MVM.View;
using ST10092081POEBudgetApp;
namespace ST10092081POEBudgetApp
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Shutdown The current application
            Application.Current.Shutdown();
            
        }
    }
}
