<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#postMessage").click(function () {
                var msg = $("#messageText").val();
                $.post('home/postmessage', { message: msg }, function (data) { });
            });
        });
    </script>
    <p>
        <label for="messageText">Message:</label><input type="text" id="messageText" />
        <input type="button" id="postMessage" value="Broadcast" />
        <a href="__logs/app_log.txt" target="_blank">View Log File</a>
    </p>
</asp:Content>
