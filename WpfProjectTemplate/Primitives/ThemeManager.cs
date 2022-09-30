using System.Windows;
using System.Collections.Generic;
using WpfProjectTemplate.Primitives.Abstract;

namespace WpfProjectTemplate.Primitives;

public class ThemeManager : IThemeManager
{
    private ResourceDictionary _current;
    private IDictionary<string, ResourceDictionary> _themes = new Dictionary<string, ResourceDictionary>();

    public void Register(string themeName, ResourceDictionary themeDictionary)
        => _themes[themeName] = themeDictionary;

    public void SetTheme(string themeName)
    {
        if(string.IsNullOrWhiteSpace(themeName))
        {
            App.Current.Resources.MergedDictionaries.Remove(_current);
            _current = null;
            return;
        }

        if (_current is not null)
            App.Current.Resources.MergedDictionaries.Remove(_current);

        var theme = _themes[themeName];
        App.Current.Resources.MergedDictionaries.Add(theme);
        _current = theme;
    }
}
