using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Appendices
{
    public abstract class Appendix : ISavable
    {
        protected static readonly string ConnectionString = "Server=EALSQL1.eal.local; Database=B_DB17_2018; User Id=B_STUDENT17; Password=B_OPENDB17;";
        public int Id { get; internal set; }
        public readonly string Title;

        // Her tilføjes en titel
        protected Appendix(string title)
        {
            Title = title;
        }

        public abstract void Save();
    }
}
