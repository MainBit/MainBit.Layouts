using MainBit.Layouts.ViewModels;
using MainBit.Layouts.Helpers;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Framework.Elements;
using System;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Orchard.Layouts.Drivers {

    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class ElementIdentityDriver : ElementDriver<Element>
    {
        protected override EditorResult OnBuildEditor(Element element, ElementEditorContext context)
        {

            var viewModel = new ElementIdentityViewModel
            {
                Identifier = element.GetIdentifier() ?? Guid.NewGuid().ToString()
            };

            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "ElementIdentity", Model: viewModel);

            if (context.Updater != null)
            {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.SetIdentifier(viewModel.Identifier);
            }

            return Editor(context, editor);
        }
    }
}