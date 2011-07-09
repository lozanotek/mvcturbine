namespace MvcTurbine.Web.Blades {
    using System.Collections;
    using System.Collections.Generic;
    using Controllers;

    /// <summary>
    /// Simple container for inferred actions for the applications.
    /// </summary>
    public class InferredActions : IEnumerable<InferredAction> {
        private static List<InferredAction> registrations;
        private static readonly InferredActions instance = new InferredActions();

        private InferredActions() {
            registrations = new List<InferredAction>();
        }

        /// <summary>
        /// Gets the current instance for the inferred action list.
        /// </summary>
        public static InferredActions Current {
            get { return instance; }
        }

        /// <summary>
        /// Adds the specified action to the underlying list.
        /// </summary>
        /// <param name="actionRegistration"></param>
        public void AddRegistration(InferredAction actionRegistration) {
            if (actionRegistration == null) return;

            registrations.Add(actionRegistration);
        }

        /// <summary>
        /// Adds the specified collection of actions to the underlying list.
        /// </summary>
        /// <param name="actionRegistrations"></param>
        public void AddRegistrations(IEnumerable<InferredAction> actionRegistrations) {
            if (actionRegistrations == null) return;

            registrations.AddRange(actionRegistrations);
        }

        public IEnumerator<InferredAction> GetEnumerator() {
            return registrations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}