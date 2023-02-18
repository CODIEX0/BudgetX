using System;
using System.Windows;
using System.Windows.Media;
using ST10092081POEBudgetApp.MVM.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace ST10092081POEBudgetApp
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Window
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            bool exception = false;
            string ExceptionString = "";
            int retry = 0;
            //creating a main window object
            MainWindow mainWindow = new MainWindow();

            while(retry == 0)
            {
                if (!string.IsNullOrEmpty(txtStudentNumber.Text) || !string.IsNullOrEmpty(txtNames.Text) || !string.IsNullOrEmpty(txtGrossMonthlyIncome.Text) || !string.IsNullOrEmpty(txtMonthlyTax.Text))
                {
                    //assign values from the sign in window to the 3 labels
                    mainWindow.lblStudentName.Content = txtNames.Text.ToUpper();
                    mainWindow.lblStudentNumber.Content = txtStudentNumber.Text.ToUpper();

                    try
                    {//try to add the value in the list                        

                        if(Expense.getGrossMonthlyIncome() <= 0 || Convert.ToDecimal(txtMonthlyTax.Text) <= 0)
                        {
                            Exception ex = new Exception("Please enter a positve number!");
                            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtMonthlyTax.Text = null;
                            txtGrossMonthlyIncome.Text = null;
                            //throw exception
                            exception = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionString = ex.Message;
                        //throw exception
                        exception = true;                      
                    }

                    if (exception == true)
                    {
                        MessageBox.Show(ExceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        txtStudentNumber.Text = null;
                        txtNames.Text = null;
                        txtMonthlyTax.Text = null;
                        txtGrossMonthlyIncome.Text = null;
                    }
                    else
                    {
                        Expense.setAvailableMonthlyMoney(Expense.getGrossMonthlyIncome());
                        
                        mainWindow.lblGrossMonthlyIncome.Content = (Expense.getGrossMonthlyIncome()).ToString("C", new CultureInfo("en-ZA"));
                        
                        //load the availble monthly imcome amount on to the label content
                        mainWindow.lblAvailableMonthlyIncome.Content = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));
                        mainWindow.lblAvailableMonthlyIncome.UpdateLayout();
                        //alert the user that they signed in successfully
                        MessageBox.Show("Successfully Signed In!", "Signed In", MessageBoxButton.OK, MessageBoxImage.Information);

                        //hide the current window
                        Visibility = Visibility.Hidden;

                        //open the main window
                        mainWindow.Show();

                        retry = 1;
                    }
                    
                }
                else if (string.IsNullOrEmpty(txtStudentNumber.Text) || string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtGrossMonthlyIncome.Text) || string.IsNullOrEmpty(txtMonthlyTax.Text))
                {
                    //prompt the user to provide all the values
                    MessageBox.Show("Please fill in all fields!", "Sign In Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    retry = 1;
                }

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//shutdown the application
            Application.Current.Shutdown();
        }

        private void txtStudentNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";
            //creating a main window object
            MainWindow mainWindow = new MainWindow();

            if (!string.IsNullOrWhiteSpace(txtStudentNumber.Text))
            {
                try
                {
                    mainWindow.lblStudentNumber.Content = txtStudentNumber.Text.ToUpper();

                    //if the user enters a negative number throw an error message
                    if (string.IsNullOrWhiteSpace(txtStudentNumber.Text))
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        txtStudentNumber.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;
                }

                if (exception == true)
                {
                    if (exceptionString == null)
                    {
                        MessageBox.Show("Enter a vaild student number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    txtStudentNumber.Text = null;
                }
                else
                {
                    mainWindow.lblStudentNumber.Content = txtStudentNumber.Text.ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Please Enter your Student Number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtNames_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";
            //creating a main window object
            MainWindow mainWindow = new MainWindow();

            if (!string.IsNullOrWhiteSpace(txtNames.Text))
            {
                try
                {
                    mainWindow.lblStudentName.Content = txtNames.Text.ToUpper();

                    //if the user enters a negative number throw an error message
                    if (string.IsNullOrWhiteSpace(txtNames.Text))
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        txtNames.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if (exceptionString == null)
                    {
                        MessageBox.Show("Enter a vaild student name and surname", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    txtNames.Text = null;
                }
                else
                {
                    mainWindow.lblStudentName.Content = txtNames.Text.ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Please Enter your Name and Surname!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtGrossMonthlyIncome_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(txtGrossMonthlyIncome.Text))
            {
                try
                {
                    //if the user enters a negative number throw an error message
                    if (Convert.ToDecimal(txtGrossMonthlyIncome.Text) <= 0)
                    {
                        Exception ex = new Exception("Enter a Positive Number!");
                        exception = true;
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        //try to read a decimal value
                        txtGrossMonthlyIncome.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if(exceptionString != "")
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    MessageBox.Show("You may have entered an invalid value,\nEnter a positive decimal value.", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtGrossMonthlyIncome.Text = null;
                }
                else
                {
                    Expense.setGrossMonthlyIncome(Convert.ToDecimal(txtGrossMonthlyIncome.Text));
                }
            }
            else
            {
                MessageBox.Show("Please Enter your Gross Monthly Income!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtMonthlyTax_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(txtMonthlyTax.Text))
            {
                try
                {                    
                    //if the user enters a negative number throw an error message
                    if(Convert.ToDecimal(txtMonthlyTax.Text) >= Convert.ToDecimal(txtGrossMonthlyIncome.Text))
                    {
                        Exception ex = new Exception("Tax Amount should be less than your Gross Monthly Income.\nEnter a lesser tax Amount!");
                        exception = true;//throw exception
                        MessageBox.Show(ex.Message, "Invalid Tax Amount", MessageBoxButton.OK, MessageBoxImage.Error);
                        //try to read a decimal value
                        txtMonthlyTax.Text = null;
                    }
                    if (Convert.ToDecimal(txtMonthlyTax.Text) <= 0)
                    {
                        Exception ex = new Exception("Enter a Positive Number!");
                        exception = true;//throw exception
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        //try to read a decimal value
                        txtMonthlyTax.Text = null;
                    }
                    
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if(exceptionString != "")
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    MessageBox.Show("You may have entered an invalid value,\nEnter a positive decimal value.", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtMonthlyTax.Text = null;
                }
                else
                {                 
                    Expense.setGrossMonthlyIncome(Expense.getGrossMonthlyIncome() - Convert.ToDecimal(txtMonthlyTax.Text));
                }
            }
            else
            {
                MessageBox.Show("Please Enter your Estimated Monthly Tax!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
