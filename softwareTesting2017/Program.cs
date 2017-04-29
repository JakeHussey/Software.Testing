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
            int seed = 2; //Int16.Parse(args[0]);

            Simulation mySim = new Simulation(seed);
            mySim.startSimulation();

            Console.WriteLine(mySim.getFinalResult());


            





            //hold console open
            Console.ReadLine();


        }


    }


    class Simulation
    {
        
        //Matrix of streets and roads connecting locations and outside city
        string[,] adjMatrix = new string[4, 5]
        {
                //{Mayfair, Mahora, Akina, Stortford Lodge, Outside City}
                {"notConnected", "Frederick St", "Willowpark Rd", "notConnected", "Karamu Rd"},
                {"Frederick St", "notConnected", "notConnected", "Tomoana Rd", "Omahu Rd"},
                {"Willowpark Rd", "notConnected", "notConnected", "Southampton St", "Havelock Rd"},
                {"notConnected", "Tomoana Rd", "Southampton St", "notConnected", "Railway Rd"}
        };

        Driver[] drivers = new Driver[5];
        string completeSimulation = "";
        Random rand;




        public Simulation(int seed)
        {
            this.rand = new Random(seed);
            drivers[0] = new Driver("Driver 1");
            drivers[1] = new Driver("Driver 2");
            drivers[2] = new Driver("Driver 3");
            drivers[3] = new Driver("Driver 4");
            drivers[4] = new Driver("Driver 5");
        }

        public void startSimulation()
        {
            int nextLocation;         

            for(int x = 0; x < drivers.Length; x++)
            {
                drivers[x].startLocation(nextRandom(3));

                for (int i = 0; i < 10; i++)
                {
                    
                    completeSimulation += drivers[x].getName() + "\n";
                    completeSimulation += "Start location " + drivers[x].getStartLocation() + "\n";
                    completeSimulation += "Next location " + drivers[x].drive(adjMatrix, nextRandom(4))[0] + "\n";
                    completeSimulation += "via " + drivers[x].drive(adjMatrix, nextRandom(4))[1] + "\n" + "\n";
                   
                    
                }


                
            }
        }

        public int nextRandom(int max)
        {
            return rand.Next(0, max);
        }


        public string getFinalResult()
        {
            return completeSimulation;
        }


        


        
    }







    class Driver
    {
        string name;
        int currentLocation;



        public Driver(string name)
        {
            this.name = name;
        }

        public void startLocation(int randomNumber)
        {
            this.currentLocation = randomNumber;
        }


        public string getName()
        {
            return name;
        }

        public string getStartLocation()
        {
            return locationString(currentLocation);
        }



        //Pick a new location and a path to that location
        public string[] drive(string[,] matrix, int randomNumber)
        {
            string[] newLocation = new string[2];
            int[] possiblePaths = new int[5];
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

 


