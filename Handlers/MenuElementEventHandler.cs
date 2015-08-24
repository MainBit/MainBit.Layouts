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
    [OrchardFeature("MainBit.Layouts.Navigation")]
    public class MenuElementEventHandler : ElementEventHandlerBase
    {
        public override void Displayed(ElementDisplayedContext context)
        {
            if (!(context.Element is Menu) || !(context.ElementShape.Menu is IShape))
            {
                return;
            }

            //context.ElementShape.Menu.Element = context.Element;
            context.ElementShape.Menu.ElementClass = context.Element.HtmlClass;   
        }
    }
}