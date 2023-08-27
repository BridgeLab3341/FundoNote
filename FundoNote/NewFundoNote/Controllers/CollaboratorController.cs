using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Context;

namespace NewFundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly NewFundoContext context;
        private readonly ICollaboratorBusiness business;
        public CollaboratorController(NewFundoContext context, ICollaboratorBusiness business)
        {
            this.context = context;
            this.business = business;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(long noteId, string email)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
                {
                    return BadRequest(new { success = false, message = "Invalid user ID in the token" });
                }
                var result = business.AddingCollaborator(userId, noteId, email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Email Collaborated successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Email Couldn't Collaborate Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("DeleteCollaborator")]
        public IActionResult DeleteCollaborator(long collobId, long noteid)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                long userId = long.Parse(userIdClaim.Value);
                var result = business.DeletingCollaborator(collobId, noteid, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborated Email Removed successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Collaborated Email Can't Removed Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllCollaborator")]
        public IActionResult GetAllCollaborators()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                long userid = long.Parse(userId.Value);
                var result = business.GetAllCollaborator(userid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborated Emails Found successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Collaborated Email Not Found Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
