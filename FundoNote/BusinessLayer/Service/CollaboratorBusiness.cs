using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CollaboratorBusiness : ICollaboratorBusiness
    {
        private readonly ICollaboratorRepo collaborator;
        public CollaboratorBusiness(ICollaboratorRepo collaborator)
        {
            this.collaborator = collaborator;
        }
        public async Task<CollaboratorEntity> AddingCollaborator(long userId, long noteId, string email, Collaborator collab)
        {
            try
            {
                return await collaborator.AddingCollaborator(userId, noteId, email,collab);
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
                return await collaborator.DeletingCollaborator(CollaboratorId, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CollaboratorEntity>> GetAllCollaborator(long userId)
        {
            try
            {
                return await collaborator.GetAllCollaborator(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
