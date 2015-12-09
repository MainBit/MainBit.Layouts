using System;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.ViewModels;
using ContentItem = Orchard.Layouts.Elements.ContentItem;
using MainBit.Layouts.Elements;
using Orchard.Forms.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard.Layouts.Services;

namespace MainBit.Layouts.Drivers
{
    public class LinkElementDriver : FormsElementDriver<Link>
    {

        public LinkElementDriver(
            IFormsBasedElementServices formsServices)
        : base(formsServices) {

        }

        protected override IEnumerable<string> FormNames
        {
            get { yield return "LinkForm"; }
        }

        protected override void DescribeForm(DescribeContext context)
        {
            context.Form("LinkForm", factory =>
            {
                var shape = (dynamic)factory;
                var form = shape.Fieldset(
                    Id: "LinkForm",
                    _Value: shape.Textbox(
                        Id: "Value",
                        Name: "Value",
                        Title: T("Url"),
                        //Value: "",
                        Description: T("A valid url, i.e. http://orchardproject.net, /content/file.pdf, ..."),
                        Classes: new[] { "text", "large" }),
                    _Text: shape.Textbox(
                        Id: "Text",
                        Name: "Text",
                        Title: T("Text"),
                        //Value: "",
                        Description: T("The text of the link. If left empty, the url will be used instead."),
                        Classes: new[] { "text", "medium" }),
                    _Target: shape.SelectList(
                        Id: "Target",
                        Name: "Target",
                        Title: T("Target"),
                        Description: T("A valid HTML target attribute value. e.g., _blank, _parent, _top, or an anchor.")));

                var targets = new string[] { null, "_blank", "_parent", "_top" };
                //form._QueryLayoutId.Add(new SelectListItem { Text = string.Empty });
                foreach (var target in targets)
                {
                    form._Target.Add(new SelectListItem { Text = target, Value = target });
                }

                return form;
            });
        }

    }
}