using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace QuickLinks
{
    public static class Extensions
    {
        public static void RenderQuickLinks(this HtmlHelper htmlHelper)
        {
            htmlHelper.RenderAction("Index", "Links", new { area = "QuickLinks" });
        }
    }
}
