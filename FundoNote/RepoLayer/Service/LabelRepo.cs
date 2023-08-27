using Microsoft.EntityFrameworkCore;
using RepoLayer.Context;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class LabelRepo : ILabelRepo
    {
        public readonly NewFundoContext fundoContext;
        public LabelRepo(NewFundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public async Task<LabelEntity> AddingLabel(long userId, long noteId, string labName)
        {
            try
            {
                var user = await fundoContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                var note = await fundoContext.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
                LabelEntity labelEntity = new LabelEntity()
                {
                    user = user,
                    Note = note,
                };
                labelEntity.LabelName = labName;
                await fundoContext.AddAsync(labelEntity);
                await fundoContext.SaveChangesAsync();
                return labelEntity;
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
                var result = await fundoContext.Labels.Where(x => x.UserId == userId).ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<LabelEntity>> GetLabelByNoteId(long noteId)
        {
            try
            {
                List<LabelEntity> result = await fundoContext.Labels.Where(x => x.NoteId == noteId).Include(x => x.Note).Include(x => x.user).ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LabelEntity> RemoveLabel(long labId, long userId)
        {
            try
            {
                var result = await fundoContext.Labels.FirstOrDefaultAsync(x => x.LabelId == labId && x.UserId == userId);
                if (result != null)
                {
                    fundoContext.Labels.Remove(result);
                    await fundoContext.SaveChangesAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LabelEntity> UpdateLabel(long labId, long userId, string labName)
        {
            try
            {
                var result = await fundoContext.Labels.FirstOrDefaultAsync(x => x.LabelId == labId && x.UserId == userId);
                if (result != null)
                {
                    result.LabelName = labName;
                    await fundoContext.SaveChangesAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
