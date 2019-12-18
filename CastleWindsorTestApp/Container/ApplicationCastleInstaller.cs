using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using CastleWindsorTestApp.Models;
using System.Data.Entity;

namespace CastleWindsorTestApp.Container
{
    /// <summary>
    /// The provide registration of application components
    /// </summary>
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // The registered application components

            container.Register(Component.For<IRepository<Employee>>().ImplementedBy<Repository<Employee>>());
            container.Register(Component.For<IRepository<Department>>().ImplementedBy<Repository<Department>>());

            // Get object IRepository
            // var repo = container.Resolve<IRepository<Employee>>();

            container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());
        }
    }
}