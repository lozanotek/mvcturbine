namespace MvcTurbine.Poco {
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class RendererReg {
        public RendererReg() {
            AcceptTypes = new List<string>();
        }

        public Type RendererType { get; set; }
        public IList<string> AcceptTypes { get; set; }
    }
}