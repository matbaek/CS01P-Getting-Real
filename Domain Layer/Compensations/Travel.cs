using Domain_Layer.Appendices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Compensations
{
    public class Travel : Compensation
    {
        public readonly DateTime DepartureDate;
        public readonly DateTime ReturnDate;
        public readonly bool OverNightStay;
        public readonly double Credit;

        public Travel(string title, Employee employee, DateTime departureDate, DateTime returnDate, bool overNightStay, double credit) : base(title, employee)
        {
            DepartureDate = departureDate;
            ReturnDate = returnDate;
            OverNightStay = overNightStay;
            Credit = credit;
        }

        internal static List<Travel> GetTravelByEmployee(Employee employee)
        {
            List<Travel> travels = new List<Travel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetTravelById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@employee", employee.Id));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string title = reader["title"].ToString();
                        DateTime departureDate = DateTime.Parse(reader["departuredate"].ToString());
                        DateTime returnDate = DateTime.Parse(reader["returndate"].ToString());
                        bool overNightStay = bool.Parse(reader["overnightstay"].ToString());
                        double credit = float.Parse(reader["credit"].ToString());

                        Travel travel = new Travel(title, employee, departureDate, returnDate, overNightStay, credit);
                        travel.Id = id;
                        travels.Add(travel);
                    }
                }
            }
            return travels;
        }

        public void AddAppendix(Expenditure appendix)
        {
            AddAppendix(appendix as Appendix);
        }

        public override List<Appendix> GetAppendices()
        {
            return Expenditure.GetExpenditureByTravel(this).ToList<Appendix>();
        }

        public override void Save()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("insert_travel_compensation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@title", Title);
                command.Parameters.AddWithValue("@employee", Employee.Id);
                command.Parameters.AddWithValue("@departuredate", DepartureDate);
                command.Parameters.AddWithValue("@returndate", ReturnDate);
                command.Parameters.AddWithValue("@overnightstay", OverNightStay);
                command.Parameters.AddWithValue("@credit", Credit);
                
                Id = Convert.ToInt32(command.ExecuteScalar());
            }
            appendices.ForEach(o => o.Save());
        }

        public int TimeSpent()
        {
            return (int) Math.Floor((DepartureDate - ReturnDate).TotalHours);
        }

        public double TotalReturn()
        {
            double spent = 0;
            foreach (Expenditure item in appendices)
            {
                if (item.Cash == true)
                {
                    spent += item.Amount;
                }
            }
            return Credit - spent;
        }
    }
}
