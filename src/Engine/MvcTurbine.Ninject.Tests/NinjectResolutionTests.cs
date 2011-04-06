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

namespace MvcTurbine.Ninject.Tests {
    using ComponentModel;
    using ComponentModel.Tests;
    using ComponentModel.Tests.Components;
    using global::Ninject;
    using NUnit.Framework;

    [TestFixture]
    public class NinjectResolutionTests : ResolutionTests {
        protected override IServiceLocator CreateServiceLocator() {
            var kernel = new StandardKernel();
            var simpleType = typeof(SimpleLogger);
            kernel.Bind<ILogger>().To<SimpleLogger>().Named(simpleType.FullName);

            var loggerType = typeof(ComplexLogger);
            kernel.Bind<ILogger>().To<ComplexLogger>().Named(loggerType.FullName);
            return new NinjectServiceLocator(kernel);
        }
    }
}
