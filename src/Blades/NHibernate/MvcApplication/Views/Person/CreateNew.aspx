<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SomeModel.Person>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
		Create New Person
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
		<script src="../../Scripts/jquery.validate.min.js" type="text/javascript"></script>
		<script type="text/javascript">
				$(function () {
						$("#newForm").validate();
				});
		</script>
		<h2>
				Create New Person</h2>
		<form action="create" id="newForm" method="post">
		<div class="editor-label">
				<%= Html.LabelFor(model => model.FirstName) %>
		</div>
		<div class="editor-field">
				<%= Html.TextBoxFor(model => model.FirstName, new { @class = "required" })%>
		</div>
		<div class="editor-label">
				<%= Html.LabelFor(model => model.LastName) %>
		</div>
		<div class="editor-field">
				<%= Html.TextBoxFor(model => model.LastName, new { @class = "required" })%>
		</div>
		<p>
				<input type="submit" value="Create" />
		</p>
		</form>
		<div>
				<%= Html.ActionLink("Back to List", "Index") %>
		</div>
</asp:Content>
