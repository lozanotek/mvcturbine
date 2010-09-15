<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnotherModel.Task>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
		Create New Task
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
		<script src="../../Scripts/jquery.validate.min.js" type="text/javascript"></script>
		<script type="text/javascript">
				$(function () {
						$("#newForm").validate();
				});
		</script>
		<h2>
				Create New Task</h2>
		<form action="create" id="newForm" method="post">
		<div class="editor-label">
				<%= Html.LabelFor(model => model.Name) %>
		</div>
		<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Name, new { @class="required"})%>
		</div>
		<div class="editor-label">
				<%= Html.LabelFor(model => model.Description) %>
		</div>
		<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Description) %>
		</div>
		<p>
				<input type="submit" value="Create" />
		</p>
		</form>
		<div>
				<%= Html.ActionLink("Back to List", "Index") %>
		</div>
</asp:Content>
