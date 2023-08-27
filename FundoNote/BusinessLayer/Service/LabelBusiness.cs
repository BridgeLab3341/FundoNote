using BusinessLayer.Interface;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }
        public async Task<LabelEntity> AddingLabel(long userId, long noteId, string labName)
        {
            try
            {
                return await labelRepo.AddingLabel(userId, noteId, labName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<LabelEntity>> GetAllLabels(long userId)
        {
            try
            {
                return await labelRepo.GetAllLabels(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<LabelEntity>> GetLabelByNoteId(long noteId)
        {
            try
            {
                return await labelRepo.GetLabelByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LabelEntity> RemoveLabel(long labId, long userId)
        {
            try
            {
                return await labelRepo.RemoveLabel(labId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<LabelEntity> UpdateLabel(long labId, long userId, string labName)
        {
            try
            {
                return await labelRepo.UpdateLabel(labId, userId, labName);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
