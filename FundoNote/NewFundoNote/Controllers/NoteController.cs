using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepoLayer.Enitities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace NewFundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBusiness noteBusiness, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBusiness = noteBusiness;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddNote")]
        public async Task<IActionResult> AddingNote(NoteRegistrationModel model)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
                {
                    return BadRequest(new { success = false, message = "Invalid user ID in the token" });
                }
                var result = await noteBusiness.AddNote(model, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Created successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Note Not Created Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllNotes")]
        public async Task<IActionResult> GetAllRecords()
        {
            try
            {
                var userid = long.Parse(User.FindFirst("UserId").Value);
                var result = await noteBusiness.GetAllNotes(userid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Found All Notes successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "NotFound Notes Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("UpdateNote")]
        public async Task<IActionResult> UpdateRecords(NoteUpdateModel model, long userId)
        {
            try
            {
                var result = await noteBusiness.UpdateNote(model, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Updated Note successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Not Updated Note Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("DeleteNote")]
        public async Task<IActionResult> DeletNoteByNoteId(long noteId)
        {
            try
            {
                var result = await noteBusiness.DeleteNote(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleted Note successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Could not Delete Note Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("Archive")]
        public async Task<IActionResult> NoteArchieve(long noteId)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result = await noteBusiness.ArchieveNote(noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Achieved Note successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Could not Achieved Note Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("Trash")]
        public async Task<IActionResult> NoteTrash(long noteId)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result = await noteBusiness.TrashNote(noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Trashed Note successfull", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Could not Trash Note Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("Pin/UnPin")]
        public async Task<IActionResult> Pin(long noteId)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result = await noteBusiness.PinUnPin(noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Pinned Note successfull", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "UnPinned Note Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("BGColor")]
        public async Task<IActionResult> BGColor(long noteId, string color)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                var userId = Int32.Parse(userid.Value);
                var result = await noteBusiness.ChangeColor(noteId, userId, color);
                if (result != null)
                {
                    return Ok(new { success = true, message = "BGColor Saved successfull", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "BGColor Not Saved Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
