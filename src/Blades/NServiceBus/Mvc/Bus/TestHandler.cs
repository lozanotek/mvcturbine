namespace Mvc.Bus {
    using log4net;
    using NServiceBus;

    public class TestHandler : IHandleMessages<TestMessage> {
        private readonly ILog log = LogManager.GetLogger(typeof (TestHandler));

        public void Handle(TestMessage message) {
            log.InfoFormat("Received message with contents '{0}'", message.Value);
        }
    }
}
