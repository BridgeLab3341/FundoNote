using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Context;
using MassTransit;
using CommonLayer.Model;
using Microsoft.VisualBasic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace NewFundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly NewFundoContext context;
        private readonly ICollaboratorBusiness business;
        private readonly IBus bus;

        public CollaboratorController(NewFundoContext context, ICollaboratorBusiness business, IBus bus)
        {
            this.context = context;
            this.business = business;
            this.bus = bus;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddCollaborator/{noteId}/{email}")]
        public IActionResult AddCollaborator(long noteId, string email, Collaborator collab)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
                {
                    return BadRequest(new { success = false, message = "Invalid user ID in the token" });
                }
                var result = business.AddingCollaborator(userId, noteId, email,collab);
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
        [Route("DeleteCollaborator/{collobId}/{noteId}")]
        public IActionResult DeleteCollaborator(long collobId, long noteId)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                long userId = long.Parse(userIdClaim.Value);
                var result = business.DeletingCollaborator(collobId, noteId, userId);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(long noteId, Collaborator collab)
        {
            try
            {
                //var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                string email = User.FindFirst(ClaimTypes.Email).Value;
                var result = business.AddingCollaborator(userId, noteId,email,collab);
                if(result!=null)
                {
                    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                    var endPoint =await bus.GetSendEndpoint(uri);
                    await endPoint.Send(collab);
                    var message = collab.Email;
                    return Ok(new { message = "Successfully Send Message to Rabbit MQ", data = message });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccesful" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
