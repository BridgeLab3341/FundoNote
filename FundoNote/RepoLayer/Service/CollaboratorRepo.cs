using CommonLayer.Model;
using MassTransit;
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
    public class CollaboratorRepo : ICollaboratorRepo
    {
        public readonly NewFundoContext fundoContext;
        private readonly IBus _bus;
        public CollaboratorRepo(NewFundoContext fundoContext, IBus _bus)
        {
            this.fundoContext = fundoContext;
            this._bus = _bus;
        }
        public async Task<CollaboratorEntity> AddingCollaborator(long userId, long noteId, string email,Collaborator collab)
        {
            try
            {
                var user = await fundoContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                var note = await fundoContext.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
                CollaboratorEntity collaborator = new CollaboratorEntity()
                {
                    user = user,
                    Note = note,
                };
                await _bus.Publish(collab);
                await fundoContext.SaveChangesAsync();
                return collaborator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CollaboratorEntity> DeletingCollaborator(long CollaboratorId, long noteId, long userId)
        {
            try
            {
                var result = await fundoContext.Collaborator.FirstOrDefaultAsync(x => x.CollaboratorId == CollaboratorId && x.NoteId == noteId);

                if (result != null)
                {
                    fundoContext.Collaborator.Remove(result);
                    await fundoContext.SaveChangesAsync();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CollaboratorEntity>> GetAllCollaborator(long userid)
        {
            try
            {
                var result = await fundoContext.Collaborator.Where(x => x.UserId == userid).ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
