using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ST10092081POEBudgetApp.MVM.Model
{
    class Savings : Expense
    {
        private static decimal savingsAmount;
        private static string savingsDescription;
        private static int numberOfYears;
        private static int interestRate;
        private static decimal savingsTotal;

        public static void SetSavingsAmount(decimal SavingAmount)
        {
            savingsAmount = SavingAmount;
        }

        public static decimal GetSavingsAmount()
        {
            return savingsAmount;
        }

        public static void SetSavingsTotal(decimal SavingsTotal)
        {
            savingsTotal = SavingsTotal;
        }

        public static decimal GetSavingsTotal()
        {
            return savingsTotal;
        }

        public static void setSavingsDescription(string SavingsDescription)
        {
            savingsDescription = SavingsDescription;
        }

        public static string  getSavingsDescription()
        {
            return savingsDescription;
        }

        public static void setNumberOfYears(int NumberOfYears)
        {
            numberOfYears = NumberOfYears;
        }

        public static int getNumberOfYears()
        {
            return numberOfYears;
        }

        public static new void setInterestRate(int InterestRate)
        {
            interestRate = InterestRate;
        }

        public static new int  getInterestRate()
        {
            return interestRate;
        }

        /* public static decimal calculateSavingsTotal(double savingsAmount, int interestRate, int numberOfYears)
        {
            int interest = interestRate / 100;
    
            decimal savingsTotal;

            savingsTotal = (decimal)Math.Round(savingsAmount * Math.Pow(1 + interest / 12, (12 * numberOfYears)), 2);
            //savingsTotal = savingsAmount * Math.Pow((1 + interest / 12), (numberOfYears * 12),2);

            return savingsTotal;
        }*/

        public static decimal calculateSavingsTotal(decimal presentValue, decimal interestRatePerYear, decimal financingPeriod)
        {
            double a, b, x;
            decimal monthlyPayment;
            a = (1 + (double)interestRatePerYear / 1200);
            b = (double)financingPeriod;
            x = Math.Pow(a, b);
            x = 1 / x;
            x = 1 - x;

            monthlyPayment = (presentValue) * (interestRatePerYear / 1200) / (decimal)x;
            return (monthlyPayment);
        }

    }
}
