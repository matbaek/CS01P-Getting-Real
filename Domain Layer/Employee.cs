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
    public class Employee
    {
        private static readonly string ConnectionString = "Server=EALSQL1.eal.local; Database=B_DB17_2018; User Id=B_STUDENT17; Password=B_OPENDB17;";
        public readonly int Id;
        public readonly string Fullname;
        public readonly Department Department;

        // Når der skal oprettes en ny Employee, så skal Employee have et ID, fulde navn og en Department tildelt.
        private Employee(int id, string fullname, int department)
        {
            Id = id;
            Fullname = fullname;
            Department = new Department(department);
        }

        // Her returneres alle Travel og Driving
        public List<Compensation> GetCompensations()
        {
            List<Compensation> compensations = new List<Compensation>();

            foreach (Travel travel in Travel.GetTravelByEmployee(this))
            {
                compensations.Add(travel);
            }

            foreach (Driving driving in Driving.GetDrivingByEmployee(this))
            {
                compensations.Add(driving);
            }

            return compensations;
        }

        // Her returneres Employee, fra databasen, udfra et ID
        internal static Employee GetEmployeeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetEmployeeById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string fullname = reader["fullname"].ToString();
                    int department = int.Parse(reader["department"].ToString());
                    return new Employee(id, fullname, department);
                }
                else
                {
                    throw new EntryPointNotFoundException();
                }
            }
        }
    }
}
