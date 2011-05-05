namespace Mvc.Bus
{
    using NServiceBus;
    using Services;

    public class AnotherHandler : IHandleMessages<TestMessage>
    {
        private LogService Service { get; set; }

        public AnotherHandler(LogService service)
        {
            Service = service;
        }

        public void Handle(TestMessage message)
        {
            Service.LogMessage("--another-- " + message.Value);
        }
    }
}