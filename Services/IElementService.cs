using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainBit.Layouts.Services
{
    public interface IElementService : IDependency
    {
        string DisplayElement(string elementTypeName, string displayType);
    }
}