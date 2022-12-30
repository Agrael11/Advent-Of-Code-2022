using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest14
    {
        private readonly string example;

        public UnitTest14()
        {
            example = new System.IO.StreamReader("Examples/Challange14/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day14.Challange1.DoChallange(example);
            Assert.IsTrue(result == 24, $"Incorrect result! Expected:24, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day14.Challange2.DoChallange(example);
            Assert.IsTrue(result == 93, $"Incorrect result! Expected:93, Got:{result}");
        }
    }
}