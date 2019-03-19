using Domain_Layer;
using Domain_Layer.Appendices;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer
{
    class Program
    {
        static void Main(string[] args)
        {
            // AccessPoint er når en employeeId har skrevet sit medarbejder ID ind, derfor starter accessPoint som null
            AccessPoint accessPoint = null;
            do
            {
                // Her bruger vi metoden inde fra Request.cs for at få inputtet fra en employee lavet om til en int variabel
                int employeeId = Request.Int("Hvad er dit medarbejder ID?");
                // Her checker vi om det indtastet medarbejder ID findes i databasen, og om der er forbindelse til databasen
                try
                {
                    accessPoint = new AccessPoint(employeeId);
                }
                catch (EntryPointNotFoundException)
                {
                    Console.WriteLine("Kunne ikke finde data for medarbejder ID " + employeeId);
                    Console.ReadKey();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Noget gik galt mellem serveren & programmet: " + e);
                    Console.ReadKey();
                }
            } while (accessPoint is null);

            // Her gemmer man department som den ansatte er tildelt
            Department department = accessPoint.Department;

            // Her danner vi en ny smartMenu, som har department ID'et som titel og exitDescription
            SmartMenu smartMenu = new SmartMenu("Afdeling " + department.Id, "Luk programmet");

            // Her tilføjes menu punkter til menu'en
            smartMenu.Attach(new ShowAllCompensations(accessPoint));

            smartMenu.Attach(new CreateDriving(accessPoint));

            smartMenu.Attach(new CreateTravel(accessPoint));

            // Her aktiveres/starter smartMenu
            smartMenu.Activate();
        }
    }
}
