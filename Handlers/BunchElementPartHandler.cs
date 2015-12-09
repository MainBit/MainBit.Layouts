using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using MainBit.Layouts.Models;
using MainBit.Layouts.Services;

namespace MainBit.Layouts.Relations.Handlers
{
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class BunchElementPartHandler : ContentHandler {
        
        public BunchElementPartHandler(IBunchService bunchService) {

            OnCreated<BunchElementPart>((context, part) => { bunchService.ResetCache(); });
            OnUpdated<BunchElementPart>((context, part) => { bunchService.ResetCache(); });
            OnRemoved<BunchElementPart>((context, part) => { bunchService.ResetCache(); });
        }
    }
}
