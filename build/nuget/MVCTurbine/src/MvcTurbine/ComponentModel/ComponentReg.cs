namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Defines the basic registration for a component.
    /// </summary>
    [Serializable]
    public class ComponentReg {
        public string Key { get; set; }
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
    }
}