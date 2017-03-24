using Orchard.DisplayManagement.Descriptors;
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
