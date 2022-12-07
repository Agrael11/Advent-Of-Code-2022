using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest06
    {
        private readonly string example;

        public UnitTest06()
        {
            example = new System.IO.StreamReader("Examples/Challange06/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day06.Challange1.DoChallange(example);
            Assert.IsTrue(result == 7, $"Incorrect result! Expected:7, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day06.Challange2.DoChallange(example);
            Assert.IsTrue(result == 19, $"Incorrect result! Expected:19, Got:{result}");
        }
    }
}