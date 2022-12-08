using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest07
    {
        private readonly string example;

        public UnitTest07()
        {
            example = new System.IO.StreamReader("Examples/Challange07/example01.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day07.Challange1.DoChallange(example);
            Assert.IsTrue(result == 95437, $"Incorrect result! Expected:95437, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day07.Challange2.DoChallange(example);
            Assert.IsTrue(result == 24933642, $"Incorrect result! Expected:24933642, Got:{result}");
        }
    }
}