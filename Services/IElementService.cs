using Orchard;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainBit.Layouts.Services
{
    public interface IElementService : IDependency
    {
        string DisplayElement(string elementTypeName, string displayType, ContentItem contentItem = null);
    }


    public class ElementService : IElementService
    {
        private readonly IElementManager _elementManager;
        private readonly IElementDisplay _elementDisplay;
        private readonly IShapeDisplay _shapeDisplay;

        public ElementService(
            IElementManager elementManager,
            IElementDisplay elementDisplay,
            IShapeDisplay shapeDisplay
            )
        {
            _elementManager = elementManager;
            _elementDisplay = elementDisplay;
            _shapeDisplay = shapeDisplay;
        }


        public string DisplayElement(string elementTypeName, string displayType, ContentItem contentItem = null)
        {
            var describeContext = DescribeElementsContext.Empty;
            var elementDescriptor = _elementManager.GetElementDescriptorByTypeName(describeContext, elementTypeName);

            if (elementDescriptor == null)
                return "";

            var element = _elementManager.ActivateElement(elementDescriptor);
            var elementShape = _elementDisplay.DisplayElement(element, contentItem, displayType);
            var elementHtml = _shapeDisplay.Display(elementShape);

            return elementHtml;
        }
    }
}