namespace AdventOfCode.Day02
{
    /// <summary>
    /// Main Class for Challange 2
    /// </summary>
    public static class Challange2
    {
        //Define Shapes and Results, with their score values
        public enum Shape { Rock = 1, Paper = 2, Scissors = 3 };
        public enum Result { Lost = 0, Draw = 3, Win = 6 };

        //Lookup tables to translate data
        private static readonly Dictionary<char, Shape> ShapeTable = new()
        { { 'A', Shape.Rock}, { 'B', Shape.Paper}, { 'C', Shape.Scissors} };
        private static readonly Dictionary<char, Result> ResultTable = new()
        { { 'X', Result.Lost}, { 'Y', Result.Draw}, { 'Z', Result.Win} };

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Initialize score variable
            int score = 0;

            foreach (string line in inputData)
            {
                //Decode inputs using ShapeTable and ResultTable. Inputs on line are separated by space character, first is opponents shape, second is winning status
                string[] plays = line.Split(' ');
                Shape opponent = ShapeTable[plays[0][0]];
                Result result = ResultTable[plays[1][0]];
                //Select shape from expected winning status
                Shape you = SelectShapeByResult(opponent, result);
                //And add both values to score
                score += (int)you;
                score += (int)result;

            }

            return score;
        }

        /// <summary>
        /// Selects used Shape depending on expected winning status
        /// </summary>
        /// <param name="opponent"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Shape SelectShapeByResult(Shape opponent, Result result)
        {
            return opponent switch
            {
                Shape.Rock => result switch
                {
                    Result.Lost => Shape.Scissors,
                    Result.Win => Shape.Paper,
                    _ => Shape.Rock,
                },
                Shape.Paper => result switch
                {
                    Result.Lost => Shape.Rock,
                    Result.Win => Shape.Scissors,
                    _ => Shape.Paper,
                },
                Shape.Scissors => result switch
                {
                    Result.Lost => Shape.Paper,
                    Result.Win => Shape.Rock,
                    _ => Shape.Scissors,
                },
                _ => opponent,
            };
        }
    }
}