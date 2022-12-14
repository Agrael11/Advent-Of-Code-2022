using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest02
    {
        private readonly string example;

        public UnitTest02()
        {
            example = new System.IO.StreamReader("Examples/Challange02/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day02.Challange1.DoChallange(example);
            Assert.IsTrue(result == 15, $"Incorrect result! Expected:15, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day02.Challange2.DoChallange(example);
            Assert.IsTrue(result == 12, $"Incorrect result! Expected:12, Got:{result}");
        }
    }
}