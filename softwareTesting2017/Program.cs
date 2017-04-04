using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softwareTesting2017
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = Int16.Parse(args[0]);
            Simulation mySim = new Simulation();
        }
    }

    class Simulation
    {
        private string[] locations = new string[] {"Mayfair", "Akina", "Stortford Lodge", "Mahora", "Outside City"};
        private string[] exits = new string[] { "Karamu Road", "Havelock Road", "Railway Road", "Omahu Road" };
        private int[] drivers = new int[5] { 1, 2, 3, 4, 5 };
        private int akinaCount = 0;

        /* 0 = Mayfair
         * 1 = Akina
         * 2 = Stortford Lodge
         * 3 = Mahora */
        private string[][] neighbourSuburbs = new string[4][]
        {
            new string[] {"Akina", "Mahora", "Outside City"},
            new string[] {"Mayfair", "Stortford Lodge", "Outside City"},
            new string[] {"Akina", "Mahora", "Outside City"},
            new string[] {"Stortford Lodge", "Mayfair", "Outside City"}
        };

        private string[][] streets = new string[4][]
        {
            new string[] {"Willowpark Road", "Riverslea Road"},
            new string[] {},
            new string[] {},
            new string[] {}
        };


        //Generates a random number from a provided seed
        int randomNumberGenerator()
        {
            int randomNumber = 0;
            return randomNumber;
        }

        
        void drive()
        {
            foreach(int driver in drivers)
            {

            }
        }

    }
}
