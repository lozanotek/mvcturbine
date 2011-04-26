﻿using MvcTurbine.Web.Blades;

namespace MvcTurbine.Web.Tests.Blades {
    using NUnit.Framework;

    [TestFixture]
    public class CoreBladesTests {

        [Test]
        public void Routing_Blade_Property_Return_Default_Routing_Blade_Instance() {
            var result = CoreBlades.Routing;

            Assert.IsNotNull(result);
            Assert.AreEqual(CoreBlades.Routing, result);
        }

        [Test]
        public void When_Routing_Blade_Property_Is_Set_To_Null_Return_Default_Routing_Blade_Instance() {
            CoreBlades.Routing = null;

            var result = CoreBlades.Routing;
            Assert.IsNotNull(result);
            Assert.AreEqual(CoreBlades.Routing, result);
        }

        [Test]
        public void When_Routing_Blade_Property_Is_Set_To_Valid_Hierarchy_Return_Same_Blade_Instance() {
            var instance = new CustomRoutingBlade();
            CoreBlades.Routing = instance;

            Assert.IsNotNull(CoreBlades.Routing);
            Assert.AreEqual(CoreBlades.Routing, instance);
        }

        [Test]
        public void DependencyResolver_Blade_Property_Return_Default_DependencyResolver_Blade_Instance()
        {
            var result = CoreBlades.DependencyResolver;

            Assert.IsNotNull(result);
            Assert.AreEqual(CoreBlades.DependencyResolver, result);
        }

        [Test]
        public void When_DependencyResolver_Blade_Property_Is_Set_To_Null_Return_Default_DependencyResolver_Blade_Instance()
        {
            CoreBlades.DependencyResolver = null;

            var result = CoreBlades.DependencyResolver;
            Assert.IsNotNull(result);
            Assert.AreEqual(CoreBlades.DependencyResolver, result);
        }

        [Test]
        public void When_DependencyResolver_Blade_Property_Is_Set_To_Valid_Hierarchy_Return_Same_Blade_Instance()
        {
            var instance = new CustomDependencyResolverBlade();
            CoreBlades.DependencyResolver = instance;

            Assert.IsNotNull(CoreBlades.DependencyResolver);
            Assert.AreEqual(CoreBlades.DependencyResolver, instance);
        }

        [Test]
        public void GetBlades_Returns_Filled_List_By_Default() {
            var result = CoreBlades.GetBlades();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count, 9);
        }

        [Test]
        public void GetBlades_Returns_Filled_List_When_Properties_Are_Null() {
            CoreBlades.Routing = null;
            CoreBlades.DependencyResolver = null;

            var result = CoreBlades.GetBlades();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count, 9);
        }
    }

    internal class CustomRoutingBlade : RoutingBlade {
    }

    internal class CustomDependencyResolverBlade : DependencyResolverBlade
    {
    }
}