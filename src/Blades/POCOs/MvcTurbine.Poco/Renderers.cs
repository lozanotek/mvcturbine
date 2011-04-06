namespace MvcTurbine.Poco {
    using System.Collections;
    using System.Collections.Generic;

    public class Renderers : IEnumerable<RendererReg> {
        private static List<RendererReg> rendererList;
        private static Renderers instance = new Renderers();

        private Renderers() {
            rendererList = new List<RendererReg>();
        }

        public static Renderers Current {
            get { return instance; }
        }

        public void Clear() {
            rendererList.Clear();
        }

        public void Remove(RendererReg renderer) {
            rendererList.Remove(renderer);
        }

        public void Add(RendererReg renderer) {
            if (renderer == null) return;
            rendererList.Add(renderer);
        }

        public void Add(IEnumerable<RendererReg> renderers) {
            if (renderers == null) return;
            rendererList.AddRange(renderers);
        }

        public IEnumerator<RendererReg> GetEnumerator() {
            return rendererList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}