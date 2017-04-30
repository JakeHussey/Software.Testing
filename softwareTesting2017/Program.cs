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
            int seed = 0; //Int16.Parse(args[0]);


            //Construct the simulation with the provided seed
            Simulation mySim = new Simulation(seed);
            mySim.startSimulation();



            //Write the end results to the console
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
                      /* {Mayfair}         {Mahora}          {Akina}    {Stortford Lodge} {Outside City} */
           /*{Mayfair}*/{"notConnected", "Frederick St", "Willowpark Rd", "notConnected", "Karamu Rd"},
            /*{Mahora}*/{"Frederick St", "notConnected", "notConnected", "Tomoana Rd", "Omahu Rd"},
             /*{Akina}*/{"Willowpark Rd", "notConnected", "notConnected", "Southampton St", "Havelock Rd"},
   /*{Stortford Lodge}*/{"notConnected", "Tomoana Rd", "Southampton St", "notConnected", "Railway Rd"}
        };

        Driver[] drivers = new Driver[5];
        Random rand;
        string completeSimulation = ""; //Output back to Main function
        



        //Initialize drivers and create a Random from the seed
        public Simulation(int seed)
        {
            this.rand = new Random(seed);
            drivers[0] = new Driver("Driver 1");
            drivers[1] = new Driver("Driver 2");
            drivers[2] = new Driver("Driver 3");
            drivers[3] = new Driver("Driver 4");
            drivers[4] = new Driver("Driver 5");
        }


        //Calls Driver methods and provides "random" numbers
        public void startSimulation()
        {        
            //for each driver
            for(int x = 0; x < drivers.Length; x++)
            {
                drivers[x].startLocation(nextRandom(3));
                completeSimulation += drivers[x].getName() + "\n";
                completeSimulation += "Start location " + drivers[x].getStartLocation() + "\n";

                //Drive to a new location 10 times
                for (int i = 0; i < 10; i++)
                {
                    string[] drive = drivers[x].drive(adjMatrix, nextRandom(3)); //Store drive output so it can be used without calling another random number

                    completeSimulation += "Next location " + drive[0] + "\n";
                    completeSimulation += "via " + drive[1] + "\n";

                    //Stop if Outside City
                    if (drive[0] == "Outside City")
                    {
                        break;
                    }
                   
                    
                }


                completeSimulation += "\n";


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
        int currentLocation; //Saves location to provide starting point for each drive



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
            int[] possiblePaths = new int[4];
            int index = 0;



            //Create an array of possible paths/locations to chose from
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[currentLocation, i] != "notConnected")
                {
                    possiblePaths[index] = i;
                    index++;
                }
            }


            
            //Randomly select path/location and generate output
            newLocation[0] = locationString(possiblePaths[randomNumber]);
            newLocation[1] = matrix[currentLocation, possiblePaths[randomNumber]];


            //Set the current location to the new location for next drive()
            currentLocation = possiblePaths[randomNumber];

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

 


