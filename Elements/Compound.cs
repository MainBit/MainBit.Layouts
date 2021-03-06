﻿using System;
using Orchard.Layouts.Elements;
using Orchard.Layouts.Helpers;
using Orchard.Environment.Extensions;

namespace MainBit.Layouts.Elements
{
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class Compound : Container
    {
        public override string Category
        {
            get { return "Content"; }
        }

        public int ContentItemId {
            get { return this.Retrieve(x => x.ContentItemId); }
            set { this.Store(x => x.ContentItemId, value); }
        }

        public int ContentItemVersionId {
            get { return this.Retrieve(x => x.ContentItemVersionId); }
            set { this.Store(x => x.ContentItemVersionId, value); }
        }




        public int ActualContentItemId {
            get {
                object contentItemId;
                if(this.Descriptor.StateBag.TryGetValue("ContentItemId", out contentItemId))
                    return Convert.ToInt32(contentItemId);
                return 0;
            }
        }

        public int ActualContentItemVersionId {
            get {
                object contentItemVersionId;
                if (this.Descriptor.StateBag.TryGetValue("ContentItemVersionId", out contentItemVersionId))
                    return Convert.ToInt32(contentItemVersionId);
                return 0;
            }
        }
    }
}