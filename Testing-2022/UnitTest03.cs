using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest03
    {
        private readonly string example;

        public UnitTest03()
        {
            example = new System.IO.StreamReader("Examples/Challange03/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day03.Challange1.DoChallange(example);
            Assert.IsTrue(result == 157, $"Incorrect result! Expected:157, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day03.Challange2.DoChallange(example);
            Assert.IsTrue(result == 70, $"Incorrect result! Expected:70, Got:{result}");
        }
    }
}