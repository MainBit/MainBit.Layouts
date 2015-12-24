using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using MainBit.Layouts.Models;
using MainBit.Layouts.Services;
using Orchard.Layouts.Services;
using Orchard.Layouts.Models;
using Orchard.ContentManagement;
using Orchard.Layouts.Helpers;
using System.Collections.Generic;
using MainBit.Layouts.Helpers;
using System;

namespace MainBit.Layouts.Relations.Handlers
{
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class CompoundElementPartHandler : ContentHandler {
        
        public CompoundElementPartHandler(ICompoundElementService CompoundElementService,
            ILayoutManager layoutManager) {

            OnCreated<CompoundElementPart>((context, part) => { CompoundElementService.ResetCache(); });
            OnUpdated<CompoundElementPart>((context, part) => { CompoundElementService.ResetCache(); });
            OnRemoved<CompoundElementPart>((context, part) => { CompoundElementService.ResetCache(); });

            OnUpdated<CompoundElementPart>((context, part) => {

                var layoutPart = part.As<LayoutPart>();
                if (layoutPart == null) return;

                var elements = layoutManager.LoadElements(layoutPart).Flatten();
                var elementIdentifiers = new List<string>();
                foreach (var element in elements.Flatten())
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
            });
        }
    }
}
