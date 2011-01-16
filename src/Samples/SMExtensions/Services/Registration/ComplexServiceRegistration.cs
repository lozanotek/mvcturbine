namespace Services.Registration {
    using MvcTurbine.StructureMap;
    using StructureMap;

    public class ComplexServiceRegistration : StructureMapRegistrator {
        public override void Register(IContainer container) {
            container.Configure(
                config => config
                    .For<IMessageService>()
                    .HttpContextScoped()
                    .Use<MessageService>());
        }
    }
}
