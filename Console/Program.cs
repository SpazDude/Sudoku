using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        /// <summary>
        /// Solves Sudoku puzzles from command line in parallel.
        /// <example>000075400000000008080190000300001060000000034000068170204000603900000020530200000</example>
        /// </summary>
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            var puzzles = new List<string>();
            var strGrid = System.Console.ReadLine();
            while (!string.IsNullOrEmpty(strGrid))
            {
                puzzles.Add(strGrid);
                strGrid = System.Console.ReadLine();
            }
            var sudoku = new Library.Sudoku();
            Parallel.ForEach(puzzles, s => System.Console.WriteLine(sudoku.Solve(s) ?? "no solution"));
            System.Console.WriteLine("{0} solutions per second", 10000.0 / stopwatch.Elapsed.TotalSeconds);
        }
    }
}