<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
		Inherits="System.Web.Mvc.ViewPage<IList<AnotherModel.Task>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Current Tasks</h2>
				<p><a href="task/createnew">Create</a></p>
				<table>
						<tr>
								<th>Name</th>
								<th>Description</th>
								<th>Created</th>
								<th></th>
						</tr>
						<% foreach (var taskModel in Model) { %>
						<tr>
								<td><%= Html.Encode(taskModel.Name) %></td>
								<td><%= Html.Encode(taskModel.Description) %></td>
								<td><%= Html.Encode(taskModel.Created) %></td>
								<td><%= Html.ActionLink("Delete", "delete", new {id = taskModel.Id})%></td>
						</tr>
						<%} %>
				</table>
</asp:Content>
