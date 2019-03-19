using Domain_Layer;
using Domain_Layer.Compensations;
using Presentation_Layer.MenuItem;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer
{
    class CreateDriving : IMenuItem
    {
        private AccessPoint accessPoint;

        // Her oprettes en CreateDriving, med 1 parametre
        public CreateDriving(AccessPoint accessPoint)
        {
            this.accessPoint = accessPoint;
        }

        // Her bliver der udfra inputs fra brugeren oprettet en ny Driving. Derefter bliver der lavet en ny SmartMenu
        public bool Activate(SmartMenu smartMenu)
        {
            string title = Request.String("Kørsels godtgørelse titel");
            string numberPlate = Request.String("Nummerplade");
            Driving drivingCompensation = new Driving(title, accessPoint.Employee, numberPlate);

            SmartMenu sm = new SmartMenu(drivingCompensation.Title, "Anullér");

            sm.Attach(new AddTrip(drivingCompensation));

            sm.Attach(new AddCompensationToDepartment(accessPoint.Department, drivingCompensation));
            
            sm.Activate();
            
            return false;
        }

        public override string ToString()
        {
            return "Opret kørsels godtgørelse";
        }
    }
}
