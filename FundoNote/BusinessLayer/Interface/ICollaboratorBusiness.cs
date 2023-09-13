using CommonLayer.Model;
using RepoLayer.Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBusiness
    {
        public Task<CollaboratorEntity> AddingCollaborator(long userId, long noteId, string email, Collaborator collab);
        public Task<CollaboratorEntity> DeletingCollaborator(long CollaboratorId, long noteId, long userId);
        public Task<List<CollaboratorEntity>> GetAllCollaborator(long userId);
    }
}
