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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIkRpdk1vZGVsLmpzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EiLCJmaWxlIjoiTW9kZWxzLmpzIiwic291cmNlc0NvbnRlbnQiOlsidmFyIExheW91dEVkaXRvcjtcclxuKGZ1bmN0aW9uIChMYXlvdXRFZGl0b3IpIHtcclxuXHJcbiAgICBMYXlvdXRFZGl0b3IuRGl2ID0gZnVuY3Rpb24gKGRhdGEsIGh0bWxJZCwgaHRtbENsYXNzLCBodG1sU3R5bGUsIGlzVGVtcGxhdGVkLCBjaGlsZHJlbikge1xyXG4gICAgICAgIExheW91dEVkaXRvci5FbGVtZW50LmNhbGwodGhpcywgXCJEaXZcIiwgZGF0YSwgaHRtbElkLCBodG1sQ2xhc3MsIGh0bWxTdHlsZSwgaXNUZW1wbGF0ZWQpO1xyXG4gICAgICAgIExheW91dEVkaXRvci5Db250YWluZXIuY2FsbCh0aGlzLCBbXCJHcmlkXCIsIFwiQ29udGVudFwiXSwgY2hpbGRyZW4pO1xyXG5cclxuICAgICAgICB0aGlzLmRyb3BUYXJnZXRDbGFzcyA9IFwibGF5b3V0LWNvbW1vbi1ob2xkZXJcIjtcclxuXHJcbiAgICAgICAgdGhpcy50b09iamVjdCA9IGZ1bmN0aW9uICgpIHtcclxuICAgICAgICAgICAgdmFyIHJlc3VsdCA9IHRoaXMuZWxlbWVudFRvT2JqZWN0KCk7XHJcbiAgICAgICAgICAgIHJlc3VsdC5jaGlsZHJlbiA9IHRoaXMuY2hpbGRyZW5Ub09iamVjdCgpO1xyXG4gICAgICAgICAgICByZXR1cm4gcmVzdWx0O1xyXG4gICAgICAgIH07XHJcbiAgICB9O1xyXG5cclxuICAgIExheW91dEVkaXRvci5EaXYuZnJvbSA9IGZ1bmN0aW9uICh2YWx1ZSkge1xyXG4gICAgICAgIHZhciByZXN1bHQgPSBuZXcgTGF5b3V0RWRpdG9yLkRpdihcclxuICAgICAgICAgICAgdmFsdWUuZGF0YSxcclxuICAgICAgICAgICAgdmFsdWUuaHRtbElkLFxyXG4gICAgICAgICAgICB2YWx1ZS5odG1sQ2xhc3MsXHJcbiAgICAgICAgICAgIHZhbHVlLmh0bWxTdHlsZSxcclxuICAgICAgICAgICAgdmFsdWUuaXNUZW1wbGF0ZWQsXHJcbiAgICAgICAgICAgIExheW91dEVkaXRvci5jaGlsZHJlbkZyb20odmFsdWUuY2hpbGRyZW4pKTtcclxuICAgICAgICByZXN1bHQudG9vbGJveEljb24gPSB2YWx1ZS50b29sYm94SWNvbjtcclxuICAgICAgICByZXN1bHQudG9vbGJveExhYmVsID0gdmFsdWUudG9vbGJveExhYmVsO1xyXG4gICAgICAgIHJlc3VsdC50b29sYm94RGVzY3JpcHRpb24gPSB2YWx1ZS50b29sYm94RGVzY3JpcHRpb247XHJcbiAgICAgICAgcmV0dXJuIHJlc3VsdDtcclxuICAgIH07XHJcblxyXG4gICAgTGF5b3V0RWRpdG9yLnJlZ2lzdGVyRmFjdG9yeShcIkRpdlwiLCBmdW5jdGlvbiAodmFsdWUpIHtcclxuICAgICAgICByZXR1cm4gTGF5b3V0RWRpdG9yLkRpdi5mcm9tKHZhbHVlKTtcclxuICAgIH0pO1xyXG5cclxufSkoTGF5b3V0RWRpdG9yIHx8IChMYXlvdXRFZGl0b3IgPSB7fSkpOyJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==