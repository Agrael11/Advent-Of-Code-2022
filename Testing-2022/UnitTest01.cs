using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest01
    {
        private readonly string example;

        public UnitTest01()
        {
            example = new System.IO.StreamReader("Examples/Challange01/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day01.Challange1.DoChallange(example);
            Assert.IsTrue(result == 24000, $"Incorrect result! Expected:24000, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day01.Challange2.DoChallange(example);
            Assert.IsTrue(result == 45000, $"Incorrect result! Expected:45000, Got:{result}");
        }
    }
}