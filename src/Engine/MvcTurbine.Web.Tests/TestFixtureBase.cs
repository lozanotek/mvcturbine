using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcTurbine.Web.Tests {
    using NUnit.Framework;
    using Rhino.Mocks;
    using Rhino.Testing.AutoMocking;

    [TestFixture]
    public class TestFixtureBase {
        public TestFixtureBase() {
            Repository = new MockRepository();
            Container = new AutoMockingContainer(Repository);
        }

        public MockRepository Repository { get; private set; }
        public AutoMockingContainer Container { get; private set; }

        public IDisposable Record() {
            return Repository.Record();
        }

        public IDisposable Playback() {
            return Repository.Playback();
        }

        public T Get<T>() where T : class {
            return Container.Get<T>();
        }

        public T Stub<T>() {
            return Container.MockRepository.Stub<T>();
        }

        public T Dynamic<T>() where T : class {
            return Container.MockRepository.DynamicMock<T>();
        }

        public T Create<T>() where T : class {
            return Container.Create<T>();
        }
    }
}
