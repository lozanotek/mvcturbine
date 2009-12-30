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

namespace MvcTurbine.ComponentModel.Tests {
    using System;
    using Components;
    using NUnit.Framework;

    public abstract class RegistrationTests : ServiceLocatorTests {
        [Test]
        public void Register_With_Specified_Type_Should_Return_Same_Type() {
            Type loggerType = typeof(SimpleLogger);

            using (locator.Batch()) {
                locator.Register<ILogger>(loggerType);
            }

            var logger = locator.Resolve<ILogger>();
            Assert.AreEqual(logger.GetType(), loggerType);
        }

        [Test]
        public void Register_With_Implementation_Type_Should_Return_Same_Type() {
            Type type = typeof(ComplexLogger);

            using (locator.Batch()) {
                locator.Register<ILogger, ComplexLogger>();
            }

            var logger = locator.Resolve<ILogger>();
            Assert.AreEqual(type, logger.GetType());
        }

        [Test]
        public void Register_With_Keyed_Implementation_Should_Return_Same_Type() {
            Type loggerType = typeof(SimpleLogger);

            using (locator.Batch()) {
                locator.Register<ILogger, SimpleLogger>(loggerType.FullName);
            }

            var logger = locator.Resolve<ILogger>(loggerType.FullName);
            Assert.AreEqual(loggerType, logger.GetType());
        }

        [Test]
        public void Register_With_Keyed_Type_Should_Return_Same_Type() {
            Type loggerType = typeof(SimpleLogger);

            using (locator.Batch()) {
                locator.Register(loggerType.FullName, loggerType);
            }

            var logger = locator.Resolve<SimpleLogger>(loggerType.FullName);
            Assert.AreEqual(loggerType, logger.GetType());
        }

        [Test]
        public void Register_With_Specified_Service_And_Type_Should_Return_Same_Type() {
            Type serviceType = typeof(ILogger);
            Type implType = typeof(SimpleLogger);

            using (locator.Batch()) {
                locator.Register(serviceType, implType);
            }

            var logger = locator.Resolve<ILogger>(implType);
            Assert.AreEqual(implType, logger.GetType());
        }
    }
}