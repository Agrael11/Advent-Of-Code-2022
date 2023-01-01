using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest15
    {
        private readonly string example;

        public UnitTest15()
        {
            example = new System.IO.StreamReader("Examples/Challange15/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day15.Challange1.DoChallange(example, 10);
            Assert.IsTrue(result == 26, $"Incorrect result! Expected:26, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day15.Challange2.DoChallange(example, 20);
            Assert.IsTrue(result == 56000011, $"Incorrect result! Expected:56000011, Got:{result}");
        }
    }
}