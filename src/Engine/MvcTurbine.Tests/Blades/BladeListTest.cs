namespace MvcTurbine.Tests.Blades {
    using System;
    using System.Collections.Generic;
    using MvcTurbine.Blades;
    using NUnit.Framework;

    [TestFixture]
    public class BladeListTest {
        [Test]
        public void Can_Add_And_Remove_Null_Blade_To_List() {

            var list = new BladeList {null};
            Assert.IsNotEmpty(list);

            list.Remove(null);
            Assert.IsEmpty(list);
        }

        [Test]
        public void Can_Add_And_Remove_Valid_Blade_To_List() {
            Blade blade = new MockBlade();

            var list = new BladeList {blade};
            Assert.IsNotEmpty(list);

            list.Remove(blade);
            Assert.IsEmpty(list);
        }

        [Test]
        public void Can_Add_Null_Blade_To_List() {
            var list = new BladeList {null};

            Assert.IsNotEmpty(list);
        }

        [Test]
        public void Can_Add_Valid_Blade_To_List() {
            var list = new BladeList {new MockBlade()};

            Assert.IsNotEmpty(list);
        }

        [Test]
        public void Can_Create_Valid_Instance() {
            var list = new BladeList();
            Assert.IsNotNull(list);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void List_Initialized_With_Null_Throws_Exception() {
            new BladeList(null);
        }

        [Test]
        public void List_Initialized_With_Valid_List_Has_Items() {
            var list = new BladeList(new List<IBlade> {new MockBlade()});
            Assert.IsNotEmpty(list);
        }
    }
}