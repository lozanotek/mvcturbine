namespace MvcTurbine.Web.Tests.Blades {
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;
    using NUnit.Framework;
    using Web.Blades;

    static class TypeExtesion {
        public static bool IsMvcType(this Type type) {
            return type.IsType<IController>() ||
                    type.IsType<IViewEngine>() ||
                    type.IsType<IModelBinder>() ||
                    type.IsType<IAuthorizationFilter>() ||
                    type.IsType<IActionFilter>() ||
                    type.IsType<IResultFilter>() ||
                    type.IsType<IExceptionFilter>();
        }
    }
}