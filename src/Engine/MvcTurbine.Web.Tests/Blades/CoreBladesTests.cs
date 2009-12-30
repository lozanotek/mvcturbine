namespace MvcTurbine.Web.Tests.Blades {
    using NUnit.Framework;
    using Web.Blades;

    [TestFixture]
    public class CoreBladesTests {
        [Test]
        public void Mvc_Blade_Property_Return_Default_Mvc_Blade_Instance() {
            MvcBlade result = CoreBlades.Mvc;

            Assert.IsNotNull(result);
            Assert.AreEqual(CoreBlades.Mvc, result);
        }

        [Test]
        public void When_Mvc_Blade_Property_Is_Set_To_Null_Return_Default_Mvc_Blade_Instance() {
            CoreBlades.Mvc = null;

            var result = CoreBlades.Mvc;
            Assert.IsNotNull(result);
            Assert.AreEqual(CoreBlades.Mvc, result);
        }

        [Test]
        public void When_Mvc_Blade_Property_Is_Set_To_Valid_Hierarchy_Return_Same_Blade_Instance() {
            var instance = new CustomMvcBlade();
            CoreBlades.Mvc = instance;

            Assert.IsNotNull(CoreBlades.Mvc);
            Assert.AreEqual(CoreBlades.Mvc, instance);
        }

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
        public void GetBlades_Returns_Filled_List_By_Default() {
            var result = CoreBlades.GetBlades();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count, 2);
        }

        [Test]
        public void GetBlades_Returns_Filled_List_When_Properties_Are_Null() {
            CoreBlades.Mvc = null;
            CoreBlades.Routing = null;

            var result = CoreBlades.GetBlades();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count, 2);
        }
    }

    internal class CustomMvcBlade : MvcBlade {
    }

    internal class CustomRoutingBlade : RoutingBlade {
    }
}