using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace SerilogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var connectionStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                //.Enrich.FromLogContext()
                .WriteTo.Async(config => config.File($"{AppContext.BaseDirectory}logs/.log", LogEventLevel.Information, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:HH:mm} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}"))
                //.WriteTo.Async(config => config.MSSqlServer(connectionStr, new MSSqlServerSinkOptions
                //{
                //    TableName = "Serilog",
                //    AutoCreateSqlTable = true
                //}, null, null, LogEventLevel.Error))
                //.WriteTo.Console()
                //.WriteTo.File($"{AppContext.BaseDirectory}Log/.log", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                //.WriteTo.MSSqlServer(connectionStr, new MSSqlServerSinkOptions
                //{
                //    TableName = "Serilog",
                //    AutoCreateSqlTable = true
                //}, null, null, LogEventLevel.Error)
                .CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    .WriteTo.File($"{AppContext.BaseDirectory}Log/.log",
            //                     rollingInterval: RollingInterval.Day,
            //                     outputTemplate: "{Timestamp:HH:mm} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine}"
            //                  )
            //    .CreateLogger();

            //Log.Verbose("Hello Serilog Verbose");
            //Log.Debug("Hello Serilog Debug");
            //Log.Information("Hello Serilog Information");
            //Log.Warning("Hello Serilog Warning");
            //Log.Error("Hello Serilog Error");
            //Log.Fatal("Hello Serilog Fatal");
            //Log.CloseAndFlush();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
