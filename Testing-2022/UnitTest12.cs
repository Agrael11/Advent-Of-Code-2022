using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest12
    {
        private readonly string example;

        public UnitTest12()
        {
            example = new System.IO.StreamReader("Examples/Challange12/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day12.Challange1.DoChallange(example);
            Assert.IsTrue(result == 31, $"Incorrect result! Expected:31, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day12.Challange2.DoChallange(example);
            Assert.IsTrue(result == 29, $"Incorrect result! Expected:29, Got:{result}");
        }
    }
}