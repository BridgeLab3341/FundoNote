using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace NewFundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabels(long noteId, string labName)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                var userId = long.Parse(userIdClaim.Value);
                var result = labelBusiness.AddingLabel(userId, noteId, labName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LabelName Added successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "LabelName Not Added Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetLabels")]
        public IActionResult GetAllLabel()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                long userid = long.Parse(userId.Value);
                var result = labelBusiness.GetAllLabels(userid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label's Found successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Label's Not Found Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetLabelByNoteId")]
        public IActionResult GetLabelByNoteID(long noteid)
        {
            try
            {
                var result = labelBusiness.GetLabelByNoteId(noteid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Found successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Label Not Found Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabels(long labId)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result = labelBusiness.RemoveLabel(labId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LabelName Deleted successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "LabelName Not Deleted Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabels(long labId, string labName)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result = labelBusiness.UpdateLabel(labId, userId, labName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LabelName Deleted successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "LabelName Not Deleted Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
