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
            AdjacencyList locationAdjacency = new AdjacencyList(4);
            Simulation mySim = new Simulation(locationAdjacency);    
        }
    }

    class AdjacencyList
    {
        LinkedList<Tuple<int, int>>[] adjacencyList;

        // Constructor - creates an empty Adjacency List
        public AdjacencyList(int vertices)
        {
            adjacencyList = new LinkedList<Tuple<int, int>>[vertices];

            for (int i = 0; i < adjacencyList.Length; ++i)
            {
                adjacencyList[i] = new LinkedList<Tuple<int, int>>();
            }
        }

        // Appends a new Edge to the linked list
        public void addEdgeAtEnd(int startVertex, int endVertex, int weight)
        {
            adjacencyList[startVertex].AddLast(new Tuple<int, int>(endVertex, weight));
        }

        // Adds a new Edge to the linked list from the front
        public void addEdgeAtBegin(int startVertex, int endVertex, int weight)
        {
            adjacencyList[startVertex].AddFirst(new Tuple<int, int>(endVertex, weight));
        }

        // Returns number of vertices
        // Does not change for an object
        public int getNumberOfVertices()
        {
            return adjacencyList.Length;
        }

        // Returns a copy of the Linked List of outward edges from a vertex
        public LinkedList<Tuple<int, int>> this[int index]
        {
            get
            {
                LinkedList<Tuple<int, int>> edgeList
                               = new LinkedList<Tuple<int, int>>(adjacencyList[index]);

                return edgeList;
            }
        }

        // Prints the Adjacency List
        public void printAdjacencyList()
        {
            int i = 0;

            foreach (LinkedList<Tuple<int, int>> list in adjacencyList)
            {
                Console.Write("adjacencyList[" + i + "] -> ");

                foreach (Tuple<int, int> edge in list)
                {
                    Console.Write(edge.Item1 + "(" + edge.Item2 + ")");
                }

                ++i;
                Console.WriteLine();
            }
        }

        // Removes the first occurence of an edge and returns true
        // if there was any change in the collection, else false
        public bool removeEdge(int startVertex, int endVertex, int weight)
        {
            Tuple<int, int> edge = new Tuple<int, int>(endVertex, weight);

            return adjacencyList[startVertex].Remove(edge);
        }
    }

    class Simulation
    {
        //private string[] locations = new string[] {"Mayfair", "Akina", "Stortford Lodge", "Mahora", "Outside City"};
        //private string[] exits = new string[] { "Karamu Road", "Havelock Road", "Railway Road", "Omahu Road" };
        //private int[] drivers = new int[5] { 1, 2, 3, 4, 5 };
        //private int akinaCount = 0;
        public Simulation(AdjacencyList myList)
        {
            /* 0 = Mayfair
            * 1 = Akina
            * 2 = Stortford Lodge
            * 3 = Mahora */
            
            myList.addEdgeAtBegin(0, 1, 0);
            myList.addEdgeAtBegin(0, 3, 1);
            myList.addEdgeAtBegin(1, 0, 2);
            myList.addEdgeAtBegin(1, 2, 3);
            myList.addEdgeAtBegin(2, 1, 4);
            myList.addEdgeAtBegin(2, 3, 5);
            myList.addEdgeAtBegin(3, 0, 6);
            myList.addEdgeAtBegin(3, 2, 7);
            myList.printAdjacencyList();
        }


        //Generates a random number from a provided seed
        public int randomNumberGenerator()
        {
            int randomNumber = 0;
            return randomNumber;
        }

    }
}
