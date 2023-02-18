using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10092081POEBudgetApp.MVM.Model
{
    //this class is for the rental option, it is derived from the Expense class   
    class Rent : Expense
    {
        //variable to store the rental amounto
        private static decimal rentalAmount;
        private static decimal monthlyRentalAmount;

        //get and set methods to access variables outside the rent class
        public static void setRentalAmount(decimal rentAmount)
        {
            rentalAmount = rentAmount;
        }

        public static decimal getRentalAmount()
        {
            return rentalAmount;
        }

        public static void setMonthlyRentalAmount(decimal rentAmount)
        {
            monthlyRentalAmount = rentAmount;
        }

        public static decimal getMonthlyRentalAmount()
        {
            return monthlyRentalAmount;
        }
    }
}
