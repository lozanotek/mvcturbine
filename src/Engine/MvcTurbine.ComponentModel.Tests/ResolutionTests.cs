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

using System.Linq;

namespace MvcTurbine.ComponentModel.Tests {
    using System.Collections;
    using System.Collections.Generic;
    using Components;
    using NUnit.Framework;

    public abstract class ResolutionTests : ServiceLocatorTests {
        [Test]
        public void Resolve_Should_Return_Valid_Instance() {
            var instance = locator.Resolve<ILogger>();
            Assert.IsNotNull(instance);
        }

        [Test]
        [ExpectedException(typeof(ServiceResolutionException))]
        public void Asking_For_Invalid_Service_Should_Raise_Exception() {
            locator.Resolve<IDictionary>();
        }

        [Test]
        public void Ask_For_Named_Instance() {
            var instance = locator.Resolve<ILogger>(typeof(ComplexLogger).FullName);
            Assert.AreSame(instance.GetType(), typeof(ComplexLogger));
        }

        [Test]
        public void Ask_For_Different_Named_Instance() {
            var instance = locator.Resolve<ILogger>(typeof(SimpleLogger).FullName);
            Assert.AreSame(instance.GetType(), typeof(SimpleLogger));
        }

        [Test]
        [ExpectedException(typeof(ServiceResolutionException))]
        public void Ask_For_Unknown_Service_Should_Throw_Exception() {
            locator.Resolve<ILogger>("test");
        }

        [Test]
        public void Ask_For_All_Registered_Services() {
            IEnumerable<ILogger> instances = locator.ResolveServices<ILogger>();
            IList<ILogger> list = new List<ILogger>(instances);
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void Ask_For_Registered_Services_With_Non_Generic_Method()
        {
            IEnumerable<object> instances = locator.ResolveServices(typeof(ILogger));
            IList<ILogger> list = new List<ILogger>(instances.OfType<ILogger>());
            Assert.AreEqual(2, instances.Count());
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void Ask_For_All_Service_Instances_For_Unknown_Type_Should_Return_Empty_Enumerable() {
            IEnumerable<IDictionary> instances = locator.ResolveServices<IDictionary>();
            IList<IDictionary> list = new List<IDictionary>(instances);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Overloaded_Resolve_Method_Should_Return_Same_Instance_Type() {
            Assert.AreEqual(
                locator.Resolve<ILogger>().GetType(),
                locator.Resolve<ILogger>(typeof(ILogger)).GetType());
        }

        [Test]
        public void Overloaded_Resolve_Method_With_Name_Should_Return_Same_Instance_Type() {
            Assert.AreEqual(
                locator.Resolve<ILogger>(typeof(ComplexLogger).FullName).GetType(),
                locator.Resolve<ILogger>(typeof(ComplexLogger)).GetType()
                );
        }

        [Test]
        public void ResolveServices_Should_Return_Same_Instace_Types() {
            var genericLoggers = new List<ILogger>(locator.ResolveServices<ILogger>());

            var loggers = locator.ResolveServices<ILogger>();
            var plainLoggers = new List<object>();
            loggers.ForEach(plainLoggers.Add);

            Assert.AreEqual(genericLoggers.Count, plainLoggers.Count);
            for (int i = 0; i < genericLoggers.Count; i++) {
                Assert.AreEqual(
                    genericLoggers[i].GetType(),
                    plainLoggers[i].GetType());
            }
        }
    }
}