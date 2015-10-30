﻿using MainBit.Layouts.Elements;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Framework.Elements;
using System.Linq;
using Orchard.Layouts.Helpers;
using MainBit.Layouts.Helpers;
using Orchard.Layouts.Services;

namespace MainBit.Layouts.Drivers
{
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class BunchElementDriver : ElementDriver<Bunch> {

        private readonly ILayoutSerializer _layoutSerializer;

        public BunchElementDriver(ILayoutSerializer layoutSerializer)
        {
            _layoutSerializer = layoutSerializer;
        }

        protected override EditorResult OnBuildEditor(Bunch element, ElementEditorContext context)
        {
            return null;
        }

        protected override EditorResult OnUpdateEditor(Bunch element, ElementEditorContext context)
        {
            return OnBuildEditor(element, context);
        }

        protected override void OnDisplaying(Bunch element, ElementDisplayContext context)
        {
            
        }

        protected override void OnLayoutSaving(Bunch element, ElementSavingContext context)
        {
        }

        protected override void OnRemoving(Bunch element, ElementRemovingContext context)
        {
        }

        protected override void OnExporting(Bunch element, ExportElementContext context)
        {
        }

        protected override void OnImporting(Bunch element, ImportElementContext context)
        {
        }

    
    }
}