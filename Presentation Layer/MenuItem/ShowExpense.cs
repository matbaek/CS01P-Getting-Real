using Domain_Layer.Appendices;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer.MenuItem
{
    class ShowExpense : IMenuItem
    {
        private Appendix Expense;

        // Her oprettes en ShowExpense, med 1 parametre
        public ShowExpense(Appendix expense)
        {
            Expense = expense;
        }

        // Her aktiveres en SmartMenu med en specifik Compensation Expense
        public bool Activate(SmartMenu smartMenu)
        {
            string description;
            if (Expense is Trip)
            {
                Trip trip = Expense as Trip;
                description = $"{trip.Title}\n{trip.DepartureDestination}\n{trip.DepartureDate}\n{trip.ArrivalDestination}\n{trip.ArrivalDate}\n{trip.Distance}";
            }
            else
            {
                Expenditure trip = Expense as Expenditure;
                description = $"{trip.Title}\n{trip.ExpenseType}\n{trip.Cash}\n{trip.Date}\n{trip.Amount}";
            }

            SmartMenu sm = new SmartMenu(ToString(), "Tilbage", description);

            sm.Activate();

            return false;
        }

        public override string ToString() => string.Format("Afregning: {0}", Expense.Title);
    }
}
