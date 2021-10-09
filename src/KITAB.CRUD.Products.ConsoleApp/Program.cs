using Microsoft.Extensions.DependencyInjection;
using KITAB.CRUD.Products.Application.Notificadores;
using KITAB.CRUD.Products.Application.Produtos;
using KITAB.CRUD.Products.Infra.Produtos;

namespace KITAB.CRUD.Products.ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            // Create service collection and configure our services
            var services = ConfigureServices();

            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<ConsoleApplication>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddAutoMapper(typeof(AutoMapping));

            services.AddScoped<INotificadorService, NotificadorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // IMPORTANT: Register our application entry point
            services.AddScoped<ConsoleApplication>();

            return services;
        }
    }
}
