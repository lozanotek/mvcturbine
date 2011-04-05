namespace Mvc.Areas {
    using System.Web.Mvc;

    public class SimpleAreaDependency : IAreaDependency {
        public string GetAreaName(AreaRegistration registration) {
            return registration.GetType().Name.Replace("AreaRegistration", string.Empty);
        }
    }
}
