using Microsoft.VisualStudio.TestTools.UnitTesting;
using softwareTesting2017;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace softwareTesting2017.Tests
{
    [TestClass()]
    public class DriverTests
    {

        [TestMethod()]
        public void locationToStringExpectedTest()
        {
            Driver testDriver = new Driver("myName");

            Assert.AreEqual("Mayfair", testDriver.locationToString(0));
            Assert.AreEqual("Mahora", testDriver.locationToString(1));
            Assert.AreEqual("Akina", testDriver.locationToString(2));
            Assert.AreEqual("Stortford Lodge", testDriver.locationToString(3));
            Assert.AreEqual("Outside City", testDriver.locationToString(4));

        }

        [TestMethod()]
        public void locationToStringBadTest()
        {
            Driver testDriver = new Driver("myName");

            testDriver.startLocation(-1);
            Assert.AreEqual("", testDriver.getCurrentLocation());

            testDriver.startLocation(5);
            Assert.AreEqual("", testDriver.getCurrentLocation());
        }

//------------------------------------------------------------------------------------------------------------


        [TestMethod()]
        public void startLocationExpectedTest()
        {
            Driver testDriver = new Driver("myName");

            testDriver.startLocation(0);
            Assert.AreEqual("Mayfair", testDriver.getCurrentLocation());

            testDriver.startLocation(1);
            Assert.AreEqual("Mahora", testDriver.getCurrentLocation());

            testDriver.startLocation(2);
            Assert.AreEqual("Akina", testDriver.getCurrentLocation());
            Assert.AreEqual("1", testDriver.getAkinaCount());

            testDriver.startLocation(3);
            Assert.AreEqual("Stortford Lodge", testDriver.getCurrentLocation());

        }

        [TestMethod()]
        public void startLocationBadTest()
        {
            Driver testDriver = new Driver("myName");

            testDriver.startLocation(-5);
            Assert.AreEqual("", testDriver.getCurrentLocation());
            Assert.AreEqual("0", testDriver.getAkinaCount());

            testDriver.startLocation(7);
            Assert.AreEqual("", testDriver.getCurrentLocation());
            Assert.AreEqual("0", testDriver.getAkinaCount());

        }

//-----------------------------------------------------------------------------------------------------------

        [TestMethod()]
        public void akinaVisitsExpectedTest()
        {
            Driver testDriver = new Driver("myName");

            testDriver.setAkinaCount(0);
            Assert.AreEqual("myName met with John Jamieson 0 time(s). \nThat passenger missed out! \n", testDriver.akinaVisits());

            testDriver.setAkinaCount(3);
            Assert.AreEqual("myName met with John Jamieson 3 time(s). \nThis driver needed lots of help! \n", testDriver.akinaVisits());

            testDriver.setAkinaCount(2);
            Assert.AreEqual("myName met with John Jamieson 2 time(s). \n", testDriver.akinaVisits());

        }

        [TestMethod()]
        public void akinaVisitsBadTest()
        {
            Driver testDriver = new Driver("myName");

            testDriver.setAkinaCount(-1);
            Assert.AreEqual("", testDriver.akinaVisits());

        }


        //---------------------------------------------------------------------------------------------------------------------------



        [TestMethod()]
        public void iterationExpectedTest()
        {
            Simulation testSim = new Simulation(0);
            Driver testDriver = new Driver("myName");
            string[,] adjMatrix = testSim.getMatrix();
            string[] testData;

            //Mayfair to Mahora
            testDriver.setAkinaCount(0);
            testDriver.startLocation(0);
            testData = testDriver.iteration(adjMatrix, 0);
            Assert.AreEqual("Mahora", testData[0]);
            Assert.AreEqual("Frederick St", testData[1]);
            Assert.AreEqual("0", testDriver.getAkinaCount());


            //Mayfair to Akina
            testDriver.setAkinaCount(0);
            testDriver.startLocation(0);
            testData = testDriver.iteration(adjMatrix, 1);
            Assert.AreEqual("Akina", testData[0]);
            Assert.AreEqual("Willowpark Rd", testData[1]);
            Assert.AreEqual("1", testDriver.getAkinaCount());


            //Akina to Stortford Lodge
            testDriver.setAkinaCount(0);
            testDriver.startLocation(2);
            testData = testDriver.iteration(adjMatrix, 1);
            Assert.AreEqual("Stortford Lodge", testData[0]);
            Assert.AreEqual("Southampton St", testData[1]);
            Assert.AreEqual("1", testDriver.getAkinaCount());


            //Akina to Outside City
            testDriver.setAkinaCount(0);
            testDriver.startLocation(2);
            testData = testDriver.iteration(adjMatrix, 2);
            Assert.AreEqual("Outside City", testData[0]);
            Assert.AreEqual("Willowpark Rd then Havelock Rd", testData[1]);
            Assert.AreEqual("1", testDriver.getAkinaCount());


            //Mayfair to Outside City
            testDriver.setAkinaCount(0);
            testDriver.startLocation(0);
            testData = testDriver.iteration(adjMatrix, 2);
            Assert.AreEqual("Outside City", testData[0]);
            Assert.AreEqual("Frederick St then Karamu Rd\nmyName has gone to Napier.", testData[1]);
            Assert.AreEqual("0", testDriver.getAkinaCount());


            //Mahora to Outside City
            testDriver.setAkinaCount(0);
            testDriver.startLocation(1);
            testData = testDriver.iteration(adjMatrix, 2);
            Assert.AreEqual("Outside City", testData[0]);
            Assert.AreEqual("Tomoana Rd then Omahu Rd\nmyName has gone to Flaxmere.", testData[1]);
            Assert.AreEqual("0", testDriver.getAkinaCount());
        }



        [TestMethod()]
        public void iterationBadTest()
        {
            Simulation testSim = new Simulation(0);
            Driver testDriver = new Driver("myName");
            string[,] adjMatrix = testSim.getMatrix();

            string[] testData = testDriver.iteration(adjMatrix, -1);


            Assert.AreEqual(null, testData[0]);
            Assert.AreEqual(null, testData[1]);

        }


    }
}