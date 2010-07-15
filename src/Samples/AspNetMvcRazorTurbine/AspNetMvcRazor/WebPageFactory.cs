//
// Author: Kazi Manzur Rashid
// All copyright and IP belongs to Author.
//
// Pulled out for consumption from the works of this blog post
// http://weblogs.asp.net/rashid/archive/2010/07/10/use-razor-as-asp-net-mvc-viewengine.aspx
//
namespace AspNetMvcRazor
{
    using Microsoft.WebPages;

    public interface IWebPageFactory
    {
        WebPage CreateInstanceFromVirtualPath(string virtualPath);
    }

    public class WebPageFactory : IWebPageFactory
    {
        public WebPage CreateInstanceFromVirtualPath(string virtualPath)
        {
            return WebPageBase.CreateInstanceFromVirtualPath(virtualPath);
        }
    }
}