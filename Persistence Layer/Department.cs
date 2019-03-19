using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence_Layer
{
    public class Department : Entry
    {
        private static readonly Dictionary<int, Department> Departments = new Dictionary<int, Department>();
        public readonly int Id;

        private Department(int id)
        {
            Id = id;
        }

        public static Department GetDepartment(int id)
        {
            if (!Departments.ContainsKey(id))
            {
                // Call the database for the employee
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("GetDepartmentById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        Departments.Add(id, new Department(id));
                    }
                    else
                    {
                        throw new EntryPointNotFoundException();
                    }
                }
            }

            return Departments[id];
        }
    }
}
