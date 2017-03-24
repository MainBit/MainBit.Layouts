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
        
        public CompoundElementPartHandler(ICompoundElementService compoundElementService,
            ILayoutManager layoutManager) {

            OnCreated<CompoundElementPart>((context, part) => { compoundElementService.ResetCache(); });
            OnUpdated<CompoundElementPart>((context, part) => { compoundElementService.ResetCache(); });
            OnRemoved<CompoundElementPart>((context, part) => { compoundElementService.ResetCache(); });
        }
    }
}
