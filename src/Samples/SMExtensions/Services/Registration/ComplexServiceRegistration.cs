namespace Services.Registration {
    using StructureMap;
    using StructureMapExtensions;

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
