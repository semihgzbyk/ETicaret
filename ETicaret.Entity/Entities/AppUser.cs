using System;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Entity.Entities;

public class AppUser : IdentityUser
{
    public string FullName {get; set;} = null!;
}
