using System;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Services;
using Orchard.Tokens;
using Orchard;
using MainBit.Layouts.Services;

namespace MainBit.Layouts.Tokens
{
    [OrchardFeature("MainBit.Layouts.Tokens")]
    public class ElementTokens : Component, ITokenProvider {
        private readonly IElementService _elementService;

        public ElementTokens(IElementService elementService)
        {
            _elementService = elementService;
        }

        public void Describe(DescribeContext context) {
            context.For("Element")
                .Token("DisplayWithType:*", T("Display:<element type|display type>"), T("Displays the specified element type with specific display type."))
            ;
        }

        public void Evaluate(EvaluateContext context) {
            context.For("Element", "")
                .Token(token => token.StartsWith("DisplayWithType:", StringComparison.OrdinalIgnoreCase) ? token.Substring("DisplayWithType:".Length) : null, TokenValue);
        }

        private string TokenValue(string tokenValue, string chainValue) {

            var delimeterIndex = tokenValue.LastIndexOf('|');

            string elementTypeName;
            string displayType = null;

            if (delimeterIndex > 0)
            {
                elementTypeName = tokenValue.Substring(0, delimeterIndex);
                if (delimeterIndex + 1 < tokenValue.Length)
                {
                    displayType = tokenValue.Substring(delimeterIndex + 1);
                }
            }
            else
            {
                elementTypeName = tokenValue;
            }

            return _elementService.DisplayElement(elementTypeName, displayType);
        }
    }
}