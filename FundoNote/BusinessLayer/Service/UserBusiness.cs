using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Enitities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public async Task<UserEntity> UserRegister(UserRegistrationModel model)
        {
            try
            {
                return await userRepo.UserRegister(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserLoginData> UserLogin(UserLoginModel userLoginData)
        {
            try
            {
                return await userRepo.UserLogin(userLoginData);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<UserEntity>> GetAllRecords()
        {
            try
            {
                return await userRepo.GetAllRecords();
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
                return await userRepo.GetByUser(userid);
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
                return await userRepo.UpdateUser(id, firstName, lastname);
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
                return await userRepo.DeleteUser(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateTokenAuthentication(long id, string email)
        {
            try
            {
                return userRepo.GenerateTokenAuthentication(id, email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                return await userRepo.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return await userRepo.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
