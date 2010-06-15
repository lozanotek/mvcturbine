namespace MvcTurbine.NServiceBus {
    using global::NServiceBus;

    public interface IMessageDispatcher {
        void Dispatch<TMessage>(TMessage message) where TMessage : IMessage;
    }
}
