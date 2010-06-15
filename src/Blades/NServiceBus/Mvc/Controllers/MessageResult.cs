namespace Mvc.Controllers {
    using System;
    using System.Web.Mvc;

    public class MessageResult : EmptyResult {
        public Guid MessageId { get; private set; }

        public MessageResult(Guid messageId) {
            MessageId = messageId;
        }

        public override void ExecuteResult(ControllerContext context) {
            context.HttpContext.Response.StatusDescription = MessageId.ToString();
            context.HttpContext.Response.Output.Write(MessageId);
        }
    }
}