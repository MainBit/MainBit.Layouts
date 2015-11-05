using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;

namespace MainBit.Layouts.Elements {
    public class Link : Element {
        public override string Category {
            get { return "Content"; }
        }

        public string Value
        {
            get { return this.Retrieve(x => x.Value); }
            set { this.Store(x => x.Value, value); }
        }

        public string Text
        {
            get { return this.Retrieve(x => x.Text); }
            set { this.Store(x => x.Text, value); }
        }

        public string Target
        {
            get { return this.Retrieve(x => x.Target); }
            set { this.Store(x => x.Target, value); }
        }
    }
}