using MainBit.Layouts.Services;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Helpers;


namespace MainBit.Layouts.Migrations
{
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class Bunches :  DataMigrationImpl {
        public int Create() {

            ContentDefinitionManager.AlterPartDefinition("BunchElementPart", part => part
                .Attachable(false)
                .WithDescription("Wield elements into one element, that can be added to an layout."));

            ContentDefinitionManager.AlterTypeDefinition("BunchElement", type => type
                .Listable()
                .WithPart("CommonPart", p => p
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false")
                    .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
                .WithPart("TitlePart")
                .WithPart("IdentityPart")
                .WithPart("BunchElementPart"));

            return 1;
        }
    }
}