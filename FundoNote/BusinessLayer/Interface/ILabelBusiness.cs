using RepoLayer.Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        public Task<LabelEntity> AddingLabel(long userId, long noteId, string labName);
        public Task<List<LabelEntity>> GetAllLabels(long userId);
        public Task<LabelEntity> RemoveLabel(long labId, long userId);
        public Task<List<LabelEntity>> GetLabelByNoteId(long noteId);
        public Task<LabelEntity> UpdateLabel(long labId, long userId, string labName);
    }
}
