using System.IO;
using System.Windows;
using Newtonsoft.Json;
using WpfProjectTemplate.Primitives;
using WpfProjectTemplate.Primitives.Abstract;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfProjectTemplate;

public partial class MainWindow : Window
{
    private readonly MainViewModel _appSettingsAccessor;

    public MainWindow(MainViewModel appSettingsAccessor)
    {
        InitializeComponent();
        _appSettingsAccessor = appSettingsAccessor;
        DataContext = _appSettingsAccessor;
    }
}

[ObservableObject]
public partial class MainViewModel
{
    private readonly IThemeManager _themeManager;

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

    public MainViewModel(IAppSettingsAccessor appSettingsAccessor, IThemeManager themeManager)
    {
        _appSettingsAccessor = appSettingsAccessor;
        _themeManager = themeManager;
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
    public void Save()
        => File.WriteAllText(App.AppSettingsFileName,
            JsonConvert.SerializeObject(AppSettings, Formatting.Indented));
}