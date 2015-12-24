using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.ViewModels
{
    public class CompoundElementPartVM
    {
        public string ElementTypeName { get; set; }
        public string ElementDescription { get; set; }
        public string ElementCategory { get; set; }

        public bool AllowEditElementTypeName { get; set; }
    }
}