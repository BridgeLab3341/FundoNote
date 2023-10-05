using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class NoteRepo : INoteRepo
    {
        public readonly NewFundoContext fundoContext;
        private readonly IConfiguration configuration;
        public NoteRepo(NewFundoContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
        }
        public async Task<NoteEntitiy> AddNote(NoteRegistrationModel note, long userId)
        {
            try
            {
                var user = await fundoContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                NoteEntitiy entitiy = new NoteEntitiy();
                entitiy.Title = note.Title;
                entitiy.Description = note.Description;
                entitiy.BGColor = note.BGColor;
                entitiy.Archive = note.Archive;
                entitiy.Remainder = note.Remainder;
                entitiy.Pin = note.Pin;
                entitiy.Trash = note.Trash;
                entitiy.CreatedTime = note.CreatedTime;
                entitiy.UpdatedTime = note.UpdatedTime;
                entitiy.UserId = userId;
                await fundoContext.Notes.AddAsync(entitiy);
                await fundoContext.SaveChangesAsync();
                if (entitiy != null)
                {
                    return entitiy;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured While Adding Note");
            }
        }
        public async Task<List<NoteEntitiy>> GetAllNotes(long userId)
        {

            try
            {
                var entitiys = await fundoContext.Notes.Where(x => x.UserId == userId).ToListAsync();
                if (entitiys != null)
                {
                    return entitiys;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured While Fetching All Note");
            }
        }
        public async Task<NoteEntitiy> UpdateNote(NoteUpdateModel model, long userId)
        {
            try
            {
                var entity = await fundoContext.Notes.FirstOrDefaultAsync(x => x.UserId == userId);
                if (entity != null)
                {
                    entity.Title = model.Title;
                    entity.Description = model.Description;
                    entity.BGColor = model.BGColor;
                    entity.Archive = model.Archive;
                    entity.Remainder = model.Remainder;
                    entity.Pin = model.Pin;
                    entity.Trash = model.Trash;
                    entity.UpdatedTime = model.UpdatedTime;
                    await fundoContext.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured While Updating Note");
            }
        }
        public async Task<NoteEntitiy> DeleteNote(long noteId)
        {
            try
            {
                NoteEntitiy noteEntitiy = await fundoContext.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
                if (noteEntitiy != null)
                {
                    fundoContext.Notes.Remove(noteEntitiy);
                    await fundoContext.SaveChangesAsync();
                    return noteEntitiy;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured While Deleting Note");
            }
        }
        public async Task<NoteEntitiy> ArchieveNote(long noteId, long userId)
        {
            try
            {
                var resu = await fundoContext.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (resu != null)
                {
                    if (resu.Archive == true)
                    {
                        resu.Archive = false;
                    }
                    else
                    {
                        resu.Archive = true;
                    }
                    await fundoContext.SaveChangesAsync();
                    return resu;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured at ArchieveNote");
            }
        }
        public async Task<NoteEntitiy> TrashNote(long noteId, long userId)
        {
            try
            {
                var resu = await fundoContext.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (resu != null)
                {
                    if (resu.Trash == true)
                    {
                        resu.Trash = false;
                    }
                    else
                    {
                        resu.Trash = true;
                    }
                    await fundoContext.SaveChangesAsync();
                    return resu;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured at TrashNote");
            }
        }
        public async Task<NoteEntitiy> PinUnPin(long noteId, long userId)
        {
            try
            {
                var resu = await fundoContext.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId && x.UserId == userId);
                if (resu != null)
                {
                    if (resu.Pin == true)
                    {
                        resu.Pin = false;
                    }
                    else
                    {
                        resu.Pin = true;
                    }
                    await fundoContext.SaveChangesAsync();
                    return resu;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured at PinUnPin");
            }
        }
        public async Task<NoteEntitiy> ChangeColor(long noteId, int userId, string newColor)
        {
            try
            {
                var res = await fundoContext.Notes.FirstOrDefaultAsync(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.BGColor = newColor;
                    await fundoContext.SaveChangesAsync();
                    return await fundoContext.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured while Changing Color");
            }
        }
        public async Task<string> UploadImage(long noteid, long userid, IFormFile image)
        {
            try
            {
                Account cloudinaryAccount = new Account(
                    configuration["Cloudinary:CloudName"],
                    configuration["Cloudinary:APIKey"],
                    configuration["Cloudinary:APISecret"]
                    );
                var user = await fundoContext.Notes.FirstOrDefaultAsync(x => x.UserId == userid && x.NoteId == noteid);
                Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                    Transformation = new Transformation().Crop("fit").Gravity("face"),
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string secureurl = uploadResult.SecureUrl.ToString();
                return "Image Url : " + secureurl;
            }
            catch (Exception)
            {
                throw new Exception("Exception Occured while Uploading Image");
            }
        }
        public async Task<List<NoteEntitiy>> FindKeyWord(long userId, string keyWord)
        {
            try
            {
                var result = await fundoContext.Notes.Where(x => x.UserId == userId && x.Title.Contains(keyWord)).ToListAsync();
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
                throw new Exception("Exception Occured while Searching key Word");
            }
        }
    }
}
