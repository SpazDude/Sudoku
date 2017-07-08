using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConsoleClient
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
            var solveBlock = new ActionBlock<string>(async x => Console.WriteLine(await SudokuSolve(x)),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = Math.Max(256, 1)
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

        static async Task<string> SudokuSolve(string puzzle)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent($"\"{puzzle}\"");
                var httpResponse = await client.PostAsync("http://localhost:63665/", httpContent);
                //var httpResponse = await client.PostAsync("https://5mtaj1l3tk.execute-api.us-west-2.amazonaws.com/prod/SudokuLambda", httpContent);
                var result = await httpResponse.Content.ReadAsStringAsync();
                return result.Substring(1, 81);
            }
        }
    }
}