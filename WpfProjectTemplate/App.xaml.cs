using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using WpfProjectTemplate.Primitives;
using Microsoft.Extensions.Configuration;
using WpfProjectTemplate.Primitives.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace WpfProjectTemplate;

public partial class App : Application
{
    public const string AppSettingsFileName = "AppSettings.json";
    public static new App Current => (App)Application.Current;
    public IServiceProvider Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var appSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(App.AppSettingsFileName));
        var plugins = new PluginsLoader(appSettings).GetAllPlugins();

        IConfigurationBuilder configBuilder = new ConfigurationBuilder();
        configBuilder = AddConfiguration(configBuilder);
        configBuilder = AddPluginConfiguration(configBuilder, plugins);
        var config = configBuilder.Build();

        IServiceCollection collection = new ServiceCollection();
        collection = ConfigureServices(collection, config);
        collection = ConfigurePluginServices(collection, config, plugins);

        Services = collection.BuildServiceProvider();

        foreach (var plugin in plugins)
            plugin.OnStartup(Services);

        var themesManager = Services.GetService<IThemeManager>();
        themesManager.Register(ThemeNames.Dark, new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/WpfProjectTemplate;Component/Themes/DarkTheme.xaml")
        });

        Services.GetService<MainWindow>().Show();
    }

    private IConfigurationBuilder AddConfiguration(IConfigurationBuilder configBuilder)
        => configBuilder
            .AddJsonFile("AppSettings.json", false, true);

    private IConfigurationBuilder AddPluginConfiguration(IConfigurationBuilder builder, IEnumerable<IPlugin> plugins)
    {
        foreach (var plugin in plugins)
            plugin.AddConfiguration(builder);
        return builder;
    }

    private IServiceCollection ConfigureServices(IServiceCollection collection, IConfiguration configuration)
        => collection
            .Configure<AppSettings>(configuration)
            .AddTransient<AppSettings>()
            .AddTransient<MainViewModel>()
            .AddSingleton<IAppSettingsAccessor, AppSettingsAccessor>()
            .AddSingleton<IThemeManager, ThemeManager>()
            .AddSingleton<MainWindow>();

    private IServiceCollection ConfigurePluginServices(IServiceCollection collection, 
        IConfiguration configuration, 
        IEnumerable<IPlugin> plugins)
    {
        foreach (var plugin in plugins)
            plugin.ConfigureServices(collection, configuration);
        return collection;
    }
}