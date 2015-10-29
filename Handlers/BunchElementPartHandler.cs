using System;
using System.Linq;
using JetBrains.Annotations;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Localization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Security;
using Orchard.Services;
using MainBit.Layouts.Relations.Services;
using MainBit.Layouts.Relations.Models;
using Orchard.Environment.Extensions;
using MainBit.Layouts.Models;
using MainBit.Layouts.Services;

namespace MainBit.Layouts.Relations.Handlers
{
    [UsedImplicitly]
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class BunchElementPartHandler : ContentHandler {
        
        public BunchElementPartHandler(IBunchService bunchService) {

            OnCreated<BunchElementPart>((context, part) => { bunchService.ResetCache(); });
            OnUpdated<BunchElementPart>((context, part) => { bunchService.ResetCache(); });
            OnRemoved<BunchElementPart>((context, part) => { bunchService.ResetCache(); });
        }
    }
}
