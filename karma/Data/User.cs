using System;
using System.Collections.Generic;

#nullable disable

namespace karma
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsUser { get; set; }
        public bool IsCharity { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] Img { get; set; }
    }
}
