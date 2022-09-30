using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WpfProjectTemplate.Primitives.Abstract;

//Todo: Test a custom plugin...
//Never tested it but I assume it should technically work hehe uwu
public interface IPlugin
{
    public IConfigurationBuilder AddConfiguration(IConfigurationBuilder configBuilder);
    public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration);
    public void OnStartup(IServiceProvider services);
}
