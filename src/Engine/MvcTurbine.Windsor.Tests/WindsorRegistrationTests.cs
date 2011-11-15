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


namespace MvcTurbine.Windsor.Tests
{
    using ComponentModel;
    using ComponentModel.Tests;
    using NUnit.Framework;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.MicroKernel.SubSystems.Configuration;


    [TestFixture]
    public class WindsorRegistrationTests : RegistrationTests
    {

        protected override IServiceLocator CreateServiceLocator()
        {
            return new WindsorServiceLocator();
        }

        [Test]
        public void it_should_run_the_WindsorInstallers_that_it_finds_in_the_assembly()
        {
            var tc = locator.Resolve<ITestInstallerInterface>();
            Assert.IsNotNull(tc);
            Assert.IsInstanceOfType(typeof(TestInstallerInterface), tc);
        }
    }

    public interface ITestInstallerInterface { }

    public class TestInstallerInterface : ITestInstallerInterface { }

    public class MockInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITestInstallerInterface>().ImplementedBy<TestInstallerInterface>());
        }
    }
}