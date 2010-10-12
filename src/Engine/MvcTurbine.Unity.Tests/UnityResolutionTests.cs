#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace MvcTurbine.Unity.Tests {
    using ComponentModel;
    using ComponentModel.Tests;
    using ComponentModel.Tests.Components;
    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    [TestFixture]
    public class UnityResolutionTests : ResolutionTests {
        protected override IServiceLocator CreateServiceLocator() {

            var container = new UnityContainer();
            var simpleType = typeof(SimpleLogger);
            var complexType = typeof(ComplexLogger);

            container.RegisterType(typeof(ILogger), simpleType);
            container.RegisterType(typeof(ILogger), simpleType, simpleType.FullName);
            container.RegisterType(typeof(ILogger), complexType, complexType.FullName);

            return new Unity.UnityServiceLocator(container);
        }

        [Test]
        public void Inject_Should_Set_Dependencies_On_Instance_When_Dependencies_Are_Not_Defined_On_The_Interface_And_Resolved_As_The_Interface_Type()
        {
            locator.Register(typeof(ITestDependency), typeof(TestDependency));

            ISample instanceAsInterface = new Sample();
            locator.Inject(instanceAsInterface);

            var instance = (Sample)instanceAsInterface;
            Assert.IsNotNull(instance.DependencyThatDoesNotExistOnInterface);
        }
    }


    public interface ISample {
    }

    public class Sample : ISample {
        [Dependency]
        public ITestDependency DependencyThatDoesNotExistOnInterface { get; set; }
    }

    public interface ITestDependency { }
    public class TestDependency : ITestDependency { }
}
