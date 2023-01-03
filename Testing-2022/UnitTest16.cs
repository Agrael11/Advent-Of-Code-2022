using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest16
    {
        private readonly string example;

        public UnitTest16()
        {
            example = new System.IO.StreamReader("Examples/Challange16/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day16.Challange1.DoChallange(example);
            Assert.IsTrue(result == 1651, $"Incorrect result! Expected:1651, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day16.Challange2.DoChallange(example);
            Assert.IsTrue(result == 1707, $"Incorrect result! Expected:1707, Got:{result}");
        }
    }
}