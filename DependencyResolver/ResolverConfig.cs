using System.Data.Entity;
using BLL.ConcreteServices;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;
using DAL.Concrete;
using DAL.Interface.Repositories;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        ///////////////////////
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModelContext>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModelContext>().InSingletonScope();
            }

            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IRoleService>().To<RoleService>();

            kernel.Bind<IRequestRepository>().To<RequestRepository>();
            kernel.Bind<IRequestService>().To<RequestService>();

            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<ICategoryService>().To<CategoryService>();

            kernel.Bind<ISectionReposytory>().To<SectionRepository>();
            kernel.Bind<ISectionService>().To<SectionService>();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
