using Domain_Layer;
using Domain_Layer.Compensations;
using Presentation_Layer.MenuItem;
using SmartMenuLibrary;
using System;

namespace Presentation_Layer
{
    internal class CreateTravel : IMenuItem
    {
        private AccessPoint accessPoint;

        public CreateTravel(AccessPoint accessPoint)
        {
            this.accessPoint = accessPoint;
        }

        public bool Activate(SmartMenu smartMenu)
        {
            string title = Request.String("Rejse godtgørelsens titel:");
            DateTime departureDate = Request.DateTime("Hvornår tog du afsted?");
            DateTime returnDate = Request.DateTime("Hvornår kom du hjem?");
            bool overNightStay = Request.Bool("Overnattede du under rejsen?");
            double credit = Request.Double("Hvor meget i kontant havde du med?");
            Travel travel = new Travel(title, accessPoint.Employee, departureDate, returnDate, overNightStay, credit);

            SmartMenu sm = new SmartMenu(travel.Title, "Anullér");

            sm.Attach(new AddExpenditure(travel));

            sm.Attach(new AddCompensationToDepartment(accessPoint.Department, travel));

            sm.Activate();

            return false;
        }

        public override string ToString()
        {
            return "Opret rejse godtgørelse";
        }
    }
}