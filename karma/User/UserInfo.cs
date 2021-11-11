using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace karma.User
{
    public class UserInfo
    {
        private int UserID { get; }
        string Name { get; }
        string Surname { get; }
        string Username { get; }
        string Email { get; }
        bool IsUser { get; }
        bool IsCharity { get; }
        bool IsAdmin { get; }
    }
}
