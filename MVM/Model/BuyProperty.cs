using System;
using System.Windows;

namespace ST10092081POEBudgetApp.MVM.Model
{
    //this class is for the home loan option, it is derived from the Expense class
    class BuyProperty : Expense
    {
        private static int numMonthsToRepay;
        private static decimal monthlyLoanRepaymentAmount;

        //get and set methods to access variables outside the buy Property class
        public static void setNumMonthsToRepay(int MonthsToRepay)
        {
            numMonthsToRepay = MonthsToRepay;
        }

        public static int getnumMonthsToRepay()
        {
            return numMonthsToRepay;
        }

        public static void setMonthlyLoanRepaymentAmount(decimal MonthlyLoanRepaymentAmount)
        {
            monthlyLoanRepaymentAmount = MonthlyLoanRepaymentAmount;
        }

        public static decimal getMonthlyLoanRepaymentAmount()
        {
            return monthlyLoanRepaymentAmount;
        }

        //method to calculate the monthly loan repayment amount
        public static decimal calculateMonthlyLoanRepayment(decimal purchPrice, decimal Deposit, decimal intRate, int numMonthsToRepay)
        {//declaring variables to use in the calculations
            decimal purchPriceMinusDeposit;
            decimal intRatePercentage;
            decimal monthlyLoanRepaymentAmount;
            try//try calculating the monthly loan repayment
            {
                //Initializing variable to store the number of years
                int numberOfYears = numMonthsToRepay / 12;
                //Initializing variable to store the purchase price minus the deposit
                purchPriceMinusDeposit = purchPrice - Deposit;
                //Initializing variable to store the interest rate in decimal form
                intRatePercentage = intRate / 100;

                monthlyLoanRepaymentAmount = purchPriceMinusDeposit * (1 + intRatePercentage) / numMonthsToRepay;
            }
            catch (Exception e)
            {//throw a format exception in a message box
                MessageBox.Show(e.Message, "Format Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0.0M;
            }
            return monthlyLoanRepaymentAmount;
        }
    }
}