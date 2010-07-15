//
// Author: Kazi Manzur Rashid
// All copyright and IP belongs to Author.
//
// Pulled out for consumption from the works of this blog post
// http://weblogs.asp.net/rashid/archive/2010/07/10/use-razor-as-asp-net-mvc-viewengine.aspx
//
namespace AspNetMvcRazor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Microsoft.WebPages;

    public class RazorView : IView
    {
        private static readonly PropertyInfo[] internalProperties = typeof(WebPageContext).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);

        private static readonly PropertyInfo viewContextProperty = internalProperties.Single(p => p.Name.Equals("ViewContext"));
        private static readonly PropertyInfo httpContextProperty = internalProperties.Single(p => p.Name.Equals("HttpContext"));

        private IWebPageFactory webPageFactory;

        public RazorView(string viewPath, string layoutPath)
        {
            if (string.IsNullOrEmpty(viewPath))
            {
                throw new ArgumentException("View path cannot be blank.", "viewPath");
            }

            ViewPath = viewPath;
            LayoutPath = layoutPath ?? string.Empty;
        }

        public string LayoutPath { get; private set; }

        public string ViewPath { get; private set; }

        protected IWebPageFactory WebPageFactory
        {
            get { return webPageFactory ?? (webPageFactory = new WebPageFactory()); }
            set { webPageFactory = value; }
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            WebPage viewInstance = WebPageFactory.CreateInstanceFromVirtualPath(ViewPath);

            if (viewInstance == null)
            {
                throw new InvalidOperationException(string.Format("Unable to create view from \"{0}\".", ViewPath));
            }

            if (!string.IsNullOrEmpty(LayoutPath))
            {
                viewInstance.LayoutPage = LayoutPath;
            }

            WebPageContext pageContext = new WebPageContext();

            httpContextProperty.SetValue(pageContext, viewContext.HttpContext, null);
            viewContextProperty.SetValue(pageContext, viewContext, null);

            viewInstance.ExecutePageHierarchy(pageContext, writer);
        }
    }
}