using System;
using System.Reflection;
using System.Collections.Generic;
using WpfProjectTemplate.Primitives.Abstract;

namespace WpfProjectTemplate.Primitives;

public class PluginsLoader
{
    private readonly AppSettings _appSettings;

    public PluginsLoader(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public IEnumerable<IPlugin> GetAllPlugins()
    {
        foreach (var pointer in _appSettings.PluginPointers)
        {
            var assembly = Assembly.LoadFrom(pointer.AssemblyFile);
            var type = assembly.GetType(pointer.TypeName);
            yield return (IPlugin)Activator.CreateInstance(type);
        }
    }
}
