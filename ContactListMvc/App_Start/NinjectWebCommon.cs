[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ContactListMvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ContactListMvc.App_Start.NinjectWebCommon), "Stop")]

namespace ContactListMvc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<ContactListMvc.Models.Repository.IRepEf>()
               .To<ContactListMvc.Models.Repository.EF.RepEf>();

            kernel.Bind<ContactListMvc.Models.Repository.IRepExcel>()
               .To<ContactListMvc.Models.Repository.EPPlus.RepExcel>();

            kernel.Bind<ContactListMvc.Models.Repository.IRepSqlServer>()
               .To<ContactListMvc.Models.Repository.SqlServer.RepSqlServer>();

            kernel.Bind<ContactListMvc.Models.ILoader>()
                .To<ContactListMvc.Models.XlsLoader>();
        }        
    }
}
