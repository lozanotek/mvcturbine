namespace MvcTurbine.NServiceBus {
    using global::NServiceBus;

    public class NServiceBusDispatcher : IMessageDispatcher {
        public IBus CurrentBus { get; private set; }

        public NServiceBusDispatcher() {
            CurrentBus = BusManager.CurrentBus;
        }

        public void Dispatch<TMessage>(TMessage message) where TMessage : IMessage {
            CurrentBus.Send(message);
        }
    }
}
