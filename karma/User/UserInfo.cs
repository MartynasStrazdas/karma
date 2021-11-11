using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace karma.User
{
    public class UserInfo
    {
        public int UserID { get;}
        public string Name { get;}
        public string Surname { get;}
        public string Username { get;}
        public string Password { get;}
        public string Email { get;}
        public bool IsUser { get; set; }
        public bool IsCharity { get; set; }
        public bool IsAdmin { get; set; }

        // UserInformation should be gathered from database
        private UserInfo()
        {
            Username = "Antonas"; 
            Password = "Wiblis"; 
            IsUser = false;
            IsCharity = false; 
            IsAdmin = false;
        }

        private static UserInfo _instance;

        public static UserInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserInfo();
            }
            return _instance;
        }
        public static UserInfo ChangePermitionsToUser()
        {
            if (_instance == null)
            {
                _instance = new UserInfo();
            }
            _instance.IsUser = true;
            return _instance;
        }
        public static UserInfo ChangePermitionsToCharity()
        {
            if (_instance == null)
            {
                _instance = new UserInfo();
            }
            _instance.IsCharity= true;
            return _instance;
        }
        public static UserInfo ChangePermitionsToAdmin()
        {
            if (_instance == null)
            {
                _instance = new UserInfo();
            }
            _instance.IsAdmin = true;
            return _instance;
        }
    }
}
