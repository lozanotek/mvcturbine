using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.Web.Controllers
{
    public class TurbineDependencyResolver : IDependencyResolver
    {
        private readonly IServiceLocator serviceLocator;

        public TurbineDependencyResolver(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IServiceLocator ServiceLocator
        {
            get { return serviceLocator; }
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return serviceLocator.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return serviceLocator.ResolveServices(serviceType);
        }
    }
}