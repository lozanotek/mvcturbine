namespace MvcTurbine.NServiceBus {
    using global::NServiceBus;

    public interface IBusConfigurator {
        void ConfigureBus(Configure configure);
    }
}
