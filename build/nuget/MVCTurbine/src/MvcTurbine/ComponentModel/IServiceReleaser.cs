namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Provides the contract for releasing an object that's been created by the underlying container.
    /// </summary>
    public interface IServiceReleaser {
        void Release(object instance);
    }
}
