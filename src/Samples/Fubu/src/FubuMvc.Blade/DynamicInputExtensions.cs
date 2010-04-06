namespace FubuMvc.Blades.UI {
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using FubuMVC.Core.Util;
    using FubuMVC.UI.Configuration;
    using FubuMVC.UI.Tags;
    using MvcTurbine.Web.Controllers;

    public static class DynamicInputExtensions {
        private static ITagGenerator<T> GetGenerator<T>(HtmlHelper<T> helper) where T : class {
            var locator = helper.ViewContext.ServiceLocator();

            var generator = locator.Resolve<ITagGenerator<T>>() as TagGenerator<T>;
            generator.Model = helper.ViewData.Model;
            return generator;
        }

        public static string InputFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class {
            var generator = GetGenerator(helper);
            return generator.InputFor(expression).ToString();
        }

        public static string LabelFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class {
            var generator = GetGenerator(helper);
            return generator.LabelFor(expression).ToString();
        }

        public static string DisplayFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class {
            var generator = GetGenerator(helper);
            return generator.DisplayFor(expression).ToString();
        }

        public static string ElementNameFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class {
            var locator = helper.ViewContext.ServiceLocator();
            var convention = locator.Resolve<IElementNamingConvention>();
            return convention.GetName(typeof(T), expression.ToAccessor());
        }
    }
}
