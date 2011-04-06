namespace MvcTurbine.Web.Config
{
    using System.Web.Mvc;

    /// <summary>
    /// Extension class for <see cref="Engine"/>.
    /// </summary>
    public static class EngineExt {
        /// <summary>
        /// Registers an <see cref="IControllerFactory"/> for the engine to use.
        /// </summary>
        /// <typeparam name="TFactory">Type that implements <see cref="IControllerFactory"/>.</typeparam>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static Engine ControllerFactory<TFactory>(this Engine engine) 
            where TFactory : IControllerFactory {

            engine.EngineRegistration<IControllerFactory, TFactory>();
            return engine;
        }

        /// <summary>
        /// Registers a <see cref="IControllerActivator"/> for the engine to use.
        /// </summary>
        /// <typeparam name="TActivator"></typeparam>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static Engine ControllerActivator<TActivator>(this Engine engine)
            where TActivator : IControllerActivator {

            engine.EngineRegistration<IControllerActivator, TActivator>();
            return engine;
        }
    }
}
