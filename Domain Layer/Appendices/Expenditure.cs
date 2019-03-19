using Domain_Layer.Compensations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Appendices
{
    public class Expenditure : Appendix
    {
        public readonly Type ExpenseType;
        public readonly bool Cash;
        public readonly DateTime Date;
        public readonly double Amount;
        private readonly Travel travel;

        public enum Type
        {
            Messeomkostninger = 54080,
            Transport = 87020,
            Ophold = 87030,
            Fortæring = 87040,
            Diverse = 87050,
            Repræsentaion = 81020,
            Bankkortgebyr = 96440
        }

        // Her dannes en Expenditure udfra de forskellige parametre. base(title) kommer inde fra Appendix.cs, som er nedarvet
        public Expenditure(string title, DateTime date, double amount, Type type, bool cash, Travel travel) : base(title)
        {
            Date = date;
            Amount = amount;
            ExpenseType = type;
            Cash = cash;
            this.travel = travel;
        }

        // Her hentes alle Expenditure fra databasen, som har en hvis Travel 
        internal static List<Expenditure> GetExpenditureByTravel(Travel travel)
        {
            List<Expenditure> driving = new List<Expenditure>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetExpenditureByTravel", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@travel", travel.Id));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string title = reader["title"].ToString();
                        int expenseType = int.Parse(reader["expensetype"].ToString());
                        bool cash = bool.Parse(reader["cash"].ToString());
                        DateTime date = DateTime.Parse(reader["date"].ToString());
                        double amount = float.Parse(reader["amount"].ToString());
                        driving.Add(new Expenditure(title, date, amount, (Type) expenseType, cash, travel));
                    }
                }
            }
            return driving;
        }

        // Her indsættes en Expenditure i databasen
        public override void Save()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("insert_expenditure_appendix", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@title", Title);
                command.Parameters.AddWithValue("@expensetype", ExpenseType);
                command.Parameters.AddWithValue("@cash", Cash);
                command.Parameters.AddWithValue("@date", Date);
                command.Parameters.AddWithValue("@amount", Amount);
                command.Parameters.AddWithValue("@travel", travel.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
