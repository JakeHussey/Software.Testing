using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace softwareTesting2017
{
    public class Program
    {
        static void Main(string[] args)
        {
            int seed = 0;

            Console.WriteLine("Enter a number between 0 and 9999 (inclusive): ");

            string input = Console.ReadLine();
            if (input != "" && int.TryParse(input, out seed))
            {
                if (seed >= 0 && seed <= 9999)
                {
                    Console.Clear();

                    //Construct the simulation with the provided seed
                    Simulation mySim = new Simulation(seed);

                    //start the driver loop
                    mySim.startSimulation();

                    //hold console open
                    Console.ReadLine();
                }
                else
                {
                    Console.Error.WriteLine("Error, value either too high or too low.");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.Error.WriteLine("Error, value is blank or not a number.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

        }


    }


    public class Simulation
    {
        
        //Matrix of streets and roads connecting locations and outside city
        string[,] adjMatrix = new string[4, 5]
        {
                              /* {Mayfair}         {Mahora}          {Akina}    {Stortford Lodge} {Outside City} */
                   /*{Mayfair}*/{"notConnected", "Frederick St", "Willowpark Rd", "notConnected", "Frederick St then Karamu Rd"},
                    /*{Mahora}*/{"Frederick St", "notConnected", "notConnected", "Tomoana Rd", "Tomoana Rd then Omahu Rd"},
                     /*{Akina}*/{"Willowpark Rd", "notConnected", "notConnected", "Southampton St", "Willowpark Rd then Havelock Rd"},
           /*{Stortford Lodge}*/{"notConnected", "Tomoana Rd", "Southampton St", "notConnected", "Southampton St then Railway Rd"}
        };

        Driver[] drivers = new Driver[5];
        Random rand;
        

        public string[,] getMatrix()
        {
            return adjMatrix;
        }


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
        public void startSimulation()
        {
            //For each driver
            for (int driver = 0; driver < drivers.Length; driver++)
            {
                string singleDrive = "";
                bool exit = false;
                int exitCounter = 0;

                drivers[driver].startLocation(nextRandom(3));
                singleDrive += drivers[driver].getName();
                singleDrive += " has started in " + drivers[driver].getCurrentLocation() + "\n";

                //Drive to a new location until Outside City is reached
                while (!exit)
                {
                    //drive[0] = location
                    //drive[1] = path
                    string[] singleIteration;
                    string currentLocation = drivers[driver].getCurrentLocation();

                    if (exitCounter >= 2) //Decrease the chance of entering Outside City
                    {
                        //Generate output
                        singleIteration = drivers[driver].iteration(adjMatrix, nextRandom(3));
                        exitCounter = 0;
                    }
                    else
                    {
                        //Generate output
                        singleIteration = drivers[driver].iteration(adjMatrix, nextRandom(2));
                        exitCounter++;
                    }

                    //Format output
                    singleDrive += drivers[driver].getName() + " is heading from " + currentLocation + " to " + singleIteration[0] + " ";
                    singleDrive += "via " + singleIteration[1] + "\n";

                    //Stop if Outside City
                    if (singleIteration[0] == "Outside City")
                    {
                        exit = true;
                    }


                }

                singleDrive += drivers[driver].akinaVisits();
                singleDrive += "-----";
                singleDrive += "\n";

                Console.WriteLine(singleDrive);
            }

        }


        //random number generator with a max value
        public int nextRandom(int max)
        {
            return rand.Next(0, max);
        }


        
    }







    public class Driver
    {
        string name;
        int currentLocation = -1;
        int akinaCounter = 0;



        public Driver(string name)
        {
            this.name = name;
        }

        //Set the drivers starting location
        public void startLocation(int randomNumber)
        {
            if(randomNumber >= 0 && randomNumber <= 4)
            {
                this.currentLocation = randomNumber;

                if (locationToString(randomNumber) == "Akina")
                {
                    akinaCounter++;
                }
            }
            else
            {
                Console.Error.WriteLine("An out of bounds number was used as the startlocation of " + name);
            }
            

            
        }


        public string getName()
        {
            return name;
        }


        public string getCurrentLocation()
        {
            return locationToString(currentLocation);
        }


        public string getAkinaCount()
        {
            return akinaCounter.ToString();
        }

        public void setAkinaCount(int akinaCount)
        {
            this.akinaCounter = akinaCount;
        }



        //Pick a new location and a path to that location
        public string[] iteration(string[,] matrix, int randomNumber)
        {
            string[] newLocation = new string[2];
            int[] possiblePaths = new int[3];
            int index = 0;


            if(randomNumber >= 0 && randomNumber <= 3)
            {
                //Create an array of possible paths/locations to chose from the matrix
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    if (matrix[currentLocation, i] != "notConnected")
                    {
                        possiblePaths[index] = i;
                        index++;
                    }
                }

                //Randomly select path/location and generate output
                newLocation[0] = locationToString(possiblePaths[randomNumber]);
                newLocation[1] = matrix[currentLocation, possiblePaths[randomNumber]];


                //Other city extra lines
                if (newLocation[1].Contains("Karamu"))
                {
                    newLocation[1] += "\n" + name + " has gone to Napier.";
                }
                else if (newLocation[1].Contains("Omahu"))
                {
                    newLocation[1] += "\n" + name + " has gone to Flaxmere.";
                }



                if (newLocation[0] == "Akina")
                {
                    akinaCounter++;
                }


                //Set the current location to the new location for next drive()
                currentLocation = possiblePaths[randomNumber]; 

            }
            else
            {
                Console.Error.WriteLine("An out of bounds number was used at iteration() of " + name);
            }

            return newLocation;

        }


        //adds extra lines to the console dependant on Akina Visits
        public string akinaVisits()
        {
            string extraLines = "";

            if(Int16.Parse(getAkinaCount()) >= 0)
            {
                extraLines = getName() + " met with John Jamieson " + getAkinaCount() + " time(s). \n";

                if (akinaCounter == 0)
                {
                    extraLines += "That passenger missed out! \n";
                }
                else if (akinaCounter == 3)
                {
                    extraLines += "This driver needed lots of help! \n";
                }
            }
            else
            {
                Console.Error.WriteLine("An out of bounds number was used at akinaVisits() of " + name);
            }

            return extraLines;
        }


        //Convert the location integer to its relative string
        public string locationToString(int location)
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

 


