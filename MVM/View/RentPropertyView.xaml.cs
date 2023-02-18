using System;
using ST10092081POEBudgetApp.MVM.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Microsoft.VisualBasic;
using System.Globalization;

namespace ST10092081POEBudgetApp.MVM.View
{
    public partial class RentPropertyView : UserControl
    {
        

        public bool check = false;
        public RentPropertyView()
        {
            InitializeComponent();
        }

        public void clearStackPanelText()
        {
            //clear stack panel text blocks
            txt1.Text = null;
            txt2.Text = null;

        }

        private void BtnEnter2_Click(object sender, RoutedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrEmpty(tbxMonthlyRentalAmount.Text))
            {
                try
                {//try to add the value in the list

                    if (Rent.getMonthlyRentalAmount() <= 0)
                    {
                        exception = true;//throw exception
                    }                   
                }
                catch (Exception ex)
                {
                    exceptionString = ex.Message;

                    exception = true;
                }

                if(exception == true)
                {
                    if (exceptionString == "")
                    {
                        MessageBox.Show("Enter a positive decimal number!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxMonthlyRentalAmount.Text = null;


                    //Prompt the to try entering the values again
                    MessageBox.Show("Please try entering again!", "Retry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if(check == false)
                    {
                        //alert the user that they have successfully entered the values without errors
                        MessageBox.Show("You have successfully entered the required values.\nSelect a different option from the other buttons!", "Monthly Rental Amount Entered", MessageBoxButton.OK, MessageBoxImage.Information);

                        //add the total monthly rental cost to the expenses generic dictionary
                        Expense.expenses.Add("Rental Monthly Amount :", Rent.getMonthlyRentalAmount());

                        //set available Monthly Amount
                        Expense.setAvailableMonthlyMoney(Expense.getAvailableMonthlyMoney() - Rent.getMonthlyRentalAmount());

                        //display the available Monthly Amount
                        MessageBox.Show("Available Monthly Amount :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Available Monthly Amount", MessageBoxButton.OK, MessageBoxImage.Information);

                        //main window object
                        MainWindow main = new();
                        //load the availble monthly imcome amount on to the label content
                        main.lblAvailableMonthlyIncome.Content = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));

                        //load the available monthly amount on the label
                        lblRentalAmount.Content = Rent.getMonthlyRentalAmount().ToString("C", new CultureInfo("en-ZA"));

                        tbxMonthlyRentalAmount.Text = null;

                        //make sure user is not able to re-enter values
                        check = true;
                    }
                    else
                    {
                        MessageBox.Show("You already entered the required values!\nSelect a different option from the other buttons.", "Expenses Already Entered!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        //allow the user to enter correct value
                        tbxMonthlyRentalAmount.Text = null;

                        MessageBox.Show("Your new Available Monthly Amount is :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Expenses Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                    }                          
                }                                
            }
            else
            {
                MessageBox.Show("Please fill in the Monthly Rental Amount Text Box!", "Monthly Rental Text Box is empty", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbxMonthlyRentalAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            

            if (!string.IsNullOrWhiteSpace(tbxMonthlyRentalAmount.Text))
            {
                try
                {
                    Convert.ToDecimal(tbxMonthlyRentalAmount.Text);                    

                    //if the user enters a negative number throw an error message
                    if (Convert.ToDecimal(tbxMonthlyRentalAmount.Text) <= 0)
                    {
                        exception = true;//throw exception
                        
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

                    tbxMonthlyRentalAmount.Text = null;

                }
                else
                {
                    Rent.setMonthlyRentalAmount(Convert.ToDecimal(tbxMonthlyRentalAmount.Text));
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
