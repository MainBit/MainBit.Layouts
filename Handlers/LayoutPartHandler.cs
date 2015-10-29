using Orchard.ContentManagement.Handlers;
using Orchard.Layouts.Models;
using Orchard.Layouts.Services;
using Orchard.Layouts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Layouts.Elements;
using MainBit.Layouts.Relations.Services;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.Relations.Handlers
{
    [OrchardFeature("MainBit.Layouts.Relations")]
    public class LayoutPartHandler : ContentHandler
    {
        private readonly ILayoutManager _layoutManager;
        private readonly ILayoutContentMapService _layoutContentMapService;

        public LayoutPartHandler(ILayoutManager layoutManager, ILayoutContentMapService layoutContentMapService)
        {
            _layoutManager = layoutManager;
            _layoutContentMapService = layoutContentMapService;

            OnUpdated<LayoutPart>((context, part) => {

                var elements = _layoutManager.LoadElements(part).Flatten();
                var contentItemIds = elements
                    .Where(e => e is ContentItem)
                    .Cast<ContentItem>()
                    .SelectMany(c => c.ContentItemIds)
                    .ToList();

                _layoutContentMapService.LayoutPartUpdated(part.Id, contentItemIds);
            });

            OnRemoved<LayoutPart>((context, part) =>
            {
                _layoutContentMapService.LayoutPartRemoved(part.Id);
            });
        }
    }
}