namespace MvcTurbine.Autofac.Tests {
    using System;
    using ComponentModel;
    using ComponentModel.Tests;
    using ComponentModel.Tests.Components;
    using global::Autofac;
    using NUnit.Framework;

    [TestFixture]
    public class AutofacResolutionTests : ResolutionTests {
        protected override IServiceLocator CreateServiceLocator() {
            var container = new ContainerBuilder();
            Type simpleType = typeof (SimpleLogger);
            Type complexType = typeof (ComplexLogger);

            container.RegisterType(simpleType).As<ILogger>().Named(simpleType.FullName, typeof (ILogger));
            container.RegisterType(complexType).As<ILogger>().Named(complexType.FullName, typeof (ILogger)).As(typeof(ComplexLogger));

            return new AutofacServiceLocator(container);
        }
    }
}