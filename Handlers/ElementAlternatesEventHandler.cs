using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                displaying.ShapeMetadata.Alternates.Add(String.Format("Elements_{0}_ContentType__{1}", context.Element.GetType().Name, context.Content.ContentItem.ContentType));

                if (context.Element is MediaItem)
                {
                    displaying.ShapeMetadata.Alternates.Add(String.Format("Elements_{0}_{1}", context.Element.GetType().Name, (context.Element as MediaItem).DisplayType));
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