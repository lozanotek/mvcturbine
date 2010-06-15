namespace MvcTurbine.NServiceBus {
    using global::NServiceBus;

    public static class BusManager {
        public static IBus CurrentBus { get; set; }
    }
}
