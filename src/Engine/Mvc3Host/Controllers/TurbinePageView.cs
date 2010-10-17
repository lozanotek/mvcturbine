namespace Mvc3Host.Controllers {
    using System.Web.Mvc;

    public class TurbinePageView : ViewPage {
        public dynamic Service {
            get { return new DynamicLocator(this); }
        }
    }

    public class TurbinePageView<TModel> : ViewPage<TModel> {
        public dynamic Service {
            get { return new DynamicLocator(this); }
        }
    }
}
