<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
		Inherits="System.Web.Mvc.ViewPage<IList<SomeModel.Person>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Current People</h2>
				<p><a href="person/createnew">Create</a></p>
				<table>
						<tr>
								<th>First Name</th>
								<th>Last Name</th>
								<th>Created</th>
								<th></th>
						</tr>
						<% foreach (var entityModel in Model) { %>
						<tr>
								<td><%= Html.Encode(entityModel.FirstName) %></td>
								<td><%= Html.Encode(entityModel.LastName) %></td>
								<td><%= Html.Encode(entityModel.Created) %></td>
								<td><%= Html.ActionLink("Delete", "delete", new {id = entityModel.Id})%></td>
						</tr>
						<%} %>
				</table>
</asp:Content>
