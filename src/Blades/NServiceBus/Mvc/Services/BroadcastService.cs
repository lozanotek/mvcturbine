namespace Mvc.Services {
    using System;
    using Bus;
    using MvcTurbine.NServiceBus;

    public class BroadcastService : IBroadcastService {
        public IMessageDispatcher Dispatcher { get; private set; }

        public BroadcastService(IMessageDispatcher dispatcher) {
            Dispatcher = dispatcher;
        }

        public Guid Broadcast(string message) {
            var busMessage = new TestMessage() {Value = message};
            Dispatcher.Dispatch(busMessage);

            return busMessage.Id;
        }
    }
}