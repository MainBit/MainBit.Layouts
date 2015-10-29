using Orchard.Environment.Extensions;
using Orchard.Layouts;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace MainBit.Layouts.AdminMenu
{
    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class Bunches : INavigationProvider {

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder
                .AddImageSet("layouts")
                .Add(T("Layouts"), "8.5", layouts => layouts
                    .Action("List", "Admin", new { id = "Layout", area = "Contents" })
                    .Add(T("Bunches"), "2", elements => elements.Action("List", "Admin", new { id = "BunchElement", area = "Contents" }).Permission(Permissions.ManageLayouts)));
        }
    }
}