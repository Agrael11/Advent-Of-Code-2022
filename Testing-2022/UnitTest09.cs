using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest09
    {
        private readonly string example1;
        private readonly string example2;

        public UnitTest09()
        {
            example1 = new System.IO.StreamReader("Examples/Challange09/example01.txt").ReadToEnd();
            example2 = new System.IO.StreamReader("Examples/Challange09/example02.txt").ReadToEnd();
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day09.Challange1.DoChallange(example1);
            Assert.IsTrue(result == 13, $"Incorrect result! Expected:13, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day09.Challange2.DoChallange(example2);
            Assert.IsTrue(result == 36, $"Incorrect result! Expected:36, Got:{result}");
        }
    }
}