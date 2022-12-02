namespace AdventOfCode.Day02
{
    /// <summary>
    /// Main Class for Challange 1
    /// </summary>
    public static class Challange1
    {
        //Define Shapes and Results, with their score values
        public enum Shape { Rock = 1, Paper = 2, Scissors = 3 };
        public enum Result { Lost = 0, Draw = 3, Win = 6 };

        //Lookup table to translate data
        private static readonly Dictionary<char, Shape> ShapeTable = new()
        { { 'A', Shape.Rock}, { 'B', Shape.Paper}, { 'C', Shape.Scissors},
        { 'X', Shape.Rock}, { 'Y', Shape.Paper}, { 'Z', Shape.Scissors}, };


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
                //Decode inputs using ShapeTable. Inputs on line are separated by space character
                string[] plays = line.Split(' ');
                Shape opponent = ShapeTable[plays[0][0]];
                Shape you = ShapeTable[plays[1][0]];
                //Add value of players symbol and winning status to score
                score += (int)you;
                score += (int)CheckWin(opponent, you);

            }

            return score;
        }

        /// <summary>
        /// Checks shape combination winning status.
        /// </summary>
        /// <param name="opponent"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Result CheckWin(Shape opponent, Shape player)
        {
            return opponent switch
            {
                Shape.Rock => player switch
                {
                    Shape.Rock => Result.Draw,
                    Shape.Paper => Result.Win,
                    Shape.Scissors => Result.Lost,
                    _ => Result.Lost,
                },
                Shape.Paper => player switch
                {
                    Shape.Rock => Result.Lost,
                    Shape.Paper => Result.Draw,
                    Shape.Scissors => Result.Win,
                    _ => Result.Lost,
                },
                Shape.Scissors => player switch
                {
                    Shape.Rock => Result.Win,
                    Shape.Paper => Result.Lost,
                    Shape.Scissors => Result.Draw,
                    _ => Result.Lost,
                },
                _ => Result.Lost,
            };
        }
    }
}