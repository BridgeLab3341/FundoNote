using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBusiness
    {
        public Task<NoteEntitiy> AddNote(NoteRegistrationModel note, long userId);
        public Task<List<NoteEntitiy>> GetAllNotes(long userId);
        public Task<NoteEntitiy> UpdateNote(NoteUpdateModel model, long userId);
        public Task<NoteEntitiy> DeleteNote(long noteId);
        public Task<NoteEntitiy> ArchieveNote(long noteId, long userId);
        public Task<NoteEntitiy> TrashNote(long noteId, long userId);
        public Task<NoteEntitiy> PinUnPin(long noteId, long userId);
        public Task<NoteEntitiy> ChangeColor(long noteId, int userId, string newColor);
        public Task<string> UploadImage(long noteid, long userid, IFormFile image);
        public Task<List<NoteEntitiy>> FindKeyWord(long userId, string keyWord);
    }
}
