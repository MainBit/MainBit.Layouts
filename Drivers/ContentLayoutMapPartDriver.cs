using MainBit.Layouts.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Drivers
{
    public class ContentLayoutMapPartDriver : ContentPartDriver<ContentLayoutMapPart>
    {
        protected override DriverResult Display(ContentLayoutMapPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_ContentLayoutMap",
                    () => shapeHelper.Parts_ContentLayoutMap()),
                ContentShape("Parts_ContentLayoutMap_SummaryAdmin",
                    () => shapeHelper.Parts_ContentLayoutMap_SummaryAdmin())
                );
        }
    }
}