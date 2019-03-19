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
    public class Driving : Compensation
    {
        public readonly string NumberPlate;

        public Driving(string title, Employee employee, string numberPlate) : base(title, employee)
        {
            NumberPlate = numberPlate;
        }

        public void AddAppendix(Trip appendix)
        {
            AddAppendix(appendix as Appendix);
        }

        public override void Save()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("insert_driving_compensation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@title", Title);
                command.Parameters.AddWithValue("@employee", Employee.Id);
                command.Parameters.AddWithValue("@numberplate", NumberPlate);

                Id = Convert.ToInt32(command.ExecuteScalar());
            }
            appendices.ForEach(o => o.Save());
        }

        internal static List<Driving> GetDrivingByEmployee(Employee employee)
        {
            List<Driving> driving = new List<Driving>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetDrivingById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@employee", employee.Id));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string title = reader["title"].ToString();
                        string numberPlate = reader["numberplate"].ToString();

                        Driving drive = new Driving(title, employee, numberPlate);
                        drive.Id = id;
                        driving.Add(drive);
                    }
                }
            }
            return driving;
        }

        public override List<Appendix> GetAppendices()
        {
            return Trip.GetTripByDrive(this).ToList<Appendix>();
        }
    }
}
