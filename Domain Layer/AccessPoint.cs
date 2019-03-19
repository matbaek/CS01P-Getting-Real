using Domain_Layer.Compensations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer
{
    public class AccessPoint
    {
        public readonly Employee Employee;

        // Her dannes en Employee udfra ID'et som er blivet givet som parameter
        public AccessPoint(int employeeId)
        {
            Employee = Employee.GetEmployeeById(employeeId);
        }

        // Her gemmes Department til en Employee 
        public Department Department => Employee.Department;

        // Her returneres alle Compensations som en medarbejder har
        public List<Compensation> GetAllCompensations()
        {
            return Employee.GetCompensations();
        }
    }
}
