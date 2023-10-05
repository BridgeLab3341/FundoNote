using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class UserRepo : IUserRepo
    {
        public readonly IConfiguration IConfiguration;
        private readonly NewFundoContext fundoContext;
        public UserRepo(NewFundoContext fundoContext, IConfiguration IConfiguration)
        {
            this.fundoContext = fundoContext;
            this.IConfiguration = IConfiguration;
        }
        public async Task<UserEntity> UserRegister(UserRegistrationModel userRegistartion)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistartion.FirstName;
                userEntity.LastName = userRegistartion.LastName;
                userEntity.Email = userRegistartion.Email;
                userEntity.Password = EncryptPassword(userRegistartion.Password);
                await fundoContext.Users.AddAsync(userEntity);
                await fundoContext.SaveChangesAsync();
                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserLoginData> UserLogin(UserLoginModel userLogin)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = await fundoContext.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email && x.Password == EncryptPassword(userLogin.Password));
                var email = userLogin.Email;
                if (userEntity != null)
                {
                    var token = GenerateTokenAuthentication(userEntity.UserId, userEntity.Email);
                    UserLoginData loginData = new UserLoginData
                    {
                        Token = token,
                        User = userEntity
                    };
                    return loginData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<UserEntity>> GetAllRecords()
        {
            try
            {
                List<UserEntity> userEntities = new List<UserEntity>();
                userEntities = await fundoContext.Users.ToListAsync();
                if (userEntities != null)
                {
                    return userEntities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserEntity> GetByUser(GetUserIdModel userid)
        {
            try
            {
                UserEntity userEntities = new UserEntity();
                userEntities = await fundoContext.Users.FirstOrDefaultAsync(x => x.UserId == userid.Id);
                if (userEntities != null)
                {
                    return userEntities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserEntity> UpdateUser(long id, string firstName, string lastname)
        {
            try
            {
                UserEntity userEntities = new UserEntity();
                userEntities = await fundoContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
                if (userEntities != null)
                {
                    userEntities.FirstName = firstName;
                    userEntities.LastName = lastname;
                    await fundoContext.SaveChangesAsync();
                    return userEntities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserEntity> DeleteUser(long id)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = fundoContext.Users.FirstOrDefault(x => x.UserId == id);
                if (userEntity != null)
                {
                    fundoContext.Users.Remove(userEntity);
                    await fundoContext.SaveChangesAsync();
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateTokenAuthentication(long id, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(IConfiguration["JwtConfig:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(ClaimTypes.Email, email),
                 new Claim("UserId",id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }
        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var userEntity = await fundoContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                var useremail = userEntity.Email;
                var id = userEntity.UserId;

                if (userEntity != null)
                {
                    var token = GenerateTokenAuthentication(id, email);
                    MsmqModel msmql = new MsmqModel();
                    msmql.SendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                var reset = await fundoContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (reset != null && password == confirmPassword)
                {
                    reset.Password = EncryptPassword(password);
                    await fundoContext.SaveChangesAsync();
                    return reset.Password;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
