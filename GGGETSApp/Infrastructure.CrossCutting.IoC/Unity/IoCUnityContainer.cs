using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using ETS.GGGETSApp.Infrastructure.CrossCutting.IoC.Resources;
using System.Configuration;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.UnitOfWork;
using ETS.GGGETSApp.Infrastructure.CrossCutting.IoC.Unity.LifetimeManagers;
using ETS.GGGETSApp.Infrastructure.CrossCutting.NetFramework.Logging;
using ETS.GGGETSApp.Infrastructure.CrossCutting.Logging;
using Domain.GGGETS;
using ETS.GGGETSApp.Infrastructure.Data.Persistence.Repositories;
using Application.GGETS;



namespace ETS.GGGETSApp.Infrastructure.CrossCutting.IoC.Unity
{
    /// <summary>
    /// Implemented container in Microsoft Practices Unity
    /// </summary>
    sealed class IoCUnityContainer
        :IContainer
    {
        #region Members

        IDictionary<string, IUnityContainer> _ContainersDictionary;


        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of IoCUnitContainer
        /// </summary>
        public IoCUnityContainer()
        {
            _ContainersDictionary = new Dictionary<string, IUnityContainer>();

                //Create root container
            IUnityContainer rootContainer = new UnityContainer();
            _ContainersDictionary.Add("RootContext", rootContainer);

                //Create container for real context, child of root container
            IUnityContainer realAppContainer = rootContainer.CreateChildContainer();
            _ContainersDictionary.Add("RealAppContext", realAppContainer);

                //Create container for testing, child of root container
            IUnityContainer fakeAppContainer = rootContainer.CreateChildContainer();
            _ContainersDictionary.Add("FakeAppContext", fakeAppContainer);

           
            ConfigureRootContainer(rootContainer);
            ConfigureRealContainer(realAppContainer);
            ConfigureFakeContainer(fakeAppContainer);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configure root container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureRootContainer(IUnityContainer container)
        {
            // Take into account that Types and Mappings registration could be also done using the UNITY XML configuration
            //But we prefer doing it here (C# code) because we'll catch errors at compiling time instead execution time, 
            //if any type has been written wrong.

            //Register Repositories mappings
            container.RegisterType<IHAWBRepository, HAWBRepository>(new TransientLifetimeManager());
            container.RegisterType<IHAWBItemRepository, HAWBItemRepository>(new TransientLifetimeManager());
            container.RegisterType<IHAWBBoxRepository, HAWBBoxRepository>(new TransientLifetimeManager());
            container.RegisterType<IPackageRepository, PackageRepository>(new TransientLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new TransientLifetimeManager());
            container.RegisterType<IMAWBRepository, MAWBRepository>(new TransientLifetimeManager());
            //container.RegisterType<IFlightRepository, FlightRepository>(new TransientLifetimeManager());
            container.RegisterType<ICountryCodeRepository, CountryCodeRepository>(new TransientLifetimeManager());
            container.RegisterType<IRegionCodeRepository, RegionCodeRepository>(new TransientLifetimeManager());
            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new TransientLifetimeManager());
            container.RegisterType<ICompanyRepository, CompanyRepository>(new TransientLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new TransientLifetimeManager());
            container.RegisterType<IAddressBookRepository, AddressBookRepository>(new TransientLifetimeManager());
            container.RegisterType<ITemplateRepository, TemplateRepository>(new TransientLifetimeManager());
            container.RegisterType<IParamRepository, ParamRepository>(new TransientLifetimeManager());
            container.RegisterType<IHSProductRepository, HSProductRepository>(new TransientLifetimeManager());
            container.RegisterType<IHSPropertyRepository, HSPropertyRepository>(new TransientLifetimeManager());
            container.RegisterType<ISysUserRepository, SysUserRepository>(new TransientLifetimeManager());
            container.RegisterType<IAppModuleRepository, AppModuleRepository>(new TransientLifetimeManager());
            container.RegisterType<IRoleRepository, RoleRepository>(new TransientLifetimeManager());
            //Register application services mappings

            container.RegisterType<IHAWBManagementService, HAWBManagementService>(new TransientLifetimeManager());
            container.RegisterType<IPackageManagementService, PackageManagementService>(new TransientLifetimeManager());
            container.RegisterType<IMAWBManagementService, MAWBManagementService>(new TransientLifetimeManager());
            //container.RegisterType<IFlightManagementService, FlightManagementService>(new TransientLifetimeManager());
            container.RegisterType<ICountryCodeManagementService, CountryCodeManagementService>(new TransientLifetimeManager());
            container.RegisterType<IRegionCodeManagementService, RegionCodeManagementService>(new TransientLifetimeManager());
            container.RegisterType<IDepartmentManagementService, DepartmentManagementService>(new TransientLifetimeManager());
            container.RegisterType<ICompanyManagementService, CompanyManagementService>(new TransientLifetimeManager());
            container.RegisterType<IUserManagementService, UserManagementService>(new TransientLifetimeManager());
            container.RegisterType<IAddressBookManagementService, AddressBookManagementService>(new TransientLifetimeManager());
            container.RegisterType<ITemplateManagementService, TemplateManagementService>(new TransientLifetimeManager());
            container.RegisterType<IParamManagementService, ParamManagementService>(new TransientLifetimeManager());
            container.RegisterType<IHSProductManagementService, HSProductManagementService>(new TransientLifetimeManager());
            container.RegisterType<IHSPropertyManagementService, HSPropertyManagementService>(new TransientLifetimeManager());
            container.RegisterType<IFindInfoManagementService, FindInfoManagementService>(new TransientLifetimeManager());
            container.RegisterType<ISysUserManagementService, SysUserManagementService>(new TransientLifetimeManager());
            container.RegisterType<IRoleManagementService, RoleManagementService>(new TransientLifetimeManager());
            container.RegisterType<IAppModuleManagementService, AppModuleManagementService>(new TransientLifetimeManager());
            //Register domain services mappings
            //container.RegisterType<IBankTransferDomainService, BankTransferDomainService>(new TransientLifetimeManager());
            

            //Register crosscuting mappings
            container.RegisterType<ITraceManager, TraceManager>(new TransientLifetimeManager());

        }

        /// <summary>
        /// Configure real container. Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureRealContainer(IUnityContainer container)
        {
            container.RegisterType<IGGGETSAppUnitOfWork, GGGETSUnitOfWork>(new PerExecutionContextLifetimeManager(), new InjectionConstructor());
        }

        /// <summary>
        /// Configure fake container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        void ConfigureFakeContainer(IUnityContainer container)
        {
            //Note: Generic register type method cannot be used here, 
            //MainModuleFakeContext cannot have implicit conversion to IMainModuleContext

           // container.RegisterType(typeof(IGGGETSAppUnitOfWork), typeof(FakeMainModuleUnitOfWork), new PerExecutionContextLifetimeManager());
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}"/>
        /// </summary>
        /// <typeparam name="TService"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}"/></typeparam>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}"/></returns>
        public TService Resolve<TService>()
        {
            //We use the default container specified in AppSettings
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(Messages.exception_DefaultIOCSettings);
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.exception_ContainerNotFound);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.Resolve<TService>();
        }
        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve"/>
        /// </summary>
        /// <param name="type"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve"/></param>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve"/></returns>
        public object Resolve(Type type)
        {
            //We use the default container specified in AppSettings
            string containerName = ConfigurationManager.AppSettings["defaultIoCContainer"];

            if (String.IsNullOrEmpty(containerName)
                ||
                String.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(Messages.exception_DefaultIOCSettings);
            }

            if (!_ContainersDictionary.ContainsKey(containerName))
                throw new InvalidOperationException(Messages.exception_ContainerNotFound);

            IUnityContainer container = _ContainersDictionary[containerName];

            return container.Resolve(type, null);
        }

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.RegisterType"/>
        /// </summary>
        /// <param name="type"><see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.RegisterType"/></param>
        public void RegisterType(Type type)
        {
            IUnityContainer container = this._ContainersDictionary["RootContext"];

            if (container != null)
                container.RegisterType(type, new TransientLifetimeManager());
        }

        #endregion
    }
}
