using System;
using Orchard.ContentManagement;
using Orchard.Layouts.Settings;
using Orchard.Layouts.Models;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.Models {
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class BunchElementPart : ContentPart, ILayoutAspect
    {
        public string SessionKey {
            get {
                var key = this.Retrieve(x => x.SessionKey);

                if (String.IsNullOrEmpty(key)) {
                    SessionKey = key = Guid.NewGuid().ToString();
                }

                return key;
            }
            set { this.Store(x => x.SessionKey, value); }
        }

        public string LayoutData {
            get { return Retrieve<string>("LayoutData"); }
            set { this.Store(x => x.LayoutData, value); }
        }

        public int? TemplateId
        {
            get { return null; }
            set { }
        }
    }
}