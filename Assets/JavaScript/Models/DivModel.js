var LayoutEditor;
(function (LayoutEditor) {

    LayoutEditor.Div = function (data, htmlId, htmlClass, htmlStyle, isTemplated, children) {
        LayoutEditor.Element.call(this, "Div", data, htmlId, htmlClass, htmlStyle, isTemplated);
        LayoutEditor.Container.call(this, ["Grid", "Content"], children);

        this.dropTargetClass = "layout-common-holder";

        this.toObject = function () {
            var result = this.elementToObject();
            result.children = this.childrenToObject();
            return result;
        };
    };

    LayoutEditor.Div.from = function (value) {
        var result = new LayoutEditor.Div(
            value.data,
            value.htmlId,
            value.htmlClass,
            value.htmlStyle,
            value.isTemplated,
            LayoutEditor.childrenFrom(value.children));
        result.toolboxIcon = value.toolboxIcon;
        result.toolboxLabel = value.toolboxLabel;
        result.toolboxDescription = value.toolboxDescription;
        return result;
    };

    LayoutEditor.registerFactory("Div", function (value) {
        return LayoutEditor.Div.from(value);
    });

})(LayoutEditor || (LayoutEditor = {}));