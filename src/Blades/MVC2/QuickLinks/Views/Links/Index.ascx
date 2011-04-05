<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="MvcContrib" %>

<link href="<%= Url.Resource("Content.QuickLinks.css") %>" rel="stylesheet" type="text/css" />
<img src="<%= Url.Resource("images.globe.png") %>" />
Quick Links
<img src="/quicklinks/images/globe.png" />

<ul class="links">
    <li><a href="http://www.google.com">Google</a></li>
    <li><a href="http://www.bing.com">Bing</a></li>
    <li><a href="http://www.yahoo.com">Yahoo</a></li>
</ul>
