namespace Mvc.Bus {
    using System;
    using NServiceBus;

    [Serializable]
    public class TestMessage : IMessage {
        public TestMessage() {
            Id = Guid.NewGuid();
        }

        public string Value { get; set; }
        public Guid Id { get; set; }
    }
}
