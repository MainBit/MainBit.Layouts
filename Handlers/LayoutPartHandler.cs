using Orchard.ContentManagement.Handlers;
using Orchard.Layouts.Models;
using Orchard.Layouts.Services;
using Orchard.Layouts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Layouts.Elements;
using MainBit.Layouts.Services;

namespace MainBit.Layouts.Handlers
{
    public class LayoutPartHandler : ContentHandler
    {
        private readonly ILayoutManager _layoutManager;
        private readonly ILayoutContentMapService _layoutContentService;

        public LayoutPartHandler(ILayoutManager layoutManager, ILayoutContentMapService layoutContentService)
        {
            _layoutManager = layoutManager;
            _layoutContentService = layoutContentService;

            OnUpdated<LayoutPart>((context, part) => {

                var elements = _layoutManager.LoadElements(part).Flatten();
                var contentItemIds = elements
                    .Where(e => e is ContentItem)
                    .Cast<ContentItem>()
                    .SelectMany(c => c.ContentItemIds)
                    .ToList();

                _layoutContentService.LayoutPartUpdated(part.Id, contentItemIds);
            });

            OnRemoved<LayoutPart>((context, part) =>
            {
                _layoutContentService.LayoutPartRemoved(part.Id);
            });
        }
    }
}