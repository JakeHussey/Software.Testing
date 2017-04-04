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
            Simulation mySim = new Simulation();
        }
    }

    class Simulation
    {
        private string[] locations = new string[5] {"Mayfair", "Akina", "Stortford Lodge", "Mahora", "Outside City"};
        private string[] exits = new string[4] { "Karamu Road", "Havelock Road", "Railway Road", "Omahu Road" };
        private string[] streets;
        private int[] drivers = new int[5] { 1, 2, 3, 4, 5 };
        private int akinaCount = 0;

        int randomNumberGenerator(int seed)
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
