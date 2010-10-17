namespace Mvc3Host.Controllers {
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Web.Views;

    public class DynamicLocator : DynamicObject {

        public IServiceLocator Locator { 
            get;
            private set;
        }

        public DynamicLocator(WebViewPage viewPage) {
            Locator = viewPage.ViewContext.ServiceLocator();
        }

        public DynamicLocator(ViewPage viewPage) {
            Locator = viewPage.ViewContext.ServiceLocator();            
        }

        public DynamicLocator(HtmlHelper helper) {
            Locator = helper.ViewContext.ServiceLocator();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            var instance = GetServiceByKey(binder.Name);
            if (instance != null) {
                result = instance;
                return true;
            }

            result = GetByContract(binder);
            return true;
        }

        private object GetByContract(GetMemberBinder binder) {
            Type contractType = GetContractType(binder.Name);

            if (contractType == null) {
                contractType = GetContractType("I" + binder.Name);

                if (contractType == null) {
                    throw new InvalidOperationException(string.Format("Service for '{0}' was not registered.",
                                                                      binder.Name));
                }
            }

            return Locator.Resolve(contractType);
        }

        private object GetServiceByKey(string name) {
            try {
                return Locator.Resolve<object>(name);
            }
            catch {
                return null;
            }
        }

        private Type GetContractType(string contractName) {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(type => type.Name.ToLower().Contains(contractName.ToLower()))
                .FirstOrDefault();
        }

    }
}
