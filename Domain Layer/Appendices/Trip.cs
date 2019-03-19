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
    public class Trip : Appendix
    {
        public readonly double Rate = 3.54;

        public readonly string DepartureDestination;
        public readonly DateTime DepartureDate;
        public readonly string ArrivalDestination;
        public readonly DateTime ArrivalDate;
        public readonly int Distance;
        private readonly Driving driving;

        // Her dannes en Trip udfra de forskellige parametre. base(title) kommer inde fra Appendix.cs, som er nedarvet
        public Trip(string title, string departureDestination, DateTime departureDate, string arrivalDestination, DateTime arrivalDate, int distance, Driving driving) : base(title)
        {
            DepartureDestination = departureDestination;
            DepartureDate = departureDate;
            ArrivalDestination = arrivalDestination;
            ArrivalDate = arrivalDate;
            Distance = distance;
            this.driving = driving;
        }

        // Her hentes alle Trip fra databasen, som har en hvis Driving (Nummerplade)
        internal static List<Trip> GetTripByDrive(Driving drive)
        {
            List<Trip> driving = new List<Trip>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetTripByDriving", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@driving", drive.Id));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string title = reader["title"].ToString();
                        string departureDestination = reader["departuredestination"].ToString();
                        DateTime departureDate = DateTime.Parse(reader["departuredate"].ToString());
                        string arrivalDestination = reader["arrivaldestination"].ToString();
                        DateTime arrivalDate = DateTime.Parse(reader["arrivaldate"].ToString());
                        int distance = int.Parse(reader["distance"].ToString());
                        driving.Add(new Trip(title, departureDestination, departureDate, arrivalDestination, arrivalDate, distance, drive));
                    }
                }
            }
            return driving;
        }

        // Her indsættes en Trip i databasen
        public override void Save()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("insert_trip_appendix", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@title", Title);
                command.Parameters.AddWithValue("@departuredestination", DepartureDestination);
                command.Parameters.AddWithValue("@departuredate", DepartureDate);
                command.Parameters.AddWithValue("@arrivaldestination", ArrivalDestination);
                command.Parameters.AddWithValue("@arrivaldate", ArrivalDate);
                command.Parameters.AddWithValue("@distance", Distance);
                command.Parameters.AddWithValue("@driving", driving.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
