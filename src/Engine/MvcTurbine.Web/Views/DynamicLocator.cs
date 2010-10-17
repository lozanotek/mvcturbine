namespace MvcTurbine.Web.Views {
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;

    public class DynamicLocator : DynamicObject {
        public IServiceLocator Locator {
            get;
            private set;
        }

        public DynamicLocator(WebViewPage viewPage) : this(viewPage.ViewContext.ServiceLocator()) {
        }

        public DynamicLocator(ViewPage viewPage) : this(viewPage.ViewContext.ServiceLocator()) {
        }

        public DynamicLocator(HtmlHelper helper) : this(helper.ViewContext.ServiceLocator()) {
        }

        public DynamicLocator(IServiceLocator locator) {
            Locator = locator;
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

        public virtual object GetByContract(GetMemberBinder binder) {
            var contractType = GetContractType(binder.Name);

            if (contractType == null) {
                // Assume that the name of the property is the name
                // of the contract sans "I"
                contractType = GetContractType("I" + binder.Name);

                if (contractType == null) {
                    // If we've made it this far, something is wrong and we should throw an error.
                    throw new InvalidOperationException(string.Format("Service for '{0}' was not registered.", binder.Name));
                }
            }

            return Locator.Resolve(contractType);
        }

        public virtual object GetServiceByKey(string name) {
            try {
                return Locator.Resolve<object>(name);
            } catch {
                // Not sure what to do here - I'm ok with this
                // since we're throwing an exception further up the chain
                return null;
            }
        }

        protected virtual Type GetContractType(string contractName) {
            // Have to go with route since we don't have an
            // Assembly FQN for the Type - perhaps we need to do some type caching 
            // here to get better perf
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(type => type.Name.ToLower().Contains(contractName.ToLower()))
                .FirstOrDefault();
        }
    }
}
