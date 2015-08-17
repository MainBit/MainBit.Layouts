using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc.Html;
using MainBit.Layouts.Services;

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
    }
}