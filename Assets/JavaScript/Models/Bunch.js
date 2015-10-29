var LayoutEditor;
(function (LayoutEditor) {

    LayoutEditor.Bunch = function (contentType, data, htmlId, htmlClass, htmlStyle, isTemplated, rule, children) {
        LayoutEditor.Element.call(this, "Bunch", data, htmlId, htmlClass, htmlStyle, isTemplated, rule);
        LayoutEditor.Container.call(this, [], children); // LayoutEditor.Container.call(this, ["Canvas", "Grid", "Content"], children);

        this.isContainable = true;
        this.dropTargetClass = "layout-common-holder";
        this.contentType = contentType;

        this.toObject = function () {
            return {
                "type": "Bunch"
            };
        };

        this.toObject = function () {
            var result = this.elementToObject();
            result.contentType = this.contentType;
            result.children = this.childrenToObject();
            result.type = "Bunch";
            return result;
        };
    };

    LayoutEditor.Bunch.from = function (value) {
        var result = new LayoutEditor.Bunch(
            value.contentType,
            value.data,
            value.htmlId,
            value.htmlClass,
            value.htmlStyle,
            value.isTemplated,
            value.rule,
            LayoutEditor.childrenFrom(value.children));

        result.toolboxIcon = value.toolboxIcon;
        result.toolboxLabel = value.toolboxLabel;
        result.toolboxDescription = value.toolboxDescription;

        return result;
    };

    LayoutEditor.registerFactory("Bunch", function (value) {
        return LayoutEditor.Bunch.from(value);
    });

})(LayoutEditor || (LayoutEditor = {}));
