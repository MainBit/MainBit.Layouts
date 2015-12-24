using System;
using Orchard.ContentManagement;
using Orchard.Layouts.Settings;
using Orchard.Layouts.Models;
using Orchard.Environment.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MainBit.Layouts.Models {
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class CompoundElementPart : ContentPart
    {
        [Required]
        public string ElementTypeName
        {
            get { return Retrieve<string>("ElementTypeName"); }
            set { this.Store(x => x.ElementTypeName, value); }
        }
        public string ElementDescription
        {
            get { return Retrieve<string>("ElementDescription"); }
            set { this.Store(x => x.ElementDescription, value); }
        }

        [Required]
        public string ElementCategory
        {
            get { return Retrieve<string>("ElementCategory"); }
            set { this.Store(x => x.ElementCategory, value); }
        }
    }
}