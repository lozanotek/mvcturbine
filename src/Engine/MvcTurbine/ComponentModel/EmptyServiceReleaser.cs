namespace MvcTurbine.ComponentModel
{
    public class EmptyServiceReleaser : IServiceReleaser
    {
        public void Release(object instance)
        {
        }
    }
}