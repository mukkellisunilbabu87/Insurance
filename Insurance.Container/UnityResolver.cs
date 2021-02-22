using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Insurance.Business;
using Insurance.Common.Business;
using Insurance.Common.DataAccess;
using Insurance.DataAccess;
using Unity;
using Unity.Lifetime;

namespace Insurance.Container
{
    public class UnityResolver : IDependencyResolver
    {
        private bool disposed = false;

        protected IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            this._container = container ?? throw new ArgumentNullException("container");
            this.Load();
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                _container.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Load()
        {
            this._container.RegisterType(typeof(IInsuranceBusiness), typeof(InsuranceBusiness));
            this._container.RegisterType(typeof(IInsuranceUOW), typeof(InsuranceUOW));
            this._container.RegisterInstance<IInsuranceRepository>(new InsuranceRepository(), InstanceLifetime.Singleton);
        }
    }
}
