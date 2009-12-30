namespace MvcTurbine.Samples.FilterInjection.Filters {
    using Services;

    /// <summary>
    /// Base class for any filter implementation
    /// </summary>
    public abstract class BaseFilter {
        protected BaseFilter(IMessageService service) {
            MessageService = service;
        }

        public IMessageService MessageService { get; private set; }

        public string GetFilterMessage() {
            return string.Format("Default message: '{0}' from filter '{1}'",
                                 MessageService.GetFilterMessage(),
                                 GetType().Name);
        }
    }
}