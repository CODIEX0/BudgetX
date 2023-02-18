using System;
using ST10092081POEBudgetApp.MVM.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Windows.Input;
using System.Windows.Media;

namespace ST10092081POEBudgetApp.MVM.View
{
    public partial class HomeLoanView : UserControl
    {
        public bool check = false;
       
        public HomeLoanView()
        {
            InitializeComponent();

        }

        public void clearStackPanelText()
        {
            //clear stack panel text blocks
            txt1.Text = null;
            txt2.Text = null;

        }

        private void BtnEnter1_Click(object sender, RoutedEventArgs e)
        {
            
            bool exception = false;
            string exceptionString = "";


            if (!string.IsNullOrEmpty(tbxPurchasePrice.Text) || !string.IsNullOrEmpty(tbxDepositAmount.Text) || !string.IsNullOrEmpty(tbxInterestRate.Text) || !string.IsNullOrEmpty(tbxNumberOfMonths.Text))
            {

                try
                {
                    //calculate the motnly loan repayment amount and assign it to the variable
                    BuyProperty.setMonthlyLoanRepaymentAmount(BuyProperty.calculateMonthlyLoanRepayment(BuyProperty.getPurchasePrice(), BuyProperty.getTotalDeposit(), BuyProperty.getInterestRate(), BuyProperty.getnumMonthsToRepay()));

                    if (Convert.ToInt32(tbxNumberOfMonths.Text) < 240 || Convert.ToInt32(tbxNumberOfMonths.Text) > 360)
                    {
                        Exception ex = new("Enter the valid number of months (between 240 and 360)!");

                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        //try to read a decimal value
                        tbxNumberOfMonths.Text = null;

                    }
                   
                    if (BuyProperty.getMonthlyLoanRepaymentAmount() <= 0)
                    {
                        Exception ex = new("You May Have Entered Invalid Values.\nPlease Retry Entering!");
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        //throw exception
                        exception = true;
                    }

                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    //throw exception
                    exception = true;

                    
                }

                //if statements, that will handle exceptions
                if (exception == true)
                {
                    //alert the user that an error has occurred 
                    MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                    //allow the user to enter correct value
                    tbxPurchasePrice.Text = null;
                    tbxDepositAmount.Text = null;
                    tbxInterestRate.Text = null;
                    tbxNumberOfMonths.Text = null;

                    //Prompt the to try entering the values again
                    MessageBox.Show("Please Retry! Entering", "Retry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else//if no exception was thrown execute this block
                {
                    if(check == false)
                    {
                        

                        if (BuyProperty.getMonthlyLoanRepaymentAmount() > (Expense.getGrossMonthlyIncome() / 3))
                        {
                            //display the monthly home loan repayment amount
                            MessageBox.Show("Sorry, your Application for a Home Loan was Unsuccessfull!\n" +
                                    "The Monthly Home Loan Amount is greater than a 1/3 of your Gross Monthly Income! Which is:" + BuyProperty.getMonthlyLoanRepaymentAmount().ToString("C", new CultureInfo("en-ZA")));


                            //Prompt the to try entering the values again
                            MessageBox.Show("Please Retry! Entering", "Retry", MessageBoxButton.OK, MessageBoxImage.Information);

                            //allow the user to enter correct value
                            tbxPurchasePrice.Text = null;
                            tbxDepositAmount.Text = null;
                            tbxInterestRate.Text = null;
                            tbxNumberOfMonths.Text = null;

                        }
                        else if (BuyProperty.getMonthlyLoanRepaymentAmount() < (Expense.getGrossMonthlyIncome() / 3))
                        {
                            //alert the user that they have successfully entered the values without errors
                            MessageBox.Show("You have successfully entered the required values.\nSelect a different option from the other buttons!", "Monthly Rental Amount Entered", MessageBoxButton.OK, MessageBoxImage.Information);

                            //set available Monthly Amount
                            Expense.setAvailableMonthlyMoney(Expense.getAvailableMonthlyMoney() - BuyProperty.getMonthlyLoanRepaymentAmount());

                            //display monthly home loan repayment amount
                            MessageBox.Show("Home Loan Monthly Payment Amount: " + BuyProperty.getMonthlyLoanRepaymentAmount().ToString("C", new CultureInfo("en-ZA")),"Monthly Home Loan Amount", MessageBoxButton.OK, MessageBoxImage.Information);

                            //display the available Monthly Amount
                            MessageBox.Show("Available Monthly Amount :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Available Monthly Amount", MessageBoxButton.OK, MessageBoxImage.Information);

                            //main window object
                            MainWindow main = new();
                            //load the availble monthly imcome amount on to the label content
                            main.lblAvailableMonthlyIncome.Content = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));
                            main.lblAvailableMonthlyIncome.UpdateLayout();

                            //load the monthly loan repayment amount on to the label content
                            lblMonthlyLoanRepaymentAmount.Content = BuyProperty.getMonthlyLoanRepaymentAmount().ToString("C", new CultureInfo("en-ZA"));

                            //add the total monthly loan repayment to the expenses generic dictionary
                            Expense.expenses.Add("Monthly Home Loan Amount :", BuyProperty.getMonthlyLoanRepaymentAmount());
                             
                            //allow the user to enter correct value
                            tbxPurchasePrice.Text = null;
                            tbxDepositAmount.Text = null;
                            tbxInterestRate.Text = null;
                            tbxNumberOfMonths.Text = null;

                            //make sure user is not able to re-enter values
                            check = true;
                        }
                      

                    }
                    else
                    {
                        MessageBox.Show("You already entered the required values!\nSelect a different option from the other buttons.", "Values Already Entered!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        //allow the user to enter correct value
                        tbxPurchasePrice.Text = null;
                        tbxDepositAmount.Text = null;
                        tbxInterestRate.Text = null;
                        tbxNumberOfMonths.Text = null;

                        MessageBox.Show("Your Monthly Home Loan Amount is :" + BuyProperty.getMonthlyLoanRepaymentAmount().ToString("C", new CultureInfo("en-ZA")), "Values Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    

                }

            }
            else if (string.IsNullOrEmpty(tbxPurchasePrice.Text) || string.IsNullOrEmpty(tbxDepositAmount.Text) || string.IsNullOrEmpty(tbxInterestRate.Text) || string.IsNullOrEmpty(tbxNumberOfMonths.Text))
            {
                //prompt the user to provide all the values
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }


        private void tbxPurchasePrice_SelectionChanged(object sender, RoutedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if(!string.IsNullOrWhiteSpace(tbxPurchasePrice.Text))
            {
                try
                {
                    BuyProperty.setPurchasePrice(Convert.ToDecimal(tbxPurchasePrice.Text));

                    //if the user enters a negative number throw an error message
                    if (BuyProperty.getPurchasePrice() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxPurchasePrice.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if(exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    tbxPurchasePrice.Text = null;
                  
                }
                else
                {                  
                    BuyProperty.setPurchasePrice(Convert.ToDecimal(tbxPurchasePrice.Text));
                }
            }                  
        }

        private void tbxDepositAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxDepositAmount.Text))
            {
                try
                {
                    BuyProperty.setTotalDeposit(Convert.ToDecimal(tbxDepositAmount.Text));

                    //if the user enters a negative number throw an error message
                    if (BuyProperty.getTotalDeposit() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxDepositAmount.Text = null;
                    }
                    if(Convert.ToDecimal(tbxDepositAmount.Text) >= Convert.ToDecimal(tbxPurchasePrice.Text))
                    {
                        Exception ex = new("Deposit Amount should be less than the Purchase Price.\nEnter a lesser Deposit Amount!");
                        exception = true;//throw exception
                        MessageBox.Show(ex.Message, "Invalid Deposit Amount", MessageBoxButton.OK, MessageBoxImage.Error);
                        //try to read a decimal value
                        tbxDepositAmount.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;
                    exception = true;//throw exception
                }

                if (exception == true)
                {
                    if(exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxDepositAmount.Text = null;
                }
                else
                {
                    BuyProperty.setPurchasePrice(Convert.ToDecimal(tbxPurchasePrice.Text));
                }
            }
        }

        private void tbxInterestRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxInterestRate.Text))
            {
                try
                {
                    BuyProperty.setInterestRate(Convert.ToInt32(tbxInterestRate.Text));

                    //if the user enters a negative number throw an error message
                    if (BuyProperty.getInterestRate() < 1 || BuyProperty.getInterestRate() > 100)
                    {
                        Exception ex = new("Interest Rate should be between 1 and 100%");

                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        //try to read a decimal value
                        tbxInterestRate.Text = null; 
                    }
                    //if the user enters a negative number throw an error message
                    if (BuyProperty.getInterestRate() <= 0)
                    {
                        exception = true;//throw exception

                        //try to read a decimal value
                        tbxInterestRate.Text = null;
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
                        MessageBox.Show("Enter a positive number", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    tbxInterestRate.Text = null;
                }
                else
                {
                    BuyProperty.setInterestRate(Convert.ToInt32(tbxInterestRate.Text));
                }
            }
        }

        private void tbxNumberOfMonths_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxNumberOfMonths.Text))
            {
                try
                {
                    if (Convert.ToInt32(tbxNumberOfMonths.Text) > 360)
                    {
                        Exception ex = new("Enter the valid number of months (between 240 and 360)!");

                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        //try to read a decimal value
                        tbxNumberOfMonths.Text = null;

                    }

                    BuyProperty.setNumMonthsToRepay(Convert.ToInt32(tbxNumberOfMonths.Text));
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
                        MessageBox.Show("Enter a positive number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    tbxNumberOfMonths.Text = null;
                }
                else
                {
                    BuyProperty.setNumMonthsToRepay(Convert.ToInt32(tbxNumberOfMonths.Text));
                }               
                
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAvailableMonthlyAmount.IsSelected)
            {
                if (Expense.getAvailableMonthlyMoney() > 0)
                {
                    clearStackPanelText();

                    txt1.Text = "Available Monthly Amount";
                    txt2.Text = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));
                }
                else if(Expense.getAvailableMonthlyMoney() < 0)
                {
                    clearStackPanelText();

                    txt1.Text = "Available Monthly Amount";
                    txt2.Text = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));
                    MessageBox.Show("Your Available Monthly Amount is Less Than 0,\nYou are now bankrupt!", "You Are Bankrupt", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    clearStackPanelText();
                    MessageBox.Show("This Option Has'nt been calculated yet!\nSelect the correct button and \ncalculate the value you want to display.", "Values Not Entered", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cmbExpensesDesendingOrder.IsSelected)
            {
                if (Expense.expenses.Count > 1)
                {
                    clearStackPanelText();
                    txt1.Text = "Expenses In Descending Order";
                    txt2.Text = Expense.displayExpensesInDescendingOrder(Expense.expenses);
                    txt2.Height = 180;
                    txt2.HorizontalAlignment = HorizontalAlignment.Center;               }
                else
                {
                    clearStackPanelText();
                    MessageBox.Show("This Option Has'nt been calculated yet!\nSelect the correct button and \ncalculate the value you want to display.", "Values Not Entered", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cmbMonthlySavingsCost.IsSelected)
            {
                if (Savings.GetSavingsTotal() > 0)
                {
                    clearStackPanelText();
                    txt1.Text = "Savings Monthly Cost";
                    txt2.Text = Savings.GetSavingsTotal().ToString("C", new CultureInfo("en-ZA"));
                }
                else
                {
                    clearStackPanelText();
                    MessageBox.Show("This Option Has'nt been calculated yet!\nSelect the correct button and \ncalculate the value you want to display.", "Values Not Entered", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cmbMonthlyVehicleCost.IsSelected)
            {
                if (Vehicle.getTotalVehicleCost() > 0)
                {
                    clearStackPanelText();
                    txt1.Text = "Vehicle Monthly Cost";
                    txt2.Text = Vehicle.getTotalVehicleCost().ToString("C", new CultureInfo("en-ZA"));
                }
                else
                {
                    clearStackPanelText();
                    MessageBox.Show("This Option Has'nt been calculated yet!\nSelect the correct button and \ncalculate the value you want to display.", "Values Not Entered", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cmbMonthlyHomeLoan.IsSelected)
            {
                if (BuyProperty.getMonthlyLoanRepaymentAmount() > 0)
                {
                    clearStackPanelText();
                    txt1.Text = "Home Loan Monthly Amount";
                    txt2.Text = BuyProperty.getMonthlyLoanRepaymentAmount().ToString("C", new CultureInfo("en-ZA"));
                }
                else
                {
                    clearStackPanelText();
                    MessageBox.Show("This Option Has'nt been calculated yet!\nSelect the correct button and \ncalculate the value you want to display.", "Values Not Entered", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (cmbMonthlyRentalAmount.IsSelected)
            {
                if (Rent.getMonthlyRentalAmount() > 0)
                {
                    clearStackPanelText();
                    txt1.Text = "Rental Monthly Amount";
                    txt2.Text = Rent.getMonthlyRentalAmount().ToString("C", new CultureInfo("en-ZA"));
                }
                else
                {
                    clearStackPanelText();
                    MessageBox.Show("This Option Has'nt been calculated yet!\nSelect the correct button and \ncalculate the value you want to display.", "Values Not Entered", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
