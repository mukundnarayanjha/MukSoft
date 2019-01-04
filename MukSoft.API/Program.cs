using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace MukSoft.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateMSSqlLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();

        private static void CreateMSSqlLogger()
        {
            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Verbose()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile(@"./logs/ErrorLog-{Date}.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level}:{EventId} [{SourceContext}] {Message}{NewLine}{Exception}")
                .WriteTo.MSSqlServer("server=.\\SQLEXPRESS;database=MukSoftDB;Trusted_Connection=True;", "LogEntries", autoCreateSqlTable: true)
                .CreateLogger();

            // using (LogContext.PushProperty("UserId", "123"))
            //Serilog.Debugging.SelfLog.Out = Console.Out;
            //Serilog.Debugging.SelfLog.Enable(msg =>
            //{
            //    Debug.Print(msg);
            //    Debugger.Break();
            //});
        }
    }
}
