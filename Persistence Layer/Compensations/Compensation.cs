using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence_Layer.Compensations
{
    public abstract class Compensation : Entry
    {
        public readonly int Id;
        public readonly string Title;
        private readonly int employee;

        protected Compensation(int id, string title, int employee)
        {
            Id = id;
            Title = title;
            this.employee = employee;
        }

        public Employee Employee
        {
            get
            {
                return Employee.GetEmployee(employee);
            }
        }
    }
}
