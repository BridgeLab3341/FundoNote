using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NewFundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> UserRegister(UserRegistrationModel model)
        {
            try
            {
                var result = await userBusiness.UserRegister(model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Registration successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Registration Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginData)
        {
            try
            {
                var result = await userBusiness.UserLogin(userLoginData);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllRecords")]
        public async Task<IActionResult> GetAllRecords()
        {
            try
            {
                var result = await userBusiness.GetAllRecords();
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Data Found Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Data NotFound UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByUserId([FromQuery] GetUserIdModel model)
        {
            try
            {
                var result = await userBusiness.GetByUser(model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Data Found Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Data NotFound UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UserUpdateFirstLastName(long id, string fitstName, string lastName)
        {
            try
            {
                var result = await userBusiness.UpdateUser(id, fitstName, lastName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Updated Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Not Updated UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUserById(long id)
        {
            try
            {
                var result = await userBusiness.DeleteUser(id);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleted User Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Not Deleted User UnSuccessfully" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgotPassword/{email}")]
        public async Task<IActionResult> UserForgotPassword(string email)
        {
            try
            {
                var result = await userBusiness.ForgotPassword(email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Data Found Token Sent Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Data NotFound Token NotSent UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ResetPassword/{Confirmpassword}")]
        public async Task<IActionResult> UserResetPassword(string password, string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = await userBusiness.ResetPassword(email, password, confirmpassword);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Password Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Password UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
