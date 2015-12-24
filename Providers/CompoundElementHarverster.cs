using MainBit.Layouts.Models;
using MainBit.Layouts.Services;
using Orchard;
using Orchard.Environment;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Framework.Harvesters;
using Orchard.Layouts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Models;
using Orchard.Layouts.Helpers;
using MainBit.Layouts.Elements;
using Orchard.Environment.Extensions;
using MainBit.Layouts.Helpers;
using Orchard.Core.Common.Models;

namespace MainBit.Layouts.Providers
{
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class CompoundElementHarverster : Component, IElementHarvester
    {
        private readonly Work<ICompoundElementService> _CompoundElementService;
        private readonly Work<IElementManager> _elementManager;
        private readonly Work<ILayoutManager> _layoutManager;
        private readonly Work<IElementFactory> _elementFactory;
        private readonly Work<ILayoutSerializer> _layoutSerializer;

        public CompoundElementHarverster(Work<ICompoundElementService> CompoundElementService, Work<IElementManager> elementManager, Work<ILayoutManager> layoutManager,
            Work<IElementFactory> elementFactory, Work<ILayoutSerializer> layoutSerializer)
        {
            _CompoundElementService = CompoundElementService;
            _elementManager = elementManager;
            _layoutManager = layoutManager;
            _elementFactory = elementFactory;
            _layoutSerializer = layoutSerializer;
        }

        public IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context)
        {
            var compoundElements = _CompoundElementService.Value.GetAll().ToArray();

            return compoundElements.Select(compoundElement => {
                return new ElementDescriptor(typeof(Compound),
                    compoundElement.ElementTypeName,
                    T(compoundElement.As<ITitleAspect>().Title),
                    T(compoundElement.ElementDescription),
                    compoundElement.ElementCategory)
                {
                    CreatingDisplay = creatingDisplayContext => CreatingDisplay(creatingDisplayContext),
                    UpdateEditor = null,
                    EnableEditorDialog = false,
                    Editor = editorContext => Editor(editorContext),
                    Displaying = elementDisplayingContext => Displaying(elementDisplayingContext),
                    
                    StateBag = new Dictionary<string, object> {
                        { "ContentItemId", compoundElement.Id },
                        { "ContentItemVersionId", compoundElement.ContentItem.VersionRecord.Id },
                        { "LayoutData",  compoundElement.As<LayoutPart>().LayoutData }
                    }
                };
            });
        }

        private void Editor(ElementEditorContext context)
        {

        }

        private void Displaying(ElementDisplayingContext context)
        {

        }



        private void CreatingDisplay(ElementCreatingDisplayShapeContext context)
        {
            // this is actualy need to put into ElementEventHandlerBase.Created method
            // but now it's impossible because child elements will be overriden by ElementSerializer.ParseNode method (((
            // there is a copy of this code in DefaultModelMaps class
            var compound = context.Element as Compound;

            compound.IsTemplated = false;
            var modifiedElements = compound.Elements;
            var originalElements = _layoutSerializer.Value.Deserialize(
                    compound.Descriptor.StateBag["LayoutData"] != null ? compound.Descriptor.StateBag["LayoutData"].ToString() : null,
                    new DescribeElementsContext { Content = context.Content })
                .ToList();
            compound.Elements = originalElements;
            MakeTemplated(compound.Elements);
            modifiedElements = modifiedElements.Flatten().ToList();
            foreach (var originalElement in compound.Elements.Flatten())
            {
                var originalElementIdentifier = originalElement.GetIdentifier();
                if (originalElementIdentifier == null)
                    continue;

                var modifiedElement = modifiedElements.FirstOrDefault(e => e.GetIdentifier() == originalElementIdentifier);
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

        private void MakeTemplated(IEnumerable<Element> elements)
        {
            foreach (var element in elements)
            {
                var container = element as Container;
                if (container != null)
                {
                    if (container.Elements.Any())
                    {
                        element.IsTemplated = true;
                        MakeTemplated(container.Elements);
                    }
                    else
                    {
                        element.IsTemplated = false;
                    }
                }
            }
        }
    }
}