using Domain_Layer.Compensations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer
{
    public class Department
    {
        private readonly List<Compensation> compensations = new List<Compensation>();
        public readonly int Id;

        // Når der skal oprettes en ny Department, så skal Department kun have et ID tildelt.
        internal Department(int id)
        {
            Id = id;
        }

        // Her tilføjes en Compensation til compensation
        public void AddCompensation(Compensation compensation)
        {
            compensation.Save();
            compensations.Add(compensation);
        }

        // Her fjernes en Compensation fra compensation
        public void RemoveCompensation(Compensation compensation)
        {
            compensations.Remove(compensation);
        }

        // Her printes compensations 
        public IList<Compensation> GetAllCompensations()
        {
            return compensations.AsReadOnly();
        }
    }
}
