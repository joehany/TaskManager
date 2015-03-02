using System.Linq;
using System.Web.Http;
using TaskManager.BLL.Managers;
using TaskManager.DAL;
using TaskManager.DAL.Base;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Interfaces.Dal;
using TaskManager.Infrastructure.Interfaces.Managers;
using Ninject.Extensions.Conventions;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TaskManager.Services.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TaskManager.Services.App_Start.NinjectWebCommon), "Stop")]

namespace TaskManager.Services.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            Type[] requestScopeObjects = { typeof(TaskManagerDataUnit) };
            kernel.Bind(x => x.FromAssembliesMatching("*")
                              .SelectAllClasses()
                              .Excluding(requestScopeObjects.ToArray())
                              .BindDefaultInterface()
                              .Configure(b => b.InTransientScope()));

            kernel.Bind(x => x.FromAssembliesMatching("*")
                              .Select(t => t.IsClass && requestScopeObjects.Contains(t))
                              .BindDefaultInterface()
                              .Configure(b => b.InRequestScope()));

        }  
    }
}
