using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistence_Layer
{
    public class Employee : Entry
    {
        private static readonly Dictionary<int, Employee> Employees = new Dictionary<int, Employee>();
        public readonly int Id;
        public readonly string Fullname;
        private readonly int department;

        private Employee(int id, string fullname, int department)
        {
            Id = id;
            Fullname = fullname;
            this.department = department;
        }

        public Department Department
        {
            get
            {
                return Department.GetDepartment(department);
            }
        }

        public static Employee GetEmployee(int id)
        {
            if (!Employees.ContainsKey(id))
            {
                // Call the database for the employee
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
                        Employees.Add(id, new Employee(id, fullname, department));
                    }
                    else
                    {
                        throw new EntryPointNotFoundException();
                    }
                }
            }

            return Employees[id];
        }
    }
}
