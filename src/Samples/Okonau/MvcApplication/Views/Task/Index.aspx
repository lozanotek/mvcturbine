<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<TaskViewModel>>" %>

<%@ Import Namespace="Okonau.Mvc.ViewModels" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("a.delete").click(function() {
                return confirm("Do you want to delete this task?");
            });
        });
    </script>

    <%var displayNo = (Model == null || Model.Count == 0); %>
    <h2>
        <%if (displayNo) {%> No <%}%>Pending Work</h2>
    <ul>
        <%
            if (displayNo) return;
            
            foreach (var viewModel in Model) {%>
        <li>
            <div>
            <label><%=Html.Encode(viewModel.Name)%></label><br />
            <span><%=Html.Encode(viewModel.Created) %></span>
            <%=Html.ActionLink("Delete", "Delete", new { id = viewModel.Id }, new { @class = "delete" })%>
            <p>
                <%=Html.Encode(viewModel.Description) %></p>
                </div>
        </li>
        <%} %>
    </ul>
</asp:Content>
