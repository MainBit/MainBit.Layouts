using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc.Html;
using MainBit.Layouts.Services;
using Orchard.Layouts.Framework.Elements;

namespace MainBit.Layouts.Helpers
{
    public static class ElementExtensions
    {
        public static MvcHtmlString DisplayElement(this HtmlHelper htmlHelper, string elementTypeName, string displayType = null)
        {
            var elementService = htmlHelper.GetWorkContext().Resolve<IElementService>();
            var html = elementService.DisplayElement(elementTypeName, displayType);
            return new MvcHtmlString(html);
        }

        public static string GetIdentifier(this Element element)
        {
            //string identifier;
            //element.Data.TryGetValue("Identifier", out identifier);
            //return identifier;
            return element.Data.ContainsKey("Identifier") ? element.Data["Identifier"] : null;
        }

        public static void SetIdentifier(this Element element, string identifier)
        {
            element.Data["Identifier"] = identifier;
        }
    }
}