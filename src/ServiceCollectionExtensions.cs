using BlazorComps.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorComps;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorComps(this IServiceCollection services)
        => services
            .AddSingleton<IClassProvider, ClassProvider>()
            .AddScoped<IBreakpointInterop, BreakpointInterop>()
            .AddScoped<ISidebarService, SidebarService>();
}