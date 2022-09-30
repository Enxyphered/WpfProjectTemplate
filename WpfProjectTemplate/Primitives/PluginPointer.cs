using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfProjectTemplate.Primitives;

[ObservableObject]
public partial class PluginPointer
{
    [ObservableProperty]
    private string _assemblyFile;

    [ObservableProperty]
    private string _typeName;
}
