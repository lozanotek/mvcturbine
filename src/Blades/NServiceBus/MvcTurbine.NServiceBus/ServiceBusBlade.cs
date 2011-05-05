namespace MvcTurbine.NServiceBus {
    using Blades;
    using ComponentModel;
    using global::NServiceBus;
    using global::NServiceBus.ObjectBuilder.Common;
    using global::NServiceBus.ObjectBuilder.Spring;

    public class ServiceBusBlade : Blade, ISupportAutoRegistration {
        public override void Spin(IRotorContext context) {
            var busConfigure = Configure.WithWeb();
            var busConfigurator = GetConfigurator(context);

            if (busConfigurator != null) {
                busConfigurator.ConfigureBus(busConfigure);
            } else {
                // Set the container to use
                var container = GetContainer();

                busConfigure
                       .BuilderFromInternalContainer(container)
                       .BinarySerializer()
                       .MsmqTransport()
                       .UnicastBus()
                       .LoadMessageHandlers();
            }

            //Start the bus and register with the runtime
            BusManager.CurrentBus = busConfigure.CreateBus().Start();
            context.ServiceLocator.Register(BusManager.CurrentBus);
        }

        public virtual IBusConfigurator GetConfigurator(IRotorContext context) {
            try {
                return context.ServiceLocator.Resolve<IBusConfigurator>();
            } catch {
                return null;
            }
        }

        public virtual IContainer GetContainer() {
            return ContainerManager.CurrentContainer ?? new SpringObjectBuilder();
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<IMessageDispatcher>());
        }
    }
}
