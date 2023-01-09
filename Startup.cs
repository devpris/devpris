using EmprestimoLivros.API.Models;
using System.Runtime.CompilerServices;

namespace EmprestimoLivros.API
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;     
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MeuDbContext>(options =>
                options.UseInMemoryDatabase("MeuDb"));
            ...
}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(WebApplication app,IWebHostEnvironment environment)
        {
         
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

        }
    }

    public interface Itartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app,IWebHostEnvironment environment);
        void ConfigureServices(IServiceCollection services);
    }

    public static class StartupExtesions
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebAppBuilder) where TStartup : IStartup
    {
        var startup = Activator.CreateInstance(typeof(TStartup), WebAppBuilder.Configuration) as IStartup;
        if (startup == null) throw new ArgumentException("Classe Startup.cs inválida!");

        startup.ConfigureServices(WebAppBuilder.Services);

        var app = WebAppBuilder.Build();

        startup.Configure(app, app.Environment);

        app.Run();

        return WebAppBuilder;
    }
}
