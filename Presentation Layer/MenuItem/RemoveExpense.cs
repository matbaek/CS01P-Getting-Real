using Domain_Layer.Compensations;
using Domain_Layer.Appendices;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer.MenuItem
{
    class RemoveExpense : IMenuItem
    {
        private Compensation Compensation;
        private Appendix Expense;

        // Her oprettes en RemoveExpense, med 2 parametre
        public RemoveExpense(Compensation compensation, Appendix expense)
        {
            Compensation = compensation;
            Expense = expense;
        }

        // Her aktiveres en SmartMenu & fjerner dette punkt i SmartMenu
        public bool Activate(SmartMenu smartMenu)
        {
            Compensation.RemoveAppendix(Expense);
            smartMenu.Detach(this);
            return true;
        }

        public override string ToString() => string.Format("Fjern {0} fra {1}", Expense.Title, Compensation.Title);
    }
}
