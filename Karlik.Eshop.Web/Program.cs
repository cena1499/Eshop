using Karlik.Eshop.Web.Models.Database;
using Karlik.Eshop.Web.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Karlik.Eshop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                if (scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<EshopDbContext>();
                    DatabaseInnit databaseInit = new DatabaseInnit();
                    databaseInit.Initialize(dbContext);

                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                    Task task = databaseInit.EnsureRoleCreated(roleManager);
                    task.Wait();
                    task.Dispose();

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    task = databaseInit.EnsureAdminCreated(userManager);
                    task.Wait();
                    task.Dispose();

                    task = databaseInit.EnsureManagerCreated(userManager);
                    task.Wait();
                    task.Dispose();
                }
            }
                
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logginBuilder =>
            {
                logginBuilder.ClearProviders();
                logginBuilder.AddConsole();
                logginBuilder.AddDebug();
                logginBuilder.AddFile("Logs/eshop-log{Date}.txt");
            });
    }
}
