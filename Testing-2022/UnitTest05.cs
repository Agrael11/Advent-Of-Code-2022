using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest05
    {
        private readonly string example;

        public UnitTest05()
        {
            example = new System.IO.StreamReader("Examples/Challange05/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day05.Challange1.DoChallange(example);
            Assert.IsTrue(result == "CMZ", $"Incorrect result! Expected:CMZ, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day05.Challange2.DoChallange(example);
            Assert.IsTrue(result == "MCD", $"Incorrect result! Expected:MCD, Got:{result}");
        }
    }
}