using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest11
    {
        private readonly string example;

        public UnitTest11()
        {
            example = new System.IO.StreamReader("Examples/Challange11/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day11.Challange1.DoChallange(example);
            Assert.IsTrue(result == 10605, $"Incorrect result! Expected:10605, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day11.Challange2.DoChallange(example);
            Assert.IsTrue(result == 2713310158, $"Incorrect result! Expected:2713310158, Got:{result}");
        }
    }
}