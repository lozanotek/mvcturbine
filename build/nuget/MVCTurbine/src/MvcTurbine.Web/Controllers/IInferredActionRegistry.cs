namespace MvcTurbine.Web.Controllers
{
    using System.Collections.Generic;

    ///<summary>
    /// Defines the simple interface for getting models for inferred actions.
    ///</summary>
    public interface IInferredActionRegistry {
        IEnumerable<InferredAction> GetActionRegistrations();
    }
}