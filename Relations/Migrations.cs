using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.Relations
{
    [OrchardFeature("MainBit.Layouts.Relations")]
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("LayoutContentMapRecord",
                table => table
                    .Column<int>("Id", c=> c.PrimaryKey().Identity())
                    .Column<int>("LayoutPartRecord_Id")
                    .Column<int>("ContentItemRecord_Id")
            );

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("ContentLayoutMapPart", part => part
                .WithDescription("Show layout parts witch content item is added to")
                .Attachable(true)
            );

            return 2;
        }
    }
}