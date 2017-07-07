using Amazon.Lambda.Core;
using Library;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaSudoku
{
    public class Function
    {
        
        /// <summary>
        /// The Amazon Lambda endpoint for the sudoku puzzle solver
        /// </summary>
        /// <param name="input">A sudoku puzzle, 81 numbers 0-9, representing empty (0) and filled (1-9) cells in 9 rows of 9 cells.</param>
        /// <param name="context">the Amazon Lambda execution context</param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.LogLine($"puzzle : {input}");
            var solution = Sudoku.Solve(input);
            logger.LogLine($"solved : {solution}");
            return solution;
        }
    }
}
