using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TARge21Shop.ApplicationServices.Services;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.SpaceshipTest.Macros;
using Microsoft.AspNetCore.Hosting;

namespace TARge21Shop.SpaceshipTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; }

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        protected T Macro<T>() where T : IMacros
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            //var testHostingEnvironment = new MockHostingEnvironment();
            //var builder = new WebHostBuilder()
            //            .Configure(app => { })
            //            .ConfigureServices(services =>
            //            {
            //                services.AddSingleton<IWebHostEnvironment>(testHostingEnvironment);
            //            });
            //var server = new TARge21ShopContext(builder);

            services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
            services.AddScoped<IFilesServices, FilesServices>();
            services.AddScoped<IWebHostEnvironment>();

            services.AddDbContext<TARge21ShopContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface
                && !x.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
