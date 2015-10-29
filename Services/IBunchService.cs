using MainBit.Layouts.Models;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Services
{
    public interface IBunchService : IDependency
    {

        BunchElementPart Get(int id);
        IEnumerable<BunchElementPart> GetAll();
        void ResetCache();
    }

    [OrchardFeature("MainBit.Layouts.Bunches")]
    public class BunchService : IBunchService
    {
        private readonly IContentManager _contentManager;
        private readonly ISignals _signals;

        public BunchService(IContentManager contentManager, ISignals signals)
        {
            _contentManager = contentManager;
            _signals = signals;

        }
        public BunchElementPart Get(int id)
        {
            return _contentManager.Get(id).As<BunchElementPart>();
        }

        public IEnumerable<BunchElementPart> GetAll()
        {
            return _contentManager.Query().ForType("BunchElement").List().Select(c => c.As<BunchElementPart>());
        }

        public void ResetCache()
        {
            _signals.Trigger(Orchard.Layouts.Signals.ElementDescriptors);
        }
    }
}