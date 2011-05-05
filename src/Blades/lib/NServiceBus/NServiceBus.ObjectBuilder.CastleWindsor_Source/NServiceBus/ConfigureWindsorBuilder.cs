namespace NServiceBus
{
    using Castle.Windsor;
    using NServiceBus.ObjectBuilder.CastleWindsor;
    using NServiceBus.ObjectBuilder.Common.Config;
    using System;
    using System.Runtime.CompilerServices;

    public static class ConfigureWindsorBuilder
    {
        public static Configure CastleWindsorBuilder(this Configure config)
        {
            ConfigureCommon.With(config, new WindsorObjectBuilder());
            return config;
        }

        public static Configure CastleWindsorBuilder(this Configure config, IWindsorContainer container)
        {
            ConfigureCommon.With(config, new WindsorObjectBuilder(container));
            return config;
        }
    }
}

