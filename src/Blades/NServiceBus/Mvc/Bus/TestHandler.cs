namespace Mvc.Bus {
    using NServiceBus;
    using Services;

    public class TestHandler : IHandleMessages<TestMessage> {
        private LogService Service { get; set; }

        public TestHandler(LogService service)
        {
            Service = service;
        }

        public void Handle(TestMessage message)
        {
            Service.LogMessage(message.Value);
        }
    }
}
