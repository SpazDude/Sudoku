using Library;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
            Task.Run(async () => { await MainAsync(args); })
                .GetAwaiter().GetResult();
            System.Console.WriteLine("{0} solutions per second", 10000.0 / stopwatch.Elapsed.TotalSeconds);
        }

        static async Task MainAsync(string[] args)
        {
            var solveBlock = new ActionBlock<string>(x => System.Console.WriteLine(Sudoku.Solve(x) ?? "no solution"),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = Math.Max(Environment.ProcessorCount - 1, 1)
                });

            var strGrid = System.Console.ReadLine();
            while (!string.IsNullOrEmpty(strGrid))
            {
                await solveBlock.SendAsync(strGrid);
                strGrid = System.Console.ReadLine();
            }
            solveBlock.Complete();
            await solveBlock.Completion;
        }
    }
}