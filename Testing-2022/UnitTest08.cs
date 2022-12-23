using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest08
    {
        private readonly string example;

        public UnitTest08()
        {
            example = new System.IO.StreamReader("Examples/Challange08/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day08.Challange1.DoChallange(example);
            Assert.IsTrue(result == 21, $"Incorrect result! Expected:21, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day08.Challange2.DoChallange(example);
            Assert.IsTrue(result == 8, $"Incorrect result! Expected:8, Got:{result}");
        }
    }
}