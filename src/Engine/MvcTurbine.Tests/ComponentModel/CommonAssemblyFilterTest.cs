﻿namespace MvcTurbine.Tests.ComponentModel {
    using MvcTurbine.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class CommonAssemblyFilterTest {
        [Test]
        public void Can_Create_Valid_Instance() {
            var filter = new CommonAssemblyFilter();
            Assert.IsNotNull(filter);
        }

        [Test]
        public void Match_When_Valid_Name_Is_Added() {
            var filter = new CommonAssemblyFilter();
            filter.AddFilter("mscorlib");

            bool result = filter.Match(typeof (string).Assembly.FullName);
            Assert.IsTrue(result);
        }

        [Test]
        public void No_Match_When_Empty_And_Name_Is_Empty() {
            var filter = new CommonAssemblyFilter();
            bool result = filter.Match(string.Empty);

            Assert.IsFalse(result);
        }

        [Test]
        public void No_Match_When_Empty_And_Name_Is_Null() {
            var filter = new CommonAssemblyFilter();
            bool result = filter.Match(null);

            Assert.IsFalse(result);
        }

        [Test]
        public void No_Match_When_Name_Is_Added_And_List_Is_Cleared() {
            var filter = new CommonAssemblyFilter();
            filter.AddFilter("mscorlib");

            filter.Clear();

            bool result = filter.Match(typeof (string).Assembly.FullName);
            Assert.IsFalse(result);
        }

        [Test]
        public void Match_When_No_Name_Is_Added() {
            var filter = new CommonAssemblyFilter();
            bool result = filter.Match(typeof (string).Assembly.FullName);

            Assert.IsTrue(result);
        }

        [Test]
        public void Match_When_No_Valid_Name_Is_Added() {
            var filter = new CommonAssemblyFilter();
            filter.AddFilter("System.Web");

            bool result = filter.Match(typeof (string).Assembly.FullName);
            Assert.IsTrue(result);
        }

        [Test]
        public void No_Match_When_Valid_And_Name_Is_Empty() {
            var filter = new CommonAssemblyFilter();
            filter.AddFilter("mscorlib");

            bool result = filter.Match(string.Empty);
            Assert.IsFalse(result);
        }

        [Test]
        public void No_Match_When_Valid_And_Name_Is_Null() {
            var filter = new CommonAssemblyFilter();
            filter.AddFilter("mscorlib");

            bool result = filter.Match(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void No_Match_When_Name_Contains_System_But_Doesnt_Start_With_It () {
           var filter = new CommonAssemblyFilter();
           
           bool result = filter.Match("Company.SystemCheck.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
           Assert.IsFalse(result);
        }
    }
}