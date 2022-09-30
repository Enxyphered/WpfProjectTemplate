using Newtonsoft.Json;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfProjectTemplate.Primitives;

[ObservableObject]
[JsonConverter(typeof(ViewModelSerializer))]
public partial class AppSettings
{
    [ObservableProperty]
    private string _connectionString;

    [ObservableProperty]
    private List<PluginPointer> _pluginPointers = new List<PluginPointer>();
}