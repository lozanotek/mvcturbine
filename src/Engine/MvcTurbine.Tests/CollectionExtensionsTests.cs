namespace MvcTurbine.Tests {
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class CollectionExtensionsTests {
        [Test]
        public void ForEach_Extension_With_Action() {
            var integers = new[] {1, 2, 3, 4, 5};
            int sum = 0;
            integers.ForEach(value => sum += value);

            Assert.AreEqual(sum, 15);
        }

        [Test]
        public void ForEach_Extension_With_Action_And_Empty_List() {
            var integers = new List<int>();
            int sum = 0;
            integers.ForEach(value => sum += value);

            Assert.AreEqual(sum, 0);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ForEach_Extension_With_Null_Action() {
            var integers = new[] {1, 2, 3, 4, 5};
            integers.ForEach(null);
        }
    }
}