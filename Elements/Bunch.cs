using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Elements;
using Orchard.Localization;
using Orchard.Layouts.Helpers;

namespace MainBit.Layouts.Elements
{
    public class Bunch : Container
    {
        public override string Category
        {
            get { return "Bunches"; }
        }

        public int BunchId
        {
            get { return ElementDataHelper.Retrieve(this, x => x.BunchId); }
            set { this.Store(x => x.BunchId, value); }
        }
    }
}