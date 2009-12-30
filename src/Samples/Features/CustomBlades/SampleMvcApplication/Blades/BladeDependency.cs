namespace MvcTurbine.Samples.CustomBlades.Blades {
    using System.Threading;

    public class BladeDependency : IBladeDependency {
        public void DoWork() {
            Thread.Sleep (5000);
        }
    }
}