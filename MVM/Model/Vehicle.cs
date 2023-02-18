using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10092081POEBudgetApp.MVM.Model
{
    //this class is for the vehicle purchase option, it is derived from the Expense class
    class Vehicle : Expense
    {//variables to store the moadel & make and the estimated insurance premium
        private static int interestRate;
        private static decimal totalDeposit;
        private static decimal purchasePrice;
        private static string modelAndMake;
        private static decimal estimatedInsurancePremium;
        private static decimal totalVehicleCost;

        //get and set methods to access variables outside the vehicle class

        public static new void setPurchasePrice(decimal purchPrice)
        {
            purchasePrice = purchPrice;
        }

        public static new decimal getPurchasePrice()
        {
            return purchasePrice;
        }

        public static void SetTotalDeposit(decimal totalDepositAmount)
        {
            totalDeposit = totalDepositAmount;
        }

        public static new decimal getTotalDeposit()
        {
            return totalDeposit;
        }

        public static new void setInterestRate(int interestRatePercentage)
        {
            interestRate = interestRatePercentage;
        }

        public static new int getInterestRate()
        {
            return interestRate;
        }

        public static void setModelAndMake(string modelMake)
        {
            modelAndMake = modelMake;
        }

        public static string getModelAndMake()
        {
            return modelAndMake;
        }

        public static void setEstimatedInsurancePremium(decimal estimatedInsurancePremiumAmount)
        {
            estimatedInsurancePremium = estimatedInsurancePremiumAmount;
        }

        public static decimal getEstimatedInsurancePremium()
        {
            return estimatedInsurancePremium;
        }

        public static void setTotalVehicleCost(decimal TotalVehicleCost)
        {
            totalVehicleCost = TotalVehicleCost;
        }

        public static decimal getTotalVehicleCost()
        {
            return totalVehicleCost;
        }

        //method to calculate the total monthly vehicle cost
        public static decimal calculateTotalMonthlyVehicleCost(decimal purchasePrice, decimal depositAmount, float interestRate, decimal estimatedInsurancePremium)
        {
            //declaring variables to use in the calculations
            decimal purchPriceMinusDeposit;
            decimal intRatePercentage;
            decimal loanRepaymentAmount;
            decimal totalVehicleMonthlyCost;

            //Initializing variable to store the purchase minus the deposit amount
            purchPriceMinusDeposit = purchasePrice - depositAmount;
            //Initializing variable to store the calculated interest rate
            intRatePercentage = Convert.ToDecimal(interestRate / 100);
            //Initializing variable to store the calculated loan repayment amount
            loanRepaymentAmount = purchPriceMinusDeposit * (1 + intRatePercentage * 5) / 60;
            //Initializing variable to store the estimated insurance premium amount plus the loan repayment amount
            totalVehicleMonthlyCost = estimatedInsurancePremium + loanRepaymentAmount;
            //return the total vehicle monthly cost
            return totalVehicleMonthlyCost;
        }

        
    }
}
