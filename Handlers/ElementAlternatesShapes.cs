using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using Orchard.DisplayManagement.Descriptors.ResourceBindingStrategy;
using Orchard.Environment;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Settings;
using Orchard.UI;
using Orchard.UI.Resources;
using Orchard.UI.Zones;
using Orchard.Utility.Extensions;
using Orchard;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.Handlers
{
    [OrchardFeature("MainBit.Layouts.Alternates")]
    public class ElementAlternatesShapes : IShapeTableProvider
    {

        public void Discover(ShapeTableBuilder builder) {

            builder.Describe("Menu")
                .OnDisplaying(displaying =>
                {
                    string displayType = displaying.Shape.DisplayType;

                    if (!string.IsNullOrWhiteSpace(displayType))
                    {
                        displaying.Shape.Metadata.Alternates.Add("Menu_" + EncodeAlternateElement(displayType));
                    }
                });

            builder.Describe("MenuItem")
                .OnDisplaying(displaying =>
                {
                    string displayType = displaying.Shape.Menu.DisplayType;

                    if (!string.IsNullOrWhiteSpace(displayType))
                    {
                        displaying.Shape.Metadata.Alternates.Add("MenuItem_" + EncodeAlternateElement(displayType));
                    }
                });

            builder.Describe("MenuItemLink")
                .OnDisplaying(displaying =>
                {
                    string displayType = displaying.Shape.Menu.DisplayType;

                    if (!string.IsNullOrWhiteSpace(displayType))
                    {
                        displaying.Shape.Metadata.Alternates.Add("MenuItemLink_" + EncodeAlternateElement(displayType));
                    }
                });
        }


        /// <summary>
        /// Encodes dashed, dots and spaces so that they don't conflict in filenames 
        /// </summary>
        /// <param name="alternateElement"></param>
        /// <returns></returns>
        private string EncodeAlternateElement(string alternateElement)
        {
            return alternateElement.Replace(" ", "__").Replace("-", "__").Replace(".", "_");
        }
    }
}
