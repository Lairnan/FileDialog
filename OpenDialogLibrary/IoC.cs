using OpenDialogLibrary.ViewModels;
using OpenDialogLibrary.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;
using OpenDialogLibrary.Services.Implementation;
using OpenDialogLibrary.Services.Interface;

namespace OpenDialogLibrary
{
    internal static class IoC
    {
        private static IServiceProvider? _provider;
        private static bool _isInitialized;
        
        internal static T Resolve<T>() where T : notnull
        {
            if (_provider == null) throw new ArgumentNullException(nameof(_provider), "Argument shouldn't be null");
            return _provider.GetRequiredService<T>();
        }

        internal static void Initialize()
        {
            if (_isInitialized) return;
            IServiceCollection services = new ServiceCollection();

            services.AddWindows();
            services.AddPages();
            services.AddServices();

            _provider = services.BuildServiceProvider();

            foreach (var service in services.Where(s => s.Lifetime == ServiceLifetime.Singleton))
                _provider.GetRequiredService(service.ServiceType);

            _isInitialized = true;
        }

        internal static void Deinitialize()
        {
            if (!_isInitialized) return;
            _provider = null;
            GC.Collect();
            _isInitialized = false;
        }

        private static void AddWindows(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowVm>();
        }

        private static void AddPages(this IServiceCollection services)
        {
            services.AddSingleton<MainPageVm>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IPageService, PageService>();
        }
    }
}
