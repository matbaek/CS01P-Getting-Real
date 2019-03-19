using Domain_Layer.Compensations;
using Domain_Layer.Appendices;
using Presentation_Layer.MenuItem;
using SmartMenuLibrary;

namespace Presentation_Layer
{
    internal class EditExpenditure : IMenuItem
    {
        private Compensation Compensation;
        private Expenditure Expenditure;

        // Her oprettes en EditExpenditure, med 2 parametre
        public EditExpenditure(Compensation travel, Expenditure expenditure)
        {
            Compensation = travel;
            Expenditure = expenditure;
        }

        // Her bliver der udfra inputs fra brugeren ændret i en Expenditure. Derefter bliver den tilføjet til den aktive SmartMenu
        public bool Activate(SmartMenu smartMenu)
        {
            string description = string.Format(
                "{0}\n{1}\n{2}\n{3}\n{4}",
                Expenditure.Title,
                Expenditure.ExpenseType,
                Expenditure.Cash,
                Expenditure.Date,
                Expenditure.Amount
            );

            SmartMenu sm = new SmartMenu(Expenditure.Title, "Tilbage", description);

            sm.Attach(new RemoveExpense(Compensation, Expenditure));
            
            int countExpenses = Compensation.CountAppendices();

            sm.Activate();

            if (countExpenses > Compensation.CountAppendices())
            {
                smartMenu.Detach(this);
            }

            return false;
        }

        public override string ToString() => string.Format("Se {0}", Compensation.Title);
    }
}