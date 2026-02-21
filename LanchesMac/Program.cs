using LanchesMac.context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac;
public class Program
{
    public static void Main(string[] args)
    {
        var host  = CreateHostBuilder(args).Build();

        //Executa migrations automaticamente ao subir a aplicação
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
