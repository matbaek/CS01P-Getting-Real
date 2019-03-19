using Domain_Layer;
using Domain_Layer.Compensations;
using Presentation_Layer;
using SmartMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer
{
    class ShowAllCompensations : IMenuItem
    {
        private AccessPoint accessPoint;

        // Her oprettes en ShowAllCompensations, med 1 parametre
        public ShowAllCompensations(AccessPoint accessPoint)
        {
            this.accessPoint = accessPoint;
        }

        // Her aktiveres en SmartMenu med alle Compensations (godtgørelser)
        public bool Activate(SmartMenu smartMenu)
        {
            List<Compensation> compensations = accessPoint.GetAllCompensations();

            SmartMenu sm = new SmartMenu("Alle godtgørelser", "Tilbage");

            foreach (Compensation compensation in compensations)
            {
                sm.Attach(new ShowCompensation(compensation));
            }

            sm.Activate();

            return false;
        }

        public override string ToString()
        {
            return "Vis alle godtgørelser";
        }
    }
}
