using CommonLayer.Model;
using RepoLayer.Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface IUserRepo
    {
        public Task<UserEntity> UserRegister(UserRegistrationModel userRegistartion);
        public Task<UserLoginData> UserLogin(UserLoginModel userLogin);
        public Task<List<UserEntity>> GetAllRecords();
        public Task<UserEntity> GetByUser(GetUserIdModel userid);
        public string GenerateTokenAuthentication(long id, string email);
        public Task<string> ForgotPassword(string email);
        public Task<string> ResetPassword(string email, string password, string confirmPassword);
        public Task<UserEntity> UpdateUser(long id, string firstName, string lastname);
        public Task<UserEntity> DeleteUser(long id);


    }
}
