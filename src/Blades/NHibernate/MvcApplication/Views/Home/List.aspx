<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SomeModel.Person>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
		List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
		<h2>
				List</h2>
		<table>
				<tr>
						<th>
								Id
						</th>
						<th>
								Name
						</th>
				</tr>
				<% foreach (var item in Model) { %>
				<tr>
						<td>
								<%= Html.Encode(item.Id) %>
						</td>
						<td>
								<%= Html.Encode(item.FullName) %>
						</td>
				</tr>
				<% } %>
		</table>
		<p>
				<%= Html.ActionLink("Create New", "Create") %>
		</p>
</asp:Content>
