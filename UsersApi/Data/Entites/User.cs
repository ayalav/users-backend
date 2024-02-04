using System;

namespace UsersApi.Data.Entites;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}