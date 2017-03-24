using MainBit.Layouts.Helpers;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using System;
using System.Collections.Generic;

namespace MainBit.Layouts.Handlers
{
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class ElementIdentifierEventHandler : ElementEventHandlerBase
    {
        public override void LayoutSaving(ElementSavingContext context) {

            var elementIdentifiers = new List<string>();
            foreach (var element in context.Elements.Flatten())
            {
                var elementIdentifier = element.GetIdentifier();
                // identifier isn't exists
                // identifier is the same as the another (elements are copies of each other)
                if (elementIdentifier == null || elementIdentifiers.Contains(elementIdentifier))
                {
                    element.SetIdentifier(Guid.NewGuid().ToString());
                }
                elementIdentifiers.Add(elementIdentifier);
            }
        }
    }
}