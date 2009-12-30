<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Okonau.Mvc.ViewModels.TaskInputModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create New Task</h2>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm()) {%>
    <p>
        <label for="Name"><strong>Name:</strong></label>
        <br />
        <%= Html.TextBox("Name","", new{size=51}) %>
        <%= Html.ValidationMessage("Name", "*") %>
    </p>
    <p>
        <label for="Description"><strong>Description:</strong></label>
        <br />
        <%=Html.TextArea("Description", "",10,40,null)%>
        <%= Html.ValidationMessage("Description", "*") %>
    </p>
    <p>
        <input type="submit" value="Create" />
    </p>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
