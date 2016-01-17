using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.DisplayManagement;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using Orchard.Layouts.Settings;
using Orchard.Layouts.ViewModels;
using Orchard.Logging;
using MainBit.Layouts.Models;
using MainBit.Layouts.Helpers;
using Orchard.Environment.Extensions;
using MainBit.Layouts.ViewModels;
using Orchard.Localization;

namespace MainBit.Layouts.Drivers {
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class CompoundElementPartDriver : ContentPartDriver<CompoundElementPart>
    {
        private readonly ILayoutSerializer _serializer;
        private readonly IElementDisplay _elementDisplay;
        private readonly IElementManager _elementManager;
        private readonly ILayoutManager _layoutManager;
        private readonly Lazy<IContentPartDisplay> _contentPartDisplay;
        private readonly IShapeDisplay _shapeDisplay;
        private readonly ILayoutModelMapper _mapper;
        private readonly ILayoutEditorFactory _layoutEditorFactory;
        private readonly HashSet<string> _stack;

        public CompoundElementPartDriver(
            ILayoutSerializer serializer,
            IElementDisplay elementDisplay,
            IElementManager elementManager,
            ILayoutManager layoutManager,
            Lazy<IContentPartDisplay> contentPartDisplay,
            IShapeDisplay shapeDisplay,
            ILayoutModelMapper mapper,
            ILayoutEditorFactory layoutEditorFactory) {

            _serializer = serializer;
            _elementDisplay = elementDisplay;
            _elementManager = elementManager;
            _layoutManager = layoutManager;
            _contentPartDisplay = contentPartDisplay;
            _shapeDisplay = shapeDisplay;
            _mapper = mapper;
            _layoutEditorFactory = layoutEditorFactory;
            _stack = new HashSet<string>();

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        protected override DriverResult Editor(CompoundElementPart part, dynamic shapeHelper)
        {
            return Editor(part, null, shapeHelper);
        }

        protected override DriverResult Editor(CompoundElementPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            return ContentShape("Parts_CompoundElement_Edit", () =>
            {
                var viewModel = new CompoundElementPartVM {
                    AllowEditElementTypeName = string.IsNullOrEmpty(part.ElementTypeName), // change to part.Id == 0 after i will have edited old elements
                    ElementTypeName = part.ElementTypeName,
                    ElementDescription = part.ElementDescription,
                    ElementCategory = part.ElementCategory
                };

                if (updater != null && updater.TryUpdateModel(viewModel, Prefix, null, null)) {

                    part.ElementDescription = viewModel.ElementDescription;
                    part.ElementCategory = viewModel.ElementCategory;

                    if(viewModel.AllowEditElementTypeName)
                    {
                        viewModel.ElementTypeName = viewModel.ElementTypeName.TrimSafe();
                        if(string.IsNullOrEmpty(viewModel.ElementTypeName))
                        {
                            updater.AddModelError("ElementTypeName", T("ElementTypeName isn't correct."));
                        }
                        else if (_elementManager.GetElementDescriptorByTypeName(DescribeElementsContext.Empty, viewModel.ElementTypeName) != null)
                        {
                            updater.AddModelError("ElementTypeName", T("ElementTypeName alredy exists."));
                        }
                        else
                        {
                            part.ElementTypeName = viewModel.ElementTypeName;
                        }
                    }
                }

                return shapeHelper.EditorTemplate(TemplateName: "Parts/CompoundElement", Model: viewModel, Prefix: Prefix);
            });
        }

        protected override void Exporting(CompoundElementPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("ElementTypeName", part.ElementTypeName);
            context.Element(part.PartDefinition.Name).SetAttributeValue("ElementDescription", part.ElementDescription);
            context.Element(part.PartDefinition.Name).SetAttributeValue("ElementCategory", part.ElementCategory);
        }

        protected override void Importing(CompoundElementPart part, ImportContentContext context)
        {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null)
            {
                return;
            }

            context.ImportAttribute(part.PartDefinition.Name, "ElementTypeName", x => part.ElementTypeName = x);
            context.ImportAttribute(part.PartDefinition.Name, "ElementDescription", x => part.ElementDescription = x);
            context.ImportAttribute(part.PartDefinition.Name, "ElementCategory", x => part.ElementCategory = x);
        }

        
    }
}