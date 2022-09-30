using Microsoft.Extensions.Options;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfProjectTemplate.Primitives.Abstract;

namespace WpfProjectTemplate.Primitives;

[ObservableObject]
public partial class AppSettingsAccessor : IAppSettingsAccessor
{
    private AppSettings _appSettings;
    public AppSettings AppSettings
    {
        get => _appSettings;
        set => SetProperty(ref _appSettings, value);
    }

    public AppSettingsAccessor(IOptionsMonitor<AppSettings> appSettingsMonitor)
    {
        _appSettings = appSettingsMonitor.CurrentValue;
        appSettingsMonitor.OnChange(newSettings => AppSettings = newSettings);
    }
}