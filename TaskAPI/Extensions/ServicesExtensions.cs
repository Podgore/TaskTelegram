namespace TaskAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void RegisterDependenciesFromAssembly<TInterface, TDependency>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDependency : TInterface
        {
            var types = typeof(TDependency).Assembly.GetTypes()
                .Where(x => typeof(TInterface).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(typeof(TInterface), type, lifetime));
            }
        }
    }
}
