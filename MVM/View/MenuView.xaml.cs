using System;
using ST10092081POEBudgetApp.MVM.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Linq;

namespace ST10092081POEBudgetApp.MVM.View
{
    public partial class MenuView : UserControl
    {
        
        public bool check = false;

        public MenuView()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";
            

            //if an exception was thrown, than run this block of code
            if (!string.IsNullOrEmpty(tbxGroceries.Text) || !string.IsNullOrEmpty(tbxWanterAndLights.Text) || !string.IsNullOrEmpty(tbxTraveltCosts.Text) || !string.IsNullOrEmpty(tbxCellPhoneAndTelephone.Text) || !string.IsNullOrEmpty(tbxOtherExpenses.Text))
            {
                try
                {
                    if(Expense.calculateSumOfExpenses(Expense.expenses) > 0)
                    {
                        if (Convert.ToDecimal(tbxGroceries.Text) < 0 || Convert.ToDecimal(tbxWanterAndLights.Text) < 0 || Convert.ToDecimal(tbxTraveltCosts.Text) < 0 || Convert.ToDecimal(tbxCellPhoneAndTelephone.Text) < 0 || Convert.ToDecimal(tbxOtherExpenses.Text) < 0)
                        {
                            Exception ex = new("Enter a Positive Decimal Number!");

                            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                            exception = true;
                        }

                        if (Expense.getGroceries() < 0 && Expense.getWaterLights() < 0 && Expense.getCommunication() < 0 && Expense.getTravelCosts() < 0 && Expense.getOtherExpenses() < 0)
                        {                          
                            if (Expense.calculateSumOfExpenses(Expense.expenses) > Expense.getGrossMonthlyIncome())
                            {
                                Exception ex = new("Your Expenses should'nt exceed your Gross Monthly Income Amount!\n" + Expense.calculateSumOfExpenses(Expense.expenses));

                                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            //alert the user that thay already entered these expenses
                            MessageBox.Show("You have already entered these expenses!\nSelect a different option from the other buttons.", "Expense Already Entered", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            exception = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                }

                if (exception == true)
                {
                    if (exceptionString != "")
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    //alert the user that an error has occurred                                          
                    //display a message box with a description of the users error, and how to correct it
                    MessageBox.Show("Sorry an error has occurred! You may have entered an invalid data type.\n" +
                        "Enter a positive number!", "Format Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                    //Prompt the to try entering the values again
                    MessageBox.Show("Please try entering again!", "Retry", MessageBoxButton.OK, MessageBoxImage.Information);

                    //allow the user to enter correct value
                    tbxGroceries.Text = null;
                    tbxWanterAndLights.Text = null;
                    tbxTraveltCosts.Text = null;
                    tbxCellPhoneAndTelephone.Text = null;
                    tbxOtherExpenses.Text = null;
                }
                else
                {
                    if(check == false)
                    {
                        if(Expense.getGroceries() > 0 && Expense.getWaterLights() > 0 && Expense.getCommunication() > 0 && Expense.getTravelCosts() > 0 && Expense.getOtherExpenses() > 0)
                        {
                            //alert the user that they have successfully entered the values without errors
                            MessageBox.Show("You have successfully entered the required values.\nSelect a different option from the other buttons!", "Expenses Entered", MessageBoxButton.OK, MessageBoxImage.Information);
                            //add values to the generic dictionary
                            Expense.expenses.Add("Groceries: ", Expense.getGroceries());
                            Expense.expenses.Add("Water and Lights: ", Expense.getWaterLights());
                            Expense.expenses.Add("CellPhone and Telephone: ", Expense.getCommunication());
                            Expense.expenses.Add("Travel Costs: ", Expense.getTravelCosts());
                            Expense.expenses.Add("Other Expenses: ", Expense.getOtherExpenses());

                            Expense.setAvailableMonthlyMoney(Expense.getAvailableMonthlyMoney() - Expense.calculateSumOfExpenses(Expense.expenses));
                            
                            //display estimated monthly expenditure
                            MessageBox.Show("Your Estimated Monthly Expenditure Amount is :" + (Expense.getGroceries() + Expense.getWaterLights() + Expense.getCommunication()
                                + Expense.getTravelCosts() + Expense.getOtherExpenses()).ToString("C", new CultureInfo("en-ZA")), "Estimated Monthly Expenses Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                            //display available monthly amount
                            MessageBox.Show("Your new Available Monthly Amount is :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Expenses Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                            //allow the user to enter correct value
                            tbxGroceries.Text = null;
                            tbxWanterAndLights.Text = null;
                            tbxTraveltCosts.Text = null;
                            tbxCellPhoneAndTelephone.Text = null;
                            tbxOtherExpenses.Text = null;
                            //make sure user is not able to re-enter values
                            check = true;
                        }
                        else
                        {
                            MessageBox.Show("You already entered the required values!\nSelect a different option from the other buttons.", "Expenses Already Entered!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            //allow the user to enter correct value
                            tbxGroceries.Text = null;
                            tbxWanterAndLights.Text = null;
                            tbxTraveltCosts.Text = null;
                            tbxCellPhoneAndTelephone.Text = null;
                            tbxOtherExpenses.Text = null;

                            MessageBox.Show("Your new Available Monthly Amount is :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Expenses Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                            MainWindow main = new MainWindow();
                            main.lblAvailableMonthlyIncome.Content = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));
                            main.lblAvailableMonthlyIncome.UpdateLayout();

                            main.btnRentProperty.IsChecked = true;
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("You already entered the required values!\nSelect a different option from the other buttons.", "Expenses Already Entered!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        //allow the user to enter correct value
                        tbxGroceries.Text = null;
                        tbxWanterAndLights.Text = null;
                        tbxTraveltCosts.Text = null;
                        tbxCellPhoneAndTelephone.Text = null;
                        tbxOtherExpenses.Text = null;

                        MessageBox.Show("Your new Available Monthly Amount is :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Expenses Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                        MainWindow main = new MainWindow();

                        main.btnRentProperty.IsChecked = true;
                    
                    }
                    
                }               
            }
            else
            {
                MessageBox.Show("Please fill in all the Text Box's!", "Some Text Box's are empty", MessageBoxButton.OK, MessageBoxImage.Warning);

                MessageBox.Show("Your Available Monthly Amount is :" + Expense.getGrossMonthlyIncome().ToString("C", new CultureInfo("en-ZA")), "Expenses Entered", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void tbxGroceries_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxGroceries.Text))
            {
                try
                {
                    Expense.setGroceries(Convert.ToDecimal(tbxGroceries.Text));
                    //if the user enters a negative number throw an error message
                    if (Expense.getGroceries() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxGroceries.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if (exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxGroceries.Text = null;
                }
                else
                {
                    Expense.setGroceries(Convert.ToDecimal(tbxGroceries.Text));
                    

                }
            }
        }

        private void tbxWanterAndLights_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxWanterAndLights.Text))
            {
                try
                {               
                    Expense.setWaterLights(Convert.ToDecimal(tbxWanterAndLights.Text));

                    //if the user enters a negative number throw an error message
                    if (Expense.getWaterLights() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxWanterAndLights.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if (exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxWanterAndLights.Text = null;

                }
                else
                {
                    Expense.setWaterLights(Convert.ToDecimal(tbxWanterAndLights.Text));
                    
                }
            }
        }

        private void tbxTraveltCosts_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxTraveltCosts.Text))
            {
                try
                {
                    Expense.setTravelCosts(Convert.ToDecimal(tbxTraveltCosts.Text));

                    //if the user enters a negative number throw an error message
                    if (Expense.getTravelCosts() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxTraveltCosts.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if (exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxTraveltCosts.Text = null;

                }
                else
                {
                    Expense.setTravelCosts(Convert.ToDecimal(tbxTraveltCosts.Text));
                    
                }
            }
        }

        private void tbxCellPhoneAndTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxCellPhoneAndTelephone.Text))
            {
                try
                {
                    Expense.setCommunication(Convert.ToDecimal(tbxCellPhoneAndTelephone.Text));

                    //if the user enters a negative number throw an error message
                    if (Expense.getCommunication() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxCellPhoneAndTelephone.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if (exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxCellPhoneAndTelephone.Text = null;

                }
                else
                {
                    Expense.setCommunication(Convert.ToDecimal(tbxCellPhoneAndTelephone.Text));
                    
                }
            }
        }

        private void tbxOtherExpenses_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxOtherExpenses.Text))
            {
                try
                {
                    Expense.setOtherExpenses(Convert.ToDecimal(tbxOtherExpenses.Text));                    

                    //if the user enters a negative number throw an error message
                    if (Expense.getOtherExpenses() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxOtherExpenses.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if (exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxOtherExpenses.Text = null;

                }
                else
                {
                    Expense.setOtherExpenses(Convert.ToDecimal(tbxOtherExpenses.Text));
                    
                }
            }
        }
    }
}
