using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing_2021
{
    [TestClass]
    public class UnitTest10
    {
        private readonly string example;
        private readonly string solution;

        public UnitTest10()
        {
            example = new System.IO.StreamReader("Examples/Challange10/example01.txt").ReadToEnd();
            solution = new System.IO.StreamReader("Examples/Challange10/result01.txt").ReadToEnd().Replace("\r\n","\n");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day10.Challange1.DoChallange(example);
            Assert.IsTrue(result == 13140, $"Incorrect result! Expected:13140, Got:{result}");
        }


        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day10.Challange2.DoChallange(example);
            Assert.IsTrue(result == solution, $"Incorrect result! Expected:\n{solution}\nGot:\n{result}\n");
        }
    }
}