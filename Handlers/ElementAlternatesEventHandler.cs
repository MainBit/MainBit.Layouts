using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Utility.Extensions;

namespace MainBit.Layouts.Handlers
{
    [OrchardFeature("MainBit.Layouts.Alternates")]
    public class ElementAlternatesEventHandler : ElementEventHandlerBase
    {
        //public override void Displayed(Orchard.Layouts.Framework.Display.ElementDisplayContext context)
        public override void Displaying(Orchard.Layouts.Framework.Display.ElementDisplayContext context)
        {
            (context.ElementShape as IShape).Metadata.OnDisplaying((displaying =>
            {
                var typeName = context.Element.GetType().Name;
                var category = context.Element.Category.ToSafeName();

                displaying.ShapeMetadata.Alternates.Add(String.Format("Elements_{0}_ContentType__{1}", typeName, context.Content.ContentItem.ContentType));

                if (context.Element is MediaItem)
                {
                    displaying.ShapeMetadata.Alternates.Add(String.Format("Elements_{0}_{1}", typeName, (context.Element as MediaItem).DisplayType));
                }

                if (context.Element.HtmlClass.StartsWith("DisplayType:"))
                {
                    string displayType;
                    var indexOfSpace = context.Element.HtmlClass.IndexOf(' ');
                    if (indexOfSpace < 0)
                    {
                        displayType = context.Element.HtmlClass.Substring("DisplayType:".Length);
                        context.Element.HtmlClass = null;
                    }
                    else
                    {
                        displayType = context.Element.HtmlClass.Substring("DisplayType:".Length, indexOfSpace - "DisplayType:".Length);
                        context.Element.HtmlClass = context.Element.HtmlClass.Substring(indexOfSpace + 1);
                    }

                    displaying.ShapeMetadata.Alternates.Add(String.Format("Elements_{0}_{1}", typeName, displayType));
                    displaying.ShapeMetadata.Alternates.Add(String.Format("Elements_{0}_{1}__{2}", typeName, displayType, category));
                }

            }));

            // for feature versions - menu exists only in dev branch
            //if ((context.Element is Menu) && (context.ElementShape.Menu is IShape))
            //{
            //    //context.ElementShape.Menu.Element = context.Element;
            //    context.ElementShape.Menu.ElementClass = context.Element.HtmlClass;   
            //}
        }
    }
}