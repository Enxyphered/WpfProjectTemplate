using System;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WpfProjectTemplate.Primitives.Abstract;

public interface IPlugin
{
    public IConfigurationBuilder AddConfiguration(IConfigurationBuilder configBuilder);
    public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration);
    public void OnStartup(IServiceProvider services);
}
