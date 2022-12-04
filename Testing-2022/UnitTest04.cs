using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest04
    {
        private readonly string example;

        public UnitTest04()
        {
            example = new System.IO.StreamReader("Examples/Challange04/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day04.Challange1.DoChallange(example);
            Assert.IsTrue(result == 2, $"Incorrect result! Expected:2, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day04.Challange2.DoChallange(example);
            Assert.IsTrue(result == 4, $"Incorrect result! Expected:4, Got:{result}");
        }
    }
}