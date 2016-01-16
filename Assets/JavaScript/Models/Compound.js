var LayoutEditor;
(function (LayoutEditor) {

    LayoutEditor.Compound = function (data, htmlId, htmlClass, htmlStyle, isTemplated, rule, children, contentType, contentTypeLabel, contentTypeClass) {
        LayoutEditor.Element.call(this, "Compound", data, htmlId, htmlClass, htmlStyle, isTemplated, rule);
        LayoutEditor.Container.call(this, [], children); // LayoutEditor.Container.call(this, ["Canvas", "Grid", "Content"], children);

        this.getIsSealed = function () { return false; } // should change

        this.isContainable = true;
        this.dropTargetClass = "layout-common-holder";
        this.contentType = contentType;
        this.contentTypeLabel = contentTypeLabel;
        this.contentTypeClass = contentTypeClass;

        this.toObject = function () {
            return {
                "type": "Compound"
            };
        };

        this.toObject = function () {
            var result = this.elementToObject();
            result.contentType = this.contentType;
            result.children = this.childrenToObject();
            result.type = "Compound";
            return result;
        };
    };

    LayoutEditor.Compound.from = function (value) {
        var result = new LayoutEditor.Compound(
            value.data,
            value.htmlId,
            value.htmlClass,
            value.htmlStyle,
            value.isTemplated,
            value.rule,
            LayoutEditor.childrenFrom(value.children),
            value.contentType,
            value.contentTypeLabel,
            value.contentTypeClass);

        result.toolboxIcon = value.toolboxIcon;
        result.toolboxLabel = value.toolboxLabel;
        result.toolboxDescription = value.toolboxDescription;

        return result;
    };

    LayoutEditor.registerFactory("Compound", function (value) {
        return LayoutEditor.Compound.from(value);
    });

})(LayoutEditor || (LayoutEditor = {}));
