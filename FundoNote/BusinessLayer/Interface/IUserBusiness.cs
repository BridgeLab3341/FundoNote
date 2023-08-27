using CommonLayer.Model;
using RepoLayer.Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public Task<UserEntity> UserRegister(UserRegistrationModel model);
        public Task<UserLoginData> UserLogin(UserLoginModel userLoginData);
        public Task<List<UserEntity>> GetAllRecords();
        public Task<UserEntity> GetByUser(GetUserIdModel userid);
        public string GenerateTokenAuthentication(long id, string email);
        public Task<string> ForgotPassword(string email);
        public Task<string> ResetPassword(string email, string password, string confirmPassword);
        public Task<UserEntity> UpdateUser(long id, string firstName, string lastname);
        public Task<UserEntity> DeleteUser(long id);
    }
}
