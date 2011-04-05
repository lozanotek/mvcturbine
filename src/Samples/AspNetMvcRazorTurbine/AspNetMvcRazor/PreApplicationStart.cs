//
// Author: Kazi Manzur Rashid
// All copyright and IP belongs to Author.
//
// Pulled out for consumption from the works of this blog post
// http://weblogs.asp.net/rashid/archive/2010/07/10/use-razor-as-asp-net-mvc-viewengine.aspx
//
namespace AspNetMvcRazor
{
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Mvc;
    using System.Web.UI;

    using Microsoft.Data;
    using Microsoft.WebPages;
    using Microsoft.WebPages.Compilation;
    using Microsoft.WebPages.Helpers;

    public class PreApplicationStart
    {
        private static bool started;

        public static void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            BuildManager.AddReferencedAssembly(typeof(Controller).Assembly);
            BuildManager.AddReferencedAssembly(typeof(WebPage).Assembly);
            BuildManager.AddReferencedAssembly(typeof(Database).Assembly);
            BuildManager.AddReferencedAssembly(typeof(ObjectInfo).Assembly);
            BuildManager.AddReferencedAssembly(typeof(HelperResult).Assembly);

            BuildProvider.RegisterBuildProvider(".cshtml", typeof(InlinePageBuildProvider));
            BuildProvider.RegisterBuildProvider(".vbhtml", typeof(InlinePageBuildProvider));

            PageParser.DefaultApplicationBaseType = typeof(HttpApplication);
            PageParser.EnableLongStringsAsResources = false;

            CodeGeneratorSettings.AddGlobalImport("System.Web.Mvc");
            CodeGeneratorSettings.AddGlobalImport("System.Web.Mvc.Ajax");
            CodeGeneratorSettings.AddGlobalImport("System.Web.Mvc.Html");
        }
    }
}