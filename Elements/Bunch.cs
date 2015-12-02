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
            get {
                object bunchId;
                if(this.Descriptor.StateBag.TryGetValue("BunchId", out bunchId))
                    return Convert.ToInt32(bunchId);
                return 0;
            }
        }

        public string BunchIdentifier
        {
            get {
                object bunchIdentifier;
                if (this.Descriptor.StateBag.TryGetValue("BunchIdentifier", out bunchIdentifier))
                    return bunchIdentifier.ToString();
                return null;
            }
        }
    }
}