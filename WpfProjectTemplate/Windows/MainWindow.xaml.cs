using System.IO;
using System.Windows;
using Newtonsoft.Json;
using WpfProjectTemplate.Primitives;
using WpfProjectTemplate.Primitives.Abstract;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace WpfProjectTemplate;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

[ObservableObject]
public partial class MainViewModel
{
    private readonly IThemeManager _themeManager;
    private readonly IServiceProvider _services;
    private readonly IAppSettingsAccessor _appSettingsAccessor;

    public AppSettings AppSettings
    {
        get => _appSettingsAccessor.AppSettings;
        set => SetProperty(
            _appSettingsAccessor.AppSettings, 
            value, 
            _appSettingsAccessor, 
            (accessor, appsettings) => accessor.AppSettings = appsettings);
    }

    public MainViewModel(IAppSettingsAccessor appSettingsAccessor, 
        IThemeManager themeManager,
        IServiceProvider services)
    {
        _appSettingsAccessor = appSettingsAccessor;
        _themeManager = themeManager;
        _services = services;
    }

    [RelayCommand]
    public void DarkMode()
    {
        _themeManager.SetTheme(ThemeNames.Dark);
    }

    [RelayCommand]
    public void BaseMode()
    {
        _themeManager.SetTheme(ThemeNames.Base);
    }

    [RelayCommand]
    public void TestDialog()
    {
        //You should always call getservice to get a new insance of a dialog
        //I guess you could have a DialogAccessor class that's responsible for doing this but meh... uwu
        if (_services.GetService<IYesOrNoDialog>().AskQuestion("Testing...", "Choose one of the options"))
            _services.GetService<IOkDialog>().ShowMessage("Results", "You pressed Yes!");
        else
            _services.GetService<IOkDialog>().ShowMessage("Results", "You pressed No! " +
                "(more accurately you didn't press yes uwu but we consider that a no)");
    }

    [RelayCommand]
    public void Save()
        => File.WriteAllText(App.AppSettingsFileName,
            JsonConvert.SerializeObject(AppSettings, Formatting.Indented));
}