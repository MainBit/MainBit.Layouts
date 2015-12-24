using MainBit.Layouts.Services;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Helpers;


namespace MainBit.Layouts.Migrations
{
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class Compounds :  DataMigrationImpl {
        public int Create() {

            ContentDefinitionManager.AlterPartDefinition("CompoundElementPart", part => part
                .Attachable(false)
                .WithDescription("Turn content items with layout part to elements that could be used in other layouts and that are partial editable."));

            ContentDefinitionManager.AlterTypeDefinition("CompoundElement", type => type
                .Listable()
                .WithPart("CommonPart", p => p
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false")
                    .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
                .WithPart("TitlePart")
                .WithPart("IdentityPart")
                .WithPart("LayoutPart")
                .WithPart("CompoundElementPart"));

            return 1;
        }
    }
}