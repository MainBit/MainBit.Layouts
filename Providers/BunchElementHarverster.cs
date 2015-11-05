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

namespace MainBit.Layouts.Providers
{
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class BunchElementHarverster : Component, ElementHarvester
    {
        private readonly Work<IBunchService> _banchService;
        private readonly Work<IElementManager> _elementManager;
        private readonly Work<ILayoutManager> _layoutManager;
        private readonly Work<IElementFactory> _elementFactory;
        private readonly Work<ILayoutSerializer> _layoutSerializer;

        public BunchElementHarverster(Work<IBunchService> elementBanchService, Work<IElementManager> elementManager, Work<ILayoutManager> layoutManager,
            Work<IElementFactory> elementFactory, Work<ILayoutSerializer> layoutSerializer)
        {
            _banchService = elementBanchService;
            _elementManager = elementManager;
            _layoutManager = layoutManager;
            _elementFactory = elementFactory;
            _layoutSerializer = layoutSerializer;
        }

        public IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context)
        {
            var bunches = _banchService.Value.GetAll().ToArray();
            var baseType = typeof(Bunch);
            //var describeContext = new DescribeElementsContext {Content = context.Content, CacheVaryParam = "Bunches" };
            //var baseElementDescriptor = _elementManager.Value.GetElementDescriptorByTypeName(describeContext, baseType.ToString());
            //var baseElement = _elementManager.Value.ActivateElement(baseElementDescriptor);

            var baseElement = new Bunch(); // if activate something that it cause of enfinity loop
            // but why snippet harvester work fine ????????????????????????????????????????????????????????
            // i create it only for string values (like category, description)

            //var baseElement = _elementFactory.Value.Activate(baseType);

            var query =
                from bunch in bunches
                let elementName = T(bunch.As<ITitleAspect>().Title)
                select new ElementDescriptor(
                    baseType,
                    string.Format("{0}-{1}", baseType.ToString(), bunch.Id),
                    elementName,
                    baseElement.Description,
                    baseElement.Category)
                {
                    ToolboxIcon = "\uf10c",
                    CreatingDisplay = creatingDisplayContext => CreatingDisplay(creatingDisplayContext),
                    Display = displayContext => Display(displayContext),
                    Editor = elementEditorContext => Editor(elementEditorContext),
                    UpdateEditor = elementEditorContext => UpdateEditor(elementEditorContext),
                    StateBag = new Dictionary<string, object> {
                        { "BunchId", bunch.Id },
                        { "LayoutData",  bunch.LayoutData }
                    }
                };

            var descriptors = query.ToArray();
            return descriptors;
        }

        private IEnumerable<IElementDriver> GetDrivers(Element rootElement)
        {
            return _elementManager.Value.GetDrivers(rootElement);
        }

        private void CreatingDisplay(ElementCreatingDisplayShapeContext context)
        {
            // this is actualy need to put into ElementEventHandlerBase.Created method
            // but now it's impossible because child elements will be overriden by ElementSerializer.ParseNode method (((
            // there is a copy of this code in DefaultModelMaps class
            var bunch = context.Element as Bunch;

            bunch.IsTemplated = false;
            var modifiedElements = bunch.Elements;
            var originalElements = _layoutSerializer.Value.Deserialize(
                    bunch.Descriptor.StateBag["LayoutData"].ToString(),
                    new DescribeElementsContext { Content = context.Content })
                .ToList();
            bunch.Elements = originalElements;
            MakeTemplated(bunch.Elements);
            modifiedElements = modifiedElements.Flatten().ToList();
            foreach (var originalElement in bunch.Elements.Flatten())
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

        private void Display(ElementDisplayContext context)
        {
            //var drivers = _elementManager.Value.GetDrivers(context.Element);

            //foreach (var driver in drivers)
            //{
            //    driver.Displaying(context);
            //}
        }

        private void Editor(ElementEditorContext elementEditorContext)
        {

        }

        private void UpdateEditor(ElementEditorContext elementEditorContext)
        {

        }
    }
}