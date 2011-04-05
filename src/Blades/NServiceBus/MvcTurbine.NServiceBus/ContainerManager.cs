namespace MvcTurbine.NServiceBus {
    using System;
    using global::NServiceBus.ObjectBuilder.Common;

    public static class ContainerManager {
        public static IContainer CurrentContainer { get; private set; }

        public static void SetContainerProvider(Func<IContainer> provider) {
            if(provider == null) {
                throw new ArgumentNullException("provider", "You must provide a valid Container provider method.");
            }

            // Get the container and associate it to the static
            // holder
            CurrentContainer = provider();
        }
    }
}
