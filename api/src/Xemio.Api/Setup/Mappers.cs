using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Xemio.Api.Mapping;

namespace Xemio.Api.Setup
{
    public static class Mappers
    {
        public static void AddMappers(this IServiceCollection self)
        {
            var mappers = from type in typeof(MapperBase<,>).GetTypeInfo().Assembly.GetTypes()
                          where type.GetTypeInfo().IsAbstract == false
                          from mapper in type.GetInterfaces().Where(f => f.GetTypeInfo().IsGenericType && f.GetGenericTypeDefinition() == typeof(IMapper<,>))
                          select new
                          {
                              Interface = mapper,
                              Implementation = type
                          };

            foreach (var mapper in mappers)
            {
                self.Add(ServiceDescriptor.Transient(mapper.Interface, mapper.Implementation));
            }
        }
    }
}