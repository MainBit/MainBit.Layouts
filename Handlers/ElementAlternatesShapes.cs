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
                    string elementClass = displaying.Shape.ElementClass;

                    if (!string.IsNullOrWhiteSpace(elementClass))
                    {
                        displaying.Shape.Metadata.Alternates.Add("Menu__ElementClass__" + EncodeAlternateElement(elementClass));
                    }
                });

            builder.Describe("MenuItem")
                .OnDisplaying(displaying =>
                {
                    string elementClass = displaying.Shape.Menu.ElementClass;

                    if (!string.IsNullOrWhiteSpace(elementClass))
                    {
                        displaying.Shape.Metadata.Alternates.Add("MenuItem__ElementClass__" + EncodeAlternateElement(elementClass));
                    }
                });

            builder.Describe("MenuItemLink")
                .OnDisplaying(displaying =>
                {
                    string elementClass = displaying.Shape.Menu.ElementClass;

                    if (!string.IsNullOrWhiteSpace(elementClass))
                    {
                        displaying.Shape.Metadata.Alternates.Add("MenuItemLink__ElementClass__" + EncodeAlternateElement(elementClass));
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
