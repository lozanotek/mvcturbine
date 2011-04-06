namespace MvcTurbine.Tests.Blades {
    using System;
    using MvcTurbine.Blades;

    public class MockBlade : Blade {
        public MockBlade() {
            Initialized += (sender, args) => { IsInitialized = true; };
            Disposed += (sender, args) => { IsDisposed = true; };
        }

        public bool IsInitialized { get; private set; }
        public bool HasSpunned { get; private set; }
        public bool IsDisposed { get; private set; }

        public override void Spin(IRotorContext context) {
            if (context == null) {
                throw new InvalidOperationException();
            }

            HasSpunned = true;
        }
    }
}