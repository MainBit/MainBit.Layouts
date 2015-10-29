using MainBit.Layouts.Relations.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Layouts.Relations.Services
{
    public interface ILayoutContentMapService : IDependency
    {
        List<LayoutContentMapRecord> GetForLayoutPart(int layoutPartId);
        List<LayoutContentMapRecord> GetForContentItem(int contentItemId);
        void LayoutPartUpdated(int layoutPartId, IEnumerable<int> contentItemIds);
        void LayoutPartRemoved(int layoutPartId);
    }

    [OrchardFeature("MainBit.Layouts.Relations")]
    public class LayoutContentMapService : ILayoutContentMapService
    {
        private readonly IRepository<LayoutContentMapRecord> _layoutContentRepository;

        public LayoutContentMapService(IRepository<LayoutContentMapRecord> layoutContentRepository)
        {
            _layoutContentRepository = layoutContentRepository;
        }

        public List<LayoutContentMapRecord> GetForLayoutPart(int layoutPartId)
        {
            return _layoutContentRepository.Fetch(r => r.LayoutPartRecord_Id == layoutPartId).ToList();
        }

        public List<LayoutContentMapRecord> GetForContentItem(int contentItemId)
        {
            return _layoutContentRepository.Fetch(r => r.ContentItemRecord_Id == contentItemId).ToList();
        }

        public void LayoutPartUpdated(int layoutPartId, IEnumerable<int> contentItemIds)
        {
            var layoutContents = GetForLayoutPart(layoutPartId);

            var deletedRecords = layoutContents.Where(r => !contentItemIds.Contains(r.ContentItemRecord_Id));
            var addedContentIds = contentItemIds.Distinct().Where(id => !layoutContents.Any(r => r.ContentItemRecord_Id == id));

            foreach (var deletedRecord in deletedRecords)
            {
                _layoutContentRepository.Delete(deletedRecord);
            }

            foreach (var addedContentId in addedContentIds)
            {
                _layoutContentRepository.Create(new LayoutContentMapRecord() {
                    LayoutPartRecord_Id = layoutPartId,
                    ContentItemRecord_Id = addedContentId
                });
            }
        }

        public void LayoutPartRemoved(int layoutPartId)
        {
            var deletedRecords = GetForLayoutPart(layoutPartId);

            foreach (var deletedRecord in deletedRecords)
            {
                _layoutContentRepository.Delete(deletedRecord);
            }
        }
    }
}