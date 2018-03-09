using Autofac;
using Quorra.Interfaces;
using Quorra.Services;

namespace Quorra.Models
{
    public class RepositoryHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BotService>().As<IBotService>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateService>().As<IUpdateService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
