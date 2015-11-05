﻿using MainBit.Layouts.Elements;
using Orchard.DisplayManagement;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using Orchard.Layouts.Elements;
using Orchard.Utility.Extensions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Orchard.Environment;
using System.Linq;
using MainBit.Layouts.Helpers;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.Services {
    public class DivModelMap : LayoutModelMapBase<Div> { }

    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class ElementBunchModelMap : ILayoutModelMap
    {
        private readonly Work<ILayoutSerializer> _layoutSerializer;
        public ElementBunchModelMap(Work<ILayoutSerializer> layoutSerializer)
        {
            _layoutSerializer = layoutSerializer;
        }

        public int Priority { get { return 0; } }
        public string LayoutElementType { get { return "Bunch"; } }
        public bool CanMap(Element element) {
            return element is Bunch; // && element.Descriptor.TypeName != "MainBit.Layouts.Elements.Bunch";
        }

        public void FromElement(Element element, DescribeElementsContext describeContext, JToken node)
        {

            if (element.Descriptor.TypeName != "MainBit.Layouts.Elements.Bunch")
            {
                // this is actualy need to put into ElementEventHandlerBase.Created method
                // but now it's impossible because child elements will be overriden by ElementSerializer.ParseNode method (((
                // there is a copy of this code in BunchElementHarverster class
                var bunch = element as Bunch;
                bunch.IsTemplated = false;

                var modifiedElements = bunch.Elements;
                var originalElements = _layoutSerializer.Value.Deserialize(element.Descriptor.StateBag["LayoutData"].ToString(), describeContext).ToList();
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
                    var modifiedElementContainer = modifiedElement as Container;
                    if (originalElementContainer != null && !originalElementContainer.Elements.Any())
                    {
                        originalElementContainer.Elements = (modifiedElement as Container).Elements;
                    }
                }

                
            }

            node["data"] = element.Data.Serialize();
            node["htmlId"] = element.HtmlId;
            node["htmlClass"] = element.HtmlClass;
            node["htmlStyle"] = element.HtmlStyle;
            node["isTemplated"] = element.IsTemplated;
            node["hasEditor"] = element.HasEditor;
            node["contentType"] = element.Descriptor.TypeName;
            node["type"] = LayoutElementType;
            node["contentTypeLabel"] = element.Descriptor.DisplayText.Text;
            node["contentTypeClass"] = element.DisplayText.Text.HtmlClassify();
            node["contentTypeDescription"] = element.Descriptor.Description.Text;

            
        }

        private void MakeTemplated(IEnumerable<Element> elements) {
            foreach (var element in elements)
            {
                var container = element as Container;
                if(container != null) {
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

        public Element ToElement(IElementManager elementManager, DescribeElementsContext describeContext, JToken node)
        {
            var elementTypeName = (string)node["contentType"];
            var descriptor = elementManager.GetElementDescriptorByTypeName(describeContext, elementTypeName);
            var element = elementManager.ActivateElement(descriptor);

            element.Data = ElementDataHelper.Deserialize((string)node["data"]);
            element.HtmlId = (string)node["htmlId"];
            element.HtmlClass = (string)node["htmlClass"];
            element.HtmlStyle = (string)node["htmlStyle"];
            element.IsTemplated = (bool)(node["isTemplated"] ?? false);

            return element;
        }
    }
}