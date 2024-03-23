
namespace PruebaTecnicaImaginemos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string basePath = args.Any() && !string.IsNullOrWhiteSpace(args[0]) && Directory.Exists(args[0]) ? args[0] : AppDomain.CurrentDomain.BaseDirectory;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var settings = env == null ? "appsettings.json" : $"appsettings.{env}.json";

            Directory.SetCurrentDirectory(basePath);
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(basePath)
                                    .AddJsonFile(settings, optional: false, reloadOnChange: false)
                                    .Build();

            CreateHostBuilder(args, configuration).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        {
            return Host
                    .CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
