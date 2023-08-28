using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public async Task<NoteEntitiy> AddNote(NoteRegistrationModel note, long userId)
        {
            try
            {
                return await noteRepo.AddNote(note, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<NoteEntitiy>> GetAllNotes(long userId)
        {
            try
            {
                return await noteRepo.GetAllNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<NoteEntitiy> UpdateNote(NoteUpdateModel model, long userId)
        {
            try
            {
                return await noteRepo.UpdateNote(model, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<NoteEntitiy> DeleteNote(long noteId)
        {
            try
            {
                return await noteRepo.DeleteNote(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<NoteEntitiy> ArchieveNote(long noteId, long userId)
        {
            try
            {
                return await noteRepo.ArchieveNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<NoteEntitiy> TrashNote(long noteId, long userId)
        {
            try
            {
                return await noteRepo.TrashNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<NoteEntitiy> PinUnPin(long noteId, long userId)
        {
            try
            {
                return await noteRepo.PinUnPin(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<NoteEntitiy> ChangeColor(long noteId, int userId, string newColor)
        {
            try
            {
                return await noteRepo.ChangeColor(noteId, userId, newColor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> UploadImage(long noteid, long userid, IFormFile image)
        {
            try
            {
                return await noteRepo.UploadImage(noteid, userid, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<NoteEntitiy>> FindKeyWord(long userId, string keyWord)
        {
            try
            {
                return await noteRepo.FindKeyWord(userId, keyWord);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
