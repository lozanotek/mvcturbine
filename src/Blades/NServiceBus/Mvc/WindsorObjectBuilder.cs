namespace Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.Core;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Releasers;
    using Castle.Windsor;
    using NServiceBus.ObjectBuilder;
    using NServiceBus.ObjectBuilder.Common;

    public class WindsorObjectBuilder : IContainer
    {
        public WindsorObjectBuilder()
        {
            Container = new WindsorContainer();
            Container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();
        }

        public WindsorObjectBuilder(IWindsorContainer container)
        {
            Container = container;
        }

        private static IEnumerable<Type> GetAllServiceTypesFor(Type t)
        {
            if (t == null)
            {
                return new List<Type>();
            }
            List<Type> list2 = new List<Type>(t.GetInterfaces()) { t };
            List<Type> list = list2;
            foreach (Type type in t.GetInterfaces())
            {
                list.AddRange(GetAllServiceTypesFor(type));
            }
            return list;
        }

        private IHandler GetHandlerForType(Type concreteComponent)
        {
            return (from h in Container.Kernel.GetAssignableHandlers(typeof(object))
                    where h.ComponentModel.Implementation == concreteComponent
                    select h).FirstOrDefault();
        }

        private static LifestyleType GetLifestyleTypeFrom(ComponentCallModelEnum callModel)
        {
            switch (callModel)
            {
                case ComponentCallModelEnum.Singleton:
                    return LifestyleType.Singleton;

                case ComponentCallModelEnum.Singlecall:
                    return LifestyleType.Transient;
            }

            return 0;
        }

        object IContainer.Build(Type typeToBuild)
        {
            try
            {
                return Container.Resolve(typeToBuild);
            }
            catch
            {
                return null;
            }
        }

        IEnumerable<object> IContainer.BuildAll(Type typeToBuild)
        {
            try
            {
                var services = Container.ResolveAll(typeToBuild);
                return (IEnumerable<object>)services;
            }
            catch
            {
                return null;
            }
        }

        void IContainer.Configure(Type concreteComponent, ComponentCallModelEnum callModel)
        {
            if (GetHandlerForType(concreteComponent) != null) return;

            var lifestyleTypeFrom = GetLifestyleTypeFrom(callModel);
            var registration = Component.For(GetAllServiceTypesFor(concreteComponent)).ImplementedBy(concreteComponent);
            registration.LifeStyle.Is(lifestyleTypeFrom);
            Container.Kernel.Register(registration);
        }

        void IContainer.ConfigureProperty(Type component, string property, object value)
        {
            var handlerForType = GetHandlerForType(component);
            if (handlerForType == null)
            {
                throw new InvalidOperationException("Cannot configure property for a type which hadn't been configured yet. Please call 'Configure' first.");
            }
            handlerForType.AddCustomDependencyValue(property, value);
        }

        void IContainer.RegisterSingleton(Type lookupType, object instance)
        {
            var registration = Component.For(lookupType).Named(Guid.NewGuid().ToString()).Instance(instance);
            Container.Kernel.Register(registration);
        }

        public IWindsorContainer Container { get; set; }
    }
}