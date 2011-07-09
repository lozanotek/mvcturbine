namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class to work around the pieces for invalid inferred actions.
    /// </summary>
    public class InferredViewResult : ViewResult {
        /// <summary>
        /// Checks whether the <see cref="ViewEngineResult"/> is valid, if not an HTTP 404 is thrown.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override ViewEngineResult FindView(ControllerContext context) {
            try {
                return base.FindView(context);
            }
            catch (InvalidOperationException e) {
                throw new HttpException(404, e.Message);
            }
        }
    }
}