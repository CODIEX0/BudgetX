using System.Windows;

namespace ST10092081POEBudgetApp
{
    public partial class App : Application
    {
        
        private void Appication_Startup(object sender, StartupEventArgs e)
        {

            SignInPage signIn = new SignInPage();
            signIn.Show();
        }
    }
}
