using Orchard.UI.Resources;

namespace MainBit.Layouts {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("MainBit.Layouts.Models").SetUrl("Models.min.js", "Models.js").SetDependencies("Layouts.LayoutEditor");
            manifest.DefineScript("MainBit.Layouts.LayoutEditors").SetUrl("LayoutEditor.min.js", "LayoutEditor.js").SetDependencies("Layouts.LayoutEditor", "MainBit.Layouts.Models");
        }
    }
}