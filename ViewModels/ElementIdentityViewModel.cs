using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.ViewModels
{
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class ElementIdentityViewModel
    {
        public string Identifier { get; set; }
    }
}