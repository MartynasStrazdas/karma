using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using karma;

public class UserInfo
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

    private UserInfo() {}

    private static UserInfo _instance;

    public static UserInfo GetInstance()
    {
        if (_instance == null)
        {
            _instance = new UserInfo();
        }
        return _instance;
    }

    public static UserInfo Login(User user)
    {
        if (_instance == null)
        {
            _instance = new UserInfo();
        }
        _instance.Id = user.Id;
        _instance.Name = user.Name;
        _instance.Username = user.Username;
        _instance.Password = user.Password;
        _instance.Email = user.Email;
        _instance.IsUser = user.IsUser;
        _instance.IsCharity = user.IsCharity;
        _instance.IsAdmin = user.IsAdmin;
        _instance.Img = user.Img;
        return _instance;
    }

    public static void Logout()
    {
        _instance.IsAdmin = false;
        _instance.IsCharity = false;
        _instance.IsUser = false;
    }
}