using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UnitTestTools;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestTools.StartTest("001");

            Console.WriteLine("Dieser Test erwartet, dass in SmallSteps003.Program.result_addition das Ergebnis der Addition von 6+3 abgelegt wird.");
            SmallSteps003.Program.Main(null);
            Assert.AreEqual(9, SmallSteps003.Program.result_addition);
            TestTools.EndTest("001");
        }
        [TestMethod]
        public void TestMethod2()
        {
            TestTools.StartTest("002");

            Console.WriteLine("Dieser Test erwartet, dass in SmallSteps003.Program.result_substraction das Ergebnis der Substraktion von 6-3 abgelegt wird.");
            SmallSteps003.Program.Main(null);
            Assert.AreEqual(3, SmallSteps003.Program.result_substraction);
            TestTools.EndTest("002");
        }
        [TestMethod]
        public void TestMethod3()
        {
            TestTools.StartTest("003");

            Console.WriteLine("Dieser Test erwartet, dass in SmallSteps003.Program.result_multiply das Ergebnis der Multiplikation von 6*3 abgelegt wird.");
            SmallSteps003.Program.Main(null);
            Assert.AreEqual(18, SmallSteps003.Program.result_multiply);
            TestTools.EndTest("003");
        }
        [TestMethod]
        public void TestMethod4()
        {
            TestTools.StartTest("004");

            Console.WriteLine("Dieser Test erwartet, dass in SmallSteps003.Program.result_multiply das Ergebnis der Divison von 6/3 abgelegt wird.");
            SmallSteps003.Program.Main(null);
            Assert.AreEqual(2, SmallSteps003.Program.result_division);
            TestTools.EndTest("004");
        }
        [TestMethod]
        public void TestMethod5()
        {
            TestTools.StartTest("005");
            string expected = "Der Rechenking sagt 6 + 3 = 9" + Environment.NewLine;
            expected += "Der Rechenking sagt 6 - 3 = 3" + Environment.NewLine;
            expected += "Der Rechenking sagt 6 * 3 = 18" + Environment.NewLine;
            expected += "Der Rechenking sagt 6 / 3 = 2" + Environment.NewLine;
            Console.WriteLine("Es wird erwarted, dass der Output von Program folgender ist:{0}{1}",
                Environment.NewLine, expected);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            SmallSteps003.Program.Main(null);
            Assert.AreEqual(expected, sw.ToString());
            TestTools.EndTest("005");
        }
        [TestMethod]
        public void TestMethod999() {
            Assert.AreEqual(true, TestTools.AllTests());
        }

    }
}
