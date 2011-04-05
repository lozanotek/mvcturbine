namespace MvcTurbine.NServiceBus {
    using global::NServiceBus;
    using global::NServiceBus.ObjectBuilder.Common;
    using global::NServiceBus.ObjectBuilder.Common.Config;

    static class ServiceLocatorExtension {
        public static Configure BuilderFromInternalContainer(this Configure config,
            IContainer container) {

            ConfigureCommon.With(config, container);
            return config;
        }
    }
}
