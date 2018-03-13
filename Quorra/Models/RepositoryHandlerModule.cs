using Autofac;
using Quorra.Data;
using Quorra.Interfaces;
using Quorra.Services;

namespace Quorra.Models
{
    public class RepositoryHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<BotService>().As<IBotService>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateService>().As<IUpdateService>().InstancePerLifetimeScope();
            builder.RegisterType<LuisService>().As<ILuisService>().InstancePerLifetimeScope();
            builder.RegisterType<HubService>().As<IHubService>().InstancePerLifetimeScope();
            builder.RegisterType<JokeService>().As<IJokeService>().InstancePerLifetimeScope();
            builder.RegisterType<HelpService>().As<IHelpService>().InstancePerLifetimeScope();
            builder.RegisterType<MemeService>().As<IMemeService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
