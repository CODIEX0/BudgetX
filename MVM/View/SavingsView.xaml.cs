using System;
using ST10092081POEBudgetApp.MVM.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Globalization;

namespace ST10092081POEBudgetApp.MVM.View
{
    /// <summary>
    /// Interaction logic for SavingsView.xaml
    /// </summary>
    public partial class SavingsView : UserControl
    {
        public bool check = false;

        public SavingsView()
        {
            InitializeComponent();
        }

        public void clearStackPanelText()
        {
            //clear stack panel text blocks
            txt1.Text = null;
            txt2.Text = null;

        }

        private void BtnEnter3_Click(object sender, RoutedEventArgs e)
        {

            bool exception = false;
            string exceptionString = "";


            if (!string.IsNullOrEmpty(tbxSavingsDescription.Text) || !string.IsNullOrEmpty(tbxSavingsAmount.Text) || !string.IsNullOrEmpty(tbxInterestRate.Text) || !string.IsNullOrEmpty(tbxNumOfYears.Text))
            {
                try
                {                   
                    //calculate the motnly loan repayment amount and assign it to the variable
                    Savings.SetSavingsTotal(Savings.calculateSavingsTotal(Savings.GetSavingsAmount(),Savings.getInterestRate(),Savings.getNumberOfYears())); 

                    if (Savings.GetSavingsTotal() <= 0)
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
                    tbxSavingsDescription.Text = null;
                    tbxSavingsAmount.Text = null;
                    tbxInterestRate.Text = null;
                    tbxNumOfYears.Text = null;

                    //Prompt the to try entering the values again
                    MessageBox.Show("Please Retry! Entering", "Retry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else//if no exception was thrown execute this block
                {
                    if (check == false)
                    {
                        //alert the user that they have successfully entered the values without errors
                        MessageBox.Show("You have successfully entered the required values.\nSelect a different option from the other buttons!", "Monthly Rental Amount Entered", MessageBoxButton.OK, MessageBoxImage.Information);

                        //display monthly savings amount
                        MessageBox.Show("Your Monthly Savings Amount is :" + Savings.GetSavingsTotal().ToString("C", new CultureInfo("en-ZA")), "Values Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                        //main window object
                        MainWindow main = new();
                        //load the availble monthly imcome amount on to the label content
                        main.lblAvailableMonthlyIncome.Content = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));

                        //load the monthly loan repayment amount on to the label content
                        lblSavingsDescription.Content = Savings.getSavingsDescription().ToUpper();
                        lblMonthlySavingsAmount.Content = Savings.GetSavingsTotal().ToString("C", new CultureInfo("en-ZA"));

                        Expense.setAvailableMonthlyMoney(Expense.getAvailableMonthlyMoney() - Savings.GetSavingsTotal());

                        //add the total monthly vehicle cost to the expenses generic list
                        Expense.expenses.Add("Savings Monthly Cost: ", Savings.GetSavingsTotal());

                        //display the available Monthly Amount
                        MessageBox.Show("Available onthly Amount :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Available Monthly Amount", MessageBoxButton.OK, MessageBoxImage.Information);                        

                        //allow the user to enter correct value
                        tbxSavingsDescription.Text = null;
                        tbxSavingsAmount.Text = null;
                        tbxInterestRate.Text = null;
                        tbxNumOfYears.Text = null;

                        //make sure user is not able to re-enter values
                        check = true;
                        
                    }
                    else
                    {
                        MessageBox.Show("You already entered the required values!\nSelect a different option from the other buttons.", "Values Already Entered!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        //allow the user to enter correct value
                        tbxSavingsDescription.Text = null;
                        tbxSavingsAmount.Text = null;
                        tbxInterestRate.Text = null;
                        tbxNumOfYears.Text = null;

                        MessageBox.Show("Your Monthly Savings Amount is :" + Savings.GetSavingsTotal().ToString("C", new CultureInfo("en-ZA")), "Values Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

            }
            else if (string.IsNullOrEmpty(tbxSavingsDescription.Text) || string.IsNullOrEmpty(tbxSavingsAmount.Text) || string.IsNullOrEmpty(tbxInterestRate.Text) || string.IsNullOrEmpty(tbxNumOfYears.Text))
            {
                //prompt the user to provide all the values
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void tbxSavingsDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxSavingsDescription.Text))
            {
                try
                {
                    Savings.setSavingsDescription(tbxSavingsDescription.Text);
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
                        MessageBox.Show("Enter The Savins Description!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxSavingsDescription.Text = null;

                }
                else
                {
                    Savings.setSavingsDescription(tbxSavingsDescription.Text);
                }
            }
        }

        private void tbxSavingsAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxSavingsAmount.Text))
            {
                try
                {
                    Savings.SetSavingsAmount(Convert.ToDecimal(tbxSavingsAmount.Text));

                    //if the user enters a negative number throw an error message
                    if (Savings.GetSavingsAmount() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxSavingsAmount.Text = null;
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

                    tbxSavingsAmount.Text = null;

                }
                else
                {
                    Savings.SetSavingsAmount(Convert.ToDecimal(tbxSavingsAmount.Text));
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
                    Savings.setInterestRate(Convert.ToInt32(tbxInterestRate.Text));

                    //if the user enters a negative number throw an error message
                    if (Savings.getInterestRate() < 1 || Savings.getInterestRate() > 100)
                    {
                        Exception ex = new("Interest Rate should be between 1 and 100%");

                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        //try to read a decimal value
                        tbxInterestRate.Text = null;
                    }
                    //if the user enters a negative number throw an error message
                    if (Savings.getInterestRate() <= 0)
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
                    Savings.setInterestRate(Convert.ToInt32(tbxInterestRate.Text));
                }
            }
        }

        private void tbxNumOfYears_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxNumOfYears.Text))
            {
                try
                {
                    Savings.setNumberOfYears(Convert.ToInt32(tbxNumOfYears.Text));

                    //if the user enters a negative number throw an error message
                    if (Savings.getNumberOfYears() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxNumOfYears.Text = null;
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
                        MessageBox.Show("Enter a positive number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxNumOfYears.Text = null;
                }
                else
                {
                    Savings.setNumberOfYears(Convert.ToInt32(tbxNumOfYears.Text));
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
                else if (Expense.getAvailableMonthlyMoney() < 0)
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
                    txt2.HorizontalAlignment = HorizontalAlignment.Center;
                }
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
