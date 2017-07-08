using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using Library;
using System.Diagnostics;

namespace SudokuWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if (context.Request.Method == "POST")
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        var value = await reader.ReadToEndAsync();
                        var puzzle = value.Substring(1, 81);
                        Debug.WriteLine(puzzle);
                        await context.Response.WriteAsync("\"" + Sudoku.Solve(puzzle) + "\"");
                    }
            });
        }
    }
}
