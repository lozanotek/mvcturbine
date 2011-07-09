namespace MvcTurbine.ComponentModel {
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Defines the interface for loading any <see cref="Assembly"/> in the applicaiton bin folder
    /// into <see cref="AppDomain.Current"/>
    /// </summary>
    public interface IBinAssemblyLoader {
        /// <summary>
        /// Loads the assemblies in the bin folder that are not currently in the <see cref="AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <returns>A list of assemblies that were loaded into the <see cref="AppDomain.CurrentDomain"/>.</returns>
        IList<string> LoadAssembliesFromBinFolder();
    }
}
