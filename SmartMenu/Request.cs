using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMenuLibrary
{
    public static class Request
    {
        // Hele denne klasse er lavet for at gøre det nemmere at tjekke om de indtastet information er i rigtig format og at der ikke kommer en NullPointerException
        // I hver metoder for man en parameter i form af string ind, fra Console.ReadLine(), hvorefter den printer parameteren ud, og venter på at brugeren skriver en string ind i console.
        // Derefter bliver det forskelligt fra hver metode, hvordan variablen fra Console.ReadLine() bliver lavet om

        public static string String(string request)
        {
            string requested = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine(request);
                requested = Console.ReadLine();
            } while (requested == "");

            return requested;
        }

        public static int Int(string request)
        {
            string requested = string.Empty;
            int output;
            do
            {
                Console.Clear();
                Console.WriteLine(request);
                requested = Console.ReadLine();
            } while (!int.TryParse(requested, out output));

            return output;
        }

        public static T Enum<T>(string request) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            string requested = string.Empty;
            T output;
            do
            {
                Console.Clear();
                Console.WriteLine(string.Format("{0}\n({1})", request, string.Join(", ", System.Enum.GetNames(typeof(T)))));
                requested = Console.ReadLine();
            } while (!System.Enum.TryParse(requested, out output) || !System.Enum.IsDefined(typeof(T), output));
            return output;
        }

        public static double Double(string request)
        {
            string requested = string.Empty;
            double output;
            do
            {
                Console.Clear();
                Console.WriteLine(request);
                requested = Console.ReadLine();
            } while (!double.TryParse(requested, out output));

            return output;
        }

        public static DateTime DateTime(string request)
        {
            string requested = string.Empty;
            DateTime output;
            do
            {
                Console.Clear();
                Console.WriteLine(request + " (DD-MM-YYYY HH:MM)");
                requested = Console.ReadLine();
            } while (!System.DateTime.TryParse(requested, out output));

            return output;
        }

        public static bool Bool(string request)
        {
            string requested = string.Empty;
            bool? output = null;
            do
            {
                Console.Clear();
                Console.WriteLine(request + " (Y/N)");
                requested = Console.ReadLine();

                switch (requested)
                {
                    case "Y":
                    case "y":
                        output = true;
                        break;
                    case "N":
                    case "n":
                        output = false;
                        break;
                }

            } while (output == null);

            return (bool)output;
        }
    }
}
