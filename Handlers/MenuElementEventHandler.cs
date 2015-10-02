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
        //public override void Displayed(Orchard.Layouts.Framework.Display.ElementDisplayContext context)
        public override void Displaying(Orchard.Layouts.Framework.Display.ElementDisplayContext context)
        {
            // for feature versions
            //if ((context.Element is Menu) && (context.ElementShape.Menu is IShape))
            //{
            //    //context.ElementShape.Menu.Element = context.Element;
            //    context.ElementShape.Menu.ElementClass = context.Element.HtmlClass;   
            //}

            
        }
    }
}