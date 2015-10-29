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

namespace MainBit.Layouts.Relations.Handlers
{
    [UsedImplicitly]
    [OrchardFeature("MainBit.Layouts.Relations")]
    public class ContentLayoutMapPartHandler : ContentHandler {
        private readonly ILayoutContentMapService _layoutContentMapService;
        private readonly IContentManager _contentManager;

        public ContentLayoutMapPartHandler(
            ILayoutContentMapService layoutContentMapService,
            IContentManager contentManager) {

            _contentManager = contentManager;
            _layoutContentMapService = layoutContentMapService;
            T = NullLocalizer.Instance;

            OnLoading<ContentLayoutMapPart>((context, part) => LazyLoadHandlers(part));
        }


        public Localizer T { get; set; }

        protected void LazyLoadHandlers(ContentLayoutMapPart part)
        {
            part.LayoutPartsField.Loader(() => {
                var records = _layoutContentMapService.GetForContentItem(part.Id);
                return _contentManager.GetMany<IContent>(records.Select(r => r.LayoutPartRecord_Id), VersionOptions.Published, QueryHints.Empty);
            });    
        }
    }
}
