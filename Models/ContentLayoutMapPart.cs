using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Relations.Models
{
    public class ContentLayoutMapPart : ContentPart
    {
        internal LazyField<IEnumerable<IContent>> LayoutPartsField = new LazyField<IEnumerable<IContent>>();

        // list of layouts part witch content item is include in
        public IEnumerable<IContent> LayoutParts
        {
            get { return LayoutPartsField.Value; }
            set { LayoutPartsField.Value = value; }
        }
    }
}