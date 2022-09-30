using System.Windows;

namespace WpfProjectTemplate.Primitives.Abstract;

public interface IThemeManager
{
    void Register(string themeName, ResourceDictionary themeResources);
    void SetTheme(string themeName);
}