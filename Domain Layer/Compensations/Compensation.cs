using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Appendices;

namespace Domain_Layer.Compensations
{
    public abstract class Compensation : ISavable
    {
        protected static readonly string ConnectionString = "Server=EALSQL1.eal.local; Database=B_DB17_2018; User Id=B_STUDENT17; Password=B_OPENDB17;";
        public int Id { get; internal set; }
        protected readonly List<Appendix> appendices = new List<Appendix>();
        public readonly string Title;
        internal readonly Employee Employee;

        // Her tilføjes en titel og en employee til en Compensation
        protected Compensation(string title, Employee employee)
        {
            Title = title;
            Employee = employee;
        }

        // Her tilføjes en Compensation til appendices
        public void AddAppendix(Appendix expense)
        {
            appendices.Add(expense);
        }

        // Her fjernes en Compensation fra appendices
        public void RemoveAppendix(Appendix expense)
        {
            appendices.Remove(expense);
        }

        // Her tilføjes en Compensation til appendices
        public abstract List<Appendix> GetAppendices();

        public int CountAppendices()
        {
            return appendices.Count;
        }

        public abstract void Save();
    }
}
