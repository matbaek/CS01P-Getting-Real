using Domain_Layer;
using Domain_Layer.Compensations;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer.MenuItem
{
    class AddCompensationToDepartment : IMenuItem
    {
        private Department Department;
        private Compensation Compensation;

        // Her oprettes en AddCompensationToDepartment, med 2 parametre
        public AddCompensationToDepartment(Department department, Compensation compensation)
        {
            Department = department;
            Compensation = compensation;
        }

        // Her aktiveres en SmartMenu
        public bool Activate(SmartMenu smartMenu)
        {
            Department.AddCompensation(Compensation);
            return true;
        }

        public override string ToString() => string.Format("Gem {0}", Compensation.Title);
    }
}
