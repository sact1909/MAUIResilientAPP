using MAUIResilientAPP.APISettings;
using MAUIResilientAPP.Views;
using Prism.Ioc;

namespace MAUIResilientAPP;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("NavigationPage/MainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>();

        containerRegistry.RegisterSingleton(typeof(IBackendClient<>), typeof(BackendClient<>));
    }
}
