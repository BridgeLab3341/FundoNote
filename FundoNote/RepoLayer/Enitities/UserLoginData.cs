using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Enitities
{
    public class UserLoginData
    {
        public string Token { get; set; }
        public UserEntity User { get; set; }    
    }
}
