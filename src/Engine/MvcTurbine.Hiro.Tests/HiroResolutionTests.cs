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

using System;
using System.Collections.Generic;
using System.Linq;
using Hiro;

namespace MvcTurbine.Hiro.Tests {
    using ComponentModel;
    using ComponentModel.Tests;
    using ComponentModel.Tests.Components;
    using NUnit.Framework;

    [TestFixture]
    public class HiroResolutionTests : ResolutionTests
    {
        protected override IServiceLocator CreateServiceLocator()
        {
            var dependencyMap = new DependencyMap();
            var simpleType = typeof(SimpleLogger);
            var complexType = typeof(ComplexLogger);

            dependencyMap.AddService("__", typeof(ILogger), complexType);
            dependencyMap.AddService<ILogger, SimpleLogger>(simpleType.FullName);
            dependencyMap.AddService<ILogger, ComplexLogger>(complexType.FullName);

            var hiroServiceLocator = new HiroServiceLocator(dependencyMap);
            return hiroServiceLocator;
        }

        [Test]
        public override void Ask_For_All_Registered_Services()
        {
            IEnumerable<ILogger> instances = locator.ResolveServices<ILogger>();
            IList<ILogger> list = new List<ILogger>(instances);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public override void Ask_For_Registered_Services_With_Non_Generic_Method()
        {
            IEnumerable<object> instances = locator.ResolveServices(typeof(ILogger));
            IList<ILogger> list = new List<ILogger>(instances.OfType<ILogger>());
            Assert.AreEqual(3, instances.Count());
            Assert.AreEqual(3, list.Count);
        }
    }
}
