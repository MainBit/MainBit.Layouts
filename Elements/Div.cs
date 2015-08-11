using Orchard.Layouts.Elements;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Elements
{
    public class Div : Container
    {

        public override string Category
        {
            get { return "Container"; }
        }

        public override LocalizedString DisplayText
        {
            get { return T("Div"); }
        }

        public override bool HasEditor
        {
            get { return false; }
        }
    }
}