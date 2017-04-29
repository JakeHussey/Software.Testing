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
            //int seed = Int16.Parse(args[0]);

            Simulation mySim = new Simulation();


            /*
             * 0 = Mayfair
             * 1 = Mahora
             * 2 = Akina
             * 3 = Stortford Lodge
             * 4 = Outside City
             * 
             * Matrix of streets and roads connecting locations and outside city
             * */
            string[,] adjMatrix = new string[4, 5]
            {
                {"notConnected", "Frederick St", "Willowpark Rd", "notConnected", "Karamu Rd"},
                {"Frederick St", "notConnected", "notConnected", "Tomoana Rd", "Omahu Rd"},
                {"Willowpark Rd", "notConnected", "notConnected", "Southampton St", "Havelock Rd"},
                {"notConnected", "Tomoana Rd", "Southampton St", "notConnected", "Railway Rd"}
            };


            int startLoc = mySim.startLocation();

            Console.WriteLine("Go from " + mySim.locationString(startLoc));

            //mySim.path(startLocation, matrix, randomNumber for picking next location) array element
            Console.WriteLine("to " + mySim.path(startLoc, adjMatrix, 2)[0]);
            Console.WriteLine("via " + mySim.path(startLoc, adjMatrix, 2)[1]);

            //hold console open
            Console.ReadLine();


        }


    }


    class Simulation
    {
        public int startLocation()
        {
            return 0;
        }



        //Pick a new location and a path to that location
        public string[] path(int currentLocation, string[,] matrix, int randomNumber)
        {
            string[] newLocation = new string[2];
            int[] possiblePaths = new int[3];
            int index = 0;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[currentLocation, i] != "notConnected")
                {
                    possiblePaths[index] = i;
                    index++;
                }
            }

            newLocation[0] = locationString(possiblePaths[randomNumber]);
            newLocation[1] = matrix[currentLocation, possiblePaths[randomNumber]];

            return newLocation;
        }


        //Convert the location integer to its relative string
        public string locationString(int location)
        {
            string stringLocation;

            switch (location)
            {
                case 0:
                    stringLocation = "Mayfair";
                    break;

                case 1:
                    stringLocation = "Mahora";
                    break;

                case 2:
                    stringLocation = "Akina";
                    break;

                case 3:
                    stringLocation = "Stortford Lodge";
                    break;

                case 4:
                    stringLocation = "Outside City";
                    break;

                default:
                    stringLocation = "";
                    break;
            }

            return stringLocation;
        }
    }
}

 


