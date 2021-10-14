namespace MASA.Contrib.Dispatcher.InMemory.Tests;

[TestClass]
public class AssemblyResolutionTests
{
    [TestMethod]
    public void TestResolveEventBus()
    {
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
        services.AddTransient(typeof(IMiddleware<>), typeof(LoggingMiddleware<>));
        services.AddEventBus();
        var serviceProvider = services.BuildServiceProvider();
        Assert.IsNotNull(serviceProvider.GetService<IEventBus>(), "Event bus injection failed");
    }

    [TestMethod]
    public void TestAddDefaultAssembly()
    {
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
        services.AddTransient(typeof(IMiddleware<>), typeof(LoggingMiddleware<>));
        services.AddEventBus(options => options.Assemblies = AppDomain.CurrentDomain.GetAssemblies());
    }

    [TestMethod]
    public void TestAddNullAssembly()
    {
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
        services.AddTransient(typeof(IMiddleware<>), typeof(LoggingMiddleware<>));
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            services.AddEventBus(options => options.Assemblies = null);
        });
    }

    [TestMethod]
    public void TestAddEmptyAssembly()
    {
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
        services.AddTransient(typeof(IMiddleware<>), typeof(LoggingMiddleware<>));
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            services.AddEventBus(options => options.Assemblies = new Assembly[0]);
        });
    }

    [TestMethod]
    public void TestEventBusByAddNullAssembly()
    {
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
        services.AddTransient(typeof(IMiddleware<>), typeof(LoggingMiddleware<>));
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            services.AddTestEventBus(ServiceLifetime.Scoped, options => options.Assemblies = null);
        });
    }

    [TestMethod]
    public void TestEventBusByAddEmptyAssembly()
    {
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
        services.AddTransient(typeof(IMiddleware<>), typeof(LoggingMiddleware<>));
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            services.AddTestEventBus(ServiceLifetime.Scoped, options => options.Assemblies = new Assembly[0]);
        });
    }
}