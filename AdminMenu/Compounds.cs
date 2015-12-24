using Orchard.Environment.Extensions;
using Orchard.Layouts;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace MainBit.Layouts.AdminMenu
{
    [OrchardFeature("MainBit.Layouts.Compounds")]
    public class Compounds : INavigationProvider {

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder
                .Add(T("Layouts"), layouts => layouts
                    .Action("List", "Admin", new { id = "Layout", area = "Contents" })
                    .Add(T("Compounds"), "2", elements => elements.Action("List", "Admin", new { id = "CompoundElement", area = "Contents" }).Permission(Permissions.ManageLayouts)));
        }
    }
}