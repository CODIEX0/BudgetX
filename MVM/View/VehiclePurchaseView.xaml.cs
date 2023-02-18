using System;
using ST10092081POEBudgetApp.MVM.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Globalization;
using Microsoft.VisualBasic;

namespace ST10092081POEBudgetApp.MVM.View
{

    //declaring a delegate that takes string agurments or methods that prints out a string
    public delegate void VehicleErrorMsg(string message);

    public partial class VehiclePurchaseView : UserControl
    {
        bool exception = false;
        string exceptionString = "";

        public bool check = false;

        public VehiclePurchaseView()
        {
            InitializeComponent();
        }

        public void clearStackPanelText()
        {
            //clear stack panel text blocks
            txt1.Text = null;
            txt2.Text = null;

        }

        //message invoked by the delegate
        public static void VehicleErrorMessage(string error)
        {
            //message shown when expenses exceed 75% of the users income
            MessageBox.Show(error, "Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void BtnEnter4_Click(object sender, RoutedEventArgs e)
        {
            
            if (!string.IsNullOrEmpty(tbxModelAndMake.Text) || !string.IsNullOrEmpty(tbxPurchasePrice.Text) || !string.IsNullOrEmpty(tbxInterestRate.Text) || !string.IsNullOrEmpty(tbxDepositAmount.Text) || !string.IsNullOrEmpty(tbxInsurancePremium.Text))
            {
                try
                {
                    // calculate the monthly vehicle cost amd assign it to the variable
                    Vehicle.setTotalVehicleCost(Vehicle.calculateTotalMonthlyVehicleCost(Vehicle.getPurchasePrice(), Vehicle.getTotalDeposit(), Vehicle.getInterestRate(), Vehicle.getEstimatedInsurancePremium()));
                    if (Vehicle.getTotalVehicleCost() <= 0)
                    {
                        Exception ex = new("You May Have Entered Invalid Values.\nPlease Retry Entering!");
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        
                        //throw exception
                        exception = true;
                    }
                }
                catch(Exception ex)
                {
                    exceptionString = ex.Message;
                    //throw exception
                    exception = true;
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

                    //allow the user to retry entering values
                    tbxModelAndMake.Text = null;
                    tbxPurchasePrice.Text = null;
                    tbxDepositAmount.Text = null;
                    tbxInterestRate.Text = null;                  
                    tbxInsurancePremium.Text = null;
                }
                else
                {
                    if (check == false)
                    {
                        

                        if (Expense.calculateSumOfExpenses(Expense.expenses) > Expense.getGrossMonthlyIncome())
                        {
                            VehicleErrorMsg errorMsg = new(VehicleErrorMessage);

                            errorMsg("Your total Expenses have exceeded 75% of your Gross Monthly Income");

                            MessageBox.Show("The Monthly Vehicle Cost is :" + Vehicle.getTotalVehicleCost().ToString("C", new CultureInfo("en-ZA")) + 
                                "\n Try entering a lesser amount.", "Values Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                            //allow the user to retry entering values
                            tbxModelAndMake.Text = null;
                            tbxPurchasePrice.Text = null;
                            tbxDepositAmount.Text = null;
                            tbxInterestRate.Text = null;
                            tbxInsurancePremium.Text = null;
                        }
                        else
                        {
                            //alert the user that they have successfully entered the values without errors
                            MessageBox.Show("You have successfully entered the required values.\nSelect a different option from the other buttons!", "Monthly Rental Amount Entered", MessageBoxButton.OK, MessageBoxImage.Information);                                                      

                            //calculate the monthly vehicle cost amd assign it to the variable
                            Vehicle.setTotalVehicleCost(Vehicle.calculateTotalMonthlyVehicleCost(Vehicle.getPurchasePrice(), Vehicle.getTotalDeposit(), Vehicle.getInterestRate(), Vehicle.getEstimatedInsurancePremium()));

                            //set available Monthly Amount
                            Expense.setAvailableMonthlyMoney(Expense.getAvailableMonthlyMoney() - Vehicle.getTotalVehicleCost());

                            //display monthly amount
                            MessageBox.Show("Your Monthly Vehicle Cost is :" + Vehicle.getTotalVehicleCost().ToString("C", new CultureInfo("en-ZA")), "Values Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);

                            //add the total monthly vehicle cost to the expenses generic dictionary
                            Expense.expenses.Add("Vehicle Monthly Cost: ", Vehicle.getTotalVehicleCost());

                            //display the available Monthly Amount
                            MessageBox.Show("Available monthly Amount :" + Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA")), "Available Monthly Amount", MessageBoxButton.OK, MessageBoxImage.Information);

                            //main window object
                            MainWindow main = new();
                            //load the availble monthly imcome amount on to the label content
                            main.lblAvailableMonthlyIncome.Content = Expense.getAvailableMonthlyMoney().ToString("C", new CultureInfo("en-ZA"));

                            //display the car model & make, and the total monthly vehicle cost amount
                            lblCarModelAndMake.Content = Vehicle.getModelAndMake().ToUpperInvariant();
                            lbltotalMonthlyVehicleCost.Content = Vehicle.getTotalVehicleCost().ToString("C", new CultureInfo("en-ZA"));

                            //allow the user to retry entering values
                            tbxModelAndMake.Text = null;
                            tbxPurchasePrice.Text = null;
                            tbxDepositAmount.Text = null;
                            tbxInterestRate.Text = null;
                            tbxInsurancePremium.Text = null;

                            //make sure user is not able to re-enter values
                            check = true;
                        } 
                        
                    }
                    else
                    {
                        MessageBox.Show("You already entered the required values!\nSelect a different option from the other buttons.", "Values Already Entered!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        //allow the user to retry entering values
                        tbxModelAndMake.Text = null;
                        tbxPurchasePrice.Text = null;
                        tbxDepositAmount.Text = null;
                        tbxInterestRate.Text = null;
                        tbxInsurancePremium.Text = null;

                        MessageBox.Show("Monthly Vehicle Cost: " + Vehicle.getTotalVehicleCost().ToString("C", new CultureInfo("en-ZA")), "Values Entered successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void tbxModelAndMake_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxModelAndMake.Text))
            {
                try
                {
                    Vehicle.setModelAndMake(tbxModelAndMake.Text);
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
                        MessageBox.Show("Enter The Car Make and Model!", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(exceptionString, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    tbxModelAndMake.Text = null;

                }
                else
                {
                    Vehicle.setModelAndMake(tbxModelAndMake.Text);
                }
            }
        }

        private void tbxPurchasePrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxPurchasePrice.Text))
            {
                try
                {
                    Vehicle.setPurchasePrice(Convert.ToDecimal(tbxPurchasePrice.Text));

                    //if the user enters a negative number throw an error message
                    if (Vehicle.getPurchasePrice() <= 0)
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
                    if (exceptionString == "")
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
                    Vehicle.setPurchasePrice(Convert.ToDecimal(tbxPurchasePrice.Text));
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
                    Vehicle.SetTotalDeposit(Convert.ToDecimal(tbxDepositAmount.Text));

                    //if the user enters a negative number throw an error message
                    if (Vehicle.getTotalDeposit() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxDepositAmount.Text = null;
                    }
                    if (Convert.ToDecimal(tbxDepositAmount.Text) >= Convert.ToDecimal(tbxPurchasePrice.Text))
                    {
                        Exception ex = new Exception("Deposit Amount should be less than the Purchase Price.\nEnter a lesser Deposit Amount!");
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
                    if (exceptionString == "")
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
                    Vehicle.SetTotalDeposit(Convert.ToDecimal(tbxDepositAmount.Text));
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
                    Vehicle.setInterestRate(Convert.ToInt32(tbxInterestRate.Text));

                    //if the user enters a negative number throw an error message
                    if (Vehicle.getInterestRate() < 1 || Vehicle.getInterestRate() > 100)
                    {
                        Exception ex = new("Interest Rate should be between 1 and 100%");

                        MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        //try to read a decimal value
                        tbxInterestRate.Text = null;
                    }
                    //if the user enters a negative number throw an error message
                    if (Vehicle.getInterestRate() <= 0)
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
                    Vehicle.setInterestRate(Convert.ToInt32(tbxInterestRate.Text));
                }
            }
        }

        private void tbxInsurancePremium_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool exception = false;
            string exceptionString = "";

            if (!string.IsNullOrWhiteSpace(tbxInsurancePremium.Text))
            {
                try
                {
                    Vehicle.setEstimatedInsurancePremium(Convert.ToDecimal(tbxInsurancePremium.Text));

                    //if the user enters a negative number throw an error message
                    if (Vehicle.getEstimatedInsurancePremium() <= 0)
                    {
                        exception = true;//throw exception
                        //try to read a decimal value
                        tbxPurchasePrice.Text = null;
                    }
                    if(Vehicle.getEstimatedInsurancePremium() > Vehicle.getPurchasePrice())
                    {
                        Exception ex = new("Your Estimated Insurance Premium, \n" +
                            "should be less than the Purchase Price.\nEnter a lesser Estimated Insurance Premium Amount!");
                        exception = true;//throw exception
                        MessageBox.Show(ex.Message, "Invalid Deposit Amount", MessageBoxButton.OK, MessageBoxImage.Error);
                        //try to read a decimal value
                        tbxInsurancePremium.Text = null;
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

                    tbxInsurancePremium.Text = null;

                }
                else
                {
                    Vehicle.setEstimatedInsurancePremium(Convert.ToDecimal(tbxInsurancePremium.Text));
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
