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
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.WebPages;

    public class RazorViewEngine : VirtualPathProviderViewEngine
    {
        private static readonly string[] supportedLanguages = new[] { "cs", "vb" };

        private IWebPageFactory webPageFactory;

        public RazorViewEngine() : this("cs") {
        }

        public RazorViewEngine(string acronym)
        {
            if (string.IsNullOrWhiteSpace(acronym))
            {
                throw new ArgumentException("Language acronym cannot be blank.", "acronym");
            }

            if (!supportedLanguages.Contains(acronym, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(string.Format("Unsupported language acronym, WebMatrix view engine currently supports {0}.", string.Join(", ", supportedLanguages.Select(lang => "\"" + lang + "\""))), "acronym");
            }

            string extension = "." + acronym.ToLowerInvariant() + "html";

            MasterLocationFormats = new[]
                                        {
                                            "~/Views/{1}/_{0}" + extension,
                                            "~/Views/Shared/_{0}" + extension
                                        };

            AreaMasterLocationFormats = new[]
                                            {
                                                "~/Areas/{2}/Views/{1}/_{0}" + extension,
                                                "~/Areas/{2}/Views/Shared/_{0}" + extension
                                            };

            ViewLocationFormats = new[]
                                      {
                                          "~/Views/{1}/{0}" + extension,
                                          "~/Views/{1}/_{0}" + extension,
                                          "~/Views/Shared/{0}" + extension,
                                          "~/Views/Shared/_{0}" + extension
                                      };

            AreaViewLocationFormats = new[]
                                          {
                                              "~/Areas/{2}/Views/{1}/{0}" + extension,
                                              "~/Areas/{2}/Views/{1}/_{0}" + extension,
                                              "~/Areas/{2}/Views/Shared/{0}" + extension,
                                              "~/Areas/{2}/Views/Shared/_{0}" + extension
                                          };

            PartialViewLocationFormats = ViewLocationFormats;
            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }

        protected IWebPageFactory WebPageFactory
        {
            get { return webPageFactory ?? (webPageFactory = new WebPageFactory()); }
            set { webPageFactory = value; }
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            // Does not support partial, should we throw exception ??
            return new RazorView(partialPath, null);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(viewPath, masterPath);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            try
            {
                WebPage viewInstance = WebPageFactory.CreateInstanceFromVirtualPath(virtualPath);

                return viewInstance != null;
            }
            catch (HttpException he)
            {
                if (he is HttpParseException)
                {
                    // The build manager found something, but instantiation failed due to a runtime lookup failure
                    throw;
                }

                if (he.GetHttpCode() == (int) HttpStatusCode.NotFound)
                {
                    // If BuildManager returns a 404 (Not Found) that means that a file did not exist.
                    // If the view itself doesn't exist, then this method should report that rather than throw an exception.
                    if (!base.FileExists(controllerContext, virtualPath))
                    {
                        return false;
                    }
                }

                // All other error codes imply other errors such as compilation or parsing errors
                throw;
            }
        }
    }
}