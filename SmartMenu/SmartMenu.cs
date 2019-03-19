using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenuLibrary
{
    public class SmartMenu
    {
        private string Title;
        private string ExitDescription;
        private string Description;
        private List<IMenuItem> MenuItems = new List<IMenuItem>();

        // Her oprettes en SmartMenu, med 2-3 parametre
        public SmartMenu(string title, string exitDescription, string description = "")
        {
            Title = title;
            ExitDescription = exitDescription;
            Description = description;
        }

        // Her returneres MenuItems listen
        public List<IMenuItem> GetAllMenuItems()
        {
            return MenuItems;
        }

        // Her tilføjes et menu punkt til MenuItems
        public void Attach(IMenuItem menuItem)
        {
            MenuItems.Add(menuItem);
        }

        // Her fjernes et menu punkt fra MenuItems
        public void Detach(IMenuItem menuItem)
        {
            MenuItems.Remove(menuItem);
        }

        // Her printes SmartMenu 
        public void Activate()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                // Her tjekkes om Title er tom
                if (Title != null && Title != string.Empty)
                {
                    ConsoleSpace();
                    Console.WriteLine(Title);
                }

                // Her tjekkes om Description er tom
                if (Description != null && Description != string.Empty)
                {
                    ConsoleSpace();
                    Console.WriteLine(Description);
                }

                ConsoleSpace();

                // Her bliver der lavet et for loop, som printer hver MenuItems linje der er
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    Console.WriteLine(i + 1 + " -> " + MenuItems[i]);
                }
                Console.WriteLine("0 -> " + ExitDescription);

                ConsoleSpace();
                Console.Write("-> ");
                string input = Console.ReadLine();

                // Her tjekkes om det er en int der er blevet skrevet
                if (int.TryParse(input, out int inputInt))
                {
                    // Her bliver der tjekket om input er 0 eller et af MenuItems linjer
                    if (inputInt == 0)
                    {
                        exit = true;
                    }
                    else if (inputInt > 0 && inputInt <= MenuItems.Count)
                    {
                        exit = MenuItems[inputInt - 1].Activate(this);
                    }
                }
                Console.Clear();
            }
        }

        private void ConsoleSpace() => Console.WriteLine("----------------");
    }
}
