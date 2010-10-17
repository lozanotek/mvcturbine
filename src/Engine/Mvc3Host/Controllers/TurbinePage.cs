namespace Mvc3Host.Controllers {
    using System.Web.Mvc;

    public abstract class TurbinePage<TModel> : WebViewPage<TModel> {
        public dynamic Service {
            get { return new DynamicLocator(this); }
        }
    }

    public abstract class TurbinePage : WebViewPage {
        public dynamic Service {
            get { return new DynamicLocator(this); }
        }
    }
}
