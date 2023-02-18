using System;
using System.Windows;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10092081POEBudgetApp.MVM.Model
{
    class Expense
    {
        //variable to store the gross monthly income
        private static Decimal grossMonthlyIncome;

        //variable to store the available monthly amount
        private static Decimal availableMonthlyMoney;

        //variable to store the estimated monthly tax amount
        private static Decimal estimatedMonthlyTax;

        //variables to store the expenses
        private static Decimal groceries;
        private static Decimal waterLights;
        private static Decimal communication;
        private static Decimal travelCosts;
        private static Decimal otherExpenses;

        //variables that will be inherited by the derived classes
        private static Decimal purchasePrice;
        private static Decimal totalDeposit;
        private static int interestRate;

        //generic dictionary to store all the expenses entered by the user and their names
        public static Dictionary<string, decimal> expenses = new Dictionary<string, decimal>();

        //accessor and mutator methods to access the private variables outside the expense class
        public static void setPurchasePrice(decimal purchPrice)
        {
            purchasePrice = purchPrice;
        }

        public static Decimal getPurchasePrice()
        {
            return purchasePrice;
        }

        public static void setTotalDeposit(Decimal totalDepositAmount)
        {
            totalDeposit = totalDepositAmount;
        }

        public static Decimal getTotalDeposit()
        {
            return totalDeposit;
        }

        public static void setInterestRate(int interestRatePercentage)
        {
            interestRate = interestRatePercentage;
        }

        public static int getInterestRate()
        {
            return interestRate;
        }
        public static void setGrossMonthlyIncome(Decimal grossMonthIncome)
        {
            grossMonthlyIncome = grossMonthIncome;
        }

        public static Decimal getGrossMonthlyIncome()
        {
            return grossMonthlyIncome;
        }

        public static void setEstimatedMonthlyTax(Decimal estimateMonthTax)
        {
            estimatedMonthlyTax = estimateMonthTax;
        }

        public static Decimal getEstimatedMonthlyTax()
        {
            return estimatedMonthlyTax;
        }

        public static void setGroceries(Decimal grocery)
        {
            groceries = grocery;
        }

        public static Decimal getGroceries()
        {
            return groceries;
        }

        public static void setWaterLights(Decimal waterAndLights)
        {
            waterLights = waterAndLights;
        }

        public static Decimal getWaterLights()
        {
            return waterLights;
        }

        public static void setCommunication(Decimal communications)
        {
            communication = communications;
        }

        public static Decimal getCommunication()
        {
            return communication;
        }

        public static void setTravelCosts(Decimal travelCost)
        {
            travelCosts = travelCost;
        }

        public static Decimal getTravelCosts()
        {
            return travelCosts;
        }

        public static void setOtherExpenses(Decimal otherExpense)
        {
            otherExpenses = otherExpense;
        }

        public static Decimal getOtherExpenses()
        {
            return otherExpenses;
        }

        public static void setAvailableMonthlyMoney(Decimal availableMonthMoney)
        {
            availableMonthlyMoney = availableMonthMoney;
        }

        public static Decimal getAvailableMonthlyMoney()
        {
            return availableMonthlyMoney;
        }
        //method to calculate the available monthly amount
        public static Decimal calculateSumOfExpenses(Dictionary<string, decimal> expense)
        {//variable to store the sum of the expenses
            Decimal amount;
            //get the sum of the expenses and add to the amount variable
            amount = expense.Sum(x => x.Value);

            return amount;//return the amount variable
        }
        //method to display all monthly expenses in descending order
        public static string displayExpensesInDescendingOrder(Dictionary<string, decimal> keyValues)
        {
            /*Code Attribution
             * Dot Net Tutorials. 2022. Generic Queue Collection Class in C# with Examples. 
             * [online] Available at: <https://dotnettutorials.net/lesson/generic-queue-collection-class-csharp/#:
             * ~:text=What%20is%20Generic%20Queue%20in%20C%23%3F%20The%20Generic,queue%20at%20the%20ATM%20machine%20
             * to%20withdraw%20money.> [Accessed 4 June 2022].*/
            
            //string variable to store all the elemnts in the generic list
            string expensesInDescendingOrder = "";
            
            //sort all the elements in the generic list
            keyValues = keyValues.OrderByDescending(u => u.Value).ToDictionary(z => z.Key, y => y.Value);

            for (int i = 0; i < keyValues.Count; i++)
            {
                //display the expense name from the expenseName list and the expenses from the expenses queue than adding to the expensesInDescendingOrder string
                expensesInDescendingOrder += (keyValues.Keys.ElementAt(i) + keyValues[keyValues.Keys.ElementAt(i)].ToString("C", new CultureInfo("en-ZA")) + "\n");
            }
            //return the variable containing the elements in the generic list and queue
            return expensesInDescendingOrder;
        }
        //method used in a delegate to display the alert message
        public static void delegateMethodForErrorMessage(string alertMessage)
        {//display the alert message to the user
            MessageBox.Show(alertMessage,"Alert Message",MessageBoxButton.OK,MessageBoxImage.Exclamation);
        }/*Code Attribution
        Tutorialspoint.com. 2022. C# - Delegates. [online] Available at:
        <https://www.tutorialspoint.com/csharp/csharp_delegates.htm> [Accessed 4 June 2022].*/
    }
}
