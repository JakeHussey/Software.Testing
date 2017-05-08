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
            Console.WriteLine("Enter a number between 0 and 9999 (inclusive): ");

            //User input seed

            int seed = Int16.Parse(Console.ReadLine());


            Console.Clear();



            //Construct the simulation with the provided seed
            Simulation mySim = new Simulation(seed);

            for (int y = 0; y < 5; y++)
            {
                //start a single iteration with the provided driver
                Console.WriteLine(mySim.startIteration(y));
            }



            


            





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


        //Calls Driver methods, provides "random" numbers and assembles returned string
        public string startIteration(int driver)
        {
            string completeIteration = "";
            bool exit = false;
            drivers[driver].startLocation(nextRandom(3));
            completeIteration += drivers[driver].getName() + "\n";
            completeIteration += "Start location " + drivers[driver].getStartLocation() + "\n";

            //Drive to a new location until Outside City is reached
            while (!exit)
            {
                //drive[0] = location     drive[1] = path
                string[] drive = drivers[driver].drive(adjMatrix, nextRandom(3)); //Store drive output so it can be used without calling another random number

                completeIteration += "Next location " + drive[0] + "\n";
                completeIteration += "via " + drive[1] + "\n";

                //Stop if Outside City
                if (drive[0] == "Outside City")
                {
                    exit = true;
                }
                   
                    
            }

            completeIteration += drivers[driver].akinaVisits();

            completeIteration += "\n";

            return completeIteration;

        }



        public int nextRandom(int max)
        {
            return rand.Next(0, max);
        }


        
    }







    class Driver
    {
        string name;
        int currentLocation; //Saves location to provide starting point for each drive
        int akinaCounter = 0;



        public Driver(string name)
        {
            this.name = name;
        }

        public void startLocation(int randomNumber)
        {
            this.currentLocation = randomNumber;

            if(locationString(randomNumber) == "Akina")
            {
                akinaCounter++;
            }
        }


        public string getName()
        {
            return name;
        }


        public string getStartLocation()
        {
            return locationString(currentLocation);
        }


        public string getAkinaCount()
        {
            return akinaCounter.ToString();
        }



        //Pick a new location and a path to that location
        public string[] drive(string[,] matrix, int randomNumber)
        {
            string[] newLocation = new string[2];
            int[] possiblePaths = new int[3];
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

            if(newLocation[0] == "Akina")
            {
                akinaCounter++;
            }


            //Set the current location to the new location for next drive()
            currentLocation = possiblePaths[randomNumber];

            return newLocation;
        }


        public string akinaVisits()
        {
            string extraLines = getName() + " met with John Jamieson " + getAkinaCount() + " time(s). \n";

            if(akinaCounter == 0)
            {
                extraLines += "That passenger missed out! \n";
            }
            else if(akinaCounter == 3)
            {
                extraLines += "This driver needed lots of help! \n";
            }

            return extraLines;
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

 


