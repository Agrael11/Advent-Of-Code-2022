using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest13
    {
        private readonly string example;

        public UnitTest13()
        {
            example = new System.IO.StreamReader("Examples/Challange13/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day13.Challange1.DoChallange(example);
            Assert.IsTrue(result == 13, $"Incorrect result! Expected:13, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day13.Challange2.DoChallange(example);
            Assert.IsTrue(result == 140, $"Incorrect result! Expected:140, Got:{result}");
        }
    }
}