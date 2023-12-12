using OpenDialogLibrary.ViewModels;
using OpenDialogLibrary.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;
using OpenDialogLibrary.Services.Implementation;
using OpenDialogLibrary.Services.Interface;

namespace OpenDialogLibrary;

internal static class IoC
{
    private static IServiceProvider? provider;
    private static bool isInitialized;
        
    internal static T Resolve<T>() where T : notnull
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider), "Argument shouldn't be null");
        return provider.GetRequiredService<T>();
    }

    internal static void Initialize()
    {
        if (isInitialized) return;
        IServiceCollection services = new ServiceCollection();

        services.AddWindows();
        services.AddPages();
        services.AddServices();

        provider = services.BuildServiceProvider();

        foreach (var service in services.Where(s => s.Lifetime == ServiceLifetime.Singleton))
            provider.GetRequiredService(service.ServiceType);

        isInitialized = true;
    }

    internal static void Deinitialize()
    {
        if (!isInitialized) return;
        provider = null;
        GC.Collect();
        isInitialized = false;
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