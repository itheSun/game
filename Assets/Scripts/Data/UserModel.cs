using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserModel
{
    private string _id;
    private string _name;
    private string _email;
    private string _password;
    private string _phone;
    private string _address;
    private string _city;
    private string _country;

    public string Id { get => _id; }
    public string Name { get => _name; }
    public string Email { get => _email; }
    public string Password { get => _password; }
    public string Phone { get => _phone; }
    public string Address { get => _address; }
    public string City { get => _city; }
    public string Country { get => _country; }

    public UserModel(string id, string name, string email, string password, string phone, string address, string city, string country)
    {
        _id = id;
        _name = name;
        _email = email;
        _password = password;
        _phone = phone;
        _address = address;
        _city = city;
        _country = country;
    }

}
