using Autofac;
using LazyProxy.Autofac;
using UserManagement.Application.Services;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.UnitOfWork;

namespace UserManagement.WebApi.Config
{
    public class AutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterLazy<IUserRepository, UserRepository>().InstancePerLifetimeScope();
            builder.RegisterLazy<IAssetRepository, AssetRepository>().InstancePerLifetimeScope();
            builder.RegisterLazy<IUnitOfWork, UnitOfWork>().InstancePerLifetimeScope();
            // builder.RegisterLazy<IUserService, UserService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
