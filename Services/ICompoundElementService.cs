using MainBit.Layouts.Elements;
using MainBit.Layouts.Helpers;
using MainBit.Layouts.Models;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Models;
using Orchard.Layouts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Services
{
    public interface ICompoundElementService : IDependency
    {
        CompoundElementPart Get(int id);
        IEnumerable<CompoundElementPart> GetAll();
        void ResetCache();
    }

    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class CompoundElementService : ICompoundElementService
    {
        private readonly IContentManager _contentManager;
        private readonly ILayoutSerializer _layoutSerializer;
        private readonly ISignals _signals;

        public CompoundElementService(IContentManager contentManager, ILayoutSerializer layoutSerializer, ISignals signals)
        {
            _contentManager = contentManager;
            _signals = signals;

        }
        public CompoundElementPart Get(int id)
        {
            return _contentManager.Get(id).As<CompoundElementPart>();
        }

        public IEnumerable<CompoundElementPart> GetAll()
        {
            return _contentManager.Query().ForType("CompoundElement").List().Select(c => c.As<CompoundElementPart>());
        }

        public void ResetCache()
        {
            _signals.Trigger(Orchard.Layouts.Signals.ElementDescriptors);
        }

        public void UpdateElement(LayoutPart layoutPart, Compound element)
        {
            if(element.ContentItemVersionId == element.ActualContentItemVersionId)
                return;

            var flattenModifiedElements = element.Elements.Flatten();
            element.Elements = _layoutSerializer.Deserialize(
                    element.Descriptor.StateBag["LayoutData"] != null ? element.Descriptor.StateBag["LayoutData"].ToString() : null,
                    new DescribeElementsContext { Content = layoutPart })
                .ToList();
            var flattenOriginalElements = element.Elements.Flatten();

            foreach (var originalElement in flattenOriginalElements)
            {
                var container = originalElement as Container;
                originalElement.IsTemplated = container != null && !container.Elements.Any();
            }

            foreach (var originalElement in flattenOriginalElements)
            {
                var originalElementIdentifier = originalElement.GetIdentifier();
                if (originalElementIdentifier == null)
                    continue;

                var modifiedElement = flattenModifiedElements.FirstOrDefault(e => e.GetIdentifier() == originalElementIdentifier);
                if (modifiedElement == null)
                    continue;

                originalElement.Data = modifiedElement.Data;

                var originalElementContainer = originalElement as Container;
                if (originalElementContainer != null && !originalElementContainer.Elements.Any())
                {
                    originalElementContainer.Elements = (modifiedElement as Container).Elements;
                }
            }
        }

    }
}