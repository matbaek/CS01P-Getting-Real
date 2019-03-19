using Domain_Layer.Compensations;
using Domain_Layer.Appendices;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer.MenuItem
{
    class AddTrip : IMenuItem
    {
        private Driving DrivingCompensation;

        // Her oprettes en AddTrip, med 1 parametre
        public AddTrip(Driving drivingCompensation)
        {
            DrivingCompensation = drivingCompensation;
        }

        // Her bliver der udfra inputs fra brugeren oprettet en ny Trip. Derefter bliver den tilføjet til den aktive SmartMenu
        public bool Activate(SmartMenu smartMenu)
        {
            string title =  Request.String("Titel på bekostningen");
            string departureDestination = Request.String("Hvor kørte du fra?");
            DateTime departureDate = Request.DateTime(string.Format("Hvornår kørte du fra {0}?", departureDestination));
            string arrivalDestination = Request.String("Hvor kørte du til?");
            DateTime arrivalDate = Request.DateTime(string.Format("Hvornår kom du til {0}?", arrivalDestination));
            int distance = Request.Int("Hvor mange kilometer (i hele tal)?");

            Trip drivingExpense = new Trip(title, departureDestination, departureDate, arrivalDestination, arrivalDate, distance, DrivingCompensation);
            DrivingCompensation.AddAppendix(drivingExpense);

            smartMenu.Attach(new EditTrip(DrivingCompensation, drivingExpense));

            return false;
        }

        public override string ToString() => "Tilføj ny bekostning";
    }
}
